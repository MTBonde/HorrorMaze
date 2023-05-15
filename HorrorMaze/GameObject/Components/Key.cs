﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Key : Component
    {

        public Door door;
        int _rotationSpeed = 100;

        public void Update()
        {
            transform.Rotation += new Vector3(0, 0, _rotationSpeed * Globals.DeltaTime);
        }

        public void OnCollision(GameObject go)
        {
            door.OpenDoor();
            CollisionManager.colliders.Remove(gameObject.GetComponent<BoxCollider>());
            SceneManager.active_scene.gameObjects.Remove(gameObject);
        }
    }
}
