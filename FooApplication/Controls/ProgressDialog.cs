using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	public partial class ProgressDialog : Form
	{
		private readonly BackgroundWorker _worker;
		public event CancelEventHandler Cancelled;
		public event RunWorkerCompletedEventHandler Completed;
		public event ProgressChangedEventHandler ProgressChanged;
		public event DoWorkEventHandler DoWork;

		public bool IsActive { get; set; }

		public bool IsCancelled => _worker.CancellationPending;

		public ProgressDialog()
		{
			InitializeComponent();

			_worker = new BackgroundWorker();
			_worker.ProgressChanged += Worker_ProgressChanged;
			_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
			_worker.DoWork += Worker_DoWork;
			_worker.WorkerSupportsCancellation = true;
			_worker.WorkerReportsProgress = true;
			Cancelled += dialog_Cancelled;
		}

		
		public void Run()
		{
			if (_worker.IsBusy)
			{
				Console.WriteLine("Worker is busy...");
				_worker.CancelAsync();
				
			}

			_worker.RunWorkerAsync();
		}

		public void Run(object argument)
		{
			_worker.RunWorkerAsync(argument);
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
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
			Completed?.Invoke(this, e);
			// Please implement close method in outsider if you wanted.
			// Close();
		}

		private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ProgressChanged?.Invoke(this, e);
		}

		public void ReportProgress(int percentProgress, object userState)
		{
			_worker.ReportProgress(percentProgress, userState);
		}

		public void ReportProgress(int percentProgress)
		{
			_worker.ReportProgress(percentProgress);
		}

		public string Message
		{
			get => LBL_Message.Text;
			set => LBL_Message.Text = value;
		}

		public int Progress
		{
			get => MyProgressBar.Value;
			set => MyProgressBar.Value = value;
		}


		private void dialog_Cancelled(object sender, CancelEventArgs e)
		{
			_worker.CancelAsync();
			Close();	
		}


		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			DoDispose();
		}

		private bool _disposed = false;

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
			if (_disposed) return;
			// If disposing equals true, dispose all managed 
			// and unmanaged resources. 
			if (disposing)
			{
				// Dispose managed resources.
				_worker.Dispose();
				Dispose();
			}
			// Note disposing has been done.
			_disposed = true;
			IsActive = false;
		}

		protected virtual void OnCancelled(CancelEventArgs e)
		{
			Cancelled?.Invoke(this, e);
		}
	}
}
