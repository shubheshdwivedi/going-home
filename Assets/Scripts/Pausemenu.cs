using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    private static bool _isGamePasued = false;
    [SerializeField] private GameObject pauseMenuUi;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (_isGamePasued) {
                Resume();
            }
            else {
                Pause();
            }
        }

    }

    public void Pause() {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        _isGamePasued = true;
    }

    public void Resume() {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        _isGamePasued = false;
    }

    public void MainMenu() {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }
}
