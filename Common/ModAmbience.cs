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
    public class LoadException : Exception
    {
        public string msg;
        public object reason;
        public LoadException(string msg, object reason)
        {
            this.msg = msg;
            this.reason = reason;
        }
        public override string Message => $"{msg} | Reason: {reason}";
    }
    /// <summary>
    /// Create a custom ambience track.
    /// <para></para>
    /// When inheriting from this class, you MUST use this.Sound and this.SoundInstance property during initialization for this not to break.
    /// <para></para>
    /// You can also get an instance of any ambience using <code>ModContent.GetInstance</code>
    /// </summary>
    public class ModAmbience
    {
        private string path;
        private Action<ModAmbience> initAction;
        private Action<ModAmbience> saveAndQuitAction;
        private Action<ModAmbience> activeUpdateAction;

        private Func<bool> _playWhen;
        public ModAmbience(Mod mod, string path, string name, float maxVolume, float volumeStep, Func<bool> playWhen, Action<ModAmbience> saveAndQuitAction,
            Action<ModAmbience> initializeAction, Action<ModAmbience> updateActiveAction)
        {
            this.path = path;
            Mod = mod;
            Name = name;
            MaxVolume = maxVolume;
            initAction = initializeAction;
            activeUpdateAction = updateActiveAction;
            VolumeStep = volumeStep;
            _playWhen = playWhen;
            volume = 0f;
            this.saveAndQuitAction = saveAndQuitAction;
            TerrariaAmbienceAPI.AllModAmbiences.Add(this);
        }
        /// <summary>
        /// This mod.
        /// </summary>
        public Mod Mod { get; private set; }
        public float volume;
        /// <summary>
        /// The sound representation of your ambient track.
        /// </summary>
        internal SoundEffect Sound { get; private set; }
        /// <summary>
        /// The instanced sound representation of your ambient track.
        /// </summary>
        internal SoundEffectInstance SoundInstance { get; private set; }
        /// <summary>
        /// Put the things in here you wish to do when the player saves and quits.
        /// </summary>
        internal void SaveAndQuit() { saveAndQuitAction?.Invoke(this); }
        /// <summary>
        /// Put all things you want to instantiate on Mod.Load() here. <para></para>
        /// The abstract members 'Sound' and 'SoundInstance' must be set there.
        /// </summary>
        internal void Initialize() 
        {
            Sound = Mod?.GetSound(path);
            SoundInstance = Sound?.CreateInstance();
            volume = 0f;
            SoundInstance.Volume = 0f;

            if (!SoundInstance.IsLooped)
                SoundInstance.IsLooped = true;
            if (SoundInstance.State != SoundState.Playing)
                SoundInstance?.Play();
            initAction?.Invoke(this);
        }
        /// <summary>
        /// Is your ambient track playing?
        /// </summary>
        public bool IsPlaying => volume > 0f;
        /// <summary>
        /// Run code while this ambience is active.
        /// </summary>
        internal void UpdateActive() { activeUpdateAction?.Invoke(this); }
        /// <summary>
        /// The name for the ambient track. Defaults to "N/A."
        /// <para>Don't forget to set the name!</para>
        /// </summary>
        public string Name { get; set; } = "N/A";
        /// <summary>
        /// Start playing the ambience track when this value is true. Defaults to false, so set the value!
        /// </summary>
        public bool WhenToPlay { get; set; }
        /// <summary>
        /// This is how fast this ambience changes volume based on whether it is active or not. <para></para>
        /// For example, if the ambience should not be playing, the volume would decrease by the value this is set to every tick. Defaults to 0.0075f.
        /// </summary>
        public float VolumeStep { get; set; } = 0.0075f;
        /// <summary>
        /// The maximum volume this ambient track can achieve at any given time.
        /// </summary>
        public float MaxVolume { get; set; }
        /// <summary>
        /// Completely internal ambient updating.
        /// </summary>
        internal void INTERNAL_Update()
        {
            if (_playWhen != null)
                WhenToPlay = _playWhen.Invoke();
            if (IsPlaying)
                UpdateActive();
            if (SoundInstance != null)
            {
                if (WhenToPlay && Main.hasFocus && volume <= MaxVolume)
                    volume += VolumeStep;
                if (!WhenToPlay || !Main.hasFocus || Main.gameMenu)
                    volume -= VolumeStep;
                volume = MathHelper.Clamp(volume, 0f, 1f);
                SoundInstance.Volume = volume * Main.ambientVolume;
            }
        }
        public override string ToString()
        {
            return $"IsPlaying: {IsPlaying} | VolStep: {VolumeStep} | Volume: {volume} | ShouldBePlaying: {WhenToPlay} | Name: {Name}";
        }
    }
}
