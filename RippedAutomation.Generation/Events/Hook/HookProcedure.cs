using System;

namespace RippedAutomation.Generation.Events.Hook
{
    internal delegate IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam);
}