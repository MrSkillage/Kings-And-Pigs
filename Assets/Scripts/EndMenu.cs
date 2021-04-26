using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public GameObject MainHUD;
    public GameObject EndGameHUD;

    public void DisplayEndGameHUD() {
        MainHUD.SetActive(false);
        EndGameHUD.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
