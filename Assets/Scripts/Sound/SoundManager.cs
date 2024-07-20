using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public List<AudioClip> clips = new();
        public static SoundManager Instance { get; private set; }

        private bool _isPlaying;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound(string clipName)
        {
            var clip = clips.Find(c => c.name == clipName);
            if (UnityEngine.Camera.main == null) return;

            if (_isPlaying) return;
            AudioSource.PlayClipAtPoint(clip, UnityEngine.Camera.main.transform.position);
            _isPlaying = true;

            StartCoroutine(ResetIsPlaying(clip.length));
        }

        private IEnumerator ResetIsPlaying(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isPlaying = false;
        }

        public bool IsPlaying()
        {
            return _isPlaying;
        }
    }
}