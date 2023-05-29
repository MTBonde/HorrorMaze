namespace HorrorMaze
{
    /// <summary>
    /// title scrren camera component, it has the update so the camera moves random
    /// Thor
    /// </summary>
    public class TitleScreenCamera : Component
    {

        float _rotateScale = 50;
        int turn;
        float time = 0, maxTime = 3;

        /// <summary>
        /// Update turns the camera, and randomly changes direction when the time is up
        /// </summary>
        public void Update()
        {
            float elapsed = Globals.DeltaTime;
            time+= elapsed;
            
            transform.Rotation += turn == 0
                ?
                new Vector3(0, 0, _rotateScale * elapsed / 3)
                :
                new Vector3(0, 0, -(_rotateScale * elapsed) / 3);
            if (time > maxTime)
                TimesUp();
        }

        /// <summary>
        /// timer used for camra direction change
        /// </summary>
        public void TimesUp()
        {
            time = 0;
            turn = Globals.Rnd.Next(2);
            maxTime = Globals.Rnd.Next(3,6);
        }
    }
}
