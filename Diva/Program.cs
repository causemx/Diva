using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva
{
	static class Program
	{
		/// <summary>
		/// 應用程式的主要進入點。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            ConfigData.ParseCommandLine(args);
            if (ConfigData.Ready)
            {
                string lang = ConfigData.GetOption(ConfigData.OptionName.Language);
                if (lang != "")
                {
                    try
                    {
                        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture =
                            new System.Globalization.CultureInfo(lang);
                    } catch (Exception e)
                    {
                        Console.WriteLine($"Failed to set UI language to {lang}:\n{e.Message}.");
                    }
                }
            }
            Application.Run(new SplashForm());
            if (SplashForm.InitOk)
                Application.Run(new Planner());
                // Application.Run(new Main());
        }
	}
}
