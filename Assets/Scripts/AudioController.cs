using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip heavyGunSfx, softGunSfx, sirenSfx, strikeSfx, engineSound; 

    public static AudioController instance;
    Dictionary<string, float> volumeDict = new Dictionary<string, float>();


    void Awake() {
        volumeDict.Add("AsterBig1", .75f);
        volumeDict.Add("AsterMed1", .5f);
        volumeDict.Add("AsterSmall1", .25f);
        volumeDict.Add("Enemy", .75f);
        instance = this;
    }

    public void ShootSfx(Transform t ) {
        AudioSource audioShootSource = Instantiate(audioSource, t.position, t.rotation);
        audioShootSource.clip = softGunSfx;
        audioShootSource.Play();
        Destroy(audioShootSource.gameObject, softGunSfx.length);
    }

    public void HeavyShootSfx(Transform t) {
        AudioSource audioHeavyShootSource = Instantiate(audioSource, t.position, t.rotation);
        audioHeavyShootSource.clip = heavyGunSfx;
        audioHeavyShootSource.Play();
        Destroy(audioHeavyShootSource.gameObject, heavyGunSfx.length);
    }

    public void SirenSfx(Transform t) {
        AudioSource audioSirenSource = Instantiate(audioSource, t.position, t.rotation);
        audioSirenSource.clip = sirenSfx;
        audioSirenSource.volume =.4f;
        audioSirenSource.Play();
        Destroy(audioSirenSource.gameObject, sirenSfx.length);
    }

    public void StrikeAsteroidSfx(string aster, Transform t) {
        AudioSource audioDtrikeSource = Instantiate(audioSource, t.position, t.rotation);
        audioDtrikeSource.clip = strikeSfx;
        audioDtrikeSource.volume = volumeDict[aster];
        audioDtrikeSource.Play();
        Destroy(audioDtrikeSource.gameObject, strikeSfx.length);
    }

    public void PlayEngineSound(Transform t) {
        AudioSource audioEngineSource = Instantiate(audioSource, t.position, t.rotation);
        audioEngineSource.clip = engineSound;
        audioEngineSource.loop = true;
        audioEngineSource.volume = 0.4f;
        audioEngineSource.Play();
    }

    public void PlayEngineAcSound(Transform t) {
        AudioSource audioEngineSource = Instantiate(audioSource, t.position, t.rotation);
        audioEngineSource.clip = engineSound;
        audioEngineSource.volume = 1f;
        audioEngineSource.Play();
        Destroy(audioEngineSource.gameObject, engineSound.length);
    }
}
