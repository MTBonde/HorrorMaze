namespace HorrorMaze
{
    public class AudioSource : Component
    {
        public AudioEmitter Emitter { get; private set; }        
        public SoundEffectInstance SoundEffectInstance { get; set; }

        public AudioSource()
        {            
            Emitter = new AudioEmitter();
        }

        public void Update()
        {
            Emitter.Position = gameObject.transform.Position3D;
        }

        public void PlaySound(SoundEffect soundEffect)
        {
            if(soundEffect != null)
            {
                SoundEffectInstance = soundEffect.CreateInstance();
                SoundEffectInstance.Play();
            }
        }
    }
}
