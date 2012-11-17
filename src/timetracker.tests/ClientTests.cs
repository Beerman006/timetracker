using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beerman006.TimeTracker.Modeling;

namespace timetracker.tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void Client_HasCorrectName()
        {
            var client = new Client("Foo");
            Assert.AreEqual("Foo", client.Name);
        }

        [TestMethod]
        public void Client_HasNoDefaultChargeCode()
        {
            var client = new Client("Foo");
            Assert.IsTrue(string.IsNullOrEmpty(client.DefaultChargeCode));
        }

        [TestMethod]
        public void Client_HasDefaultChargeCode()
        {
            var client = new Client("Foo", "default charge code");
            Assert.AreEqual("default charge code", client.DefaultChargeCode);
        }

        [TestMethod]
        public void Client_WorkTypeGivenDefaultChargeCode()
        {
            var client = new Client("Foo", "default");
            client.AddWorkType("work");
            Assert.AreEqual("default", client.GetChargeCodeFromWorkType("work"));
        }

        [TestMethod]
        public void Client_WorkTypeGivenChargeCode()
        {
            var client = new Client("Foo", "default");
            client.AddWorkType("work", "charge code");
            Assert.AreEqual("charge code", client.GetChargeCodeFromWorkType("work"));
        }

        [TestMethod]
        public void Client_UnknownWorkTypeGivesDefaultChargeCode()
        {
            var client = new Client("Foo", "default");
            Assert.AreEqual("default", client.GetChargeCodeFromWorkType("work"));
        }

        [TestMethod]
        public void Client_NoWorkTypeGivesDefaultChargeCode()
        {
            var client = new Client("Foo", "default");
            Assert.AreEqual("default", client.GetChargeCodeFromWorkType(string.Empty));
        }

        [TestMethod]
        public void Client_DuplicateWorkTypeDoesntThrow()
        {
            var client = new Client("Foo", "default");
            client.AddWorkType("type");
            client.AddWorkType("type");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Client_DuplicateWorkTypesUnderDifferentChargeCodesThrows()
        {
            var client = new Client("Foo", "default");
            client.AddWorkType("type", "Foo1");
            client.AddWorkType("type", "Foo2");
        }
    }
}
