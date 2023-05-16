using System.Diagnostics;

namespace HorrorMaze
{
    public static class BackupAudioManager
    {

        public static AudioListener audioListener = new AudioListener();
        public static SoundEffect music, ingame;
        private static SoundEffectInstance musicSource;

        public static void LoadMusic()
        {
            music = GameWorld.Instance.Content.Load<SoundEffect>("creepy_music");
            ingame = GameWorld.Instance.Content.Load<SoundEffect>("SoundFX\\creepy_sound");
        }

        public static void StartBackgroundMusic()
        {
            if (musicSource == null)
                musicSource = music.CreateInstance();
            else
            {
                musicSource.Stop();
                musicSource.Dispose();
                musicSource = null;
                musicSource = ingame.CreateInstance();
            }
            musicSource.Volume = 0.1f;
            musicSource.Play();
        }

        public static void StatBackgroundSound()
        {
            if (musicSource == null)
                musicSource = ingame.CreateInstance();
            else
            {
                musicSource.Stop();
                musicSource.Dispose();
                musicSource = null;
                musicSource = ingame.CreateInstance();
            }
            musicSource.Volume = 0.05f;
            musicSource.Play();
        }

        public static void ApplySpacialSound(SoundEffectInstance soundEffectInstance, AudioEmitter emitter, float maxListenDistance)
        {
            float volume = Vector3.Distance(audioListener.Position, emitter.Position) / maxListenDistance;
            Debug.WriteLine(volume);
            if (volume > 0 && volume <= 1)
            {
                if(soundEffectInstance.State == SoundState.Stopped)
                    soundEffectInstance.Play();
                soundEffectInstance.Apply3D(audioListener, emitter);
                soundEffectInstance.Volume = Math.Clamp((1 - volume) * 2,0,1);
            }
            else if(soundEffectInstance.State == SoundState.Playing)
                soundEffectInstance.Stop();
        }
    }
}
