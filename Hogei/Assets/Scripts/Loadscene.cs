using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadscene : MonoBehaviour {

    public int LevelIndex;

    private void OnCollisionEnter(Collision collision)
    {
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene(LevelIndex);
    }
}
