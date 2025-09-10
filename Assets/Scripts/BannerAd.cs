using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAd : MonoBehaviour
{
    public string unitID = "ca-app-pub-3940256099942544/6300978111";
    public string test_unitID = "ca-app-pub-3940256099942544/6300978111";

    private BannerView banner;
    public AdPosition position;
    public void Start(){
        InitAd();
    }
    private void InitAd(){
        string id = Debug.isDebugBuild ? test_unitID : unitID;
        banner = new BannerView(id, AdSize.SmartBanner, position);
        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);
    }
    public void ToggleAd(bool active){
        if(active){
            banner.Show();
        }
        else{
            banner.Hide();
        }
    }
    public void DestroyAd(){
        banner.Destroy();
    }
}