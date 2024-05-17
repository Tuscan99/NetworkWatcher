using NetworkWatcher.Components.Pages;
using NetworkWatcher.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
//using Microsoft.AspNetCore.SignalR.Client;
using Serilog;
using NetworkWatcher.Services;

namespace NetworkWatcher.Services
{
    public class ServiceHub : Hub
	{
		private readonly INetworkWatcherRepository _repository;

        public static List<Client> clients = new List<Client>();
		//public static List<string> onlineClientsIds = new List<string>();
        //public static List<string> newOnlineClientsIds = new List<string>();

        //
        public static string testId = string.Empty;

        public static List<LogItem> instantLogItems = new List<LogItem>();
        public ServiceHub(INetworkWatcherRepository networkWatcherRepository)
		{
			_repository = networkWatcherRepository;               
		}

        ClientsList clientsList = new ClientsList();

        //Ping Clients has moved to ClientsList
        public async Task SendResponse(string connectionId)
        {
            //if (!newOnlineClientsIds.Contains(connectionId))
            //{
            //    newOnlineClientsIds.Add(connectionId);
            //}
            if (!Common.onlineClientsIds.Contains(connectionId))
            {
                Common.onlineClientsIds.Add(connectionId);
            }


                //Console.WriteLine(connectionId);
                //refresh list
                //if (newOnlineClientsIds != null && !newOnlineClientsIds.Any(i => i == connectionId))
                //{
                //    newOnlineClientsIds.Add(connectionId);
                //}
        }

        //
        public async Task SendMessage(string message)
        {

        }

        public async Task Send(DeviceModelDto device)
        {
            var request = Context.GetHttpContext().Request;
            string host = request.Host.Value;
            string? ip = request.Headers["X-Real-IP"];

            //await this.Clients.All.SendAsync("ReceiveMessage", device);
            await this.Clients.Caller.SendAsync("ReceiveMessage", device);

            if (!string.IsNullOrEmpty(device.DeviceId))
            {
                Client? client2 = _repository?.GetClientByDevice(device.DeviceId).Result;
                if (client2 == null)
                {
                    Client client = new Client()
                    {
                        RemoteId = "-",
                        DeviceId = device.DeviceId,
                        ConnectionId = Context.ConnectionId,
                        IPAddress = ip,
                        Name = device.Name,
                        WifiSsid = "-",
                        WifiSignalStrength = null,
                        Description = "",
                        LastConnectionDateTime = DateTimeOffset.Now
                    };

                    if (!string.IsNullOrEmpty(device.Id))
                    {
                        client.RemoteId = device.Id;
                    }

                    if (_repository != null)
                    {
                        _repository.AddClient(client);
                    }

                    clients.Add(client);

                }
                else
                {
                    if (device.Name != null && client2.Name != device.Name)
                    {
                        client2.Name = device.Name;
                    }
                    if (!string.IsNullOrEmpty(device.Id))
                    {
                        client2.RemoteId = device.Id;
                    }
                    client2.IPAddress = ip;
                    client2.WifiSsid = device.WifiSsid;
                    client2.WifiSignalStrength = device.WifiSignalStrength;

                    client2.ConnectionId = Context.ConnectionId;
                    client2.LastConnectionDateTime = DateTimeOffset.Now;

                    if (_repository != null)
                    {
                        await _repository.UpdateClient(client2);
                    }

                    var temp = clients.Find(cl => cl.DeviceId == client2.DeviceId);
                    temp.IPAddress = ip;
                    temp.Name = client2.Name;   //
                    temp.WifiSsid = client2.WifiSsid;
                    temp.WifiSignalStrength = client2.WifiSignalStrength;
                    temp.ConnectionId = client2.ConnectionId;
                    temp.LastConnectionDateTime = DateTimeOffset.Now;
                    
                }
            }
        }

