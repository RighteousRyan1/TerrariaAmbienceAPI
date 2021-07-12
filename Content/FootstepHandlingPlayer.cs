using Terraria.ModLoader;
using Terraria;
using TerrariaAmbienceAPI.Common;

namespace TerrariaAmbienceAPI.Content
{
    public class FootstepHandlingPlayer : ModPlayer
    {
        public override void PostUpdate() => HandleFootstepping();

        /*private int _legFrameSnapShotNew;
        private int _legFrameSnapShotOld;*/
        public void HandleFootstepping()
        {
            /*var allFoot = TerrariaAmbienceAPI.AllFootsteps;
            _legFrameSnapShotNew = player.legFrame.Y / 20;

            bool stepping = (_legFrameSnapShotNew == 44 && _legFrameSnapShotOld != 44) || (_legFrameSnapShotNew == 25 && _legFrameSnapShotOld != 25);
            bool landing = _legFrameSnapShotNew != 14 && _legFrameSnapShotOld == 14;

            if (stepping)
            {
                foreach (var fstep in allFoot)
                {
                    fstep.RandomizeFootstepSound();
                    if (fstep.onAnyTileType)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, fstep.variations > 1 ? $"{fstep.soundPath}_{fstep.rand}" : $"{fstep.soundPath}"), player.Bottom).Volume = fstep.volume / 2;
                    }
                }
            }
            if (landing)
            {
                foreach (var fstep in allFoot)
                {
                    fstep.RandomizeFootstepSound();
                    if (fstep.onAnyTileType)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, fstep.variations > 1 ? $"{fstep.soundPath}_{fstep.rand}" : $"{fstep.soundPath}"), player.Bottom).Volume = fstep.volume;
                    }
                }
            }

            _legFrameSnapShotOld = _legFrameSnapShotNew;*/
        }
    }
    public class TileUnderHandler : GlobalTile
    {
        public override void FloorVisuals(int type, Player player)
        {
            /*var allFoot = TerrariaAmbienceAPI.AllFootsteps;
            foreach (var fstep in allFoot)
            {
                if (fstep.tileTypes.Contains(type))
                    fstep.onAnyTileType = true;
                else
                    fstep.onAnyTileType = false;
            }*/
        }
    }
}