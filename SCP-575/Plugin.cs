namespace SCP_575
{
    using MEC;
    using SCP_575.Shared;
    using System;

    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public static Plugin Singleton;

        public override string Author { get; } = "iomatix";
        public override string Name { get; } = "SCP-575 NPC";
        public override string Prefix { get; } = "SCP575";


        public override Version Version { get; } = new(7, 9, 1);
        public override Version RequiredExiledVersion { get; } = new(9, 6, 0);

        public EventHandlers EventHandlers { get; private set; }


        public NestingObjects.Npc Npc { get; private set; }

        public override void OnEnabled()
        {

            Singleton = this;
            
            EventHandlers = new EventHandlers(this);
            Npc = new NestingObjects.Npc(this);

            LabApi.Events.Handlers.ServerEvents.RoundStarted += EventHandlers.OnRoundStarted;
            LabApi.Events.Handlers.ServerEvents.RoundEnded += EventHandlers.OnRoundEnded;
            LabApi.Events.Handlers.ServerEvents.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;

            LabApi.Events.Handlers.PlayerEvents.Hurting += EventHandlers.OnPlayerHurting;
            LabApi.Events.Handlers.PlayerEvents.Hurt += EventHandlers.OnPlayerHurt;
            LabApi.Events.Handlers.PlayerEvents.Dying += EventHandlers.OnPlayerDying;
            LabApi.Events.Handlers.PlayerEvents.Death += EventHandlers.OnPlayerDeath;

            Exiled.Events.Handlers.Player.SpawnedRagdoll += EventHandlers.OnSpawnedRagdoll;

            AudioManager.Enable();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            foreach (CoroutineHandle handle in EventHandlers.Coroutines) Timing.KillCoroutines(handle);
            EventHandlers.Coroutines.Clear();

            LabApi.Events.Handlers.ServerEvents.RoundStarted -= EventHandlers.OnRoundStarted;
            LabApi.Events.Handlers.ServerEvents.RoundEnded -= EventHandlers.OnRoundEnded;
            LabApi.Events.Handlers.ServerEvents.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;

            LabApi.Events.Handlers.PlayerEvents.Hurting -= EventHandlers.OnPlayerHurting;
            LabApi.Events.Handlers.PlayerEvents.Hurt -= EventHandlers.OnPlayerHurt;
            LabApi.Events.Handlers.PlayerEvents.Dying -= EventHandlers.OnPlayerDying;
            LabApi.Events.Handlers.PlayerEvents.Death -= EventHandlers.OnPlayerDeath;

            Exiled.Events.Handlers.Player.SpawnedRagdoll -= EventHandlers.OnSpawnedRagdoll;

            AudioManager.Disable();

            EventHandlers = null;
            Npc = null;

            base.OnDisabled();
        }
    }
}