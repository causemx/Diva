using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Diva.EnergyConsumption
{
    public class PowerModelTools
    {
        public static readonly string PowerModelToolsRootPath = AppDomain.CurrentDomain.BaseDirectory + "Power Model Tools\\";
        private static readonly string[] PowerModelFiles = new string[]
            { "r_axy", "r_dxy", "r_dz_neg", "r_dz_pos", "r_h", "r_mvxy", "r_mvz_neg", "r_mvz_pos" };
        private static readonly DirectoryInfo TrainedModelDirectory =
            new DirectoryInfo(PowerModelToolsRootPath + "Trained_Model\\");
        private ProcessStartInfo startInfo;
        private Action<string> SetupInput;
        private Action<string> SetupOutput;

        public StreamWriter StdIn;
        public StreamReader StdOut;
        public StreamReader StdErr;
        public EventHandler Done;

        public static PowerModelTools MissionGenerator;
        public static PowerModelTools Trainer;
        public static PowerModelTools Predictor;

        private PowerModelTools(string filename, string arguments)
        {
            startInfo = new ProcessStartInfo
            {
                Arguments = arguments,
                CreateNoWindow = true,
                FileName = PowerModelToolsRootPath + filename,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = PowerModelToolsRootPath
            };
        }

        static PowerModelTools()
        {
            MissionGenerator = new PowerModelTools("", "")
            {
                SetupInput = (i) =>
                {
                },
                SetupOutput = (o) =>
                {
                }
            };
            Trainer = new PowerModelTools("Training.exe", "input.log 1")
            {
                SetupInput = (i) =>
                {
                    try
                    {
                        File.Copy(i, PowerModelToolsRootPath + "input.log", true);
                    } catch { }
                },
                SetupOutput = (o) =>
                {
                    try
                    {
                        DirectoryInfo dest = null;
                        o = PowerModel.PowerModelRootPath + o + "\\" ;
                        if (!Directory.Exists(o))
                            Directory.CreateDirectory(o);
                        dest = new DirectoryInfo(o);
                        foreach (var fi in TrainedModelDirectory.GetFiles())
                            fi.CopyTo(o + fi.Name, true);
                    } catch { }
                }
            };
            Predictor = new PowerModelTools("Predict.exe", "Param.txt input.waypoints")
            {
                SetupInput = (i) =>
                {
                    try
                    {
                        var a = i.Split(new char[] { '|' });
                        File.Copy(a[0], PowerModelToolsRootPath + "input.waypoints", true);
                        DirectoryInfo src = new DirectoryInfo(PowerModel.PowerModelRootPath + a[1]);
                        foreach (var fi in src.GetFiles())
                            fi.CopyTo(TrainedModelDirectory + fi.Name, true);
                    }
                    catch { }
                },
                SetupOutput = (o) =>
                {
                    var errmsg = Predictor.StdErr.ReadToEnd();
                    System.Windows.Forms.MessageBox.Show(errmsg != "" ?
                        errmsg : Predictor.StdOut.ReadToEnd());
                }
            };
        }

        public void Start(string input, string output)
        {
            Process proc = new Process { StartInfo = startInfo };
            SetupInput(input);
            proc.Start();
            StdErr = proc.StandardError;
            StdIn = proc.StandardInput;
            StdOut = proc.StandardOutput;
            SetupOutput(output);
            proc.Dispose();
        }

        public Task StartBackground(string input, string output)
        {
            Task task = Task.Run(() =>
            {
                Process proc = new Process { StartInfo = startInfo };
                SetupInput(input);
                proc.Start();
                StdErr = proc.StandardError;
                StdIn = proc.StandardInput;
                StdOut = proc.StandardOutput;
                SetupOutput(output);
                Done?.Invoke(proc, null);
                proc.Dispose();
            });
            return task;
        }
    }
}
