﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace HorrorMaze
{
    /// <summary>    ///
    /// Contains a collection of global accesibel fields, properties and methods.
    /// </summary>
    public static class Globals
    {
        // FIELDS
        #region Fields        
        public static GameTime GameTime;

        public static Random Rnd = new Random();

        public static Vector2 ScreenCenter = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2,
                                                         GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
        public static Vector2 ScreenSize = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth,
                                                       GameWorld.Instance.Graphics.PreferredBackBufferHeight);

   
        #endregion Fields

        // PROPERTIES
        #region Properties
        public static float DeltaTime { get; set; }
        public static bool DebugModeToggled { get; set; } // Debug
        
        //public static SpriteFont DebugFont { get; set; }
        #endregion Properties

        // METHODS
        #region Methods
        public static void Update(GameTime gameTime)
        {
            // DeltaTime Calculation
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

          
        }
        #endregion Methods
    }
}