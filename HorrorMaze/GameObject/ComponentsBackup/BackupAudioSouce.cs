namespace HorrorMaze
{
    public class BackupAudioSouce : Component
    {

        AudioEmitter _emitter = new AudioEmitter();
        SoundEffectInstance _soundEffectInstance;
        SoundEffect _soundEffect;
        bool running;
        public float maxDistance = 10;
        public bool Spacial = false;
        public bool loop = false;

        public void SetSoundEffect(string path)
        {
            _soundEffect = GameWorld.Instance.Content.Load<SoundEffect>(path);
        }

        public void Update()
        {
            if (running)
            {
                _emitter.Position = transform.Position3D;
                if(Spacial)
                    BackupAudioManager.ApplySpacialSound(_soundEffectInstance,_emitter,maxDistance);
                else if (!loop)
                    if(_soundEffectInstance.State == SoundState.Stopped)
                        running = false;
            }
        }

        public void Play()
        {
            if (!running)
            {
                running = true;
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
    }
}
