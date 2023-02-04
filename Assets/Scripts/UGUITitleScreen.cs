using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UGUITitleScreen : MonoBehaviour
{
    public void NewGameButtonPressed()
    {
        Debug.Log("New Game Button Pressed");
        SceneManager.LoadScene(1); // hardcoded scene id for now
    }
    public void QuitButtonPressed()
    {
        Debug.Log("Quit Button Pressed, exiting game.");
        Application.Quit();
    }
}
