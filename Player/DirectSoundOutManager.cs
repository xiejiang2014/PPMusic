using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;

namespace PPMusic.Player
{
    public static class DirectSoundOutManager
    {
        public static IEnumerable<DirectSoundDeviceInfo> PlayDevices => DirectSoundOut.Devices.Where(v => !string.IsNullOrWhiteSpace(v.ModuleName));
    }
}