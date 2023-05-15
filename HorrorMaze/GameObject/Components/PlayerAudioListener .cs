
namespace HorrorMaze
{
    /// <summary>
    /// Represents the audio listener for the player.
    /// by Thor
    /// </summary>
    public class PlayerAudioListener : Component
    {
        /// <summary>
        /// Gets the audio listener associated with the player.
        /// </summary>
        public AudioListener Listener { get; private set; }

        /// <summary>
        /// Initializes a new instance of the PlayerAudioListener class.
        /// </summary>
        public PlayerAudioListener()
        {
            Listener = new AudioListener();
        }

        /// <summary>
        /// Updates the position and orientation of the audio listener based on the player's position and rotation.
        /// </summary>
        public void Update()
        {
            // Update the listener's position 
            Listener.Position = gameObject.transform.Position3D;

            // Calculate the forward vector based on the player's rotation
            Listener.Forward = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z + 90)));

            // Set the upward vector of the listener
            Listener.Up = new Vector3(0,0,1);
        }
    }
}

