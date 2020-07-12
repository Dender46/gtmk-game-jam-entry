using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip[] gulpSounds = new AudioClip[3];
    static AudioSource audioSrc;

    private void Start() {
        audioSrc = GetComponent<AudioSource>();

        for (int i = 0; i < gulpSounds.Length; i++)
            gulpSounds[i] = Resources.Load<AudioClip>("gulpSound" + (i+1));
    }

    public static void PlaySound(string name) {
        switch(name) {
            case "gulp": audioSrc.PlayOneShot(gulpSounds[Random.Range(0, gulpSounds.Length)]); break;
        }
    }
}
