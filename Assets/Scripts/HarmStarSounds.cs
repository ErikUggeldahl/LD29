using UnityEngine;
using System.Collections;

public class HarmStarSounds : MonoBehaviour
{
    const float SOUND_CHANCE = 0.005f;

    [SerializeField]
    AudioClip[] sounds;
	
    void Update()
    {
        if (Random.value < SOUND_CHANCE)
        {
            var rand = Random.Range(0, sounds.Length);
            audio.clip = sounds[rand];
            audio.Play();
        }
    }
}

