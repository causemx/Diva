using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Diva.EnergyConsumption
{
    class PowerModelTools
    {
        static readonly string PowerModelUtilitiesRootPath = AppDomain.CurrentDomain.BaseDirectory;
        private ProcessStartInfo startInfo;
        private Action<string> SetupInput;
        private Action<string> SetupOutput;

        public StreamWriter StdIn;
        public StreamReader StdOut;
        public StreamReader StdErr;
        public EventHandler Done;

        public static PowerModelTools Trainer;
        public static PowerModelTools Predictor;

        private PowerModelTools(string filename, string arguments)
        {
            startInfo = new ProcessStartInfo
            {
                Arguments = arguments,
                CreateNoWindow = true,
                FileName = filename,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = PowerModelUtilitiesRootPath
            };
        }

        static PowerModelTools()
        {
            Trainer = new PowerModelTools("Training.exe", "1 input.log")
            {
                SetupInput = (i) =>
                {
                    try
                    {
                        File.Copy(i, "input.log", true);
                    } catch { }
                },
                SetupOutput = (o) =>
                {
                    try
                    {
                        new DirectoryInfo("Trained_Model").MoveTo(o);
                    } catch { }
                }
            };
            Predictor = new PowerModelTools("Predict.exe", "Param.txt foo.waypoints")
            {
                SetupInput = (i) => { },
                SetupOutput = (o) => { }
            };
        }

        public void Start(string input, string output)
        {
            Process proc = new Process { StartInfo = startInfo };
            proc.Start();
            StdErr = proc.StandardError;
            StdIn = proc.StandardInput;
            StdOut = proc.StandardOutput;
            proc.Dispose();
        }

        public Task StartAsync(string input, string output)
        {
            var tcs = new TaskCompletionSource<int>();
            Process proc = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };
            proc.Exited += (o, a) =>
            {
                tcs.TrySetResult(proc.ExitCode);
                Done?.Invoke(proc, a);
            };
            proc.Start();
            StdErr = proc.StandardError;
            StdIn = proc.StandardInput;
            StdOut = proc.StandardOutput;
            proc.Dispose();
            return tcs.Task;
        }
    }
}
