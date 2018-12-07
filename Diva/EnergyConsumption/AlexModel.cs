using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Diva.Utilities;

namespace Diva.EnergyConsumption
{
    public class AlexModel : PowerModel
    {
        private class AlexModelTools
        {
            public static readonly string AlexModelToolsRoot = AppDomain.CurrentDomain.BaseDirectory + "Power Model Tools\\";
            public static readonly string[] PowerModelFiles = new string[]
                { "r_axy", "r_dxy", "r_dz_neg", "r_dz_pos", "r_h", "r_mvxy", "r_mvz_neg", "r_mvz_pos" };
            private static readonly DirectoryInfo TrainedModelDirectory =
                new DirectoryInfo(AlexModelToolsRoot + "Trained_Model\\");
            private ProcessStartInfo startInfo;
            private Action<string> SetupInput;
            private Action<string> SetupOutput;

            public StreamWriter StdIn;
            public StreamReader StdOut;
            public StreamReader StdErr;
            public object Output;

            public static AlexModelTools MissionGenerator;
            public static AlexModelTools Trainer;
            public static AlexModelTools Predictor;

            private AlexModelTools(string filename, string arguments)
            {
                startInfo = new ProcessStartInfo
                {
                    Arguments = arguments,
                    CreateNoWindow = true,
                    FileName = AlexModelToolsRoot + filename,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = AlexModelToolsRoot
                };
            }

            static AlexModelTools()
            {
                MissionGenerator = new AlexModelTools("Training_Pattern.exe", "")
                {
                    SetupInput = (i) => { MissionGenerator.startInfo.Arguments = i; },
                    SetupOutput = (o) => { MissionGenerator.Output = AlexModelToolsRoot + "new_training_pattern.txt"; }
                };
                Trainer = new AlexModelTools("Training.exe", "input.log 1")
                {
                    SetupInput = (i) =>
                    {
                        try
                        {
                            File.Copy(i, AlexModelToolsRoot + "input.log", true);
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
                Predictor = new AlexModelTools("Predict.exe", "Param.txt input.waypoints")
                {
                    SetupInput = (i) =>
                    {
                        try
                        {
                            DirectoryInfo src = new DirectoryInfo(i);
                            foreach (var fi in src.GetFiles())
                                fi.CopyTo(TrainedModelDirectory + fi.Name, true);
                        }
                        catch { }
                    },
                    SetupOutput = (o) =>
                    {
                        try
                        {
                            var errmsg = Predictor.StdErr.ReadToEnd();
                            if (errmsg != "")
                                System.Windows.Forms.MessageBox.Show(errmsg);
                            var output = Predictor.StdOut.ReadToEnd();
                            double.TryParse(output.Split(new char[] { ' ' })[1], out var power);
                            Predictor.Output = power;
                        }
                        catch { }
                    }
                };
            }

            public object Start(string input, string output)
            {
                Process proc = new Process { StartInfo = startInfo };
                SetupInput(input);
                proc.Start();
                StdErr = proc.StandardError;
                StdIn = proc.StandardInput;
                StdOut = proc.StandardOutput;
                SetupOutput(output);
                proc.Dispose();
                return Output;
            }
        }

        public AlexModel(string name)
        {
            ModelName = name;
        }

        public static List<Locationwp> GenerateTrainingMission(Locationwp home) =>
            QGCWaypointFileUtlity.ImportWaypoints(
                (string)AlexModelTools.MissionGenerator.Start(
                home.lat.ToString() + "," + home.lng.ToString(), null),
                out var newhome);

        public static void TrainNewModel(string file, string name) =>
            AlexModelTools.Trainer.Start(file, name);

        public new static bool ContainsModel(string path) =>
            !AlexModelTools.PowerModelFiles.Where(f => !File.Exists(path + "\\" + f)).Any();

        public override double CalculateEnergyConsumption(Mavlink.MavDrone drone, List<Locationwp> wps, Locationwp home)
        {
            QGCWaypointFileUtlity.ExportParams(AlexModelTools.AlexModelToolsRoot + "Param.txt", drone);
            QGCWaypointFileUtlity.ExportWaypoints(AlexModelTools.AlexModelToolsRoot + "input.waypoints", wps, home);
            return (double)AlexModelTools.Predictor.Start(ModelPath, null);
        }
    }
}
