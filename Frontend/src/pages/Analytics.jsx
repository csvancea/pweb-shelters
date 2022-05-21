import React, { useState, useMemo, useEffect, useCallback } from "react";
import Input from "../components/Input";
import PageLayout from "../utils/PageLayout";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  BarElement,
  ArcElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Line, Bar, Pie } from "react-chartjs-2";
import ChartDataLabels from "chartjs-plugin-datalabels";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import axiosInstance from "../configs/Axios";
import { shortDate } from "../utils/Functions";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  ArcElement,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

const topShelterOptions = (labels) => ({
  maintainAspectRatio: false,
  showAllTooltips: true,
  legend: {
    display: false,
  },
  indexAxis: "y",
  elements: {
    bar: {
      borderWidth: 2,
    },
  },
  responsive: true,
  plugins: {
    legend: {
      display: false,
    },
    title: {
      display: false,
    },
    tooltip: {
      enabled: true,
    },
    datalabels: {
      formatter: (_, context) => labels[context.dataIndex],
      font: {
        weight: "bold",
      },
      labels: {
        title: {
          display: false,
        },
        value: {
          color: "#111111",
        },
      },
    },
  },
  scales: {
    x: {
      grid: {
        display: false
      },
      ticks: {
        precision: 0
      }
    },
    y: {
      grid: {
        display: false,
      },
      ticks: {
        display: false,
      },
    },
  },
});

const evolutionOptions = {
  maintainAspectRatio: false,
  responsive: true,
  plugins: {
    legend: {
      display: false
    },
    title: {
      display: false,
    },
    datalabels: {
      display: false,
    },
  },
  scales: {
    y: {
      ticks: {
        precision: 0
      }
    }
  }
};

const groupAges = (ageDistribution) => {
  return [
    {
      label: '0-7',
      count: ageDistribution.filter(x => x.age >= 0 && x.age <= 7).reduce((accumulator, x) => {return accumulator + x.count;}, 0)
    },
    {
      label: '8-17',
      count: ageDistribution.filter(x => x.age >= 8 && x.age <= 17).reduce((accumulator, x) => {return accumulator + x.count;}, 0)     
    },
    {
      label: '18-59',
      count: ageDistribution.filter(x => x.age >= 18 && x.age <= 59).reduce((accumulator, x) => {return accumulator + x.count;}, 0)     
    },
    {
      label: '60+',
      count: ageDistribution.filter(x => x.age >= 60).reduce((accumulator, x) => {return accumulator + x.count;}, 0)     
    }
]
};

const Analytics = () => {
  const { getAccessTokenSilently } = useAuth0();
  const [metrics, setMetrics] = useState({refugeeCounts: [], ageDistribution: [], topShelters: []});
  const [days, setDays] = useState(7);
  const [top, setTop] = useState(3);

  const getMetrics = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.metrics.getAllShelters, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => {console.log(data); setMetrics(data)});
  }, [getAccessTokenSilently]);

  useEffect(() => {
    getMetrics();
  }, [getMetrics]);

  const data = useMemo(
    () => ({
      labels: metrics.refugeeCounts.map(r => shortDate(r.date)).slice(-days),
      datasets: [
        {
          data: metrics.refugeeCounts.map(r => r.count).slice(-days),
          lineTension: 0.4,
          borderColor: "#17A9FA",
          backgroundColor: "#17A9FA",
          innerHeight: "200px",
          borderRadius: 8
        },
      ],
    }),
    [metrics.refugeeCounts, days]
  );

  const ageDistribution = useMemo(
    () => ({
      labels: groupAges(metrics.ageDistribution).map(r => r.label),
      datasets: [
        {
          data: groupAges(metrics.ageDistribution).map(r => r.count),
          lineTension: 0.4,
          borderColor: "#FFFFFF",
          backgroundColor: ["#2D6CA3", "#17a9fa","#00cadd", "#00d1be"],
          innerHeight: "250px",
          borderRadius: 8
        },
      ],
    }),
    [metrics.ageDistribution]
  );

  const topShelters = useMemo(
    () => ({
      labels: metrics.topShelters.map(r => r.name).slice(0, top),
      datasets: [
        {
          data: metrics.topShelters.map(r => r.count).slice(0, top),
          lineTension: 0.4,
          borderColor: "#17A9FA",
          backgroundColor: "#17A9FA",
          innerHeight: "250px",
          borderRadius: 8,
          datalabels: {
            offset: -75,
            anchor: "end",
            align: "end",
          },
        },
      ],
    }),
    [metrics.topShelters, top]
  );

  return (
    <PageLayout>
      <div className="row-between">
        <h2>Analytics</h2>
      </div>
      <div className="flex flex-col gap-10">
        <div>
          <p className="mb-4 section-title">Evolution of sheltered refugees over time</p>
          <label>Last</label>
          <Input
            className="input-analytics"
            min="2"
            max="31"
            type="number"
            value={days}
            onChange={(e) =>
              setDays(e.target.value && parseInt(e.target.value))
            }
          />
          <label>days</label>
          <div className="graph">
            <Line
              options={evolutionOptions}
              data={data}
              plugins={[ChartDataLabels]}
            />
          </div>
        </div>
        <div>
          <p className="mb-4 section-title">Most occupied shelters</p>
          <label>Top</label>
          <Input
            className="input-analytics"
            min="1"
            max={metrics.topShelters.filter((e) => e.count > 0).length}
            type="number"
            value={top}
            onChange={(e) =>
              setTop(e.target.value && parseInt(e.target.value))
            }
          />
          <label>Shelters</label>
          <div className="graph">
            <Bar
              data={topShelters}
              plugins={[ChartDataLabels]}
              options={topShelterOptions(metrics.topShelters.map(r => r.name))}
            />
          </div>
        </div>
        <div>
          <p className="mb-4 section-title">Age distribution of sheltered refugees</p>
          <div className="graph">
            <Pie
              data={ageDistribution}
              options={{maintainAspectRatio: false}}
            />
          </div>
        </div>
      </div>
    </PageLayout>
  );
};

export default Analytics;
