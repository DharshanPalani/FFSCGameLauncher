using System;
using System.IO;

public static class GamePathHelper
{
    public static void CreateGamePath()
    {
        string gamePath = @"C:\FriendlyFriendsStudentCouncil";

        Directory.CreateDirectory(gamePath);
    }
}
