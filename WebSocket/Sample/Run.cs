using System;
using System.Threading.Tasks;
using GatecoinServiceInterface.Client;
using GatecoinServiceInterface.WebSocket.Client;
using GatecoinServiceInterface.WebSocket.Model;
using Newtonsoft.Json;

namespace GatecoinServiceInterface.WebSocket.Sample
{
    public class Run
    {
        private const string Root = "https://streaming.gtcprojects.com";

        public static async Task StartPublic()
        {
            var builder = new StreamingClientBuilder(Root);

            using (var client = await builder.BuildTraderClient().Start())
            {
                void TradeHandler(TradeDto arg) => Console.WriteLine(JsonConvert.SerializeObject(arg));

                var subscription = client.SubscribeAll(TradeHandler);
                var subscriptionBtcUsd = client.Subscribe("BTCUSD", TradeHandler);

                Console.WriteLine("Waiting for events");
                Console.ReadLine();

                subscription.Dispose();
                subscriptionBtcUsd.Dispose();
            }
        }

        public static async Task StartTradePrivate(string publicKey, string privateKey)
        {
            var urlHub = $"{Root}/v1/hub/trade";
            var timeStamp = DateTime.UtcNow;

            var builder = new StreamingClientBuilder(Root);

            builder
                .WithAccessToken(
                    token =>
                    {
                        token.DateTime = timeStamp;
                        token.SignedMessage = ServiceSignature.CreateToken($"{urlHub}{timeStamp.ToUnixTimeString()}", privateKey);
                        token.PublicKey = publicKey;
                    });

            using (var client = await builder.BuildTraderClient().Start())
            {
                void TradeHandler(TradeDto arg) => Console.WriteLine(JsonConvert.SerializeObject(arg));

                var subscription = client.SubscribeAll(TradeHandler);
                var subscriptionBtcUsd = client.Subscribe("BTCUSD", TradeHandler);

                Console.WriteLine("Waiting for events");
                Console.ReadLine();

                subscription.Dispose();
                subscriptionBtcUsd.Dispose();
            }
        }

        public static async Task StartTradePrivate()
        {
            const string publicKey = "publicKey";
            const string privateKey = "privateKey";

            await StartTradePrivate(publicKey, privateKey);
        }

        private class TestServiceClient : ServiceClient
        {
            public string PublicKey => ApiPublicKey;
            public string PrivateKey => ApiPrivateKey;
        }

        public static async Task StartSubscribeAndStartPrivate()
        {
            const string apiUsername = "your_login";
            const string apiPassword = "your_pass";
            const string apiBypassCaptchaCode = "your_captcha";

            var client = new TestServiceClient();
            var res = client.Login(apiUsername, apiPassword, apiBypassCaptchaCode);

            if (!res)
            {
                Console.WriteLine("Failed to login in StartSubscribeAndStartPrivate");
                return;
            }

            await StartTradePrivate(client.PublicKey, client.PrivateKey);
        }
    }
}