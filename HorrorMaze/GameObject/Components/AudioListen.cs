

namespace HorrorMaze
{
    public class AudioListen : Component
    {
        public AudioListen Listener;
        public GameObject GameObject;

        public AudioListen()
        {
            this.GameObject = GameObject;
            Listener = new AudioListen();
        }

        public void Update()
        {
            // Update the listener's position 
            Listener.transform.Position3D = GameObject.transform.Position3D;
        }
    }
}
