using System;

namespace Topshelf.StarterPack.Core.Service
{
    public interface IWindowsService
    {
        void Start();
        void Stop();
        void OnStart();
        void OnStop();
    }
}
