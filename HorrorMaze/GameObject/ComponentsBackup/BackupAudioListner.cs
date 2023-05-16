namespace HorrorMaze
{
    public class BackupAudioListner : Component
    {



        public void Update()
        {
            BackupAudioManager.audioListener.Position = gameObject.transform.Position3D;
            BackupAudioManager.audioListener.Forward = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z + 90)));
            BackupAudioManager.audioListener.Up = new Vector3(0, 0, 1);
        }
    }
}
