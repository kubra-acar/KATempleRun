using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSelection : MonoBehaviour
{
    public void ColorBlue()
    {
        PlayerPrefs.SetString("ColorPref", "blue");
        SceneManager.LoadScene("SampleScene");
    }
    public void ColorPurple()
    {
        PlayerPrefs.SetString("ColorPref", "purple");
        SceneManager.LoadScene("SampleScene");
    }
    public void ColorRed()
    {
        PlayerPrefs.SetString("ColorPref", "red");
        SceneManager.LoadScene("SampleScene");
    }
    public void ColorYellow()
    {
        PlayerPrefs.SetString("ColorPref", "yellow");
        SceneManager.LoadScene("SampleScene");
    }
}