using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ButtonType
{
    start,instructions,exit
}

public class BasicButtonScript : MonoBehaviour {
    public ButtonType buttonType;

    public Dictionary<ButtonType, string> buttonLabels = new Dictionary<ButtonType, string>()
    {
        { ButtonType.start, "Start Button" },
        { ButtonType.exit, "EXIT" },
        { ButtonType.instructions, "How to Play" }
    };

	// Use this for initialization
	void Start () {
        setButton(buttonType);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setButton(ButtonType type)
    {
        Button myButton = this.transform.GetChild(0).GetComponent<Button>();
        myButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = buttonLabels[type];
        
        // these don't show up in the inspector, even when the game is running, but the work, trust me.
        switch(type)
        {
            case ButtonType.exit:
                myButton.onClick.AddListener(() => { Debug.Log("Qutting Game."); Application.Quit(); });
                break;
            case ButtonType.start:
                myButton.onClick.AddListener(() => { Debug.Log("Entering Game Scene."); SceneManager.LoadScene("GameScreen"); });
                break;
            case ButtonType.instructions:
                myButton.onClick.AddListener(() => { Debug.Log("Entering Instructions Scene."); SceneManager.LoadScene("InstructionsScreen"); });
                break;
        }
    }
}
