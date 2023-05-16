using System.Diagnostics;

namespace HorrorMaze
{
    public static class BackupAudioManager
    {

        public static AudioListener audioListener = new AudioListener();

        public static void ApplySpacialSound(SoundEffectInstance soundEffectInstance, AudioEmitter emitter, float maxListenDistance)
        {
            float volume = Vector3.Distance(audioListener.Position, emitter.Position) / maxListenDistance;
            Debug.WriteLine(volume);
            if (volume > 0 && volume <= 1)
            {
                if(soundEffectInstance.State == SoundState.Stopped)
                    soundEffectInstance.Play();
                soundEffectInstance.Apply3D(audioListener, emitter);
                soundEffectInstance.Volume = 1 - volume;
            }
            else if(soundEffectInstance.State == SoundState.Playing)
                soundEffectInstance.Stop();
        }
    }
}
