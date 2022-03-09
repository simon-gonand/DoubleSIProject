using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTravel : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("DeckBuilding");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
