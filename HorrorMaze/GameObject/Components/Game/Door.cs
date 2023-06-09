
namespace HorrorMaze
{
    /// <summary>
    /// a door that can open and close
    /// Niels
    /// </summary>
    public class Door : Component
    {

        Vector3 _openPos, _closePos;

        /// <summary>
        /// sets up the needed components
        /// </summary>
        public void Awake()
        {
            //sets up the renderer
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\door");
            //saves the closed position
            _closePos = transform.Position3D;
            //saves the open position
            _openPos = _closePos + new Vector3(0, 0, 1.8f);
            //sets up the collider
            BoxCollider doorCol = gameObject.AddComponent<BoxCollider>();
            doorCol.size = new Vector3(1, 0.2f, 1.9f);
            doorCol.offset = new Vector3(0, 0, 1);
        }

        /// <summary>
        /// opens the door
        /// </summary>
        public void OpenDoor()
        {
            transform.Position3D = _openPos;
        }

        /// <summary>
        /// closes the door
        /// </summary>
        public void CloseDoor()
        {
            transform.Position3D = _closePos;
        }
    }
}
