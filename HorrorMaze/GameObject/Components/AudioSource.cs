using Microsoft.Xna.Framework.Audio;

namespace HorrorMaze
{
    /// <summary>
    /// Represents an audio source component.
    /// by Thor
    /// </summary>
    public class AudioSource : Component
    {       
        public Dictionary<string, SoundEffect> _SoundeffectsPlaying= new();
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
    }
}