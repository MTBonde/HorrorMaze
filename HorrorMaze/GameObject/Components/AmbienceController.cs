namespace HorrorMaze
{
    public class AmbienceController : Component
    {

        BackupAudioSouce souce;
        float currentTimerTime, timerTime = 10;

        public void Awake()
        {
            souce = gameObject.AddComponent<BackupAudioSouce>();
            souce.AddSoundEffect("SoundFX\\Ambience\\scary1");
            souce.AddSoundEffect("SoundFX\\Ambience\\scary2");
            souce.AddSoundEffect("SoundFX\\Ambience\\scary3");
            souce.AddSoundEffect("SoundFX\\Ambience\\scary4");
            souce.randomLoopEffects = true;
            souce.Spacial = true;
            souce.maxDistance = 3f;
        }

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
                transform.Position = new Vector2(Globals.Rnd.Next(0, 2), Globals.Rnd.Next(0, 2)) + SceneManager.GetGameObjectByName("Player").transform.Position;
                souce.Play();
            }
        }
    }
}
