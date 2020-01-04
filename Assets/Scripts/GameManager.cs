using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : SingleTon<GameManager>
{
    [Header("Color Settings")]
    public int useColor;


    [Header("Pool Setting")]
    [Range(0, 5)]
    public float poolingTime;


    public GameObject gameoverScreen;


    private void Start()
    {
        gameoverScreen.SetActive(false);

        StartCoroutine("Pooling");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            GameOver(true);
        }
    }


 
    IEnumerator Pooling()
    {
        while (true)
        {
            int ID = Random.Range(0, useColor);
            PoolManager.Instance.PopFromPool(Random.Range(0, useColor), PoolManager.Instance.parents[ID]).SetActive(true);

            yield return new WaitForSeconds(poolingTime);
        }
    }


    public void Difficulty()
    {
        if (poolingTime > 0.1)   poolingTime *= 0.9f;
        if (useColor < 6)        ++useColor;
    }

    public void GameOver(bool axisOutofRange)
    {
        if (axisOutofRange)
        {
            Time.timeScale = 0f;

            PlayerData.Instance.SaveData("HighScore", ScoreManager.Instance.HighScore);


            gameoverScreen.SetActive(true);

            if (PlayerData.Instance.UsingExtraLife)
                gameoverScreen.transform.Find("ExtraLife").gameObject.SetActive(false);
        }
    }


   

}
	
	
