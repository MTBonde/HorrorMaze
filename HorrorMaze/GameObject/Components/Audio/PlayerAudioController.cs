
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
        //        if(!_playerAudioSource._SoundEffectsPlaying.ContainsKey("breathing"))
        //        {
        //            _playerAudioSource.PlaySound("breathing", _audioManager.GetSoundEffect("breathing"));
        //        }
        //        else
        //        {
        //            CalculateVolumenBasedOnEnergy("breathing");
        //        }
        //    }
        //    else if(_playerAudioSource._SoundEffectsPlaying.ContainsKey("breathing"))
        //    {
        //        _playerAudioSource.StopSound("breathing");
        //    }

        //    if(gameObject.GetComponent<PlayerController>().isSprinting == true)
        //    {
        //        if(!_playerAudioSource._SoundEffectsPlaying.ContainsKey("running"))
        //        {
        //            _playerAudioSource.PlaySound("running", _audioManager.GetSoundEffect("running"));
        //        }
        //        else
        //        {
        //            CalculateVolumenBasedOnEnergy("running");
        //        }
        //    }
        //    else if(_playerAudioSource._SoundEffectsPlaying.ContainsKey("running"))
        //    {
        //        _playerAudioSource.StopSound("running");
        //    }
        //    else
        //    {
        //        //TODO: ONLY PLAY WHEN MOVING??!
        //        _playerAudioSource.PlaySound("walking", _audioManager.GetSoundEffect("walking"));
        //    }
        //}


        /// <summary>
        /// Calculates the volume of the sound effect based on the energi.
        /// </summary>          
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
