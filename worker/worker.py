import pika
import time

time.sleep(30)

print("Worker started")

connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='rabbitmq'))
channel = connection.channel()
channel.queue_declare(queue='task_queue', durable=True)

print("Worker connected")


def callback(ch, method, properties, body):
    cmd = body.decode()
    print("Received %s" % cmd)
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_qos(prefetch_count=1)
channel.basic_consume(queue='task_queue', on_message_callback=callback)
channel.start_consuming()
