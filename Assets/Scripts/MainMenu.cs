using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int scene = 1;
    
   public void PlayGame() {
        print(scene);
        int sceneToContinue = PlayerPrefs.GetInt("scene");
        print(sceneToContinue);
        if (sceneToContinue != 0 && sceneToContinue != 9)
            scene = sceneToContinue;
        else
            PlayerPrefs.SetInt("scene", scene);
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
