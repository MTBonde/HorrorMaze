namespace HorrorMaze
{
    /// <summary>
    /// This Class Controls the component that plays random ambient stinger at a random position aroudn the player
    /// Thor
    /// </summary>
    public class AmbienceController : Component
    {
        AudioSource _source;
        float _currentTimerTime, _timerTime = 10;

        /// <summary>
        /// sets up needed components
        /// </summary>
        public void Awake()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.AddSoundEffect("SoundFX\\Ambience\\scary1");
            _source.AddSoundEffect("SoundFX\\Ambience\\scary2");
            _source.AddSoundEffect("SoundFX\\Ambience\\scary3");
            _source.AddSoundEffect("SoundFX\\Ambience\\scary4");
            _source.randomLoopEffects = true;
            _source.Spacial = true;
            _source.maxDistance = 3f;
        }

        /// <summary>
        /// updates play timer and plays sound if timer reach play time
        /// </summary>
        public void Update()
        {
            if(_currentTimerTime < _timerTime)
            {
                _currentTimerTime += Globals.DeltaTime;
            }
            else
            {
                _currentTimerTime = 0;
                _timerTime = Globals.Rnd.Next(10,20);
                transform.Position = new Vector2(Globals.Rnd.Next(-2, 2),
                                                 Globals.Rnd.Next(-2, 2)) + SceneManager.GetGameObjectByName("Player").transform.Position;
                _source.Play();
            }
        }
    }
}
