using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using System;

namespace AntiDDoSProtection
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "AntiDDoSProtection";
        public override string Prefix => "AntiDDoSProtection";
        public override string Author => "aksueikava";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 11, 0);
        public override PluginPriority Priority { get; } = PluginPriority.Default;

        private Harmony _harmony = new Harmony("AntiDDoSProtection");
        public static Plugin Instance;
        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;
            _harmony.PatchAll();
            RegisterEvents();

            Log.Debug($"{base.Name} has been enabled.");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            _harmony.UnpatchAll();
            UnregisterEvents();

            Log.Debug($"{base.Name} has been disabled.");
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();
            Exiled.Events.Handlers.Server.RestartingRound += _handlers.OnRestartingRound;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= _handlers.OnRestartingRound;
            _handlers = null;
        }
    }
}