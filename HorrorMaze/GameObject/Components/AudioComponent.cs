



namespace HorrorMaze
{
    public class AudioComponent : Component
    {
        public AudioListener Listener { get; private set; }
        public AudioEmitter Emitter { get; private set; }
        public GameObject GameObject { get; private set; }
        public SoundEffectInstance SoundEffectInstance { get; set; }
        private bool _isListener = false;

        public AudioComponent(GameObject gameObject)
        {
            GameObject = gameObject;
            this._isListener = IsListener();

            if(_isListener)
            {
                
            }
            if(!_isListener)
            {
                
            }
        }

        public bool IsListener()
        {
            Listener = new AudioListener();
            return _isListener = true;
        }

        public bool IsEmitter()
        {
            Emitter = new AudioEmitter();
            return _isListener = false;
        }

        public void Update()
        {
            if(_isListener)
            {
                UpdateListener();
            }
            else
            {
                UpdateEmitter();
            }
        }

        private void UpdateListener()
        {
            Listener.Position = GameObject.transform.Position3D;
           

        }

        private void UpdateEmitter()
        {
            Emitter.Position = GameObject.transform.Position3D;

            if(SoundEffectInstance != null && !SoundEffectInstance.IsDisposed && SoundEffectInstance.State != SoundState.Stopped)
            {
                SoundEffectInstance.Apply3D(Listener, Emitter);
            }
        }

        public void PlaySound(SoundEffect soundEffect)
        {
            if(!_isListener)
            {
                SoundEffectInstance = soundEffect.CreateInstance();
                SoundEffectInstance.Play();
            }
        }
    }
}
