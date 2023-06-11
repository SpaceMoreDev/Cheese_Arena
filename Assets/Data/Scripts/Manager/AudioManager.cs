using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> sounds = new List<AudioClip>();
        private static AudioManager current;
        private static AudioSource audioSource;

        void Awake()
        {
            current = this;
            audioSource = GetComponent<AudioSource>();
        }

        public static void StopAudio()
        {
            audioSource.Stop();
        }

        public static void PlayAudio(string audioName)
        {
            foreach(AudioClip audio in current.sounds)
            {
                if(audio.name == audioName)
                {
                    audioSource.PlayOneShot(audio);
                    return;
                }
            }
            Debug.LogError("Audio Clip doesn't exist in the list >w<");
        }

        public static void PlayAudio(int audioName)
        {

            audioSource.PlayOneShot(current.sounds[audioName]);
            return;
            
            Debug.LogError("Audio Clip doesn't exist in the list >w<");
        }
    }
}
