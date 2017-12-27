using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LagoVista.Core.IOC;
using LagoVista.MQTT.Tests.Utils;
using LagoVista.MQTT.Core;
using LagoVista.MQTT.Core.Clients;
using System.Threading.Tasks;

namespace LagoVista.MQTT.Tests
{
    [TestClass]
    public class ConnectionTests
    {
        MQTTDeviceClient _mqttDeviceClient;

        [TestInitialize]
        public void Init()
        {
            _mqttDeviceClient = new MQTTDeviceClient(new TestChannel());
        }

        [TestMethod]
        public async Task ShouldSuccessfullyConnect()
        {
            _mqttDeviceClient.BrokerHostName = "mqttdev.nuviot.com";
            _mqttDeviceClient.BrokerPort = 1883;
            //_mqttDeviceClient.DeviceId = /* add a valid user name */;
            //_mqttDeviceClient.Password = /* Add a valid password */;
            var result = await _mqttDeviceClient.ConnectAsync();
            Assert.AreEqual(LagoVista.Core.Networking.Interfaces.ConnAck.Accepted, result.Result);
        }

        [TestMethod]
        public async Task ShouldNotConnect()
        {
            _mqttDeviceClient.BrokerHostName = "mqttdev.nuviot.com";
            _mqttDeviceClient.BrokerPort = 1883;
            //_mqttDeviceClient.DeviceId = /* add some sort of password */;
            _mqttDeviceClient.ShowDiagnostics = true;
            //_mqttDeviceClient.Password = /* Add an invalid password */;
            var result = await _mqttDeviceClient.ConnectAsync();
            Assert.AreEqual(LagoVista.Core.Networking.Interfaces.ConnAck.NotAuthorized, result.Result);
        }
    }
}
