using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;
    private bool _isPaused = false;

    public void OnPlayButton() {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton() {
        Application.Quit();
    }

    public void OnToMenuButton() {
        PauseResume();
        SceneManager.LoadScene(0);
    }

    public void PauseResume() {
        if (_isPaused) {
            Time.timeScale = 1.0f;
            _pauseMenu.SetActive(false);
        } else {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
        }

        _isPaused = !_isPaused;
    }
}
