namespace HorrorMaze
{
    public class AmbienceController : Component
    {

        AudioSouce souce;
        float currentTimerTime, timerTime = 10;

        /// <summary>
        /// sets up needed components
        /// </summary>
        public void Awake()
        {
            souce = gameObject.AddComponent<AudioSouce>();
            souce.AddSoundEffect("SoundFX\\Ambience\\scary1");
            souce.AddSoundEffect("SoundFX\\Ambience\\scary2");
            souce.AddSoundEffect("SoundFX\\Ambience\\scary3");
            souce.AddSoundEffect("SoundFX\\Ambience\\scary4");
            souce.randomLoopEffects = true;
            souce.Spacial = true;
            souce.maxDistance = 3f;
        }

        /// <summary>
        /// updates play timer and plays sound if timer reach play time
        /// </summary>
        public void Update()
        {
            if(currentTimerTime < timerTime)
            {
                currentTimerTime += Globals.DeltaTime;
            }
            else
            {
                currentTimerTime = 0;
                timerTime = Globals.Rnd.Next(10,20);
                transform.Position = new Vector2(Globals.Rnd.Next(-2, 2), Globals.Rnd.Next(-2, 2)) + SceneManager.GetGameObjectByName("Player").transform.Position;
                souce.Play();
            }
        }
    }
}
