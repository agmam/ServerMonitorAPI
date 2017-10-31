using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class EventType : Entity
    {
        public string Name { get; private set; }
        public bool ShouldNotify { get; set; }
        public decimal PeakValue { get; set; }
        public int RiskEstimate { get; set; }
        public List<Event> Events { get; set; }
        public enum Type { Highcpu = 1, LowMemory = 2, ServerDown = 3, LowDiskSpace = 4 }

        public void setName(Type name)
        {
            switch (name)
            {
                case Type.Highcpu:
                    Name = "Highcpu";
                    break;
                case Type.LowDiskSpace:
                    Name = "LowDiskSpace";
                    break;
                case Type.ServerDown:
                    Name = "ServerDown";
                    break;
                case Type.LowMemory:
                    Name = "LowMemory";
                    break;
                default:
                    Name = "Warning";
                    break;
            }
        
        }
    }
}