using NetworkWatcher.DbContexts;
using NetworkWatcher.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NetworkWatcher.Services;

namespace NetworkWatcher.Services
{
	public class NetworkWatcherRepository : INetworkWatcherRepository
    {
		private readonly AppDBContext _context;

		public NetworkWatcherRepository(AppDBContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<List<Client>> GetAllClients()
		{
			return await _context.Clients.ToListAsync();
		}

		public async Task<Client?> GetClientByDevice(string deviceId)
		{
			return await _context.Clients.Where(cl => cl.DeviceId == deviceId).FirstOrDefaultAsync();
		}

		public async Task<bool> AddClient(Client client)
		{
			await _context.Clients.AddAsync(client);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateClient(Client client)
		{
			_context.Clients.Update(client);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> RemoveClient(Client client)
		{
			_context.Clients.Remove(client);
			await _context.SaveChangesAsync();
			return true;
		}

        //LogItems
        //=======================================================================================================
        public async Task<List<LogItem>> GetAllLogItems()
        {
            return await _context.LogItems.ToListAsync();
        }

        public async Task<LogItem?> GetLogItemById(int logItemId)
        {
            return await _context.LogItems.Where(l => l.Id == logItemId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddLogItem(LogItem logItem)
        {
            await _context.LogItems.AddAsync(logItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLogItem(LogItem logItem)
        {
            _context.LogItems.Update(logItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveLogItem(LogItem logItem)
        {
            _context.LogItems.Remove(logItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<OuterLogItem>> AddRangeLogsAsync(List<OuterLogItem> outerLogItems)
        {
            await _context.OuterLogItems.AddRangeAsync(outerLogItems);
            await _context.SaveChangesAsync();
            return outerLogItems;
        }

        //extra
        public async Task<List<LogItem>> GetAllLast24hLogItems()            //TODO
        {
            //return await _context.LogItems
            //    .Where(li => li.DateTime > DateTimeOffset.Now.Subtract(TimeSpan.FromHours(24))).ToListAsync();
            DateTimeOffset yesterday = DateTimeOffset.Now - TimeSpan.FromHours(36);
            //return await _context.LogItems
            //    .Where(li => li.DateTime > yesterday).ToListAsync();
            return await _context.LogItems
                .Where(li => li.DateTime > yesterday).ToListAsync();
        }


        //OuterLogItems
        //==============================================================
        public async Task<List<OuterLogItem>> GetAllOuterLogItems()
        {
            return await _context.OuterLogItems.ToListAsync();
        }

        public async Task<OuterLogItem?> GetOuterLogItemById(int outerLogItemId)
        {
            return await _context.OuterLogItems.Where(l => l.Id == outerLogItemId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddOuterLogItem(OuterLogItem logItem)
        {
            await _context.OuterLogItems.AddAsync(logItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOuterLogItem(OuterLogItem logItem)
        {
            _context.OuterLogItems.Update(logItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOuterLogItem(OuterLogItem logItem)
        {
            _context.OuterLogItems.Remove(logItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<OuterLogItem>> AddRangeOuterLogsAsync(List<OuterLogItem> outerLogItems)
        {
            await _context.OuterLogItems.AddRangeAsync(outerLogItems);
            await _context.SaveChangesAsync();
            return outerLogItems;
        }


    }
}
