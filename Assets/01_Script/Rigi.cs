using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rigi : MonoBehaviour
{
    private void Start()
    {
        int level;
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("TotalLevelText", 1);
            level = 1;
        }
        SceneManager.LoadScene(level);
        print(level);

    }
}