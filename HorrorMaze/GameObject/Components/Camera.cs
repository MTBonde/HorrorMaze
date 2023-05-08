using System.Linq;

namespace HorrorMaze
{
    //// Enum to represent the different types of camera
    //public enum CameraType
    //{
    //    ArcBallCamera,
    //    FixedCamera,
    //    FreeLookCamera, // cheaty debug cam
    //    FirstPersonCamera,
    //    ThirdPersonCamera
    //}

    public class Camera : Component
    {

        // FIELDS
        #region Fields
        //// The position of the camera in the 3D world
        //private Vector3 _position = Vector3.Zero;
        // The point the camera is looking at
        private Vector3 _lookAt;
        // A reference vector used to calculate the lookAt vector based on the camera rotation
        private Vector3 _baseCameraReference = new Vector3(0, 0, 1);
        //// The rotation of the camera around the Y-axis
        //private float _rotation;
        // Flag to indicate if the view matrix needs to be recalculated
        private bool _needViewResync = true;
        // Cached view matrix to avoid unnecessary calculations
        private Matrix _cachedViewMatrix;
        #endregion

        // PROPERTIES
        #region Properties
        // The projection matrix for the camera
        public Matrix Projection { get; private set; }
        //// The type of camera behavior
        //public CameraType CameraType { get; set; }
        // The target point the camera looks at
        public Vector3 Target { get; set; }

        // The position of the camera, updating the position will also update the lookAt vector
        public Vector3 Position
        {
            get => transform.Position3D;
            set
            {
                transform.Position3D = value;
                UpdateLookAt();
            }
        }

        // The rotation of the camera, updating the rotation will also update the lookAt vector
        public float Rotation
        {
            get => transform.Rotation.Z;
            set
            {
                transform.Rotation = new Vector3(0,0,value);
                UpdateLookAt();
            }
        }

        // The view matrix, calculated based on the camera's position and lookAt vector
        //public Matrix View => _needViewResync ? UpdateCachedViewMatrix() : _cachedViewMatrix;
        public Matrix View 
        { 
            get 
            { 
                if (_needViewResync)
                {
                    _cachedViewMatrix = Matrix.CreateLookAt(Position, _lookAt, Vector3.Up); 
                    SceneManager.active_scene.viewMatrix = _cachedViewMatrix;
                }
                return _cachedViewMatrix;
            } 
        }
        #endregion

        // CONSTRUCTOR
        #region Constructor
        // The constructor initializes the camera with a given position, rotation, and camera type
        public Camera()
        {
            float nearClip = 0.1f;
            float farClip = 1000f;
            var aspectRatio = GameWorld.Instance.Graphics.GraphicsDevice.Viewport.AspectRatio;
            SceneManager.active_scene.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), aspectRatio, nearClip, farClip);
            SceneManager.active_scene.viewMatrix = Matrix.CreateLookAt(new Vector3(0,0,1.6f), new Vector3(0,1,1.6f), new Vector3(0f, 0f, 1f));
        }

        #endregion

        // METHODS
        #region Methods
        public void Awake()
        {
            MoveTo(transform.Position3D, transform.Rotation.Z);
            SceneManager.active_scene.viewMatrix = UpdateCachedViewMatrix();
            //SceneManager.active_scene.camPosition = transform.Position3D;
            //SceneManager.active_scene.viewMatrix = Matrix.CreateRotationZ(transform.Rotation.Z);
        }

        public void Update()
        {
            MoveTo(transform.Position3D, transform.Rotation.Z);
            SceneManager.active_scene.viewMatrix = UpdateCachedViewMatrix();
        }
        //// The Update method is called every frame to update the camera behavior based on the camera type
        //public void Update(GameTime gameTime, Vector3 characterPosition, float eyeLevel, float characterRotation, Vector3 movement)
        //{
        //    switch(CameraType)
        //    {
        //        case CameraType.ArcBallCamera:
        //            break;
        //        case CameraType.FixedCamera:
        //            break;
        //        case CameraType.FreeLookCamera:
        //            break;
        //        case CameraType.FirstPersonCamera:
        //            UpdateFirstPersonCamera(characterPosition, eyeLevel, characterRotation, movement);
        //            break;
        //        case CameraType.ThirdPersonCamera:
        //            break;
        //    }
        //}
        //// Update the first-person camera based on the character's position, eye level, rotation, and movement
        //private void UpdateFirstPersonCamera(Vector3 characterPosition, float eyeLevel, float characterRotation, Vector3 movement)
        //{
        //    // Update camera position based on character position and eye level
        //    Position = characterPosition + new Vector3(0, eyeLevel, 0);

        //    // Update camera rotation based on character orientation
        //    Rotation = characterRotation;

        //    // Update camera movement based on character movement
        //    MoveTo(Position + movement, Rotation);
        //}
        // Move the camera to a new position and rotation
        public void MoveTo(Vector3 position, float rotation)
        {
            // Update the camera's internal position with the given position
            transform.Position3D = position;
            // Update the camera's internal rotation with the given rotation
            transform.Rotation = new Vector3(0,0,rotation);
            // Update the camera's lookAt vector based on the new position and rotation
            UpdateLookAt();
        }

        // Update the lookAt vector based on the camera's position and rotation
        private void UpdateLookAt()
        {
            // Create a rotation matrix around the Y-axis using the camera's rotation
            Matrix rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(180.1f)) + Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z));
            // Transform the base camera reference vector by the rotation matrix to get the lookAt offset
            Vector3 lookAtOffset = Vector3.Transform(_baseCameraReference, rotationMatrix);
            // Add the lookAt offset to the camera's position to calculate the new lookAt vector
            _lookAt = transform.Position3D + lookAtOffset;
            // Set the flag to indicate that the view matrix needs to be updated
            _needViewResync = true;
        }

        // Update the cached view matrix and reset the needViewResync flag
        private Matrix UpdateCachedViewMatrix()
        {
            // Create a new view matrix using the camera's position, lookAt vector, and the up vector
            _cachedViewMatrix = Matrix.CreateLookAt(transform.Position3D, _lookAt, Vector3.Up);
            // Reset the flag to indicate that the view matrix has been updated
            _needViewResync = false;
            // Return the updated view matrix
            return _cachedViewMatrix;
        }

        // Preview the new position of the camera after moving forward by the given scale
        public Vector3 PreviewMove(float scale)
        {
            // Create a rotation matrix around the Y-axis using the camera's rotation
            Matrix rotate = Matrix.CreateRotationX(MathHelper.ToRadians(180.1f)) + Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z));
            // Create a forward vector with the given scale
            Vector3 forward = new Vector3(0, 0, scale);
            // Transform the forward vector by the rotation matrix
            forward = Vector3.Transform(forward, rotate);
            // Add the transformed forward vector to the camera's position to calculate the new position
            return transform.Position3D + forward;
        }

        // Move the camera forward by the given scale
        public void MoveForward(float scale) => MoveTo(PreviewMove(scale), transform.Rotation.Z);

        // Set the camera's lookAt vector to point directly at the target
        public void LookAt(Vector3 target)
        {
            // Update the camera's internal target with the given target
            Target = target;
            // Set the camera's lookAt vector to be the same as the target
            _lookAt = target;
            // Set the flag to indicate that the view matrix needs to be updated
            _needViewResync = true;
        }

        #endregion
    }
}
