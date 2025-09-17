using Plugin.AdMob;
using Plugin.AdMob.Services;

namespace RedValley.Mobile.Services;

public class DummyRedValleyInterstitualAdService : IRedValleyInterstitualAdService
{
    public void LoadAd()
    {
    }

    public async Task ShowAd(Action onAdShownAction)
    {
        await Task.Run(() => { });
    }
}