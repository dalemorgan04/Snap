using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snap.Models
{
    public class SnapEvent
    {
        public bool EventTriggered { get; set; } = false;
        public Player TriggerPlayer { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
