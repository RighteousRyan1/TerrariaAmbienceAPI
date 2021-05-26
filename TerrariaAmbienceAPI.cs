using Microsoft.Xna.Framework;
using NDMod.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerrariaAmbienceAPI.Common;
using TerrariaAmbienceAPI.Common.Utilities;
using ReLogic.Graphics;
using System.Text;

namespace TerrariaAmbienceAPI
{
    public class TerrariaAmbienceAPI : Mod
    {
        public static List<ModAmbience> AllModAmbiences { get; set; } = new List<ModAmbience>();

        public override void PostSetupContent()
        {
            AllModAmbiences = OOPHelper.GetSubclasses<ModAmbience>();

            foreach (ModAmbience ambience in AllModAmbiences)
            {
                ambience.Initialize();
                ambience.InternalInit();

                ContentInstance.Register(ambience);
            }
            On.Terraria.Main.DoUpdate += Main_DoUpdate;
            On.Terraria.Main.DrawInterface += Main_DrawInterface;
        }

        private void Main_DrawInterface(On.Terraria.Main.orig_DrawInterface orig, Terraria.Main self, Microsoft.Xna.Framework.GameTime gameTime)
        {
            var cfg = ModContent.GetInstance<DebugConfig>();

            string text = string.Empty;

            int index = 0;

            bool tooLong = false;

            foreach (ModAmbience a in AllModAmbiences)
            {
                index++;
                if (a != null && AllModAmbiences.Count > 0)
                {
                    if (cfg.debug_AsmNames && !tooLong)
                        text += index < 2 ? $"{a.Name} ({a.GetType().Assembly.GetName().Name})" : $", {a.Name} ({a.GetType().Assembly.GetName().Name})";
                    else if (!cfg.debug_AsmNames && !tooLong)
                        text += index < 2 ? $"{a.Name}" : $", {a.Name}";

                    if (tooLong && cfg.debug_AsmNames)
                        text += $"\n{a.Name} ({a.GetType().Assembly.GetName().Name})";
                    if (tooLong && !cfg.debug_AsmNames)
                        text += $"\n{a.Name}";


                    if (Main.fontMouseText.MeasureString(text).X > Main.screenWidth)
                        tooLong = true;
                }
            }

            if (cfg.debug_ViewAllLoadedModAmbiences)
            {
                var sb = Main.spriteBatch;

                sb.Begin();

                sb.DrawString(Main.fontMouseText, text, new Vector2(4, 2), Color.White);

                sb.End();
            }
            orig(self, gameTime);
        }

        private void Main_DoUpdate(On.Terraria.Main.orig_DoUpdate orig, Terraria.Main self, Microsoft.Xna.Framework.GameTime gameTime)
        {
            orig(self, gameTime);

            foreach (ModAmbience ambience in AllModAmbiences)
            {
                ambience.UpdateThisAmbience();

                ambience.ClampVolume();
            }
        }

        public override void PostUpdateEverything()
        {
            foreach (ModAmbience ambience in AllModAmbiences)
            {
                ambience.UpdateActive();
                ambience.UpdateThisAmbience();
            }
        }
        public override void PreSaveAndQuit()
        {
            foreach (ModAmbience ambience in AllModAmbiences)
            {
                ambience.SaveAndQuit();
            }
        }
    }
}