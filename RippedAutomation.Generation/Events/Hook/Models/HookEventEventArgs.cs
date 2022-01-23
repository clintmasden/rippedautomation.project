using System;

namespace RippedAutomation.Generation.Events.Hook.Models
{
    /// <summary>
    ///     HookEvent EventArgs
    /// </summary>
    public class HookEventEventArgs : EventArgs
    {
        public HookEvent HookEvent { get; set; }
    }
}