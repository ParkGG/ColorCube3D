using UnityEngine;
using GoogleMobileAds.Api;
using System;


public class AdMobInterstitialAd : SingleTon<AdMobInterstitialAd>
{
    private InterstitialAd interstitial;

    private string DeviceID = "2fe5fdfff1afb8143d0cdade05021152";

    public void Start()
    {
        this.RequestInterstitial();

        DontDestroyOnLoad(this);
    }


    private void RequestInterstitial()
    {
        string testadUnitId = "ca-app-pub-3940256099942544/1033173712";
        string adUnitId = "ca-app-pub-7029312326194885/9622525763";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdLoaded += HandleOnAdLoaded;
        interstitial.OnAdClosed += HandleOnAdClosed;
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice(DeviceID).Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);



    }

    public void Show()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        this.RequestInterstitial();


    }
   

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

   

    private void OnApplicationQuit()
    {
        interstitial.Destroy();
    }

}
