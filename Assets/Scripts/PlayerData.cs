using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class PlayerData : SingleTon<PlayerData>
{
    public int highScore;

    public int starCoin;

    private bool usingExtralife = false;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
            { SaveData("HighScore", 0); }
        else
            { highScore = GetData("HighScore"); }


        if (!PlayerPrefs.HasKey("StarCoin"))
            { SaveData("StarCoin", 0); }
        else
            { starCoin = GetData("StarCoin"); }


        SceneController.Instance.LoadScene("MainScene");

        DontDestroyOnLoad(this);

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();

    }

    public int GetData(string key)
    {
        int _dataIn = PlayerPrefs.GetInt(key);
        return _dataIn;
    }


    public bool UsingExtraLife
    {
        get { return usingExtralife; }
        set { usingExtralife = value; }
    }

    public int StarCoin
    {
        get { return starCoin; }
        set {
            starCoin = value;
            SaveData("StarCoin", starCoin);
        }
    }


    private void OnApplicationQuit()
    {
        Resources.UnloadUnusedAssets();
        Application.Quit();
    }

}
