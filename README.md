
FileUploadService

Overview
The FileUploadService is a C# project that provides a Web API for uploading files. It is designed to be a microservice that allows clients to upload files securely.

Features
File upload functionality through HTTP POST requests.
JWT (JSON Web Token) for secure authorization.
Swagger for API documentation.
Rabbit mq for send queue

SendEmailService

Overview
The SendEmailService is a C# console application designed to be part of a microservices architecture. It utilizes RabbitMQ for communication between microservices and provides email sending functionality.

Features
Rabbit mq for recive queue
.Net Mail for send email


Steps for using the program

1. clone code according to link https://github.com/IlikeMcDonaldsSomuch/File-Upload-Service.git
2. Load docker Desktop using the command docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
3. login rabbitmq page user guest password guest
   

