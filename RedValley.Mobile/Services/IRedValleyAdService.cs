namespace RedValley.Mobile.Services;

public interface IRedValleyAdService
{
    void LoadAd();
    Task ShowAd(Action onAdShownAction);
}