        /// <summary>
        /// On connected
        /// </summary>
        /// <returns></returns>
		public override Task OnConnectedAsync()
		{
            var httpContext = Context?.GetHttpContext();
            var request = httpContext?.Request;
            string? host = request.Host.Value;
            string? ip = request.Headers["X-Real-IP"];

            string connectionId = Context.ConnectionId;

            /*
            if (_repository != null)
            {
                var clientsTempList = _repository.GetAllClients().Result;
                foreach (string id in onlineClientsIds.ToList())
                {
                    Client? tempClient = clientsTempList.FirstOrDefault(c => c.ConnectionId == id);
                    if (tempClient == null)
                    {
                        onlineClientsIds.RemoveAll(i => i == id);
                    }
                }
            }
            */

            Common.onlineClientsIds.Add(Context.ConnectionId);
            //clientsList.ChangeStateOnline();

            AddLog(Context.ConnectionId, true);
            /*
            Client? client = _repository.GetAllClients().Result.FirstOrDefault(c => c.ConnectionId == connectionId);
            if (client != null)
            {
                LogItem logItem = new LogItem()
                {
                    DateTime = DateTimeOffset.Now,
                    DeviceId = client.DeviceId,
                    ConnectionId = connectionId,
                    EventType = EventType.Connection,
                    WifiSsid = client.WifiSsid,
                    WifiSignalStrength = client.WifiSignalStrength,
                    Message = string.Empty
                };
                _repository.AddLogItem(logItem);

                Log.Information($"Client - DeviceId: {client.DeviceId}, ConnectionId: {Context.ConnectionId} is connected");
            }
            else
            {
                LogItem logItem = new LogItem()
                {
                    DateTime = DateTimeOffset.Now,
                    DeviceId = "-",
                    ConnectionId = connectionId,
                    EventType = EventType.Connection,
                    WifiSsid = "-",
                    WifiSignalStrength = -1000,
                    Message = string.Empty
                };
                _repository.AddLogItem(logItem);
                Log.Information($"Unknown client - ConnectionId: {Context.ConnectionId} is connected");
            }
            */
            
            return base.OnConnectedAsync();
		}

        /// <summary>
        /// On disconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
		public override Task OnDisconnectedAsync(Exception? exception)
		{
            //var request = Context.GetHttpContext().Request;
            //string host = request.Host.Value;

            //TODO

            string connectionId = Context.ConnectionId;
            if (Common.onlineClientsIds.Contains(connectionId) && Common.onlineClientsIds.Count > 0)
            {
                //onlineClients.Remove(connectionId);
                Common.onlineClientsIds.RemoveAll(oc => oc == connectionId);
                //newOnlineClientsIds.RemoveAll(oc => oc == connectionId);
            }

            AddLog(connectionId, false);

            /*
            Client? client = _repository.GetAllClients().Result.FirstOrDefault(c => c.ConnectionId == connectionId);
            if (client != null)
            {
                LogItem logItem = new LogItem()
                {
                    DateTime = DateTimeOffset.Now,
                    DeviceId = client.DeviceId,
                    ConnectionId = connectionId,
                    EventType = EventType.Disconnection,

                    Message = string.Empty
                };
                _repository.AddLogItem(logItem);

                Log.Information($"Client - DeviceId: {client.DeviceId}, ConnectionId: {Context.ConnectionId} is disconnected {exception}");
            }
            else
            {
                Log.Information($"Client - ConnectionId: {Context.ConnectionId} is disconnected {exception}");
            }
            */

            CleanOnDisconnect(connectionId);
            //clientsList.ChangeStateOffline();

            return base.OnDisconnectedAsync(exception);
		}


