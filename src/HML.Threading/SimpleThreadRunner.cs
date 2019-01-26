// Decompiled with JetBrains decompiler
// Type: HML.Threading.SimpleThreadRunner
// Assembly: HML.Threading, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ACD5401D-E261-4DAA-9A77-BDEA756AB621
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\HML.Threading.dll

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Threading;
using System.Windows.Forms;

namespace HML.Threading
{
    public sealed class SimpleThreadRunner
    {
        internal Thread _thrdKeepTrack;
        private DateTime _stTime;
        private DateTime _edTime;
        private Thread _thrd;
        private RunnerState _state;
        private Form _uifrm;

        public event SimpleThreadRunner.TaskDoneEventHandler TaskDone;

        public SimpleThreadRunner(Thread thrd, Form UIForm)
        {
            try
            {
                this._thrd = thrd;
                this._thrdKeepTrack = new Thread(new ThreadStart(this.KeepTrack));
                this._state = RunnerState.Waiting;
                this._uifrm = UIForm;
                this._stTime = new DateTime();
                this._edTime = new DateTime();
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new SimpleThreadRunnerException("Unexpected error occurred");
            }
        }

        public SimpleThreadRunner(ThreadStart tsd, Form UIForm)
        {
            try
            {
                this._thrd = new Thread(tsd);
                this._thrdKeepTrack = new Thread(new ThreadStart(this.KeepTrack));
                this._state = RunnerState.Waiting;
                this._uifrm = UIForm;
                this._stTime = new DateTime();
                this._edTime = new DateTime();
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new SimpleThreadRunnerException("Unexpected error occurred");
            }
        }

        internal void KeepTrack()
        {
            while (this._thrd.IsAlive)
                Thread.Sleep(2000);
            if (this._state != RunnerState.Stopped)
                this._state = RunnerState.Finished;
            this._edTime = DateAndTime.Now;
            if (this._uifrm != null)
            {
                lock (this._uifrm)
                    this._uifrm.BeginInvoke((Delegate)new SimpleThreadRunner.TaskDoneEventRaiseDelegate(this.TaskDoneEventRaiseMethod));
            }
            else
                this.TaskDoneEventRaiseMethod();
        }

        internal void TaskDoneEventRaiseMethod()
        {
            TaskDoneEventHandler taskDoneEvent = this.TaskDone;
            if (taskDoneEvent == null)
                return;
            taskDoneEvent((object)this, new EventArgs());
        }

        public void Start()
        {
            if (this._thrd == null)
                throw new SimpleThreadRunnerException("Thread not initialized.");
            if (this._state == RunnerState.Finished | this._state == RunnerState.Stopped)
                throw new SimpleThreadRunnerException("Thread already ended.");
            try
            {
                this._thrd.Start();
                this._thrdKeepTrack.Start();
                this._stTime = DateAndTime.Now;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new SimpleThreadRunnerException("Unexpected error occurred.");
            }
        }

        public void Stop()
        {
            if (this._thrd == null)
                throw new SimpleThreadRunnerException("Thread not initialized.");
            if (this._state != RunnerState.Waiting)
                return;
            try
            {
                this._thrd.Abort();
                this._edTime = DateAndTime.Now;
                this._state = RunnerState.Stopped;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new SimpleThreadRunnerException("Unexpected error occurred.");
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this._stTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this._edTime;
            }
        }

        public RunnerState ThreadExecutionState
        {
            get
            {
                return this._state;
            }
        }

        internal delegate void TaskDoneEventRaiseDelegate();

        public delegate void TaskDoneEventHandler(object sender, EventArgs e);
    }
}
