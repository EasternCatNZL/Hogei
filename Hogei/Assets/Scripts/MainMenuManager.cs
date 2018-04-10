using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void LoadLevel(int _levelIndex)
    {
        SceneManager.LoadScene(_levelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
