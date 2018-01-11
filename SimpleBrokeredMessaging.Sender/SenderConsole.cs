

namespace SimpleBrokeredMessaging.Sender
{

    using System;
    using Microsoft.ServiceBus.Messaging;
    public class SenderConsole
    {
        static string connectionString = "Endpoint=sb://gcexchange.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=73Tg/PfMEiqIKGKHSa/9QnqOEQtubmRww8+/ibpAGi8=";
        static string queuePath = "queue1";
        static void Main(string[] args)
        {

            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queuePath);

            for(int i=0;i<10;i++)
            {
                var message = new BrokeredMessage("Message " + i);
                queueClient.Send(message);
                Console.WriteLine("Sent message" + i);
            }
            queueClient.Close();
            Console.ReadLine();

        }
    }
}
