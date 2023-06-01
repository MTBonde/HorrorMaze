
namespace HorrorMaze
{
    public delegate void KeyEvent();

    /// <summary>
    /// A key that can be used to call events on collision
    /// Niels
    /// </summary>
    public class Key : Component
    {

        public Door door;
        int _rotationSpeed = 100;
        public event KeyEvent keyEvent;
        public AudioSouce pickupSound;

        //adds needed components and sets their variables
        public void Awake()
        {
            gameObject.AddComponent<BoxCollider>().size = new Vector3(0.25f, 0.25f, 0.25f);
            pickupSound = gameObject.AddComponent<AudioSouce>();
            pickupSound.SetSoundEffect("SoundFX\\key_pickup");
        }

        //rotates the key around its z axis (camera up axis)
        public void Update()
        {
            transform.Rotation += new Vector3(0, 0, _rotationSpeed * Globals.DeltaTime);
        }

        //called when something collides with the key
        public void OnCollision(GameObject go)
        {
            //chexks if the player is colliding with the key
            if (go != null)
            {
                //Invokes all events attact to the key
                if (keyEvent != null)
                    keyEvent.Invoke();
                //open an attached door(should be replaced with key event)
                if(door != null)
                {
                    pickupSound.Play();
                    door.OpenDoor();
                }
                //removes the key from the world
                CollisionManager.colliders.Remove(gameObject.GetComponent<BoxCollider>());
                SceneManager.active_scene.gameObjects.Remove(gameObject);
            }
        }
    }
}
