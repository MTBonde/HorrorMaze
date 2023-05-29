namespace HorrorMaze
{
    /// <summary>
    /// title scrren camera component, it has the update so the camera moves random
    /// Thor
    /// </summary>
    public class TitleScreenCamera : Component
    {

        private float _rotateSpeed = 50;
        private int _directionToTurn;
        private float _time = 0, _maxTime = 3;

        /// <summary>
        /// Update turns the camera, and randomly changes direction when the time is up
        /// </summary>
        public void Update()
        {
            // Get the time elapsed since the last frame
            float elapsed = Globals.DeltaTime;
            // tick up timer using delta time
            _time += elapsed;

            // Rotate the transform depending on the _directionToTurn variable.
            // If _directionToTurn is 0, rotate to the right; otherwise, rotate to the left.
            // ternary conditional operator = condition ? consequence : alternative
            transform.Rotation += _directionToTurn == 0 
                ? new Vector3(0, 0, _rotateSpeed * elapsed / 3) 
                : new Vector3(0, 0, -(_rotateSpeed * elapsed) / 3);

            // If time is bigger then maxTime, call TimesUp 
            if(_time > _maxTime)
                TimesUp();
        }

        /// <summary>
        /// timer used for camera direction change
        /// </summary>
        private void TimesUp()
        {
            // Reset the timer
            _time = 0;
            // Randomly determine the next _directionToTurn direction
            _directionToTurn = Globals.Rnd.Next(2);
            // Randomly determine the maximum time until the next direction change
            _maxTime = Globals.Rnd.Next(3, 6);
        }
    }
}
