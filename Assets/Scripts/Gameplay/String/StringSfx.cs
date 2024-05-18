using UnityEngine;

namespace Gameplay.String
{
    //воспроизводит звук струны (ноты)
    public class StringSfx : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        public void Play() => _audioSource.PlayOneShot(_audioClip);
    }
}