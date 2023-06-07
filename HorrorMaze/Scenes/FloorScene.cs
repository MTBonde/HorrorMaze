namespace HorrorMaze
{
    public class FloorScene : Scene
    {

        public DebugManager debugManager = new DebugManager();

        #region Methods
        public override void SetupScene()
        {
            //hides the cursor
            GameWorld.Instance.IsMouseVisible = false;
            //stops music track and starts ambient track
            AudioManager.StatBackgroundSound();
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            #region instatiating of GameObjects
            //player
            GameObject player = new GameObject();
            player.AddComponent<PlayerController>();
            player.name = "Player";

            //Ambience
            GameObject amb = new GameObject();
            amb.AddComponent<AmbienceController>();

            SpawnDebugCamera();
            #endregion

            // set timers
            SceneManager._gameTimer.ResetTimer();
            SceneManager._gameTimer.StartTimer();
        }


        private void SpawnDebugCamera()
        {
            GameObject debugCam = new GameObject();
            debugCam.transform.Position3D = new Vector3(0, 0, 10);
            debugCam.transform.Rotation = new Vector3(0, 10, 270);
            debugCam.AddComponent<Camera>().enabled = false;
            debugCam.AddComponent<DebugCameraController>().enabled = false;
            debugCam.name = "DebugCam";
        }
        #endregion
    }
}
