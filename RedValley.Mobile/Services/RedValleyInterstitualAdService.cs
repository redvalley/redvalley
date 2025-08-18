﻿#if !PRO_VERSION
using Plugin.AdMob;
using Plugin.AdMob.Services;

namespace RedValley.Mobile.Services;

public class RedValleyInterstitualAdService : RedValleyAdService<IInterstitialAd, IInterstitialAdService>, IRedValleyInterstitualAdService
{
    public RedValleyInterstitualAdService(IInterstitialAdService adService, string adUnitId) : base(adService, adUnitId)
    {
    }

    protected override bool IsAdLoaded => Ad?.IsLoaded ?? false;

    protected override IInterstitialAd CreateAd()
    {
        return AdService.CreateAd(AdUnitId);
    }

    protected override void DoLoadAd()
    {
        Ad?.Load();
    }

    protected override void DoShowAd()
    {
        Ad?.Show();
    }

    protected override void AttachAdFailedToLoadHandler()
    {
        var appOpenAd = this.Ad;
        if (appOpenAd != null)
            appOpenAd.OnAdFailedToLoad += (sender, error) => { OnAdFailedToLoad(); };
    }

    protected override void AttachAdLoadedHandler()
    {
        var appOpenAd = this.Ad;
        if (appOpenAd != null)
            appOpenAd.OnAdLoaded += (sender, error) => { OnAdLoaded(); };
    }

    protected override void AttachAdFailedToShowHandler(Action onAdShownAction)
    {
        var appOpenAd = this.Ad;
        if (appOpenAd != null)
            appOpenAd.OnAdFailedToShow += (sender, error) => { OnAdFailedToShow(onAdShownAction); };
    }

    protected override void AttachAdDismissedHandler(Action onAdShownAction)
    {
        var appOpenAd = this.Ad;
        if (appOpenAd != null)
            appOpenAd.OnAdDismissed += (sender, error) => { OnAdDismissed(onAdShownAction); };
    }
}
#endif