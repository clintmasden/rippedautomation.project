using System;

namespace RippedAutomation.Generation.Client.Models
{
    public class GenerationClientSettings
    {
        public TimeSpan AutomationTransactionTimeout { get; set; }

        public bool HasGraphicThreadLoop { get; set; }

        public int IgnoreProcessId { get; set; }
    }
}