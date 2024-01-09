using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColor : MonoBehaviour
{
    public GameObject player;
    string pickedColor;
    private void Start()
    {
        pickedColor = PlayerPrefs.GetString("ColorPref");

        if (pickedColor == "blue")
        {
            player.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (pickedColor == "purple")
        {
            player.GetComponent<Renderer>().material.color = Color.magenta;
        }
        if (pickedColor == "red")
        {
            player.GetComponent<Renderer>().material.color = Color.red;
        }
        if (pickedColor == "yellow")
        {
            player.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
}