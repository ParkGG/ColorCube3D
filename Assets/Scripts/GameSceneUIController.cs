using UnityEngine;
using UnityEngine.UI;


public class GameSceneUIController : MonoBehaviour
{
    public Text scoreText;

    int _fontSizeMin = 80;
    int _fontSizeMax = 120;

    int targetValue = 0;
    int FollowerValue = 0;



    void Update()
    {
        ScoreCounting();
    }


    void ScoreCounting()
    {
        int Rate = 7;


        if (targetValue != ScoreManager.Instance.CurrentScore)
        {
            targetValue = ScoreManager.Instance.CurrentScore;
            scoreText.fontSize = _fontSizeMax;
        }

        if (scoreText.fontSize > _fontSizeMin)
        {
            scoreText.fontSize -= 1;
        }

        int diff = (int)targetValue - FollowerValue;
        int debugValue = UnityEngine.Mathf.CeilToInt(diff / Rate);           


        if (1 <= debugValue)
            FollowerValue += debugValue;
        else if (FollowerValue < targetValue)
            FollowerValue += 1;

        scoreText.text = FollowerValue.ToString();

    }


    public void ExtraLife()
    {
        PlayerData.Instance.UsingExtraLife = true;

        SceneController.Instance.LoadScene("GameScene");

        AdMobRewardedAd.Instance.extralifeAdShow();

    }

    public void ReturnMain()
    {
        SceneController.Instance.LoadScene("MainScene");

        AdMobInterstitialAd.Instance.Show();

    }

}
