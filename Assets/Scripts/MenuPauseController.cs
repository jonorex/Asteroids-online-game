using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPauseController : MonoBehaviour
{

    public GameObject pausePainel;

    public AudioSource audioSource;

    public AudioClip btnSound;

    public void ResumeGame() {
        TakeBtnSound();
        Time.timeScale = 1;
        pausePainel.SetActive(false);
    }

    public void PauseGame() {
        TakeBtnSound();
        Time.timeScale = 0;
        pausePainel.SetActive(true); 
    }

    public void RestartGame() {
        TakeBtnSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        TakeBtnSound();
        SceneManager.LoadSceneAsync(0);
    }

    public void TakeBtnSound() {
        AudioSource btnAudioSource = Instantiate(audioSource);
        btnAudioSource.clip = btnSound;
        btnAudioSource.Play();
        Destroy(btnAudioSource.gameObject, btnSound.length);
    }
}
