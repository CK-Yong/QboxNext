﻿using System.Linq;
using NUnit.Framework;
using QboxNext.Qboxes.Parsing.Protocols;

namespace QboxNext.Qboxes.Parsing.Mini.Response
{
    [TestFixture]
    public class MiniResponseTest
    {
        [Test]
        public void ParseResponseWithFirmwareUrlTest()
        {
            // Arrange, voorbeeld response uit qplat-52
            const string source = "040A470BD80001\"firmware-acc.QboxNext.nl\"02\"qserver-acc.QboxNext.nl\"";

            // Act
            var actual = new MiniResponse().Parse(source) as ResponseParseResult;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.DeviceSettings);
            Assert.IsTrue(actual.DeviceSettings.Count() == 2, "2 device settings expected");
        }
    }
}
