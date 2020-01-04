using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingleTon<ScoreManager> {

    int highScore = 0;
    int currentScore = 0;
    int addScore = 10;    //추가할 점수
    

    List<int> difficultyList = new List<int>
        { 500, 1000, 2000, 4000, 8000, 16000 };


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Add(int addCount)
    {
        currentScore += ((addCount* addCount) * addScore );

        if (difficultyList.Count > 0 && currentScore >= difficultyList[0])
        {
            Difficulty();
        }
    }

    void Difficulty()
    {
        GameManager.Instance.Difficulty();
        difficultyList.RemoveAt(0);
    }


    public int HighScore
    {
        get { return highScore; }
        set
        {
            if (value > highScore)
                highScore = value;
        }
    }

    public int CurrentScore
    {
        get { return currentScore; }
    }


}
