
namespace HorrorMaze
{
    public class AudioManager
    {
        private const string _creepyMusicPath = "creepy_music";
        private const string _creepySoundPath = "SoundFX\\creepy_sound";

        private Dictionary<string, SoundEffect> _soundEffects;
        private PlayerAudioListener _playerAudioListener;
        private List<AudioSource> _audioSources;
        private List<Song> _songs;
        private SoundEffectInstance _musicSource;

        public AudioManager()
        {
            _soundEffects = new Dictionary<string, SoundEffect>();
            _audioSources = new List<AudioSource>();
            _songs = new List<Song>();
            LoadDefaultSounds();
        }

        //public static void ApplySpacialSound(SoundEffectInstance soundEffectInstance, AudioEmitter emitter, float maxListenDistance)
        //{
        //    //sets volume based on max distance
        //    float volume = Vector3.Distance(audioListener.Position, emitter.Position) / maxListenDistance;
        //    //apply spacial if volume is higher than 0
        //    if(volume > 0 && volume <= 1)
        //    {
        //        //if (soundEffectInstance.State == SoundState.Stopped)
        //        //    soundEffectInstance.Play();
        //        soundEffectInstance.Apply3D(audioListener, emitter);
        //        soundEffectInstance.Volume = Math.Clamp((1 - volume) * 2, 0, 1);
        //    }
        //    //stops the sound while sound is equal to 0
        //    else if(soundEffectInstance.State == SoundState.Playing)
        //        soundEffectInstance.Stop();
        //}

        private void LoadDefaultSounds()
        {
            _soundEffects[_creepyMusicPath] = GameWorld.Instance.Content.Load<SoundEffect>(_creepyMusicPath);
            _soundEffects[_creepySoundPath] = GameWorld.Instance.Content.Load<SoundEffect>(_creepySoundPath);
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
            _soundEffects[soundEffectName] = GameWorld.Instance.Content.Load<SoundEffect>($"SoundFX\\{soundEffectName}");
        }

        public SoundEffect GetSoundEffect(string soundEffectName)
        {
            return _soundEffects.ContainsKey(soundEffectName) ? _soundEffects[soundEffectName] : null;
        }

        public void AddAudioSource(AudioSource audioSource)
        {
            _audioSources.Add(audioSource);
        }

        public void StartBackgroundMusic()
        {
            SwitchBackgroundSound(_soundEffects[_creepyMusicPath], 0.1f);
        }

        public void StartBackgroundSound()
        {
            SwitchBackgroundSound(_soundEffects[_creepySoundPath], 0.05f);
        }

        private void SwitchBackgroundSound(SoundEffect soundEffect, float volume)
        {
            if(_musicSource != null)
            {
                _musicSource.Stop();
                _musicSource.Dispose();
            }

            _musicSource = soundEffect.CreateInstance();
            _musicSource.Volume = volume;
            _musicSource.Play();
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
            foreach(AudioSource audioSource in _audioSources)
            {
                foreach(var soundEffectPair in audioSource._SoundEffectsPlaying)
                {
                    var soundEffectInstance = soundEffectPair.Value;
                    if(soundEffectInstance != null)
                    {
                        soundEffectInstance.Stop();
                        soundEffectInstance.Dispose();
                    }
                }

                audioSource._SoundEffectsPlaying.Clear();
            }

            if(_musicSource != null)
            {
                _musicSource.Stop();
                _musicSource.Dispose();
                _musicSource = null;
            }
        }
    }
}
