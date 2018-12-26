using Diva.Mavlink;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Utilities
{
	class QGCWaypointFileUtlity
	{
        private static string QGCWaypointFileMagic = "QGC WPL 110";

        public static List<Locationwp> ImportWaypoints(string file)
        {
            List<Locationwp> cmds = new List<Locationwp>();
            try
            {
                bool validLine(int l, string[] c)
                {
                    bool f(string s) => float.TryParse(s, out var o);
                    bool d(string s) => float.TryParse(s, out var o);
                    return c.Length == 12 && c[0] == l.ToString() &&
                        // we may have more than waypoint command in mission
                        (Enum.TryParse<MAVLink.MAV_CMD>(c[3], out var res) ||
                            int.TryParse(c[3], out int cmd) &&
                            Enum.IsDefined(typeof(MAVLink.MAV_CMD), cmd)) &&
                        f(c[4]) && f(c[5]) && f(c[6]) && f(c[7]) &&
                        d(c[8]) && d(c[9]) && f(c[10]) && c[11] == "1";
                }
                using (StreamReader reader = new StreamReader(file))
                {
                    if (reader.ReadLine() != QGCWaypointFileMagic)
                        throw new FileFormatException(file);
                    string line;
                    for (int lineno = 0; (line = reader.ReadLine()) != null; lineno++)
                    {
                        var cols = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (!validLine(lineno, cols))
                            throw new FileFormatException(file);
                        cmds.Add(new Locationwp
                        {
                            id = ushort.Parse(cols[3]),
                            p1 = float.Parse(cols[4]),
                            p2 = float.Parse(cols[5]),
                            p3 = float.Parse(cols[6]),
                            p4 = float.Parse(cols[7]),
                            lat = double.Parse(cols[8]),
                            lng = double.Parse(cols[9]),
                            alt = float.Parse(cols[10])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Planner.log.Error(ex);
            }
            return cmds;
        }

        public static void ExportWaypoints(string file, List<Locationwp> cmds, Locationwp home)
        {
            string formatHomeCoordinate(double d) => d.ToString("0.000000", CultureInfo.InvariantCulture);
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine(QGCWaypointFileMagic);
                    try
                    {
                        sw.WriteLine("0\t1\t0\t16\t0\t0\t0\t0\t" +
                            formatHomeCoordinate(home.lat) + "\t" +
                            formatHomeCoordinate(home.lng) + "\t" +
                            formatHomeCoordinate(home.alt) + "\t1");
                    }
                    catch (Exception ex)
                    {
                        Planner.log.Error(ex);
                        sw.WriteLine("0\t1\t0\t0\t0\t0\t0\t0\t0\t0\t0\t1");
                    }

                    int count = 0;

                    foreach (Locationwp wp in cmds)
                    {
                        sw.Write((count + 1)); // seq
                        sw.Write("\t" + 0); // current
                        sw.Write("\t" + "Relative"); //frame 
                        sw.Write("\t" + wp.id);
                        sw.Write("\t" + wp.p1);
                        sw.Write("\t" + wp.p2);
                        sw.Write("\t" + wp.p3);
                        sw.Write("\t" + wp.p4);
                        sw.Write("\t" + wp.lat);
                        sw.Write("\t" + wp.lng);
                        sw.Write("\t" + wp.alt);
                        sw.Write("\t" + 1);
                        sw.WriteLine("");

                        count = count + 1;
                    }

                    sw.Close();
                }
                //MessageBox.Show("Mission Saved");
            }
            catch (Exception ex)
            {
                Planner.log.Error(ex);
            }
        }

        public static void ExportParams(string file, MavDrone activeDrone)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(activeDrone.Status.param["WPNAV_SPEED"]);
                sw.WriteLine(activeDrone.Status.param["WPNAV_SPEED_UP"]);
                sw.WriteLine(activeDrone.Status.param["WPNAV_SPEED_DN"]);
                sw.WriteLine(activeDrone.Status.param["LAND_SPEED"]);
                sw.WriteLine(activeDrone.Status.param["LAND_SPEED_HIGH"]);
                sw.WriteLine(activeDrone.Status.param["RTL_SPEED"]);
                sw.WriteLine(activeDrone.Status.param["RTL_ALT"]);
                sw.WriteLine(activeDrone.Status.param["WPNAV_ACCEL"]);
                sw.WriteLine(activeDrone.Status.param["WPNAV_ACCEL_Z"]);
                sw.Close();
                sw.Dispose();
            }
            catch { }
        }
    }
}
