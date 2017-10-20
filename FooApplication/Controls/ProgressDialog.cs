using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	public partial class ProgressDialog : Form
	{
		private BackgroundWorker worker;
		public event CancelEventHandler Cancelled;
		public event RunWorkerCompletedEventHandler Completed;
		public event ProgressChangedEventHandler ProgressChanged;
		public event DoWorkEventHandler DoWork;

		public bool isActive { get; set; }

		public bool IsCancelled
		{
			get { return worker.CancellationPending; }
		}

		public ProgressDialog()
		{
			InitializeComponent();

			worker = new BackgroundWorker();
			worker.ProgressChanged += Worker_ProgressChanged;
			worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
			worker.DoWork += worker_DoWork;
			worker.WorkerSupportsCancellation = true;
			worker.WorkerReportsProgress = true;
			this.Cancelled += dialog_Cancelled;
		}

		
		public void Run()
		{
			if (worker.IsBusy)
			{
				Console.WriteLine("worker is busy...");
				worker.CancelAsync();
				
			}

			worker.RunWorkerAsync();
			ShowDialog();
		}

		public void Run(object argument)
		{
			worker.RunWorkerAsync(argument);
			ShowDialog();
		}

		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			
			if (DoWork != null)
			{
				DoWork(this, e);
				e.Cancel = IsCancelled || e.Cancel;
			}
			else
			{
				MessageBox.Show("No work to do!", "No Work", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (Completed != null)
			{
				Completed(this, e);
			}


			Close();
		}

		private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (ProgressChanged != null)
			{
				ProgressChanged(this, e);
			}
		}

		public void ReportProgress(int percentProgress, object userState)
		{
			worker.ReportProgress(percentProgress, userState);
		}

		public void ReportProgress(int percentProgress)
		{
			worker.ReportProgress(percentProgress);
		}

		public string Message
		{
			get { return lbl_message.Text; }
			set { lbl_message.Text = value; }
		}

		public int Progress
		{
			get { return progressBar.Value; }
			set
			{
				progressBar.Value = value;
			}
		}

		private void but_nagtive_Click(object sender, EventArgs e)
		{
			Close();
			/*
			if (MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				but_nagtive.Enabled = false;
				this.Message = "Cancelling...";
				if (Cancelled != null)
				{
					Cancelled(this, new CancelEventArgs(true));
				}
			}*/
		}

		void dialog_Cancelled(object sender, CancelEventArgs e)
		{
			worker.CancelAsync();
			Close();
			DoDispose();
			/*
			if (Cancelled != null)
			{
				Cancelled(this, e);
			}*/
		}


		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Console.WriteLine("onClosed");
			DoDispose();
		}

		private bool disposed = false;

		public void DoDispose()
		{
			DoDispose(true);
			// This object will be cleaned up by the Dispose method. 
			// Therefore, you should call GC.SupressFinalize to 
			// take this object off the finalization queue 
			// and prevent finalization code for this object 
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		protected virtual void DoDispose(bool disposing)
		{
			// Check to see if Dispose has already been called. 
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed 
				// and unmanaged resources. 
				if (disposing)
				{
					// Dispose managed resources.
					worker.Dispose();
					this.Dispose();
				}
				// Note disposing has been done.
				disposed = true;
				isActive = false;
			}
		}
	}
}
