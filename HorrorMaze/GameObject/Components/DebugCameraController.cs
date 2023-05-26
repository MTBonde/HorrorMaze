﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class DebugCameraController : Component
    {

        int _speed = 5;

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
                transform.Position += new Vector2(0,1) * _speed * Globals.DeltaTime;
            if (keyState.IsKeyDown(Keys.S))
                transform.Position += new Vector2(0,-1) * _speed * Globals.DeltaTime;
            if (keyState.IsKeyDown(Keys.D))
                transform.Position += new Vector2(1,0) * _speed * Globals.DeltaTime;
            if (keyState.IsKeyDown(Keys.A))
                transform.Position += new Vector2(-1,0) * _speed * Globals.DeltaTime;
        }
    }
}