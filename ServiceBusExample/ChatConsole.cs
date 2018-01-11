namespace ServiceBusExample
{
    using System;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    class ChatConsole
    {
        static string connectionString = "Endpoint=sb://gcexchange.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=73Tg/PfMEiqIKGKHSa/9QnqOEQtubmRww8+/ibpAGi8=";
        static string topic = "chatmessagetopic";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter the name");
                var userName = Console.ReadLine();

                var manager = NamespaceManager.CreateFromConnectionString(connectionString);

                if (!manager.TopicExists(topic))
                {
                    manager.CreateTopic(topic);
                }

                var description = new SubscriptionDescription(topic, userName)
                {
                    AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
                };

                manager.CreateSubscription(description);


                var factory = MessagingFactory.CreateFromConnectionString(connectionString);
                var topicClient = factory.CreateTopicClient(topic);
                var subscriptionClient = factory.CreateSubscriptionClient(topic, userName);

                subscriptionClient.OnMessage(msg => ProcessMessage(msg));
                var helloMessage = new BrokeredMessage("Has entered the room");
                helloMessage.Label = userName;
                topicClient.Send(helloMessage);
                while (true)
                {
                    string text = Console.ReadLine();
                    if (text == "exit")
                    {
                        break;
                    }

                    var chatMessage = new BrokeredMessage(text);
                    chatMessage.Label = userName;
                    topicClient.Send(chatMessage);
                }
                factory.Close();
            }
            catch(Exception ex)
            {
            }
        }

        private static void ProcessMessage(BrokeredMessage msg)
        {
            string user = msg.Label;
            string text = msg.GetBody<string>();
            Console.WriteLine(user + " > " + text);
        }
    }
}
