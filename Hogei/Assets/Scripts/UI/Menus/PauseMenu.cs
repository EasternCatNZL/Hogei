using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject audioPanel;
    public GameObject rebindPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //turn all panels off
    public void TurnAllPanelsOff()
    {
        pausePanel.SetActive(false);
        audioPanel.SetActive(false);
        rebindPanel.SetActive(false);
    }

    //turn on panels
    public void TurnOnPause()
    {
        TurnAllPanelsOff();
        pausePanel.SetActive(true);
    }

    private void TurnOnAudio()
    {
        TurnAllPanelsOff();
        audioPanel.SetActive(true);
    }

    private void TurnOnRebind()
    {
        TurnAllPanelsOff();
        rebindPanel.SetActive(true);
    }

    //resume the game
    private void ResumeGame()
    {
        //turn off all panels
        TurnAllPanelsOff();
        //have pause handler call unpause
        //PauseHandler.UnpauseEvent();
    }
}