        private void AddLog(string connectionId, bool isConnectionEvent)
        {
            string eventname = "";
            EventType et = EventType.Connection;

            if (isConnectionEvent)
            {
                eventname = "connected";
                et = EventType.Connection;
            }
            else
            {
                eventname = "disconnected";
                et = EventType.Disconnection;
            }

            Client? client = _repository.GetAllClients().Result.FirstOrDefault(c => c.ConnectionId == connectionId);
            if (client != null)
            {
                LogItem logItem = new LogItem()
                {
                    DateTime = DateTimeOffset.Now,
                    DeviceId = client.DeviceId,
                    ConnectionId = connectionId,
                    EventType = et,
                    WifiSsid = client.WifiSsid,
                    WifiSignalStrength = client.WifiSignalStrength,
                    Message = string.Empty
                };
                _repository.AddLogItem(logItem);
                instantLogItems.Add(logItem);

                Log.Information($"Client: {client.Name}, DeviceId: {client.DeviceId}, ConnectionId: {Context.ConnectionId} is {eventname}");
            }
            else
            {
                LogItem logItem = new LogItem()
                {
                    DateTime = DateTimeOffset.Now,
                    DeviceId = "-",
                    ConnectionId = connectionId,
                    EventType = et,
                    WifiSsid = "-",
                    WifiSignalStrength = -1000,
                    Message = string.Empty
                };
                _repository.AddLogItem(logItem);
                instantLogItems.Add(logItem);

                Log.Information($"Unknown client - ConnectionId: {Context.ConnectionId} is {eventname}");
            }
        }

        private void CleanOnDisconnect(string connectionId)
        {
            if (_repository != null)
            {
                Client? clientDbToClear = _repository.GetAllClients().Result.FirstOrDefault(c => c.ConnectionId == connectionId);
                if (clientDbToClear != null)
                {
                    clientDbToClear.ConnectionId = "-                       ";
                    clientDbToClear.IPAddress = "-               ";
                    clientDbToClear.WifiSsid = "-       ";
                    clientDbToClear.WifiSignalStrength = null;
                    _repository.UpdateClient(clientDbToClear);

                    
                    Client? clientMemToClear = clients.FirstOrDefault(c => c.DeviceId == clientDbToClear.DeviceId);
                    if (clientMemToClear != null)
                    {
                        clientMemToClear.ConnectionId = "-                       ";
                        clientMemToClear.IPAddress = "-               ";
                        clientMemToClear.WifiSsid = "-       ";
                        clientMemToClear.WifiSignalStrength = null;
                    }
                }
            }
        }

        //private Task<bool> IsConnected(string connectionId)
        //{
        //    Ping pinger = null;
        //    bool isConnected = false;
        //    try
        //    {
        //        pinger = new Ping();
        //        PingReply pingReply = pinger.SendPingAsync(ip);
        //    }
        //}
//

        public async Task GetDeviceName(string deviceId)
        {
            string name = string.Empty;
            Client? client = _repository?.GetClientByDevice(deviceId).Result;
            if (client != null)
            {
                name = client.Name;
            }
            await this.Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", name);
            //return name;
        }


        //Outer logs ----------
        public async Task SendRequest(string connectionId, DateTimeOffset fromDate)
        {
            bool IsSuccessful = false;
            try
            {
                await this.Clients.Client(connectionId).SendAsync("ReceiveRequest", IsSuccessful);
            }
            catch
            {
                throw new Exception(connectionId);
            }
        }
        //is invoked from outside
        //public async Task SendLogs(string connectionId, List<OuterLogItem> outerLogItems)
        public async Task SendLogs(List<OuterLogItemDto> outerLogItemsDto)
        {
            try
            {
                if (outerLogItemsDto != null)
                {
                    List<OuterLogItem> outerLogItems = new List<OuterLogItem>();
                    List<OuterLogItem>? oldLogItems = await _repository.GetAllOuterLogItems();
                    foreach (OuterLogItemDto dto in outerLogItemsDto)
                    {
                        OuterLogItem logItem = new OuterLogItem()
                        {
                            RemoteId = dto.Id,
                            DeviceId = dto.DeviceId,
                            WifiSsid = dto.WifiSsid,
                            WifiSignalStrength = dto.WifiSignalStrength,
                            ConnectDate = dto.ConnectDate,
                            StatusRaw = dto.StatusRaw,
                            Description = dto.Description
                        };

                        if (!oldLogItems.Any(li => li.ConnectDate == dto.ConnectDate))
                        {
                            outerLogItems.Add(logItem);
                        }
                    }

                    await _repository.AddRangeLogsAsync(outerLogItems);
                }
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
            }
            
        }


        public async Task CloseConnection()
        {
            Console.WriteLine("closeconnection");
            //Context.Abort();
        }

        
    }
}
