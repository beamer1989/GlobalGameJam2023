using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UGUITitleScreen : MonoBehaviour
{
    public Text loading;

    public void Start()
    {
        loading.enabled = false;
    }
    public void NewGameButtonPressed()
    {
        Debug.Log("New Game Button Pressed");
        loading.enabled = true;
        SceneManager.LoadScene(1); // hardcoded scene id for now
    }
    public void TutorialButtonPressed()
    {
        Debug.Log("Tutorial Button Pressed");
        loading.enabled = true;
        SceneManager.LoadScene(2); // hardcoded scene id for now
    }
    public void QuitButtonPressed()
    {
        Debug.Log("Quit Button Pressed, exiting game.");
        Application.Quit();
    }
}
