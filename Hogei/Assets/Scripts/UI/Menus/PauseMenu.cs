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
    private void TurnAllPanelsOff()
    {
        pausePanel.SetActive(false);
        audioPanel.SetActive(false);
        rebindPanel.SetActive(false);
    }

    //turn on panels
    private void TurnOnPause()
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
}
