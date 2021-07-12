using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace TerrariaAmbienceAPI.Content
{
	[Label("Debugging Config (Modders)")]
	public class DebugConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;
		[Label("Display all ModAmbiences")]
		[DefaultValue(false)]
		public bool debug_ViewAllLoadedModAmbiences;

		[Label("Display ModAmbience AssemblyNames")]
		[DefaultValue(false)]
		public bool debug_AsmNames;
	}
}
