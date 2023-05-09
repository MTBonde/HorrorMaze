
namespace HorrorMaze
{
    public class EnemyAudioController : Component
    {
        private AudioSource enemyAudioSource;
        private PlayerAudioListener playerAudioListener;
        private AudioManager audioManager;

        public EnemyAudioController()
        {
            
        }
        public void Setup(AudioSource enemyAudioSource, PlayerAudioListener playerAudioListener, AudioManager audioManager)
        {
            this.enemyAudioSource = enemyAudioSource;
            this.playerAudioListener = playerAudioListener;
            this.audioManager = audioManager;
        }

        public void Update()
        {
            float distance = Vector3.Distance(playerAudioListener.Listener.Position, enemyAudioSource.Emitter.Position);

            if(distance <= 10f && enemyAudioSource.SoundEffectInstance == null)
            {
                enemyAudioSource.PlaySound(audioManager.GetSoundEffect("heartbeat"));
            }
            else if(distance <= 5f && enemyAudioSource.SoundEffectInstance == null)
            {
                enemyAudioSource.PlaySound(audioManager.GetSoundEffect("breathing"));
            }
            else if(distance > 10f && enemyAudioSource.SoundEffectInstance != null)
            {
                enemyAudioSource.SoundEffectInstance.Stop();
                enemyAudioSource.SoundEffectInstance.Dispose();
                enemyAudioSource.SoundEffectInstance = null;
            }
        }
    }
}
