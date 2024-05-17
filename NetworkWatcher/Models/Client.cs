using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetworkWatcher.Models
{
	public class Client
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		/// <summary>
		/// Id received from mobile client
		/// </summary>
		public string? RemoteId { get; set; } = string.Empty;

		/// <summary>
		/// ConnectionId
		/// </summary>
        public string? ConnectionId { get; set; } = string.Empty;

        //public string DeviceMACAddress { get; set; } = string.Empty;
        /// <summary>
        /// Client's device identifier
        /// </summary>
        public string? DeviceId { get; set; } = string.Empty;

		public string? IPAddress { get; set; } = string.Empty ;

		/// <summary>
		/// Client's name
		/// </summary>
		public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// Wi-fi SSID
        /// </summary>
		public string? WifiSsid { get; set;} = string.Empty;

        /// <summary>
        /// Signal Strength
        /// </summary>
        public int? WifiSignalStrength { get; set; }

        /// <summary>
        /// Description, comments
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// The time of the first connection
        /// </summary>
        //public DateTime FirstConnectionDateTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// The time of the last connection
        /// </summary>
        public DateTimeOffset? LastConnectionDateTime { get; set; } //= DateTime.MinValue;

        //public DateTime LastReconnectionDateTime { get; set; } = DateTime.MinValue;
        /// <summary>
        /// The time of the last disconnection
        /// </summary>
        //public DateTime LastDisconnectionDateTime { get; set; } = DateTime.MinValue;
		
	}
}
