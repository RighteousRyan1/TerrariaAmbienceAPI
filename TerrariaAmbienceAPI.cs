using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerrariaAmbienceAPI.Common;
using TerrariaAmbienceAPI.Common.Utilities;
using ReLogic.Graphics;
using TerrariaAmbienceAPI.Content;
using System;
using Microsoft.Xna.Framework.Audio;

namespace TerrariaAmbienceAPI
{
    public class TerrariaAmbienceAPI : Mod
    {
        public static List<ModAmbience> AllModAmbiences { get; internal set; } = new List<ModAmbience>();
        public override object Call(params object[] args)
        {
            var mod = args[0] as Mod;
            var path = args[1] as string;
            var name = args[2] as string;
            var maxVol = args[3] as float?;
            var volStep = args[4] as float?;
            var playWhen = args[5] as Func<bool>;

            try
            {
                var newModAmbience = new ModAmbience(mod, path, name, maxVol.Value, volStep.Value, playWhen);
                
                newModAmbience?.Initialize();
                Logger.Debug($"Ambience with name '{name}' was initialized.");
                return newModAmbience;
            }
            catch
            {
                mod.Logger.Error($"ModAmbience with name '{((name != string.Empty && name != null) ? name : "Unknown")}' failed to initialize.");
                return "TerrariaAmbienceAPI: Mod.Call Failed | Exception Thrown";
            }
        }
        public override void PostSetupContent()
        {
            On.Terraria.Main.DoUpdate += UpdateModAmbiences;
            On.Terraria.Main.DrawInterface += DrawDebugInfo;
        }
        public override void Load()
        {
        }
        public override void PostAddRecipes()
        {
            Logger.Info($"Loaded {AllModAmbiences.Count} ModAmbience(s).");
        }
        public override void Unload()
        {
            AllModAmbiences = new List<ModAmbience>();
        }
        private void DrawDebugInfo(On.Terraria.Main.orig_DrawInterface orig, Main self, GameTime gameTime)
        {
            orig(self, gameTime);
            var cfg = ModContent.GetInstance<DebugConfig>();

            string text = string.Empty;

            int index = 0;
            var sb = Main.spriteBatch;

            sb.Begin();

            foreach (ModAmbience a in AllModAmbiences)
            {
                index++;
                if (a != null && AllModAmbiences.Count > 0)
                {
                    string modName = a.Mod.Name;
                    if (cfg.debug_AsmNames)
                        text += $"\n{a.Name} ({modName})";
                    if (!cfg.debug_AsmNames)
                        text += $"\n{a.Name}";
                }
            }

            if (cfg.debug_ViewAllLoadedModAmbiences)
            {
                sb.DrawString(Main.fontMouseText, text, Main.playerInventory ? new Vector2(16, 350) : new Vector2(16, 50), Color.White);
            }
            sb.End();
        }
        private void UpdateModAmbiences(On.Terraria.Main.orig_DoUpdate orig, Main self, GameTime gameTime)
        {
            orig(self, gameTime);

            foreach (ModAmbience ambience in AllModAmbiences)
            {
                ambience.INTERNAL_Update();
            }
        }
    }
}