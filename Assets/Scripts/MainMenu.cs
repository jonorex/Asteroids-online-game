using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    public AudioSource audioSource;

    public AudioClip menuMusic, btnSound;

    public Button playButton;


    public Text record;

    public static MainMenu Instance;


    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        TakeMenuSong();
        LoadRecord();
    }

    public void PlayGame() {
        ClientTCP.instance.ReadyMessage();
        
    }

    public void LoadRecord() {
        record.text = PlayerPrefs.GetInt("score").ToString();
    }


    public void TakeMenuSong() {
        AudioSource menuAudioSource = Instantiate(audioSource);
        menuAudioSource.clip = menuMusic;
        menuAudioSource.volume = 0.5f;
        menuAudioSource.loop = true;
        menuAudioSource.Play();
    }

    public void TakeBtnSound() {
        AudioSource btnAudioSource = Instantiate(audioSource);
        btnAudioSource.clip = btnSound;
        btnAudioSource.Play();
        Destroy(btnAudioSource.gameObject, btnSound.length);
    }
    
}
