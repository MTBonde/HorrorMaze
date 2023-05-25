﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// DebugManager, can be accessed by pressing F3
    /// </summary>
    public class DebugManager
    {
        double _frameRate = 0;                  // Keeps track of the current frame rate of the game
        bool _canPressF3 = true;                // Checks whether or not the F3 button can be pressed again
        private string _framerateText;          // The Framerate text rounded

        /// <summary>
        /// Debug manager constructer, creates an instance of the debug manager
        /// </summary>
        public DebugManager()
        {
            // Turn of debug mode
            Globals.DebugModeToggled = false;
        }

        /// <summary>
        /// Updates the debug manager, is called in gamemanager's update
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Get the keyboard state to check inputs
            KeyboardState _keyboardState = Keyboard.GetState();

            // Check for debug input (F3) and if it's allowed to be pressed again
            if(_keyboardState.IsKeyDown(Keys.F3) && _canPressF3 == true)
            {
                //Toggles the debug mode
                if(Globals.DebugModeToggled == false)
                {
                    Globals.DebugModeToggled = true;
                    //if game scene swap to debug cam/controls
                    if(SceneManager.GetGameObjectByName("Player") != null)
                    {
                        SceneManager.GetGameObjectByName("Player").GetComponent<PlayerController>().enabled = false;
                        SceneManager.GetGameObjectByName("Player").GetComponent<Camera>().enabled = false;
                        SceneManager.GetGameObjectByName("Enemy").GetComponent<BackupEnemy>().enabled = false;
                        SceneManager.GetGameObjectByName("Maze").GetComponent<MazeRenderer>()._renderDist = 25;
                        SceneManager.GetGameObjectByName("DebugCam").GetComponent<Camera>().enabled = true;
                        SceneManager.GetGameObjectByName("DebugCam").GetComponent<DebugCameraController>().enabled = true;
                    }
                }
                else
                {
                    Globals.DebugModeToggled = false;
                    //if game scene swap to player cam/controls
                    if (SceneManager.GetGameObjectByName("Player") != null)
                    {
                        SceneManager.GetGameObjectByName("Player").GetComponent<PlayerController>().enabled = true;
                        SceneManager.GetGameObjectByName("Player").GetComponent<Camera>().enabled = true;
                        SceneManager.GetGameObjectByName("Enemy").GetComponent<BackupEnemy>().enabled = true;
                        SceneManager.GetGameObjectByName("Maze").GetComponent<MazeRenderer>()._renderDist = 5;
                        SceneManager.GetGameObjectByName("DebugCam").GetComponent<Camera>().enabled = false;
                        SceneManager.GetGameObjectByName("DebugCam").GetComponent<DebugCameraController>().enabled = false;
                    }
                }

                // F3 can't be pressed until the button is released
                _canPressF3 = false;
            }
            else if(_keyboardState.IsKeyUp(Keys.F3))       // F3 is released
            {
                // F3 can now be pressed again
                _canPressF3 = true;
            }

            // Update the frame rate tracker
            _frameRate = (1 / Globals.DeltaTime);
            //_framerateText = _frameRate.ToString("N2");
        }

        /// <summary>
        /// Debug manager draw method, is called in gamemanager's draw event
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
    }
}
