
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
            Listener.Forward = gameObject.transform.Rotation;
            Listener.Up = Vector3.Up;
        }
    }
}
