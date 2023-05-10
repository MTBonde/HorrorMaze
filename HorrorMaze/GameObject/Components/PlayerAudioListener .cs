
namespace HorrorMaze
{
    public class PlayerAudioListener : Component
    {
        public AudioListener Listener { get; private set; }       

        public PlayerAudioListener()
        {            
            Listener = new AudioListener();
        }

        public void Update()
        {
            // Update the listener's position 
            Listener.Position = gameObject.transform.Position3D;
            Listener.Forward = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z + 90)));
            Listener.Up = Vector3.Up;        
        }
    }
}
