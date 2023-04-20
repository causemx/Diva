using System;

namespace Diva.Events
{
    public class ModeChangedEventArgs : EventArgs
    {
        private uint currentMode;
        private uint lastMode;
        public uint CurrentMode => currentMode;
        public uint LastMode => lastMode;

        public ModeChangedEventArgs(uint _lastMode, uint _currentMode)
        {
            currentMode = _currentMode;
            lastMode = _lastMode;

        }
    }
}
