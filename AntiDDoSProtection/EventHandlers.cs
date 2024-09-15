using AntiDDoSProtection.Patches;
using Exiled.API.Features;

namespace AntiDDoSProtection
{
    public class EventHandlers
    {
        public void OnRestartingRound()
        {
            AntiUDPFloodPatch.ClearRequestData();
            Log.Debug("Cleared request data after round restart.");
        }
    }
}