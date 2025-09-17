using Plugin.AdMob;
using Plugin.AdMob.Services;

namespace RedValley.Mobile.Services;

public class DummyRedValleyAppOpenAdService : IRedValleyAppOpenAdService
{
    public void LoadAd()
    {
    }

    public async Task ShowAd(Action onAdShownAction)
    {
        await Task.Run(() => { });
    }
}