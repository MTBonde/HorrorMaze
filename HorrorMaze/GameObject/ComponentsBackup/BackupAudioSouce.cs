using System.IO;

namespace HorrorMaze
{
    public class BackupAudioSouce : Component
    {

        AudioEmitter _emitter = new AudioEmitter();
        SoundEffectInstance _soundEffectInstance;
        SoundEffect _soundEffect;
        List<SoundEffect> _soundEffects = new List<SoundEffect>();
        bool running;
        public float maxDistance = 10;
        public bool Spacial = false;
        public bool loop = false;
        public bool randomLoopEffects;
        public float volume = 1;
        bool remove = false;

        public void SetSoundEffect(string path)
        {
            _soundEffect = GameWorld.Instance.Content.Load<SoundEffect>(path);
        }

        public void AddSoundEffect(string path)
        {
            _soundEffects.Add(GameWorld.Instance.Content.Load<SoundEffect>(path));
        }

        public void Update()
        {
            if (running && !remove)
            {
                _emitter.Position = transform.Position3D;
                _soundEffectInstance.Volume = volume;
                if(Spacial)
                    BackupAudioManager.ApplySpacialSound(_soundEffectInstance,_emitter,maxDistance);
                else if (!loop)
                {
                    if(_soundEffectInstance.State == SoundState.Stopped)
                        running = false;
                }
                if (loop && randomLoopEffects && _soundEffectInstance.State == SoundState.Stopped)
                {
                    _soundEffectInstance = _soundEffects[Globals.Rnd.Next(0, _soundEffects.Count)].CreateInstance();
                    _soundEffectInstance.Play();
                }
            }
        }

        public bool IsPlaying()
        {
            return running;
        }

        public void Play()
        {
            if (!running)
            {
                running = true;
                if(randomLoopEffects)
                    _soundEffectInstance = _soundEffects[Globals.Rnd.Next(0, _soundEffects.Count)].CreateInstance();
                else
                    _soundEffectInstance = _soundEffect.CreateInstance();
                _soundEffectInstance.IsLooped = loop;
                _soundEffectInstance.Play();
            }
        }

        public void Stop() 
        { 
            if (running)
            {
                running = false;
                _soundEffectInstance.Stop();
            }
        }

        public void StopSound()
        {
            if(_soundEffectInstance != null)
            {
                _soundEffectInstance.Stop();
                _soundEffectInstance.Dispose();
                _soundEffectInstance = null;
                remove = true;
            }
        }
    }
}
