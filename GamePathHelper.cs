using System;
using System.IO;

public static class GamePathHelper
{
    public static void CreateGamePath(string gamePath)
    {
        Directory.CreateDirectory(gamePath);
    }

    public static bool GamePathExists(string gamePath)
    {
        return Directory.Exists(gamePath);
    }
}
