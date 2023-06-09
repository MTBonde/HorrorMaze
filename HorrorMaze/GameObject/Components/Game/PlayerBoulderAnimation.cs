
namespace HorrorMaze
{
    public class PlayerBoulderAnimation : Component
    {

        float time = 0, timer = 1f, timer2 = 2f, timer3 = 3f;
        float turnAmount;
        bool spawned = false;
        AudioSource breakingSound;

        public void Update()
        {
            time += Globals.DeltaTime;
            if (time < timer)
            {
                if(breakingSound == null) 
                {
                    turnAmount = -(transform.Rotation.Z%360);
                    breakingSound = gameObject.AddComponent<AudioSource>();
                    breakingSound.SetSoundEffect("SoundFX\\break");
                    breakingSound.Play();
                }
                transform.Rotation += new Vector3(0,0,turnAmount * Globals.DeltaTime);
            }
            else if (time < timer2)
            {
                if (!spawned)
                {
                    spawned = true;
                    GameObject boulder = new GameObject();
                    boulder.AddComponent<TutorialEnemy>();
                }
            }
            else if (time < timer3)
            {
                transform.Rotation += new Vector3(0, 0, 180 * Globals.DeltaTime);
            }
            else
            {
                gameObject.GetComponent<PlayerController>().enabled = true;
                gameObject.GetComponent<PlayerController>().RefillSprint();
                SceneManager.GetGameObjectByName("Boulder").GetComponent<TutorialEnemy>().wait = false;
                enabled = false;
            }
        }
    }
}
