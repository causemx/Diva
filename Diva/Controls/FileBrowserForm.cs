using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class FileBrowserForm : Form
	{

		public List<string> listFiles = new List<string>();
		public event EventHandler MissionClick;

		public FileBrowserForm()
		{
			InitializeComponent();
			OnLoad();

		}


		private void OnLoad()
		{
			listFiles.Clear();
			LvMission.Items.Clear();

			//TODO: remove the hard code

			try
			{
				String myMissionPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Missions\";
				Planner.log.Info("temp path: " + myMissionPath);

				if (!Directory.Exists(myMissionPath))
				{
					Directory.CreateDirectory(myMissionPath);
				}

				foreach (string item in Directory.GetFiles(myMissionPath))
				{
					imageList1.Images.Add(Icon.ExtractAssociatedIcon(item));
					FileInfo fi = new FileInfo(item);
					listFiles.Add(fi.FullName);
					LvMission.Items.Add(fi.Name, imageList1.Images.Count - 1);
				}
			}
			catch (Exception exception)
			{
				Planner.log.Debug(exception.ToString());
			}

		}


		private void LvMission_SelectedIndexChanged(object sender, EventArgs e)
		{
			
			
			if (LvMission.FocusedItem != null)
			{
				LvMission.FocusFile = listFiles[LvMission.FocusedItem.Index];
				this.MissionClick(sender, e);
			}

		}

		
	}

}
