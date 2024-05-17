using NetworkWatcher.Models;

namespace NetworkWatcher.Services
{
	public interface INetworkWatcherRepository
    {
		Task<List<Client>> GetAllClients();

		Task<Client?> GetClientByDevice(string clientId);
		Task<bool> AddClient(Client client);
		Task<bool> UpdateClient(Client client);
		Task<bool> RemoveClient(Client client);

		//=============

		Task<List<LogItem>> GetAllLogItems();
		Task<LogItem?> GetLogItemById(int logItemId);
		Task<bool> AddLogItem(LogItem logItem);
		Task<bool> UpdateLogItem(LogItem logItem);
		Task<bool> RemoveLogItem(LogItem logItem);
        Task<List<OuterLogItem>> AddRangeLogsAsync(List<OuterLogItem> logItems);

		//extra
		Task<List<LogItem>> GetAllLast24hLogItems();

        //=============

        Task<List<OuterLogItem>> GetAllOuterLogItems();
        Task<OuterLogItem?> GetOuterLogItemById(int logItemId);
        Task<bool> AddOuterLogItem(OuterLogItem logItem);
        Task<bool> UpdateOuterLogItem(OuterLogItem logItem);
        Task<bool> RemoveOuterLogItem(OuterLogItem logItem);
        Task<List<OuterLogItem>> AddRangeOuterLogsAsync(List<OuterLogItem> logItems);
    }
}
