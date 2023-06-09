
namespace HorrorMaze
{
    /// <summary>
    /// Statue enemy the follow you when your not looking at it
    /// Thor
    /// </summary>
    public class Statue : Enemy
    {
        private float _speed = 1f; // TODO : adjust speed
        private int _huntingRange = 3;
        private int _huntingMaxRange = 6;
        private bool _isPlayerLooking;

        // Audio scream
        AudioSouce _scream;
        // MIMIC transfrom sound
        AudioSouce _statueHuntingAudiosource;

        public override void Awake()
        {
            base.Awake();
            Speed = _speed;

            // set model             
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig"); // TODO: PHANTOM model

            // Enemy Audio TODO: statue scraping soundL SOUNDS
            AudioSouce StatueAudioSouce = gameObject.AddComponent<AudioSouce>();
            StatueAudioSouce.AddSoundEffect("SoundFX\\zombie1"); // TODO : statue sound           
            StatueAudioSouce.randomLoopEffects = true;
            StatueAudioSouce.loop = true;
            StatueAudioSouce.Spacial = true;
            StatueAudioSouce.maxDistance = 3f;
            StatueAudioSouce.Play();

            // statue hunting sound
            _statueHuntingAudiosource = gameObject.AddComponent<AudioSouce>();

            // Enemy Scream Audio
            _scream = gameObject.AddComponent<AudioSouce>();
            // _ghoulscream.SetSoundEffect("SoundFX\\chasing_scream_1"); TODO: NY skrig LYD til Phantom
            _scream.Spacial = true;
            _scream.maxDistance = 5;
        }

        public override void Update_()
        {
            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            float distanceToPlayer = Vector3.Distance(transform.Position3D, playerPos);

            // If the player is within _huntingRange tiles and not looking, start hunting
            if(distanceToPlayer <= _huntingRange && !_isPlayerLooking)
            {
                isHunting = true;
                _statueHuntingAudiosource.Play();
            }
            // If the player is looking, stop hunting
            else if(_isPlayerLooking)
            {
                isHunting = false;
            }

            base.Update_();
        }

        // TODO : This method should be called whenever the player looks at the statue
        public void OnPlayerLook()
        {
            _isPlayerLooking = true;
        }

        // TODO : This method should be called whenever the player looks away from the statue
        public void OnPlayerLookAway()
        {
            _isPlayerLooking = false;
        }
    }
}
