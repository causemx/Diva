using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Dialogs
{
    public class DialogProgress : DialogOrigin
    {
        #region UI component
        private System.Windows.Forms.Label lbl_progress_message;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel1;
        #endregion
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbl_progress_message = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.lbl_progress_message);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 195);
            this.panel1.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar.Location = new System.Drawing.Point(95, 133);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(165, 23);
            this.progressBar.TabIndex = 4;
            // 
            // lbl_progress_message
            // 
            this.lbl_progress_message.AutoEllipsis = true;
            this.lbl_progress_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_progress_message.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_progress_message.Location = new System.Drawing.Point(118, 14);
            this.lbl_progress_message.MaximumSize = new System.Drawing.Size(150, 112);
            this.lbl_progress_message.MinimumSize = new System.Drawing.Size(0, 112);
            this.lbl_progress_message.Name = "lbl_progress_message";
            this.lbl_progress_message.Size = new System.Drawing.Size(118, 112);
            this.lbl_progress_message.TabIndex = 3;
            this.lbl_progress_message.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // DialogProgress
            // 
            this.ClientSize = new System.Drawing.Size(346, 290);
            this.Controls.Add(this.panel1);
            this.Name = "DialogProgress";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Exception workerException;
        public ProgressWorkerEventArgs doWorkArgs;

        public object objlock = new object();
        public int _progress = -1;
        public string _status = "";
        public bool running = false;

        public delegate void DoWorkEventHandler(object sender, ProgressWorkerEventArgs e, object passdata = null);

        public event DoWorkEventHandler DoWork;

        public DialogProgress(string caption, MessageBoxButtons mbs, MessageBoxIcon mbi) : base(caption, mbs, mbi)
        {
            InitializeComponent();
            doWorkArgs = new ProgressWorkerEventArgs();
        }

        public void RunBackgroundOperationAsync()
        {
            ThreadPool.QueueUserWorkItem(RunBackgroundOperation);
            this.ShowDialog();
        }

        public void RunBackgroundOperation(object o)
        {
            running = true;
            log.Info("RunBackgroundOperation");

            while (this.IsHandleCreated == false)
            {
                Thread.Sleep(200);
            }

            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Refresh();
                });
            }
            catch
            {
                running = false;
                return;
            }

            log.Info("Focus ctl");

            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    log.Info("in focus invoke");
                    // if this windows isnt the current active windows, popups inherit the wrong parent.
                    if (!this.Focused)
                    {
                        this.Focus();
                        this.Refresh();
                    }
                });
            }
            catch { running = false; return; }

            try
            {
                log.Info("DoWork");
                if (this.DoWork != null) DoWork(this, doWorkArgs);
                log.Info("DoWork Done");
            }
            catch (Exception e)
            {
                // The background operation thew an exception.
                // Examine the work args, if there is an error, then display that and the exception details
                // Otherwise display 'Unexpected error' and exception details
                // timer1.Stop();
                ShowDoneWithError(e, doWorkArgs.ErrorMessage);
                running = false;
                return;
            }

            // stop the timer
            // timer1.Stop();

            // run once more to do final message and progressbar
            if (this.IsDisposed || this.Disposing || !this.IsHandleCreated)
            {
                return;
            }

            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Timer1_Tick(null, null);
                });
            }
            catch
            {
                running = false;
                return;
            }

            if (doWorkArgs.CancelRequested && doWorkArgs.CancelAcknowledged)
            {
                //ShowDoneCancelled();
                running = false;
                this.BeginInvoke((MethodInvoker)this.Close);
                return;
            }

            if (!string.IsNullOrEmpty(doWorkArgs.ErrorMessage))
            {
                ShowDoneWithError(null, doWorkArgs.ErrorMessage);
                running = false;
                return;
            }

            if (doWorkArgs.CancelRequested)
            {
                ShowDoneWithError(null, "Operation could not cancel");
                running = false;
                return;
            }

            ShowDone();
            running = false;
        }

        private void ShowDone()
        {
            if (!this.IsHandleCreated)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                this.progressBar.Value = 100;
            });

            Thread.Sleep(100);

            this.BeginInvoke((MethodInvoker)this.Close);
        }

        private void ShowDoneWithError(Exception exception, string doWorkArgs)
        {
            var errMessage = doWorkArgs ?? "There was an unexpected error";

            if (this.Disposing || this.IsDisposed)
                return;

            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Text = "Error";
                        this.lbl_progress_message.Text = errMessage;
                        this.progressBar.Visible = false;
                        this.workerException = exception;
                    });
                }
                catch { } // disposing
            }
        }

        protected override void Cancel_Click(object sender, EventArgs e)
        {
            base.Cancel_Click(sender, e);

            lbl_progress_message.Text = "Cancelling...";
            progressBar.Style = ProgressBarStyle.Marquee;

            doWorkArgs.CancelRequested = true;
        }

        /// <summary>
        /// Called from the BG thread
        /// </summary>
        /// <param name="progress">progress in %, -1 means inderteminate</param>
        /// <param name="status"></param>
        public void UpdateProgressAndStatus(int progress, string status)
        {
            // we don't let the worker update progress when  a cancel has been
            // requested, unless the cancel has been acknowleged, so we know that
            // this progress update pertains to the cancellation cleanup
            if (doWorkArgs.CancelRequested && !doWorkArgs.CancelAcknowledged)
                return;


            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => UpdateProgressAndStatus(progress, status)));
                return;
            }


            int pgv = -1;

            lock (objlock)
            {
                _progress = progress;
                _status = status;
                lbl_progress_message.Text = _status;
            }

            if (pgv == -1)
            {
                this.progressBar.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                try
                {
                    this.progressBar.Value = pgv;
                } // Exception System.ArgumentOutOfRangeException: Value of '-12959800' is not valid for 'Value'. 'Value' should be between 'minimum' and 'maximum'.
                catch { }

            }
        }

        /// <summary>
        /// prevent using invokes on main update status call "UpdateProgressAndStatus", as this is slow on mono
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.Disposing || this.IsDisposed)
                return;

            int pgv = -1;
            lock (objlock)
            {
                pgv = _progress;
                lbl_progress_message.Text = _status;
            }
            if (pgv == -1)
            {
                this.progressBar.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                try
                {
                    this.progressBar.Value = pgv;
                } // Exception System.ArgumentOutOfRangeException: Value of '-12959800' is not valid for 'Value'. 'Value' should be between 'minimum' and 'maximum'.
                catch { } // clean fail. and ignore, chances are we will hit this again in the next 100 ms
            }
        }

        private void ProgressReporterDialogue_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
    }

    public class ProgressWorkerEventArgs : EventArgs
    {
        public string ErrorMessage;
        volatile bool _CancelRequested = false;
        public bool CancelRequested
        {
            get
            {
                return _CancelRequested;
            }
            set
            {
                // _CancelRequested = value; if (CancelRequestChanged != null) CancelRequestChanged(this, new PropertyChangedEventArgs("CancelRequested"));
                _CancelRequested = value; CancelRequestChanged?.Invoke(this, new PropertyChangedEventArgs("CancelRequested"));
            }
        }
        public volatile bool CancelAcknowledged;

        public event PropertyChangedEventHandler CancelRequestChanged;
    }
}
