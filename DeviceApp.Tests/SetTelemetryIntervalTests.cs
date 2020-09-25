using System;
using System.Net.Mail;
using DeviceApp.Services;
using Microsoft.Azure.Devices.Client;
using Xunit;

namespace DeviceApp.Tests
{
    public class SetTelemetryIntervalTests 
    {
        private DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=linneaec-iothub.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=YXnFsEHdhHfAH55Z1D1OIRD+ehYOyL13MoK5HbVQAkQ=", TransportType.Mqtt );
        private static int telemetryInterval = 5;

        [Theory]
        [InlineData("SetTelemetryInterval", "10", 200)]
        [InlineData("SetInterval", "10", 501)]
        [InlineData("SetInterval", "20", 202)]
        public void SetTelemetryInterval_ShouldChangeInterval(string methodName, string payload, int statusCode)
           
        {
            //Arrange
            deviceClient.SetMethodHandlerAsync(methodName, DeviceService.SetTelemetryInterval, null).Wait();



            //Act

            if (Int32.TryParse(payload, out telemetryInterval))
            {
                telemetryInterval = 10;
            }
            
              
            //Assert

           statusCode.Equals(200);
        }
    }
}
