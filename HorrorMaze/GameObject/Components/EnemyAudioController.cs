
namespace HorrorMaze
{
    public class EnemyAudioController : Component
    {
        private AudioSource _enemyAudioSource;
        private PlayerAudioListener _playerAudioListener;
        private AudioManager _audioManager;

        public EnemyAudioController()
        {
            
        }
        public void Setup(AudioSource enemyAudioSource, PlayerAudioListener playerAudioListener, AudioManager audioManager)
        {
            this._enemyAudioSource = enemyAudioSource;
            this._playerAudioListener = playerAudioListener;
            this._audioManager = audioManager;
        }

        public void Update()
        {
            float distance = Vector3.Distance(_playerAudioListener.Listener.Position, _enemyAudioSource.Emitter.Position);
            float maxDistance = 5f; // TODO: Rename to the right sound
            float maxDistanceBreathing = 5f;

            if(distance <= maxDistance && _enemyAudioSource.SFXInstance == null)
            {
                _enemyAudioSource.PlaySound(_audioManager.GetSoundEffect("heartbeat"));
                CalculateVolumenBasedOnDistance(distance, maxDistance);
                //if(_enemyAudioSource.SFXInstance.Volume > 0.1f)
                //    StopAllSound();

            }
            else if(distance <= maxDistanceBreathing && _enemyAudioSource.SFXInstance == null)
            {
                _enemyAudioSource.PlaySound(_audioManager.GetSoundEffect("breathing"));
            }
            else if(distance > maxDistance && _enemyAudioSource.SFXInstance != null)
            {
                StopAllSound();
            }
        }

        private void StopAllSound()
        {
            _enemyAudioSource.SFXInstance.Stop();
            _enemyAudioSource.SFXInstance.Dispose();
            _enemyAudioSource.SFXInstance = null;
        }

        private void CalculateVolumenBasedOnDistance(float distance, float maxDistance)
        {
            // Calculate the volume based on the distance
            float volume = 1f - (distance / maxDistance);

            // Clamp the volume between 0 and 1
            volume = MathHelper.Clamp(volume, 0f, 1f); 

            // Apply the volume to the sound effect instance
            _enemyAudioSource.SFXInstance.Volume = volume;
        }
    }
}
