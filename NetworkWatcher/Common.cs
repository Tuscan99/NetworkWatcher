using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetworkWatcher.Services;
using Microsoft.AspNetCore.SignalR;


namespace NetworkWatcher
{
    public class Common
    {
        //private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(6);
        //private readonly Timer _timer;

        public static List<string> onlineClientsIds = new List<string>();

        //private readonly static Lazy<NetworkWatcher> _instance = new Lazy<NetworkWatcher>(() => new NetworkWatcher(GlobalHost.ConnectionManager.GetHubContext<ServiceHub>().Clients));


        //private NetworkWatcher(IHubConnectionContext<ServiceHub> hub)
        //{
        //    TimerCallback tmcb = new TimerCallback(TestFunction);

        //    _timer = new Timer(tmcb, null, _updateInterval, _updateInterval);
        //    //_timer = new Timer()
        //}
        

        //private static void TestFunction(object state)
        //{
        //    Console.WriteLine("tick");
        //}
    }
}
