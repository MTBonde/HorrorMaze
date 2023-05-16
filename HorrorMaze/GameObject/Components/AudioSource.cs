using Microsoft.Xna.Framework.Audio;

namespace HorrorMaze
{
    /// <summary>
    /// Represents an audio source component.
    /// by Thor
    /// </summary>
    public class AudioSource : Component
    {       
        public Dictionary<string, SoundEffectInstance> _SoundEffectsPlaying = new();
        /// <summary>
        /// Gets the audio emitter associated with the audio source.
        /// </summary>
        public AudioEmitter Emitter { get; private set; }

        /// <summary>
        /// Gets or sets the sound effect instance associated with the audio source.
        /// </summary>
        public SoundEffectInstance SFXInstance { get; set; }

        /// <summary>
        /// Initializes a new instance of the AudioSource class.
        /// </summary>
        public AudioSource()
        {
            Emitter = new AudioEmitter();
        }

        /// <summary>
        /// Updates the audio source position based on the parent game object's position.
        /// </summary>
        public void Update()
        {
            Emitter.Position = gameObject.transform.Position3D;
        }

        /// <summary>
        /// Plays the specified sound effect on the audio source.
        /// </summary>
        /// <param name="soundEffect">The sound effect to play.</param>
        public void PlaySound(SoundEffect soundEffect)
        {
            if(soundEffect != null)
            {
                SFXInstance = soundEffect.CreateInstance();
                SFXInstance.IsLooped = true;
                SFXInstance.Play();
                //_SoundeffectsPlaying.Add(gameObject.name, soundEffect);
            }
        }
        public void StopSound()
        {
            if(SFXInstance != null)
            {
                SFXInstance.Stop();
                SFXInstance.Dispose();
                SFXInstance=null;
            }
        }

        public void PlaySound(string soundName, SoundEffect soundEffect)
        {
            if(soundEffect != null && !_SoundEffectsPlaying.ContainsKey(soundName))
            {
                SoundEffectInstance instance = soundEffect.CreateInstance();
                //instance.IsLooped = true;
                instance.Play();

                _SoundEffectsPlaying.Add(soundName, instance);
            }
        }

        public void StopSound(string soundName)
        {
            if(_SoundEffectsPlaying.ContainsKey(soundName))
            {
                _SoundEffectsPlaying[soundName].Stop();
                _SoundEffectsPlaying[soundName].Dispose();
                _SoundEffectsPlaying.Remove(soundName);
            }
        }
    }
}