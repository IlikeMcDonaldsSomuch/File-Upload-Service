using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendEmailService;
using System.Net;
using System.Net.Mail;
using System.Text;
var appconstant = new AppConstants();
var factory = new ConnectionFactory
{
    HostName = appconstant.Localhost
};
var connection = factory.CreateConnection();
using
var channel = connection.CreateModel();
channel.QueueDeclare("product", exclusive: false);
var result = string.Empty;
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Product message received: {message}");
    SendEmail($"Product message: {message}","Test Send Email",appconstant);
};
//read the message
channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
Console.ReadKey();



static void SendEmail(string body,string subject, AppConstants appConstants)
{

    var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = appConstants.Port,
        Credentials = new NetworkCredential(appConstants.Sender, appConstants.Key),
        EnableSsl = true,
    };

    MailMessage mailMessage = new MailMessage();
    mailMessage.From = new MailAddress(appConstants.Sender);
    mailMessage.To.Add(appConstants.Recive);
    mailMessage.Subject = subject;
    mailMessage.Body = $"This is a test email sent through Gmail SMTP in notification {body}";
    smtpClient.Send(mailMessage);
}