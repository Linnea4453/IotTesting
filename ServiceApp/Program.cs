using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace ServiceApp
{
    class Program
    {
        private static ServiceClient serviceclient = ServiceClient.CreateFromConnectionString("HostName=linneaec-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=oNIM8ijUPIBTlmVwTOnrL150m1P/yFoGCbQCbyKOzsI=");


        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();

            InvokeMethod("Deviceapp", "SetTelemetryInterval").GetAwaiter();

        }

        static async Task InvokeMethod(string deviceId, string methodName)
        {
            var methodInvokation = new CloudToDeviceMethod("SetTelemetryInterval") { ResponseTimeout = TimeSpan.FromSeconds(30) };

            var response = await serviceclient.InvokeDeviceMethodAsync("DeviceApp", methodInvokation);
            Console.WriteLine($"response satus: { response.Status}");
            Console.WriteLine(response.GetPayloadAsJson());
        }
    }
}
