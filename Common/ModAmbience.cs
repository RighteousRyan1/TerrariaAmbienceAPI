using System;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System.Data.SqlTypes;

namespace TerrariaAmbienceAPI.Common
{
    /// <summary>
    /// Create a custom ambience track.
    /// <para></para>
    /// When inheriting from this class, you MUST use this.Sound and this.SoundInstance property during initialization for this not to break.
    /// <para></para>
    /// You can also get an instance of any ambience using <code>ModContent.GetInstance</code>
    /// </summary>
    public class ModAmbience
    {
        public Mod mod => ModLoader.Mods.First(mod => mod.Code == GetType().Assembly);

        public float volume;
        public SoundEffect Sound { get; set; }

        public SoundEffectInstance SoundInstance { get; set; }
        /// <summary>
        /// Put the things in here you wish to do when the player saves and quits.
        /// </summary>
        public virtual void SaveAndQuit() { }
        /// <summary>
        /// Put all things you want to instantiate on Mod.Load() here. <para></para>
        /// Be ABSOLUTELY SURE to set both this ambience's Sound and SoundInstance
        /// </summary>
        public virtual void Initialize() 
        {
            volume = 0f;
            SoundInstance.Volume = 0f;
        }
        public bool IsPlaying => volume > 0f; 
        /// <summary>
        /// Run code while this ambience is active.
        /// </summary>
        public virtual void UpdateActive() 
        {
        }
        /// <summary>
        /// The name for the disaster. Defaults to "N/A."
        /// <para>Don't forget to set the name!</para>
        /// </summary>
        public virtual string Name => "N/A";
        /// <summary>
        /// Start playing the ambience track when this value is true. Defaults to false, so set the value!
        /// </summary>
        public virtual bool WhenToPlay => false;

        /// <summary>
        /// This is how fast this ambience changes volume based on whether it is active or not. <para></para>
        /// For example, if the ambience should not be playing, the volume would decrease by the value this is set to every tick. Defaults to 0.0075f.
        /// </summary>
        public virtual float VolumeStep => 0.0075f;
        internal void UpdateThisAmbience()
        {
            if (SoundInstance != null)
            {
                // Main.NewText($"vol: {SoundInstance.Volume} | IsPlaying: {IsPlaying}");
                if (volume <= 1 && volume >= 0)
                    SoundInstance.Volume = volume;
                if (WhenToPlay && Main.hasFocus)
                {
                    volume += VolumeStep;
                }
                if (!WhenToPlay || !Main.hasFocus || Main.gameMenu)
                {
                    volume -= VolumeStep;
                }
                volume *= Main.ambientVolume;
            }
            if (Main.gameMenu)
                volume -= VolumeStep;
        }
        internal void ClampVolume()
        {
            if (volume > 1)
            {
                volume = 1;
            }
            if (volume < 0)
            {
                volume = 0;
            }

            if (IsPlaying)
                UpdateActive();
        }
        public override string ToString()
        {
            return $"IsPlaying: {IsPlaying} | SFX: {SoundInstance} | VolStep: {VolumeStep} | Volume: {SoundInstance.Volume} | ShouldBePlaying: {WhenToPlay} | Name: {Name}";
        }
    }
}
