namespace HorrorMaze
{
    public class Camera : Component
    {

        // FIELDS
        #region Fields
        private Vector3 position = Vector3.Zero;
        private Vector3 lookAt;
        private Vector3 baseCameraReference = new Vector3(0, 0, 1);
        private float rotation;
        private bool needViewResync = true;
        private Matrix cachedViewMatrix;
        #endregion

        // PROPERTIES
        #region Properties
        public Matrix Projection { get; private set; }
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateLookAt();
            }
        }
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                UpdateLookAt();
            }
        }
        public Matrix View
        {
            get
            {
                if(needViewResync) cachedViewMatrix = Matrix.CreateLookAt(Position, lookAt, Vector3.Up);
                return cachedViewMatrix;
            }
        }
        #endregion

        // CONSTRUCTOR
        #region Constructor
        public Camera(Vector3 position, float rotation, float aspectRatio, float nearClip, float farClip)
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
            MoveTo(position, rotation);
        }
        #endregion

        // METHODS
        #region Methods
        public void MoveTo(Vector3 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
            UpdateLookAt();
        }
        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(rotation);
            Vector3 lookAtOffset = Vector3.Transform(baseCameraReference, rotationMatrix);
            lookAt = position + lookAtOffset;
            needViewResync = true;
        }
        public Vector3 PreviewMove(float scale)
        {
            Matrix rotate = Matrix.CreateRotationY(rotation);
            Vector3 forward = new Vector3(0, 0, scale);
            forward = Vector3.Transform(forward, rotate);
            return (position + forward);
        }
        public void MoveForward(float scale)
        {
            MoveTo(PreviewMove(scale), rotation);
        }
        #endregion

    }
}
