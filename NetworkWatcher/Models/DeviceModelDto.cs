using Microsoft.EntityFrameworkCore;

namespace NetworkWatcher.Models
{
	public class DeviceModelDto
	{

        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset LastOnlineDate { get; set; }
        public string WifiSsid { get; set; }
        public int WifiSignalStrength { get; set; }
    }
}
