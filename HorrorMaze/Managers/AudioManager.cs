// AudioManager.cs
using Microsoft.Xna.Framework.Audio;

using System.Collections.Generic;

namespace HorrorMaze
{
    public class AudioManager
    {
        private Dictionary<string, SoundEffect> soundEffects;
        private PlayerAudioListener playerAudioListener;
        private List<AudioSource> audioSources;

        public AudioManager()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            audioSources = new List<AudioSource>();
        }

        public void SetPlayerAudioListener(PlayerAudioListener playerAudioListener)
        {
            this.playerAudioListener = playerAudioListener;
        }

        public void LoadSoundEffect(string soundEffectName)
        {
            SoundEffect soundEffect = GameWorld.Instance.Content.Load<SoundEffect>($"SoundFX\\{soundEffectName}");
            soundEffects.Add(soundEffectName, soundEffect);
        }

        public SoundEffect GetSoundEffect(string soundEffectName)
        {
            return soundEffects.ContainsKey(soundEffectName) ? soundEffects[soundEffectName] : null;
        }

        public void AddAudioSource(AudioSource audioSource)
        {
            audioSources.Add(audioSource);
        }

        public void Update()
        {
            if(playerAudioListener != null)
            {
                playerAudioListener.Update();
                foreach(var audioSource in audioSources)
                {
                    audioSource.Update();
                    if(audioSource.SoundEffectInstance != null)
                    {
                        audioSource.SoundEffectInstance.Apply3D(playerAudioListener.Listener, audioSource.Emitter);
                    }
                }
            }
        }

    }
}
