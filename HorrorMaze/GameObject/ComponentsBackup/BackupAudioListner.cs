﻿namespace HorrorMaze
{
    /// <summary>
    /// used to update the audioManagers listner position
    /// Niels/Thor
    /// </summary>
    public class BackupAudioListner : Component
    {



        //sets the audio listners up to be z posetive
        public void Start()
        {
            BackupAudioManager.audioListener.Up = new Vector3(0, 0, 1);
        }

        // updates the audio managers listner position
        public void Update()
        {
            BackupAudioManager.audioListener.Position = gameObject.transform.Position3D;
            BackupAudioManager.audioListener.Forward = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z + 90)));
        }
    }
}
