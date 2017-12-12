using System;
using System.Collections.Generic;
using Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMonitorAPI.Logic;

namespace ServerMonitorAPI.Tests
{
    [TestClass]
    public class EventCheckerTests
    {    private static List<EventType> eventTypes = new List<EventType>();
        private static ServerDetailAverage serverDetailAverage = new ServerDetailAverage();

        [TestInitialize]
        public void TestInitializer()
        {   eventTypes.Clear();
            serverDetailAverage = null;
            EventType lowMemoryEventType = new EventType()
            {
                Id = 1,
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 1,
                RiskEstimate = 1,
            };
            lowMemoryEventType.setName(EventType.Type.LowMemory);
            eventTypes.Add(lowMemoryEventType);

            EventType highCpuEventType = new EventType()
            {
                Id = 2, 
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 1,
                RiskEstimate = 2,
            };
            highCpuEventType.setName(EventType.Type.Highcpu); eventTypes.Add(highCpuEventType);

            EventType highCpuTemperatureEventType = new EventType()
            {
                Id = 3,
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 1,
                RiskEstimate = 1,
            };
            highCpuTemperatureEventType.setName(EventType.Type.HighCpuTemperature); eventTypes.Add(highCpuTemperatureEventType);

            EventType networkUtilizationHighEventType = new EventType()
            {
                Id = 4,
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 1,
                RiskEstimate = 1,
            };
            networkUtilizationHighEventType.setName(EventType.Type.HighNetworkUtilization); eventTypes.Add(networkUtilizationHighEventType);

            EventType serverDownEventType = new EventType()
            {
                Id = 5,
                ShouldNotify = false,
                Created = DateTime.Now,
                PeakValue = 1,
                RiskEstimate = 1,
            };
            serverDownEventType.setName(EventType.Type.ServerDown); eventTypes.Add(serverDownEventType);

            EventType lowDiskSpaceEventType = new EventType()
            {
                Id = 6,
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 1,
                RiskEstimate = 3,
            };
            lowDiskSpaceEventType.setName(EventType.Type.LowDiskSpace); eventTypes.Add(lowDiskSpaceEventType);
        }


        [TestMethod]
        public void CheckForEventwithNullDetail()
        {
            serverDetailAverage = null;
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(0, events.Count);
        }
        [TestMethod]
        public void CheckForEventwithNullEventtype()
        {
            serverDetailAverage = null;
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, null);
            Assert.AreEqual(0, events.Count);
        }

        [TestMethod]
        public void CheckForEventwithOneEventCpu()
        {
            serverDetailAverage = new ServerDetailAverage()
            {
                CPUUtilization = 10
            };
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(new EventType().setName(EventType.Type.Highcpu), events[0].EventType.Name);
        }

        [TestMethod]
        public void CheckForEventwithOneEventNetwork()
        {
            serverDetailAverage = new ServerDetailAverage()
            {
                NetworkUtilization = 10
            };
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(new EventType().setName(EventType.Type.HighNetworkUtilization), events[0].EventType.Name);
        }

        [TestMethod]
        public void CheckForEventwithOneEventTemp()
        {
            serverDetailAverage = new ServerDetailAverage()
            {
                Temperature = 10
            };
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(new EventType().setName(EventType.Type.HighCpuTemperature), events[0].EventType.Name);
        }
        [TestMethod]
        public void CheckForEventwithOneEventRam()
        {
            serverDetailAverage = new ServerDetailAverage()
            {
                RAMAvailable = 0,
                RAMTotal = 10
            };
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(new EventType().setName(EventType.Type.LowMemory), events[0].EventType.Name);
        }

        [TestMethod]
        public void CheckForEventwithOneEventDisk()
        {
            serverDetailAverage = new ServerDetailAverage()
            {
                HarddiskTotalSpace = 10
                , HarddiskUsedSpace = 10
            };
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(new EventType().setName(EventType.Type.LowDiskSpace), events[0].EventType.Name);
        }

        [TestMethod]
        public void CheckForEventwithMultiEvent()
        {
            serverDetailAverage = new ServerDetailAverage()
            {
                Temperature = 10,
                CPUUtilization = 10,
                RAMTotal = 10,
                RAMAvailable = 0  ,
                NetworkUtilization = 10,
                HarddiskTotalSpace = 10,
                HarddiskUsedSpace = 10
            };
            var eventchecker = new EventChecker();
            var events = eventchecker.CheckForEvent(serverDetailAverage, eventTypes);
            Assert.AreEqual(5, events.Count);
        }
    }
}
