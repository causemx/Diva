using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diva.Utilities
{
    class BackgroundLoop
    {
        static private List<BackgroundLoop> tasks = new List<BackgroundLoop>();

        static public BackgroundLoop Start(Action<CancellationToken> loop, EventHandler eventHandler = null)
            => new BackgroundLoop(loop, eventHandler);
        static public void FreeTasks(int timeout = 0)
        {
            lock (tasks) tasks.ForEach(t => t.cts.Cancel());
            new Thread(() =>
            {
                Task[] ts;
                lock (tasks) ts = tasks.Select(t => t.task).ToArray();
                Task.WaitAll(ts, timeout);
            }).Start();
        }

        private CancellationTokenSource cts;
        private Task task;
        private BackgroundLoop(Action<CancellationToken> loop, EventHandler eventHandler)
        {
            cts = new CancellationTokenSource();
            task = new Task(() =>
            {
                try { loop(cts.Token); } catch { }
                lock (tasks) tasks.Remove(this);
                LoopExited?.Invoke(this, null);
            });
            lock (tasks) tasks.Add(this);
            if (eventHandler != null) LoopExited += eventHandler;
            task.Start();
        }

        public void Cancel() => cts.Cancel();
        public bool IsRunning => tasks.Contains(this);
        public EventHandler LoopExited;
    }
}
