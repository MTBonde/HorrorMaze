namespace HorrorMaze
{
    public class TitleScreenCamera : Component
    {

        float _rotateScale = 50;
        int turn;
        float time = 0, maxTime = 3;

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
                timesUp();
        }

        public void timesUp()
        {
            time = 0;
            turn = Globals.Rnd.Next(2);
            maxTime = Globals.Rnd.Next(3,6);
        }
    }
}
