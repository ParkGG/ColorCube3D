using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class AdMobRewardedAd : SingleTon<AdMobRewardedAd>
{
    private RewardedAd starcoindAd;
    private RewardedAd extralifeAd;

    private string DeviceID = "2fe5fdfff1afb8143d0cdade05021152";
    

    private void Start()
    {
        this.starcoindAd = RequestReward();
        this.extralifeAd = RequestReward();

        DontDestroyOnLoad(this);
    }


    private RewardedAd RequestReward()
    {

        string testadUnitId = "ca-app-pub-3940256099942544/5224354917";
        string adUnitId = "ca-app-pub-7029312326194885/9239382383";

        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        AdRequest request = new AdRequest.Builder().AddTestDevice(DeviceID).Build();
        rewardedAd.LoadAd(request);

        return rewardedAd;


    }

    public void starcoindAdShow()
    {
        if (this.starcoindAd.IsLoaded())
        {
            this.starcoindAd.Show();
        }
    }

    public void extralifeAdShow()
    {
        if (this.extralifeAd.IsLoaded())
        {
            this.extralifeAd.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        if (!this.starcoindAd.IsLoaded())
            this.starcoindAd = RequestReward();
        if (!this.extralifeAd.IsLoaded())
            this.extralifeAd = RequestReward();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        PlayerData.Instance.StarCoin += 20;


    }
 
}
