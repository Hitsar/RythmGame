using UnityEngine;
using UnityEngine.Audio;

namespace UI.MainMenu
{
    //меняет громкость AudioMixer в зависимости от Slider
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        
        public void Change(float value) => _audioMixer.SetFloat("Master", value);
    }
}