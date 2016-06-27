using log4net;
using System;
using System.Reflection;
using System.Timers;

namespace Topshelf.StarterPack.Core.Service.Periodic
{
    public abstract class PeriodicServiceBase : IPeriodicService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Timer ServiceTimer { get; private set; }

        public int ServiceTimerPeriod { get; private set; }

        public PeriodicServiceBase(int period)
        {
            ServiceTimerPeriod = period;
        }

        public void OnTimerExpired(object state, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                Log.InfoFormat("Timer expired.");
                OnPeriodExpired();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("An error has occurred during OnTimerExpired: \n{0}", ex);
            }
            finally
            {
                ServiceTimer.Enabled = true;
            }
        }

        public void Start()
        {
            try
            {
                Log.Info("Starting periodic service.");
                ServiceTimer = new Timer(ServiceTimerPeriod * 60 * 1000)
                {
                    AutoReset = false
                };
                ServiceTimer.Elapsed += OnTimerExpired;
                ServiceTimer.Start();
                OnStart();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("An error occured while starting the service: \n{0}", ex);
            }
        }

        public void Stop()
        {
            try
            {
                Log.Info("Stopping periodic service.");
                if (ServiceTimer != null)
                    ServiceTimer.Dispose();

                OnStop();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("An error occured while stopping the service: \n{0}", ex);
            }
        }

        public abstract void OnStart();

        public abstract void OnStop();

        public abstract void OnPeriodExpired();
    }
}
