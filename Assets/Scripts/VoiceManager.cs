using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.IO;
using System.Text;
using System;

public class VoiceManager : MonoBehaviour
{
    private GrammarRecognizer grammarRec;
    private GrammarRecognizer grammarRecPas;
    private string _outAction = "";

    public static VoiceManager instance;

    public bool hasSaidPassword;
    public PlayerCombat playerCombat;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        playerCombat = FindObjectOfType<PlayerCombat>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Setup Singleton
        if (instance == null) instance = this;

        // Setup GrammarRecognizer
        grammarRecPas = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "PasswordGrammar.xml"), ConfidenceLevel.Low);
        Debug.Log("Grammar has Loaded!");
        grammarRecPas.OnPhraseRecognized += grammarRecPas_OnPhraseRecognized;
        grammarRecPas.Start();
        if (grammarRecPas.IsRunning) Debug.Log("GrammerRecognizer is Running!");

        // Setup GrammarRecognizer
        grammarRec = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "BasicGrammar.xml"), ConfidenceLevel.Low);
        Debug.Log("Password Grammar has Loaded!");
        grammarRec.OnPhraseRecognized += grammarRec_OnPhraseRecognized;
        grammarRec.Start();
        if (grammarRec.IsRunning) Debug.Log("GrammerRecognizerPassword is Running!");

        hasSaidPassword = false;
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
            switch (_outAction)
            {
                case "Attack":
                    Debug.Log("You said Attack!");
                    playerCombat.Attack();
                    break;
                case "Jump":
                    Debug.Log("You said Jump!");
                    playerMovement.Jump();
                    break;
                case "JumpAttack":
                    Debug.Log("[-----You said JumpAttack!-----]");
                    playerMovement.Jump();
                    playerCombat.Attack();
                    break;
                default:
                    break;
            }
        }
        Debug.Log(message);
    }

    private void grammarRecPas_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        var message = new StringBuilder();
        var meanings = args.semanticMeanings;
        // Loops through phrases and applies logic based on switch 
        foreach (var meaning in meanings) {
            var keyString = meaning.key.Trim();
            var valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Out Action: " + valueString + " \n");
            _outAction = valueString;
            // Checks for keywords setup in xml file out.action=""
            switch (_outAction)
            {
                case "Answer":
                    Debug.Log("You said Answer!");
                    break;
                case "Password":
                    Debug.Log("You said Password!");
                    break;
                case "ThePassword":
                    Debug.Log("[------You have said The Password!------]");
                    hasSaidPassword = true;
                    break;
                default:
                    break;
            }
        }
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasSaidPasword()
    {
        if (hasSaidPassword) return true;
        else return false;
    }

    public void resetPassword()
    {
        hasSaidPassword = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        hasSaidPassword = false;
    }

    private void OnApplicationQuit()
    {
        // Closes down the grammarRecognizer on Quitting application
        if (grammarRec != null && grammarRec.IsRunning)
        {
            grammarRec.OnPhraseRecognized -= grammarRec_OnPhraseRecognized;
            grammarRec.Stop();
        }
        // Closes down the grammarRecognizer on Quitting application
        if (grammarRecPas != null && grammarRecPas.IsRunning)
        {
            grammarRecPas.OnPhraseRecognized -= grammarRecPas_OnPhraseRecognized;
            grammarRecPas.Stop();
        }
    }
}
