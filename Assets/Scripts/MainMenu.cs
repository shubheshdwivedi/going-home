using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int scene = 1;
    
   public void PlayGame() {
        int sceneToContinue = PlayerPrefs.GetInt("scene");
        if (sceneToContinue != 0 && sceneToContinue != 9)
            scene = sceneToContinue;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame() {
        print("Quiting now!");
        Application.Quit();
    }

    public void LoadScene(int scene) {
        SceneManager.LoadScene(scene);
    }
}
