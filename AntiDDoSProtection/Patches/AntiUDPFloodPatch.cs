using HarmonyLib;
using LiteNetLib;
using System;
using System.Collections.Concurrent;
using System.Net;
using Exiled.API.Features;
using System.Linq;
using AntiDDoSProtection.Helpers;

namespace AntiDDoSProtection.Patches
{
    [HarmonyPatch(typeof(NetManager), nameof(NetManager.OnMessageReceived))]
    public class AntiUDPFloodPatch
    {
        private static readonly ConcurrentDictionary<IPEndPoint, RequestStats> RequestCounts = new ConcurrentDictionary<IPEndPoint, RequestStats>();

        public static bool Prefix(NetManager __instance, NetPacket packet, IPEndPoint remoteEndPoint)
        {
            var config = Plugin.Instance.Config;
            var player = Player.List.FirstOrDefault(p => p.IPAddress.Equals(remoteEndPoint.Address.ToString()));

            if (player != null)
            {
                Log.Debug($"Packet received from {player.Nickname} (IP: {remoteEndPoint.Address}, SteamID: {player.UserId}, Packet Size: {packet.Size} bytes)");
            }
            else
            {
                Log.Debug($"Packet received from unknown player (IP: {remoteEndPoint.Address}, Packet Size: {packet.Size} bytes)");
            }

            if (IsWhitelisted(remoteEndPoint.Address, config.WhitelistIPs))
            {
                Log.Debug($"IP {remoteEndPoint.Address} is whitelisted. Skipping checks...");
                return true;
            }

            if (IsFlood(remoteEndPoint, config.MaxRequestsPerSecond))
            {
                Log.Warn($"Detected UDP Flood from {remoteEndPoint.Address}, blocking...");
                return false;
            }

            if (PacketChecks.IsFragmentedPacket(packet, config.MaxPacketSize) ||
                PacketChecks.IsTooSmallPacket(packet, config.MinPacketSize) ||
                PacketChecks.IsLDAPPacket(packet) ||
                PacketChecks.IsKnownPatternPacket(packet) ||
                PacketChecks.IsNTPPacket(packet))
            {
                Log.Warn($"Detected suspicious packet from {remoteEndPoint.Address}, blocking...");
                return false;
            }

            if (packet.Size > config.MaxPacketSize)
            {
                Log.Warn($"Detected large UDP packet from {remoteEndPoint.Address}, blocking...");
                return false;
            }

            return true;
        }

        private static bool IsFlood(IPEndPoint endPoint, int maxRequestsPerSecond)
        {
            var now = DateTime.UtcNow;
            var requestStats = RequestCounts.GetOrAdd(endPoint, new RequestStats());

            lock (requestStats)
            {
                requestStats.RequestTimes.RemoveAll(t => (now - t).TotalSeconds > 1);
                requestStats.RequestTimes.Add(now);

                return requestStats.RequestTimes.Count > maxRequestsPerSecond;
            }
        }

        private static bool IsWhitelisted(IPAddress address, System.Collections.Generic.List<string> whitelist)
        {
            return whitelist.Contains(address.ToString());
        }

        public static void ClearRequestData()
        {
            RequestCounts.Clear();
        }
    }
}