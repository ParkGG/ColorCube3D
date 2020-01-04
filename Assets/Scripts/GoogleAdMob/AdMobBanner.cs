using UnityEngine;
using GoogleMobileAds.Api;
using System;


public class AdMobBanner : SingleTon<AdMobBanner>
{

    private BannerView banner;

    private string DeviceID = "2fe5fdfff1afb8143d0cdade05021152";

    public void Awake()
    {
        MobileAds.Initialize( 
            (initStatus) => {
             
            }
            );

        this.RequestBanner();

        //Debug.Log("Device Id : " + SystemInfo.deviceUniqueIdentifier);
        DontDestroyOnLoad(this);
    }


    private void RequestBanner()
    {
        string testadUnitId = "ca-app-pub-3940256099942544/6300978111";
        string adUnitId = "ca-app-pub-7029312326194885/9354745100";

        banner = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        banner.OnAdLoaded += HandleOnAdLoaded;
        banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        

        AdRequest request = new AdRequest.Builder().AddTestDevice(DeviceID).Build();

        banner.LoadAd(request);


    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        banner.Show();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }


    private void OnApplicationQuit()
    {
        banner.Destroy();
    }

}
