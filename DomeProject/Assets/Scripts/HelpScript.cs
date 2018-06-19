using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpScript : MonoBehaviour
{
    public string opening;
    public string objective;
    public string farmingHelp;
    public string waterHelp;
    public string treeHelp;
    public Crosstales.RTVoice.Tool.SpeechText myST;
    public AudioSource myAS;


    private void Start()
    {
        myST.Source = myAS;
    }
    public void Speaker(int index)
    {
        if (index == 1)
        {
            myST.Text = opening;

        }
        else if (index == 2)
        {
            myST.Text = objective;
        }
        else if (index == 3)
        {
            myST.Text = farmingHelp;
        }
        else if (index == 4)
        {
            myST.Text = waterHelp;
        }
        else if (index == 5)
        {
            myST.Text = treeHelp;
        }



        myST.Speak();
    }
}
