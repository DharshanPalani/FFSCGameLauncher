using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;

namespace GameLauncher
{
    public static class GameFetchHelper
    {
        private const string DownloadUrl = "https://www.dropbox.com/scl/fi/jeuo2nwpppibc0qbzki1a/FFSC_4.zip?rlkey=774mj7g2tf168wkl4lqxdxt1l&st=g5djpkwh&dl=1";
        private const string ZipFileName = "download.zip";

        public static async Task InstallGameAsync(string installPath)
        {
            string zipPath = Path.Combine(installPath, ZipFileName);

            try
            {
                Console.WriteLine("Starting game download...");

                using HttpClient client = new HttpClient();
                var response = await client.GetAsync(DownloadUrl);
                response.EnsureSuccessStatusCode();

                await using var fs = new FileStream(zipPath, FileMode.Create);
                await response.Content.CopyToAsync(fs);

                Console.WriteLine("Download complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Download failed: " + ex.Message);
                return;
            }

            try
            {
                Console.WriteLine("Extracting game files...");
                ZipFile.ExtractToDirectory(zipPath, installPath, overwriteFiles: true);
                Console.WriteLine("Extraction complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Extraction failed: " + ex.Message);
                return;
            }

            try
            {
                Console.WriteLine("Cleaning up...");
                File.Delete(zipPath);
                Console.WriteLine("Zip file deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete zip file: " + ex.Message);
            }
        }
    }
}
