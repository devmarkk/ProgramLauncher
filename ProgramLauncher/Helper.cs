using Microsoft.Toolkit.Uwp.Notifications;
using System.Text.Json;

namespace ProgramLauncher
{
    internal class Helper
    {
        public static void ShowMessage(string message)
        {
            new ToastContentBuilder()
                  .AddArgument("action", "viewConversation")
                  .AddArgument("conversationId", 9813)
                  .AddText("Message:")
                  .AddText(message)
                  .Show();
        }

        public static bool TryLoadConfiguration(out Configuration configuration)
        {
            configuration = null;

            //IConfigurationRoot configurationRoot =
            //    new ConfigurationBuilder()
            //            .SetBasePath(Directory.GetCurrentDirectory())
            //            .AddJsonFile("appsettings.json")
            //            .Build();


            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            if (!File.Exists(path))
            {
                ShowMessage($"Arquivo de configuração não encontrado em: {path}");
                return false;
            }

            string fileContent = File.ReadAllText(path);

            configuration = JsonSerializer.Deserialize<Configuration>(fileContent);

            if (configuration is null || !configuration.IsValid())
            {
                ShowMessage($"Não foi possível mapear o arquivo de configuração em: {path}");
                return false;
            }

            return true;
        }
    }

    public class Configuration
    {
        public Configuration()
        {
            DaysOff = Array.Empty<DayOfWeek>();
            Paths = Array.Empty<string>();
        }

        public DayOfWeek[] DaysOff { get; set; }

        public string[] Paths { get; set; }

        public bool IsValid()
        {
            return Paths.Any();
        }
    }
}
