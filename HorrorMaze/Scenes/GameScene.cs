
namespace HorrorMaze
{
    public class GameScene : Scene
    {



        #region Methods
        public override void SetupScene()
        {
            // Initialize AudioManager
            AudioManager audioManager = new AudioManager();

            // Load sound effects
            audioManager.LoadSoundEffect("heartbeat","SoundFX\\heartbeat");
            //audioManager.LoadSoundEffect("breathing");
            //audioManager.LoadSoundEffect("Footsteps");

            
            //GameWorld.Instance.IsMouseVisible = true;
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //Test maze
            Maze maze = new Maze();
            MazeCell[,] cells = maze.GenerateMaze(10, 10);
            GameObject go = new GameObject();
            go.AddComponent<MazeRenderer>().SetMaze(cells);

            //test enemy
            GameObject enemy = new GameObject();
            enemy = new GameObject();
            enemy.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            enemy.transform.Position3D = new Vector3(0.5f, 0.5f, 0);
            enemy.AddComponent<Pathing>().mazeCells = cells;
            enemy.AddComponent<Enemy>();
            AudioComponent enemyAudioComponent = enemy.AddComponent<AudioComponent>();
            enemyAudioComponent.IsEmitter();

            // test thread
            ThreadManager.Startup(enemy);

            // Add the AudioComponent to AudioManager
            audioManager.AddAudioComponent(enemyAudioComponent);

            //test player           
            GameObject player = new GameObject();           
            player.transform.Position3D = new Vector3(0.5f, 1.5f, 1.6f);
            player.transform.Rotation = new Vector3(0, 0, 0);
            player.AddComponent<PlayerController>();
            player.AddComponent<Camera>();
            // Set up the listener AudioComponent and attach it to the player, and set it to true via the islistener method
            AudioComponent listenerAudioComponent = player.AddComponent<AudioComponent>();
            listenerAudioComponent.IsListener();



            // TODO : IN UPDATE
            // Calculate the distance between the listener and the emitter
            float distance = Vector3.Distance(listenerAudioComponent.Listener.Position, enemyAudioComponent.Emitter.Position);

            // Play heartbeat sound if the emitter is within a distance of 10
            if(distance <= 10f && enemyAudioComponent.SoundEffectInstance == null)
            {
                enemyAudioComponent.PlaySound(audioManager.GetSoundEffect("heartbeat"));
            }
            // Play breathing sound if the emitter is within a distance of 5
            else if(distance <= 5f && enemyAudioComponent.SoundEffectInstance == null)
            {
                //enemyAudioComponent.PlaySound(audioManager.GetSoundEffect("breathing"));
            }
            // Stop the sound if the emitter is outside the distance of 10
            else if(distance > 10f && enemyAudioComponent.SoundEffectInstance != null)
            {
                enemyAudioComponent.SoundEffectInstance.Stop();
                enemyAudioComponent.SoundEffectInstance.Dispose();
                enemyAudioComponent.SoundEffectInstance = null;
            }
        }
        #endregion
    }
}
