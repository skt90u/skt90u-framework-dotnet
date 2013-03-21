using System;
using System.Threading;

namespace JUtil.Service
{
    public abstract class ServiceBase : IService
    {
        private readonly object _mainFuncArgs;
        private readonly double _sampleInterval;

        protected abstract IServiceObject CreateServiceObject();

        protected ServiceBase(object mainFuncArgs, double sampleInterval)
        {
            
            this._mainFuncArgs = mainFuncArgs;
            this._sampleInterval = sampleInterval;
        }

        private void Main(object args)
        {
            try
            {
                IServiceObject serviceObject = CreateServiceObject();

                serviceObject.Initialize(args);

                while (!IsFinish)
                {
                    while (IsSuspend) System.Threading.Thread.Sleep(BusyWaitingDelay);

                    serviceObject.RunOnce();

                    System.Threading.Thread.Sleep((int)_sampleInterval);
                }
            }
            catch (Exception ex)
            {
                Log.E(ex);
            }
        }

        #region Worker
        Thread _worker = null;
        Thread Worker
        {
            get
            {
                if (_worker == null)
                {
                    ParameterizedThreadStart st = new ParameterizedThreadStart(Main);

                    _worker = new Thread(st);
                }
                return _worker;
            }
            set
            {
                _worker = value;
            }
        }
        #endregion

        #region Switchs

        private readonly object _lockObj = new object();

        #region IsFinish
        private bool IsFinish
        {
            get
            {
                return _isFinish;
            }
            set
            {
                lock (_lockObj)
                {
                    _isFinish = value;
                }
            }
        }
        private bool _isFinish = false;
        #endregion

        #region IsSuspend
        private bool IsSuspend
        {
            get
            {
                return _isSuspend;
            }
            set
            {
                lock (_lockObj)
                {
                    _isSuspend = value;
                }
            }
        }
        private bool _isSuspend = false;
        #endregion

        #endregion

        #region DelayTimes

        private const int BusyWaitingDelay = 100;

        #endregion

        #region IService members

        public void Start()
        {
            IsFinish = false;

            IsSuspend = false;

            if (Worker.ThreadState == ThreadState.Unstarted)
            {
                Worker.Start(_mainFuncArgs);
            }
        }

        public void Stop()
        {
            IsSuspend = false;

            IsFinish = true;

            if (Worker.ThreadState == ThreadState.Running ||
                Worker.ThreadState == ThreadState.WaitSleepJoin)
            {
                Worker.Join();
            }

            Worker = null;
        }

        public void Suspend()
        {
            IsSuspend = true;
        }

        public void Continue()
        {
            IsSuspend = false;
        }

        #endregion
    }
}
