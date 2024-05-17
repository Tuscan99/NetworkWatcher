using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetworkWatcher.Models
{
	public class LogItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string? DeviceId { get; set; } = "default";

		public string? ConnectionId { get; set; }

        public string? WifiSsid { get; set; } = string.Empty;

        public int? WifiSignalStrength { get; set; }

        public EventType? EventType { get; set; }

        public DateTimeOffset? DateTime { get; set; }

        public string? Message { get; set; } = string.Empty;
	}

	public enum EventType
	{
		Connection,
		Disconnection,
		Timeout
	}
}
