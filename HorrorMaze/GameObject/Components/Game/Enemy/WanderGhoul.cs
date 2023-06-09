

namespace HorrorMaze
{
    /// <summary>
    /// Wandering ghoul
    /// Thor
    /// </summary>
    public class WanderGhoul : Enemy
    {
        private float _speed = 1; // TODO : adjust speed
        private int _huntingRange;
        private int _huntingMaxRange;

        // Audio Wanderer scream
        AudioSouce _scream;


        public override void Awake()
        {
            base.Awake();
            Speed = _speed;

            // set model             
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig"); // TODO: GHoul model

            // Enemy Audio
            AudioSouce GrudgeAudioSouce = gameObject.AddComponent<AudioSouce>();
            GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie1");
            GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie2");
            GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie3");
            GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie4");
            GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie5");
            GrudgeAudioSouce.randomLoopEffects = true;
            GrudgeAudioSouce.loop = true;
            GrudgeAudioSouce.Spacial = true;
            GrudgeAudioSouce.maxDistance = 3f;
            GrudgeAudioSouce.Play();

            // Enemy Scream Audio
            _scream = gameObject.AddComponent<AudioSouce>();
            // _ghoulscream.SetSoundEffect("SoundFX\\chasing_scream_1"); TODO: NY skrig LYD til ghoul
            _scream.Spacial = true;
            _scream.maxDistance = 5;
        }

        public override void Update_()
        {
            base.Update_();

            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            float distanceToPlayer = Vector3.Distance(transform.Position3D, playerPos);

            // If the player is within 5 tiles, start hunting
            if(distanceToPlayer <= 5 && !isHunting)
            {
                isHunting = true;
            }
            // If the player is out of sight, return to wandering
            else if(isHunting && distanceToPlayer > 5)
            {
                isHunting = false;
            }
        }
    }
}
