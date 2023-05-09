

using Microsoft.Xna.Framework.Audio;

namespace HorrorMaze
{
    public class AudioManager
    {
        
        private Dictionary<string, SoundEffect> _soundEffects;
        private List<AudioComponent> _audioComponents;

        public AudioManager()
        {
            
            _soundEffects = new Dictionary<string, SoundEffect>();
            _audioComponents = new List<AudioComponent>();
        }

        public void LoadSoundEffect(string soundEffectName, string path)
        {
            if(!_soundEffects.ContainsKey(soundEffectName))
            {
                SoundEffect soundEffect = GameWorld.Instance.Content.Load<SoundEffect>(path);
                _soundEffects.Add(soundEffectName, soundEffect);
            }
        }

        public SoundEffect GetSoundEffect(string soundEffectName)
        {
            if(_soundEffects.TryGetValue(soundEffectName, out SoundEffect soundEffect))
            {
                return soundEffect;
            }

            return null;
        }

        public void AddAudioComponent(AudioComponent audioComponent)
        {
            _audioComponents.Add(audioComponent);
        }

        public void RemoveAudioComponent(AudioComponent audioComponent)
        {
            _audioComponents.Remove(audioComponent);
        }

        public void Update()
        {
            foreach(AudioComponent audioComponent in _audioComponents)
            {
                audioComponent.Update();
            }
        }
    }
}
