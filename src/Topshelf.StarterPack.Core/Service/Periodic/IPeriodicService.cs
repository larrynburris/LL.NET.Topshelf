using System.Timers;

namespace Topshelf.StarterPack.Core.Service.Periodic
{
    public interface IPeriodicService : IWindowsService
    {
        Timer ServiceTimer { get; }
        int ServiceTimerPeriod { get; }
        void OnPeriodExpired();
        void OnTimerExpired(object state, ElapsedEventArgs elapsedEventArgs);
    }
}
