using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace SimpleBrokeredMessaging.Receiver
{
    public class ReceiveConsole
    {
        static string connectionString = "Endpoint=sb://gcexchange.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=73Tg/PfMEiqIKGKHSa/9QnqOEQtubmRww8+/ibpAGi8=";
        static string queuePath = "queue1";

        static string topic = "chatmessagetopic";

        static void Main(string[] args)
        {
            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queuePath);

            queueClient.OnMessage(msg => ProcessMessage(msg));
            Console.WriteLine("Please Enter to exit..");
            Console.ReadLine();
        }

        private static void ProcessMessage(BrokeredMessage msg)
        {
            var text = msg.GetBody<string>();
            Console.WriteLine("Received " + text);
        }
    }
}
