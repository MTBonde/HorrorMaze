namespace HorrorMaze
{
    public class AudioSource : Component
    {
        public SoundEffect heatbeat;
        public SoundEffectInstance _soundEffectInstance;
        public AudioEmitter _emitter = new AudioEmitter();
        public GameObject GameObject;

        public AudioSource(GameObject gameObject)
        {
            this.GameObject = gameObject;

            // TODO: MOVE TO AUDIOMANAGER
            heatbeat = GameWorld.Instance.Content.Load<SoundEffect>("SoundFX\\heartbeat");

            _emitter.Position = gameObject.transform.Position3D;

            PlaySound();
        }

        public void Update()
        {
            // Update the emitter's position with the gameObject's position
            _emitter.Position = GameObject.transform.Position3D;

            // Apply the 3D settings to the sound effect instance
           // _soundEffectInstance.Apply3D(GameWorld.Instance.AudioListener, _emitter);
        }

        public void PlaySound()
        {
            _soundEffectInstance = heatbeat.CreateInstance();
            _soundEffectInstance.Play();
        }
    }
}
