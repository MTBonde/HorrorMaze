
using System.Diagnostics;

namespace HorrorMaze
{
    /// <summary>
    /// Control the audio behavior for enemy.
    /// by Thor
    /// </summary>
    public class EnemyAudioController : Component
    {
        private AudioSource _enemyAudioSource;
        private PlayerAudioListener _playerAudioListener;
        private AudioManager _audioManager;

        public EnemyAudioController()
        {
            
        }
        /// <summary>
        /// Set up enemy audio controller with the required audio source, audio listener, and audio manger.
        /// </summary>
        /// <param name="enemyAudioSource">The audio source for the enemy.</param>
        /// <param name="playerAudioListener">The audio listener for the player.</param>
        /// <param name="audioManager">The audio manager instance.</param>
        public void Setup(AudioSource enemyAudioSource, PlayerAudioListener playerAudioListener, AudioManager audioManager)
        {
            this._enemyAudioSource = enemyAudioSource;
            this._playerAudioListener = playerAudioListener;
            this._audioManager = audioManager;
        }

        /// <summary>
        /// Updates the audio behavior of the enemy based on the player's distance.
        /// </summary>
        public void Update()
        {
            float distance = Vector3.Distance(_playerAudioListener.Listener.Position, _enemyAudioSource.Emitter.Position);
            float maxDistanceGrudge = 3f; // Maximum distance for the screech sound
            float maxDistanceBreathing = 5f;
            float maxDistanceHeartbeat = 10f; // TODO: Rename to the right sound

            Debug.WriteLine(distance);

            if(distance <= maxDistanceGrudge && HasLineOfSightToPlayer())
            {
                if(!_enemyAudioSource._SoundEffectsPlaying.ContainsKey("grudge"))
                {
                    _enemyAudioSource.PlaySound("grudge", _audioManager.GetSoundEffect("grudge"));
                }
                else
                {
                    CalculateVolumenBasedOnDistance("grudge", distance, maxDistanceGrudge);
                }
            }
            else if(_enemyAudioSource._SoundEffectsPlaying.ContainsKey("grudge"))
            {
                _enemyAudioSource.StopSound("grudge");
            }

            if(distance <= maxDistanceHeartbeat)
            {
                if(!_enemyAudioSource._SoundEffectsPlaying.ContainsKey("heartbeat"))
                {
                    _enemyAudioSource.PlaySound("heartbeat", _audioManager.GetSoundEffect("heartbeat"));
                }
                else
                {
                    CalculateVolumenBasedOnDistance("heartbeat", distance, maxDistanceHeartbeat);
                }
            }
            else if(_enemyAudioSource._SoundEffectsPlaying.ContainsKey("heartbeat"))
            {
                _enemyAudioSource.StopSound("heartbeat");
            }

        }


        private bool HasLineOfSightToPlayer()
        {
            // Check if the enemy has a line of sight to the cam
            // raycasting??

            return true; // Return true if there is a line of sight, false otherwise, set to true until raycast is made
        }

        /// <summary>
        /// Stops and disposes the sound instance of the enemy.
        /// </summary>
        private void StopAllSound()
        {
            _enemyAudioSource.SFXInstance.Stop();
            _enemyAudioSource.SFXInstance.Dispose();
            _enemyAudioSource.SFXInstance = null;
        }

        /// <summary>
        /// Calculates the volume of the sound effect based on the distance between the player and the enemy.
        /// </summary>
        /// <param name="distance">The distance between the player and the enemy.</param>
        /// <param name="maxDistance">The maximum distance for the sound effect.</param>
        private void CalculateVolumenBasedOnDistance(float distance, float maxDistance)
        {
            // Calculate the volume based on the distance
            float volume = 1f - (distance / maxDistance);

            // Clamp the volume between 0 and 1
            volume = MathHelper.Clamp(volume, 0f, 1f); 

            // Apply the volume to the sound effect instance
            _enemyAudioSource.SFXInstance.Volume = volume;
        }

        private void CalculateVolumenBasedOnDistance(string soundName, float distance, float maxDistance)
        {
            // Calculate the volume based on the distance
            float volume = 1f - (distance / maxDistance);

            // Clamp the volume between 0 and 1
            volume = MathHelper.Clamp(volume, 0f, 1f);

            // Apply the volume to the sound effect instance
            if(_enemyAudioSource._SoundEffectsPlaying.ContainsKey(soundName))
            {
                _enemyAudioSource._SoundEffectsPlaying[soundName].Volume = volume;
            }
        }
    }
}
