using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI text;
    public GameObject passwordText;
    public char[] letters;
    private char[] displayLetters = {'X', 'X', 'X', 'X'};
    int score;
    int passwordCounter;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text.text = score.ToString();
        if (instance == null) instance = this;
        UpdateText();
    }

    public void UpdateScore(int value)
    {
        score += value;
        text.text = score.ToString();
    }

    public void UpdatePassword(char letter, int index)
    {
        displayLetters[index] = letter;
        UpdateText();
    }

    void UpdateText()
    {
        passwordCounter = 0;
        foreach (TextMeshProUGUI text in passwordText.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.text = displayLetters[passwordCounter++].ToString();
        }
    }

    public string getPassword()
    {
        string word = "";

        foreach (char let in letters)
        {
            word += let.ToString();
        }
        return word;
    }

    public bool hasPassword()
    {
        passwordCounter = 0;
        for (int i=0; i< displayLetters.Length; i++)
        {
            if (displayLetters[i] == letters[i])
            {
                passwordCounter++;
            }
        }
        if (passwordCounter == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
