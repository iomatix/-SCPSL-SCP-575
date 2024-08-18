namespace SCP_575
{
    using Exiled.API.Interfaces;
    using SCP_575.ConfigObjects;
    using System.ComponentModel;

    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        // Unavailable: Playable, Random
        [Description("The type of SCP-575 that will be used. Valid options: Npc")]
        public InstanceType SpawnType { get; set; } = InstanceType.Npc;

        [Description("The configs for NPC instances of SCP-575.")]
        public NpcConfig NpcConfig { get; set; } = new NpcConfig();

        //[Description("The configs for playable instances of SCP-575.")]
        //public PlayableConfig PlayableConfig { get; set; } = new PlayableConfig();

        [Description("Whether of not debug messages are displayed in the console.")]
        public bool Debug { get; set; } = false;
    }
}