using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace AntiDDoSProtection
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Are debug messages displayed?")]
        public bool Debug { get; set; } = false;

        [Description("Maximum number of packets allowed from a single IP per second.")]
        public int MaxRequestsPerSecond { get; set; } = 200;

        [Description("Maximum allowed size of a UDP packet in bytes.")]
        public int MaxPacketSize { get; set; } = 1024;

        [Description("Minimum allowed size of a UDP packet in bytes.")]
        public int MinPacketSize { get; set; } = 1;

        [Description("List of IP addresses that are whitelisted and bypass all checks.")]
        public List<string> WhitelistIPs { get; set; } = new List<string>
        {
            "127.0.0.1"
        };
    }
}