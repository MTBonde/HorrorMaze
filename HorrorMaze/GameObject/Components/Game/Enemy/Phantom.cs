
namespace HorrorMaze
{
    /// <summary>
    /// Phantom Ghost enemy the can hunt player trough walls at reduced speed
    /// Thor
    /// </summary>
    public class Phantom : Enemy
    {
        private float _speed = 0.5f; // TODO : adjust speed
        private int _huntingRange = 3;
        private int _huntingMaxRange = 6;

        // Audio scream
        AudioSouce _scream;

        public Phantom()
        {
            canGoTroughWalls = true;
        }

        public override void Awake()
        {
            base.Awake();
            Speed = _speed;

            // set model             
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig"); // TODO: PHANTOM model

            // Enemy Audio TODO: PHANTOM ETHERIAL SOUNDS
            AudioSouce PhantomAudioSouce = gameObject.AddComponent<AudioSouce>();
            PhantomAudioSouce.AddSoundEffect("SoundFX\\zombie1");
            PhantomAudioSouce.AddSoundEffect("SoundFX\\zombie2");
            PhantomAudioSouce.AddSoundEffect("SoundFX\\zombie3");
            PhantomAudioSouce.AddSoundEffect("SoundFX\\zombie4");
            PhantomAudioSouce.AddSoundEffect("SoundFX\\zombie5");
            PhantomAudioSouce.randomLoopEffects = true;
            PhantomAudioSouce.loop = true;
            PhantomAudioSouce.Spacial = true;
            PhantomAudioSouce.maxDistance = 3f;
            PhantomAudioSouce.Play();

            // Enemy Scream Audio
            _scream = gameObject.AddComponent<AudioSouce>();
            // _ghoulscream.SetSoundEffect("SoundFX\\chasing_scream_1"); TODO: NY skrig LYD til Phantom
            _scream.Spacial = true;
            _scream.maxDistance = 5;
        }

        public override void Update_()
        {
            base.Update_();

            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            float distanceToPlayer = Vector3.Distance(transform.Position3D, playerPos);

            // If the player is within 3 tiles through walls, start hunting
            if(distanceToPlayer <= _huntingRange && !isHunting)
            {
                isHunting = true;
            }
            // If the player is beyond 6 tiles, stop hunting and return to normal speed
            else if(isHunting && distanceToPlayer > _huntingMaxRange)
            {
                isHunting = false;
                Speed = 1; // Return to normal speed
            }
        }
    }
}
