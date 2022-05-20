﻿using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour , IUnityAdsInitializationListener , IUnityAdsShowListener , IUnityAdsLoadListener
{
    public static AdsManager instance;
   
    public Text testTxt;
    [SerializeField] private bool initializeOnStart = true; 
    public enum AdType
    {
        BANNER,
        INTERSTITIAL,
        VIDEOINTERSTITIAL,
        REWARDED,
        NATIVE
    };

    internal bool isRewardedAdAvailable()
    {
        return true;
    }

    public enum RewardType
    {
        FREECOINS,
        DOUBLEREWARD,
        SKIPLEVEL
    };
    private RewardType rewardType = RewardType.FREECOINS;
    private int coinsToReward = 0;

    private bool admob_isInitialized = false;
    private bool unity_isInitialized = false;

    private BannerView admob_bannerView;
    private InterstitialAd admob_interstitialAd;
    private RewardedAd admob_rewardedAd;

    private UnityEvent OnAdLoadedEvent;
    private UnityEvent OnAdFailedToLoadEvent;
    private UnityEvent OnAdOpeningEvent;
    private UnityEvent OnAdFailedToShowEvent;
    private UnityEvent OnAdClosedEvent;

    public int CoinsToReward { get => coinsToReward; set => coinsToReward = value; }

    public bool IsRewardedAdAvailable()
    {
        return true;
    }

    public RewardType RewardType1 { get => rewardType; set => rewardType = value; }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        if (initializeOnStart)
            Initialize();

    }

    void Initialize()
    {

        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(Admob_HandleInitCompleteAction);


        //Untiy Ads
        Advertisement.Initialize(Constants.unityId_Appkey, false,this);
    }


    void Log(string _str)
    {

        //Toolbox.GameManager.Log("Ads=" + _str);
        Debug.Log("Ads=" + _str);

        if (testTxt)
        {
            testTxt.text = _str;
        }
    }

    public void HideBannerAd()
    {
        Log("Hiding banner");
        Admob_DestroyBannerAd();
    }

    public void RequestBannerWithSpecs()
    {
        ShowAd(AdType.BANNER);
    }

    public void SetNShowRewardedAd(RewardType _type, int _coins)
    {
        rewardType = _type;
        coinsToReward = _coins;

        ShowAd(AdType.REWARDED);
    }
    public void RequestAd(AdType _type)
    {



        Log("Request Ad. Type = " + _type);

        switch (_type)
        {
            case AdType.BANNER:

                if (!Toolbox.DB.prefs.NoAdsPurchased)
                {
                    Admob_RequestBannerAd();
                }

                break;

            case AdType.INTERSTITIAL:

                if (!Toolbox.DB.prefs.NoAdsPurchased)
                {
                    Log("Requesting Interstitial");
                    Admob_RequestAndLoadInterstitialAd();
                    Unity_LoadIAd();
                }

                break;


            case AdType.REWARDED:

                Admob_RequestAndLoadRewardedAd();
                Unity_LoadRAd();

                break;
        }

    }

    public void ShowAd(AdType _type)
    {



        if (!admob_isInitialized /*|| !Toolbox.GameManager.IsNetworkAvailable()*/)
        {
            Log("Not Initialized");
            return;
        }


        Log("Trying to Show = " + _type);

        switch (_type)
        {

            case AdType.BANNER:
                //if(admob_bannerView != null)
                //{
                //    RequestAd(AdType.BANNER);
                //}
                //else
                //{
                //    
                //}
                admob_bannerView.Show();

                break;

            case AdType.INTERSTITIAL:

                //if (Toolbox.DB.prefs.NoAdsPurchased)
                //    return;
                #region Acctual Showing the IAD

                if (admob_interstitialAd.IsLoaded())
                    Admob_ShowInterstitialAd();
                else
                    Unity_ShowIAd();

                #endregion
                break;

            case AdType.VIDEOINTERSTITIAL:

                break;

            case AdType.REWARDED:

                if (admob_rewardedAd.IsLoaded())
                    Admob_ShowRewardedAd();
                else
                    Unity_ShowRAd();

                break;
        }
    }


    #region CallBacks

    void RewardPlayer()
    {
     //   Toolbox.DB.prefs.GoldCoins += coinsToReward;

        switch (rewardType)
        {

            case RewardType.FREECOINS:

                //Toolbox.GameManager.InstantiatePopup_Message(coinsToReward + " coins awarded.");

                break;

            case RewardType.DOUBLEREWARD:

                //Toolbox.GameManager.InstantiatePopup_Message(coinsToReward + "x2 coins awarded.");

                break;

            case RewardType.SKIPLEVEL:

              //  FindObjectOfType<LevelFailListner>().UnlockAndPlayNextLevel();

                break;
        }
    }

    #endregion

    #region ADMOB

    private void Admob_HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Log("Initialization complete");
            admob_isInitialized = true;
            Admob_RequestBannerAd();
            Admob_RequestAndLoadInterstitialAd();
            Admob_RequestAndLoadRewardedAd();
        });

        if (!admob_isInitialized)
        {
            Log("Initialization not completed");
        }
    }

    private AdRequest Admob_CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #region BANNER ADS

    public void Admob_RequestBannerAd()
    {
        if (!admob_isInitialized)
        {
            Log("Not Initialized");
            return;
        }

        Log("Requesting Banner Ad.");


        if (admob_bannerView != null)
        {
            admob_bannerView.Destroy();
        }


        admob_bannerView = new BannerView(Constants.admobId_Banner, AdSize.Banner, AdPosition.Top);

        // Add Event Handlers
        //admob_bannerView.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        //admob_bannerView.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        //admob_bannerView.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        //admob_bannerView.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();

        // Load a banner ad
        admob_bannerView.LoadAd(Admob_CreateAdRequest());
    }

    public void Admob_DestroyBannerAd()
    {
        if (admob_bannerView != null)
        {
            admob_bannerView.Hide();
        }
    }

    #endregion

    #region INTERSTITIAL ADS

    public void Admob_RequestAndLoadInterstitialAd()
    {
        if (!admob_isInitialized)
        {
            Log("Not Initialized");
            return;
        }

        if (admob_interstitialAd != null && admob_interstitialAd.IsLoaded())
            return;

        Log("Requesting Interstitial Ad.");

        // Clean up interstitial before using it
        if (admob_interstitialAd != null)
        {
            admob_interstitialAd.Destroy();
        }
        admob_interstitialAd = new InterstitialAd(Constants.admobId_Interstitial);

        // Add Event Handlers
        //admob_interstitialAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        //admob_interstitialAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        //admob_interstitialAd.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        //admob_interstitialAd.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();

        // Load an interstitial ad
        admob_interstitialAd.LoadAd(Admob_CreateAdRequest());
    }

    public void Admob_ShowInterstitialAd()
    {
        if (!admob_isInitialized)
        {
            Log("Not Initialized");
            return;
        }

        if (admob_interstitialAd != null && admob_interstitialAd.IsLoaded())
        {
            admob_interstitialAd.Show();
        }
        else
        {
            Log("Interstitial ad is not ready yet");
        }
    }

    public void Admob_DestroyInterstitialAd()
    {

        if (admob_interstitialAd != null)
        {
            admob_interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void Admob_RequestAndLoadRewardedAd()
    {
        if (!admob_isInitialized)
        {
            Log("Not Initialized");
            return;
        }

        if (admob_rewardedAd != null && admob_rewardedAd.IsLoaded())
            return;

        Log("Requesting Rewarded Ad.");

        // create new rewarded ad instance
        admob_rewardedAd = new RewardedAd(Constants.admobId_RewardedVid);

        // Add Event Handlers
        //admob_rewardedAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        //admob_rewardedAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        //admob_rewardedAd.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        //admob_rewardedAd.OnAdFailedToShow += (sender, args) => OnAdFailedToShowEvent.Invoke();
        //admob_rewardedAd.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
        admob_rewardedAd.OnUserEarnedReward += (sender, args) => RewardPlayer();

        // Create empty ad request
        admob_rewardedAd.LoadAd(Admob_CreateAdRequest());
    }

    public void Admob_ShowRewardedAd()
    {
        if (!admob_isInitialized)
        {
            Log("Not Initialized");
            return;
        }

        if (admob_rewardedAd != null)
        {
            admob_rewardedAd.Show();
        }
        else
        {
            Log("Rewarded ad is not ready yet.");
        }
    }

    #endregion

    #endregion

    #region UnityAds

    public void OnInitializationComplete()
    {
        Log("Unity Ads initialization complete.");
        unity_isInitialized = true;

        Unity_LoadIAd();
        Unity_LoadRAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        unity_isInitialized = false;
    }

    public void Unity_LoadIAd()
    {
        if (!unity_isInitialized)
            return;

        Debug.Log("Loading UNITY IAd: ");
        Advertisement.Load(Constants.unityId_IADkey,this);
    }

    public void Unity_ShowIAd()
    {
        if (!unity_isInitialized)
            return;

        Debug.Log("Showing Ad: UNITY IAd");
        Advertisement.Show(Constants.unityId_IADkey,this);
    }

    public void Unity_LoadRAd()
    {
        if (!unity_isInitialized)
            return;

        Debug.Log("Loading Ad: Unity RAd");
        Advertisement.Load(Constants.unityId_RADkey,this);
    }

    public void Unity_ShowRAd()
    {
        if (!unity_isInitialized)
            return;

        Debug.Log("Showing Ad: UNITY RAd");
        Advertisement.Show(Constants.unityId_RADkey,this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(Constants.unityId_RADkey) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            RewardPlayer();

            Unity_LoadRAd();
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsAdLoaded(string _adUnitId)
    {
        Debug.Log("Ad Loaded: " + _adUnitId);

        if (_adUnitId.Equals(Constants.unityId_RADkey))
        {
            // Configure the button to call the ShowAd() method when clicked:
            //  _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            // _showAdButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");

        
    }


    #endregion
}