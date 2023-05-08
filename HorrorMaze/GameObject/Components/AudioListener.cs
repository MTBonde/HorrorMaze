

namespace HorrorMaze
{
    public class AudioListener : Component
    {
        public AudioListener Listener;
        public GameObject GameObject;

        public AudioListener()
        {
            this.GameObject = GameObject;
            Listener = new AudioListener();
        }

        public void Update()
        {
            // Update the listener's position 
            Listener.transform.Position3D = GameObject.transform.Position3D;
        }
    }
}
