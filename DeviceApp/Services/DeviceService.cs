using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;


namespace DeviceApp.Services
{
    public static class DeviceService
    {
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=linneaec-iothub.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=YXnFsEHdhHfAH55Z1D1OIRD+ehYOyL13MoK5HbVQAkQ=", TransportType.Mqtt);
        private static int telemetryInterval = 5;
        private static int ttelemetryInterval = 5;
        private static Random rnd = new Random();
        public static Task<MethodResponse> SetTelemetryInterval(MethodRequest request, object userContext)  //ta emot funktion som sätter telemetryintervalen
        {
            var payload = Encoding.UTF8.GetString(request.Data).Replace("\"", "");

            Console.WriteLine(payload);


            if 
               (Int32.TryParse(payload, out ttelemetryInterval)) //HÄR sätts den
            {
                telemetryInterval = 10;
                Console.WriteLine($"interval set to{ telemetryInterval } seconds.");

                string json = "{\"result\": \" Exucuted direct method: " + request.Name + "\"  }"; //Json meddelandet som skapas, bygger ihop själv


                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 200));
            }
            else
            {
                string json = "{\"result\": \" Method not implemented: \"}"; //Json meddelandet som skapas, bygger ihop själv
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 501));
            }
        }

        private static async Task SendMessageAsync() //använd i uppgit 4 Skicka meddelanden


        {
            while (true)
            {

                double temp = 10 + rnd.NextDouble() * 15;
                double hum = 40 + rnd.NextDouble() * 20;

                var data = new
                {
                    temperature = temp,
                    humidity = hum
                };

                var json = JsonConvert.SerializeObject(data);
                var payload = new Message(Encoding.UTF8.GetBytes(json));  // Nästlade satser som vi ska börja använda
                payload.Properties.Add("temperaturAlert", (temp > 30) ? "true" : "false");      //andra IF satsen vi tittat på

                await deviceClient.SendEventAsync(payload);
                Console.WriteLine($"Messege sent: {json}"); // Med ett test kolla så payloaden blev rätt

                await Task.Delay(telemetryInterval * 1000);
            }
        }
    }
}
