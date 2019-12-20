# Test_IP
API + Worker in .net core with docker configuration


Basically the project is designed as two main components and this is that the component should do:

1-Test.API.IP:
 -This is a RestFul API that will receive the HTTP request from the client, validate if the IP group is in the correct format, if so, send an asynchronous call to test each IP, this request will be published on RabbitMQ queue and it will wait for response. When all asynchronous processes are completed, it will analyze each result and return an array of results for N IPs that the client sent to the request.
This API has swagger installed.


2-Test.IP_Worker
  -This is a worker with .net core, this worker will listen to specific messages with RabbitMQ and EasyNetQ to simulate the HTTP request with direct responses. When a request arrives at the queue, the worker will validate the information and try to ping the IP and return a result of success or error for the specific IP.
  

Both projects have Docker configuration to be able to implement the components as a service in a Docker Container.

Pendings:
  -Test.API.IP.Tests, the project for testing was created but no completed.
  -Deploy the solution to a cloud service to test the solution in real time(I ran out of time).
 
