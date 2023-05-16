// AudioManager.cs
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace HorrorMaze
{
    public class AudioManager
    {
        public Dictionary<string, SoundEffect> _soundEffects;
        private PlayerAudioListener _playerAudioListener;
        private List<AudioSource> _audioSources;
        private List<Song> _songs;

        public AudioManager()
        {
            _soundEffects = new Dictionary<string, SoundEffect>();
            _audioSources = new List<AudioSource>();
            _songs = new List<Song>();
        }

        public void AddSong(string songName)
        {
            _songs.Add(GameWorld.Instance.Content.Load<Song>(songName));
        }

        public void SetPlayerAudioListener(PlayerAudioListener playerAudioListener)
        {
            this._playerAudioListener = playerAudioListener;
        }

        public void LoadSoundEffect(string soundEffectName)
        {
            SoundEffect soundEffect = GameWorld.Instance.Content.Load<SoundEffect>($"SoundFX\\{soundEffectName}");
            _soundEffects.Add(soundEffectName, soundEffect);
        }

        public SoundEffect GetSoundEffect(string soundEffectName)
        {
            return _soundEffects.ContainsKey(soundEffectName) ? _soundEffects[soundEffectName] : null;
        }

        public void AddAudioSource(AudioSource audioSource)
        {
            _audioSources.Add(audioSource);
        }

        public void Update()
        {
            if(_playerAudioListener != null)
            {
                _playerAudioListener.Update();
                foreach(AudioSource audioSource in _audioSources)
                {
                    audioSource.Update();
                    if(audioSource.SFXInstance != null)
                    {
                        audioSource.SFXInstance.Apply3D(_playerAudioListener.Listener, audioSource.Emitter);
                    }
                }
            }
        }

        public void StopAllSounds()
        {
            _soundEffects = new Dictionary<string, SoundEffect>();
            foreach(AudioSource audioSource in _audioSources)
            {
                if(audioSource.SFXInstance != null)
                {
                    audioSource.SFXInstance.Stop();
                    audioSource.SFXInstance.Dispose();
                    audioSource.SFXInstance = null;
                }
            }
        }
    }
}
