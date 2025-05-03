using System;

namespace GameLauncher
{
    public class GameConfig
    {
        public string GameInstallPath { get; set; } = "";
        public string GameVersion { get; set; } = "";
        public DateTime LastLaunched { get; set; }
    }
}
