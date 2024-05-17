using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetworkWatcher.Models
{
    public class OuterLogItemDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string DeviceId { get; set; }

        public string WifiSsid { get; set; }

        public int WifiSignalStrength { get; set; }

        public DateTimeOffset ConnectDate { get; set; }

        //public string Status { get; set; }

        public LogStatus StatusRaw { get; set; }
        //public LogStatus StatusRaw
        //{
        //    get => Enum.TryParse(Status, out LogStatus result) ? result : LogStatus.Disconnected;
        //    set => Status = value.ToString();
        //}

        public string Description { get; set; }

    }

}
