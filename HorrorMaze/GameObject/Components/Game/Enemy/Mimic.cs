
namespace HorrorMaze
{
    /// <summary>
    /// Mimic enemy that looks like a wall and then transform when player is close enough
    /// Thor
    /// </summary>
    public class Mimic : Enemy
    {
        private Enemy _transformedEnemy;

        private float _speed = 1; // TODO : adjust speed
        //private int _huntingRange = 2;
        //private int _huntingMaxRange = 6;
                
        private int _mimicTransformRange = 5;
        private int _mimicMaxTransformRange = 7;

        // Audio scream
        AudioSource _scream;

        // MIMIC transfrom sound
        AudioSource _mimicTransfromAudiosource;


        public override void Awake()
        {
            base.Awake();
            Speed = _speed;

            // set model             
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig"); // TODO: MIMIC model

            // MIMIC transfrom sound
            _mimicTransfromAudiosource = gameObject.AddComponent<AudioSource>();

            // Enemy Audio TODO: PHANTOM ETHERIAL SOUNDS
            AudioSource GrudgeAudioSouce = gameObject.AddComponent<AudioSource>();
            
            GrudgeAudioSouce.randomLoopEffects = true;
            GrudgeAudioSouce.loop = true;
            GrudgeAudioSouce.Spacial = true;
            GrudgeAudioSouce.maxDistance = 3f;
            GrudgeAudioSouce.Play();

            

            // Enemy Scream Audio
            _scream = gameObject.AddComponent<AudioSource>();
            // _ghoulscream.SetSoundEffect("SoundFX\\chasing_scream_1"); TODO: NY skrig LYD til Phantom
            _scream.Spacial = true;
            _scream.maxDistance = 5;
        }

        public override void Update_()
        {
            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            float distanceToPlayer = Vector3.Distance(transform.Position3D, playerPos);

            // If the player is within _mimicTransformRange tiles and not already hunting, start hunting
            if(distanceToPlayer <= _mimicTransformRange && !isHunting)
            {
                isHunting = true;
                // Transform into a Ghoul or Phantom randomly
                _transformedEnemy = Globals.Rnd.Next(0, 2) == 0 ? new WanderGhoul() : new Phantom();
                _mimicTransfromAudiosource.Play();
                _transformedEnemy.Start();
            }
            // If the player is beyond _mimicMaxTransformRange tiles, stop hunting and transform back into a wall
            else if(isHunting && distanceToPlayer > _mimicMaxTransformRange)
            {
                isHunting = false;
                _transformedEnemy = null;
            }

            // Use hunting behavior of transformed enemy
            if(_transformedEnemy != null)
            {
                _transformedEnemy.Update_();
            }
            else
            {
                base.Update_();
            }
        }
    }
}
