
using System.Diagnostics;

namespace HorrorMaze
{
    /// <summary>
    /// Control the audio behavior for player.
    /// by Thor
    /// </summary>
    public class PlayerAudioController : Component
    {
        private AudioSource _playerAudioSource;
        private PlayerAudioListener _playerAudioListener;
        private AudioManager _audioManager;

        public PlayerAudioController()
        {
            
        }
        /// <summary>
        /// Set up enemy audio controller with the required audio source, audio listener, and audio manger.
        /// </summary>
        /// <param name="_playerAudioSource">The audio source for the player.</param>
        /// <param name="playerAudioListener">The audio listener for the player.</param>
        /// <param name="audioManager">The audio manager instance.</param>
        public void Setup(AudioSource playerAudioSource, PlayerAudioListener playerAudioListener, AudioManager audioManager)
        {
            this._playerAudioSource = playerAudioSource;
            this._playerAudioListener = playerAudioListener;
            this._audioManager = audioManager;
        }

        /// <summary>
        /// Updates the audio behavior of the player
        /// </summary>
        //public void Update()
        //{
        //    if(gameObject.GetComponent<PlayerController>().PlayBreathingSound == true)
        //    {
        //        if(_playerAudioSource.SFXInstance == null)
        //            _playerAudioSource.PlaySound(_audioManager.GetSoundEffect("Breathing"));
        //        else
        //            CalculateVolumenBasedOnEnergy();
        //    }
        //    if(gameObject.GetComponent<PlayerController>().PlayBreathingSound == false && _playerAudioSource.SFXInstance != null)
        //    {
        //        _playerAudioSource.StopSound();
        //    }
        //}

        public void Update()
        {
            if(gameObject.GetComponent<PlayerController>().PlayBreathingSound == true)
            {
                if(!_playerAudioSource._SoundEffectsPlaying.ContainsKey("Breathing"))
                {
                    _playerAudioSource.PlaySound("Breathing", _audioManager.GetSoundEffect("Breathing"));
                }
                else
                {
                    CalculateVolumenBasedOnEnergy("Breathing");
                }
            }
            else if(_playerAudioSource._SoundEffectsPlaying.ContainsKey("Breathing"))
            {
                _playerAudioSource.StopSound("Breathing");
            }

            // TODO: FOOTSTEPS
        }




        /// <summary>
        /// Stops and disposes the sound instance of the enemy.
        /// </summary>
        private void StopAllSound()
        {
            _playerAudioSource.SFXInstance.Stop();
            _playerAudioSource.SFXInstance.Dispose();
            _playerAudioSource.SFXInstance = null;
        }

        /// <summary>
        /// Calculates the volume of the sound effect based on the distance between the player and the enemy.
        /// </summary>
        /// <param name="distance">The distance between the player and the enemy.</param>
        /// <param name="maxDistance">The maximum distance for the sound effect.</param>
        private void CalculateVolumenBasedOnEnergy()
        {
            float energy = gameObject.GetComponent<PlayerController>().energy;
            float maxEnergy = gameObject.GetComponent<PlayerController>().maxEnergy;

            // Calculate the volume based on the distance
            float volume = 1f - (energy / maxEnergy);

            // Clamp the volume between 0 and 1
            volume = MathHelper.Clamp(volume, 0f, 1f);

            // Apply the volume to the sound effect instance
            _playerAudioSource.SFXInstance.Volume = volume;
        }

        private void CalculateVolumenBasedOnEnergy(string soundName)
        {
            float energy = gameObject.GetComponent<PlayerController>().energy;
            float maxEnergy = gameObject.GetComponent<PlayerController>().maxEnergy;

            // Calculate the volume based on the energy
            float volume = 1f - (energy / maxEnergy);

            // Clamp the volume between 0 and 1
            volume = MathHelper.Clamp(volume, 0f, 1f);

            // Apply the volume to the sound effect instance
            if(_playerAudioSource._SoundEffectsPlaying.ContainsKey(soundName))
            {
                _playerAudioSource._SoundEffectsPlaying[soundName].Volume = volume;
            }
        }
    }
}
