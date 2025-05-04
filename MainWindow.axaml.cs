using Avalonia.Controls;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameLauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _ = OnStartClickedAsync(sender, e);
        }

        private async Task OnStartClickedAsync(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string configPath = ConfigHelper.GetConfigPath();
            GameConfig config;

            if (File.Exists(configPath))
            {
                string existingConfigJson = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<GameConfig>(existingConfigJson)
                    ?? throw new InvalidOperationException("Failed to parse config.");
                Console.WriteLine($"Existing config loaded: {existingConfigJson}");
            }
            else
            {
                config = new GameConfig
                {
                    GameInstallPath = @"C:\FriendlyFriendsStudentCouncil\",
                    GameVersion = "1.0.0"
                };

                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Console.WriteLine("New config written to file.");
            }

            if (!GamePathHelper.GamePathExists(config.GameInstallPath))
            {
                Console.WriteLine("Game path does not exist. Creating it now.");
                GamePathHelper.CreateGamePath(config.GameInstallPath);
            }
            else
            {
                Console.WriteLine("Game path exists.");
            }

            if (!RunGameHelper.CheckGameExecutableExists(config.GameInstallPath))
            {
                Console.WriteLine("Game executable does not exist.");
                await GameFetchHelper.InstallGameAsync(config.GameInstallPath);
            }
            else
            {
                Console.WriteLine("Game executable exists.");
            }

            RunGameHelper.RunGameExecutable(config.GameInstallPath);
            Close();
        }
    }
}
