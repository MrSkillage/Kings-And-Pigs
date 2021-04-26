using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using UnityEngine.Windows.Speech;
using System.Text;
using System;

public class MainMenu : MonoBehaviour
{
    private GrammarRecognizer grammarRec;
    private string _outAction = "";

    public GameObject mainMenu;
    public GameObject optionsMenu;

    private void Start()
    {
        // Setup GrammarRecognizer set state to START and run coroutine SetupBattle
        grammarRec = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "MenuGrammar.xml"), ConfidenceLevel.Low);
        Debug.Log("Grammer has loaded!");
        grammarRec.OnPhraseRecognized += grammarRec_OnPhraseRecognized;
        grammarRec.Start();
        if (grammarRec.IsRunning) Debug.Log("GrammerRecognizer is Running!");
    }

    private void grammarRec_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        var message = new StringBuilder();
        var meanings = args.semanticMeanings;
        // Loops through phrases and applies logic based on switch 
        foreach (var meaning in meanings) {
            var keyString = meaning.key.Trim();
            var valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Out Action: " + valueString + " \n");
            _outAction = valueString;
            // Checks for keywords setup in xml file out.action=""
            switch (_outAction) {
                case "Start":
                    PlayGame();
                    break;
                case "Options":
                    Options();
                    break;
                case "Quit":
                    QuitGame();
                    break;
                case "Back":
                    Back();
                    break;
                default:
                    break;
            }
        }
        Debug.Log(message);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("You have Quit!");
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // Closes down the grammarRecognizer on Quitting application
        if (grammarRec != null && grammarRec.IsRunning)
        {
            grammarRec.OnPhraseRecognized -= grammarRec_OnPhraseRecognized;
            grammarRec.Stop();
        }
    }
}
