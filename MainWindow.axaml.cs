using Avalonia.Controls;
using System;
using System.IO;
using System.Text.Json;

namespace GameLauncher;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnStartClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var configPath = ConfigHelper.GetConfigPath();

        GamePathHelper.CreateGamePath();
        
        if (File.Exists(configPath))
        {
            var existingConfig = File.ReadAllText(configPath);
            Console.WriteLine("Existing config loaded: " + existingConfig);
        }
        else
        {
            var config = new
            {
                GameInstallPath = @"C:\FriendlyFriendsStudentCouncil\",
                GameVersion = "2.0.0"
            };

            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine("New config written to file.");
        }


        RunGameHelper.RunGameExecutable();
        
        Close();
    }
}
