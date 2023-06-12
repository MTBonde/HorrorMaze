﻿namespace HorrorMaze
{
    /// <summary>
    /// DebugManager, can be accessed by pressing F3
    /// Thor
    /// </summary>
    public class DebugManager
    {
        private int _inDebugModeRenderDistance = 25;
        private int _inGameModeRenderDistance = 5;

        private double _frameRate = 0;                  // Keeps track of the current frame rate of the game
        private bool _canPressF3 = true;                // Checks whether or not the F3 button can be pressed again
        private string _framerateText;          // The Framerate text rounded

        /// <summary>
        /// Debug manager constructer, creates an instance of the debug manager
        /// </summary>
        public DebugManager()
        {
            // Turn of debug mode from beginning
            Globals.DebugModeToggled = false;
        }

        // METHODS
        #region METHODS
        /// <summary>
        /// Updates the debug manager, is called in scenemanager's update
        /// thor/niels
        /// </summary>
        /// <param name="gameTime"></param>        
        public void Update(GameTime gameTime)
        {
            // Obtain the current keyboard state to handle inputs
            KeyboardState _keyboardState = Keyboard.GetState();

            // If F3 key is pressed and allowed to be, ToggleDebugMode function is called
            if(_keyboardState.IsKeyDown(Keys.F3) && _canPressF3)
            {
                // Toggles the debug mode (between debug and game mode)
                ToggleDebugMode();

                // Prevents the debug mode from being toggled again until F3 is released
                _canPressF3 = false;
            }
            // If F3 is released, allow it to be pressed again to toggle debug mode
            else if(_keyboardState.IsKeyUp(Keys.F3))
            {
                _canPressF3 = true;
            }

            // Calculate framerate based on deltatime
            _frameRate = (1 / Globals.DeltaTime);
        }

        /// <summary>
        /// Toggles between the debug and game mode camera
        /// thor/niels
        /// </summary>
        private void ToggleDebugMode()
        {
            // Switch the global DebugModeToggled flag to its opposite value
            Globals.DebugModeToggled = !Globals.DebugModeToggled;

            // If debug mode has been toggled on
            if(Globals.DebugModeToggled)
            {
                // If a player exists, is not null.
                if(SceneManager.scene == 2 || SceneManager.scene == 7)
                {
                    // Disable player control
                    SceneManager.GetGameObjectByName("Player").GetComponent<PlayerController>().enabled = false;

                    // Disable player camera
                    SceneManager.GetGameObjectByName("Player").GetComponent<Camera>().enabled = false;

                    // Change render distance to debug mode render distance
                    SceneManager.GetGameObjectByName("Maze").GetComponent<MazeRenderer>()._renderDist = _inDebugModeRenderDistance;

                    // Enable debug camera
                    SceneManager.GetGameObjectByName("DebugCam").GetComponent<Camera>().enabled = true;

                    // Enable debug camera controller
                    SceneManager.GetGameObjectByName("DebugCam").GetComponent<DebugCameraController>().enabled = true;
                    if(SceneManager.scene == 7)
                    {

                    }
                }
            }
            // If debug mode has been toggled off
            else
            {
                // If a player exists
                if(SceneManager.GetGameObjectByName("Player") != null)
                {
                    // Enable player control
                    SceneManager.GetGameObjectByName("Player").GetComponent<PlayerController>().enabled = true;

                    // Enable player camera
                    SceneManager.GetGameObjectByName("Player").GetComponent<Camera>().enabled = true;

                    // Enable enemy
                    SceneManager.GetGameObjectByName("Enemy").GetComponent<Enemy>().enabled = true;

                    // Change render distance to game mode render distance
                    SceneManager.GetGameObjectByName("Maze").GetComponent<MazeRenderer>()._renderDist = _inGameModeRenderDistance;

                    // Disable debug camera
                    SceneManager.GetGameObjectByName("DebugCam").GetComponent<Camera>().enabled = false;

                    // Disable debug camera controller
                    SceneManager.GetGameObjectByName("DebugCam").GetComponent<DebugCameraController>().enabled = false;
                }
            }
        }

        /// <summary>
        /// Debug manager draw method, is called in gamemanager's draw event
        /// not in use
        /// thor
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Check if debug mode is toggled
            if(Globals.DebugModeToggled)
            {
                // Draws the frame rate out to the screen
                spriteBatch.DrawString(Globals.DebugFont, $"{_frameRate.ToString("N2")} FPS", new Vector2(10, 10), Color.Black);
                //spriteBatch.DrawString(Globals.DebugFont, $"{GameScreen.Instance.NumberOfObjects} objects on screen", new Vector2(10, 50), Color.Black);
            }
        }
        #endregion METHODS
    }
}
