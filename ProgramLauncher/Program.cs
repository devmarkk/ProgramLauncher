// See https://aka.ms/new-console-template for more information
using ProgramLauncher;
using System.Diagnostics;

if (!Helper.TryLoadConfiguration(out Configuration configuration))
    return;


DateTime now = DateTime.Now;

if (configuration.DaysOff.Contains(now.DayOfWeek))
    return;

string errorMessages = string.Empty;

foreach (var path in configuration.Paths)
{
    try
    {
        Process.Start(path);
    }
    catch (Exception)
    {
        errorMessages += path.Split("\\").LastOrDefault() + ", ";
    }
}

if (!string.IsNullOrEmpty(errorMessages))
    Helper.ShowMessage(errorMessages);

