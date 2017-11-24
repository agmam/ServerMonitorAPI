using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class EventType
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public bool ShouldNotify { get; set; }
        public decimal PeakValue { get; set; }
        public int RiskEstimate { get; set; }
        public List<Event> Events { get; set; }
        public enum Type { Highcpu = 1, LowMemory = 2, ServerDown = 3, LowDiskSpace = 4, HighNetworkUtilization = 5, HighCpuTemperature = 6 }

        public String setName(Type name)
        {
            switch (name)
            {
                case Type.Highcpu:
                    Name = "High Cpu";
                    break;
                case Type.LowDiskSpace:
                    Name = "Low disk space";
                    break;
                case Type.ServerDown:
                    Name = "Server down";
                    break;
                case Type.LowMemory:
                    Name = "Low memory";
                    break;
                case Type.HighNetworkUtilization:
                    Name = "High network utilization";
                    break;
                case Type.HighCpuTemperature:
                    Name = "High cpu temperature";
                    break;
                default:
                    Name = "Warning";
                    break;
            }
            return Name;
        }
    }
}