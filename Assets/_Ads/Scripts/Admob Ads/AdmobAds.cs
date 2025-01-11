using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmobAds : Ads
{
    [SerializeField] AdsDataSO _data;

    private BannerView AdBanner;
    private InterstitialAd AdInterstitial;
    private RewardedAd AdReward;

    private bool _isInitComplete = false;

    private void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TagForChildDirectedTreatment = TagForChildDirectedTreatment.Unspecified
        };

        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus =>
        {
            _isInitComplete = true;
            RequestAndShowBanner();
            RequestInterstitialAd();
        });
    }

    private void OnDestroy()
    {
        DestroyBannerAd();
        DestroyInterstitialAd();
    }

    public override bool IsRewardedAdLoaded()
    {
        return AdReward != null;
    }

    AdRequest CreateAdRequest()
    {
        return new AdRequest
        {
            Extras = { { "npa", PlayerPrefs.GetInt("npa", 1).ToString() } }
        };
    }

    #region Banner Ad
    public override void ShowBanner()
    {
        if (!_isInitComplete) return;

        AdBanner?.Show();
    }

    public void RequestAndShowBanner()
    {
        if (!_data.BannerEnabled) return;

        AdSize adaptiveSize = AdSize.GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        AdBanner = new BannerView(_data.BannerID, adaptiveSize, AdPosition.Bottom);
        AdBanner.LoadAd(CreateAdRequest());
    }

    public void DestroyBannerAd()
    {
        AdBanner?.Destroy();
    }
    #endregion

    #region Interstitial Ad
    private void RequestInterstitialAd()
    {
        if (!_data.InterstitialEnabled) return;

        InterstitialAd.Load(_data.InterstitialID, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError($"Failed to load interstitial ad: {error}");
                    return;
                }

                AdInterstitial = ad;
                AdInterstitial.OnAdFullScreenContentClosed += HandleInterstitialAdClosed;
            });
    }

    public override void ShowInterstitial()
    {
        AdInterstitial?.Show();
    }

    private void DestroyInterstitialAd()
    {
        AdInterstitial?.Destroy();
        AdInterstitial = null;
    }
    #endregion

    #region Rewarded Ad
    private void RequestRewardAd()
    {
        if (!_data.RewardedEnabled) return;

        RewardedAd.Load(_data.RewardedID, CreateAdRequest(),
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError($"Failed to load rewarded ad: {error}");
                    return;
                }

                AdReward = ad;
                AdReward.OnAdFullScreenContentClosed += HandleOnRewardedAdClosed;
                AdReward.Show(reward =>
                {
                    AdsManager.HandleRewardedAdWatchedComplete();
                });
            });
    }

    public override void ShowRewarded()
    {
        if (AdReward != null && AdReward.CanShowAd())
        {
            AdReward.Show(reward =>
            {
                AdsManager.HandleRewardedAdWatchedComplete();
            });
        }
    }
    #endregion

    #region Event Handlers
    private void HandleInterstitialAdClosed()
    {
        DestroyInterstitialAd();
        RequestInterstitialAd();
    }

    private void HandleOnRewardedAdClosed()
    {
        RequestRewardAd();
    }

    private void HandleOnRewardedAdWatched(object sender, Reward e)
    {
        AdsManager.HandleRewardedAdWatchedComplete();
    }
    #endregion
}