using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common;

internal class DummyClient : IGoogleMobileAdsBannerClient, IGoogleMobileAdsInterstitialClient
{
	public DummyClient(IAdListener listener)
	{
		Debug.Log((object)"Created DummyClient");
	}

	public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
	{
		Debug.Log((object)"Dummy CreateBannerView");
	}

	public void LoadAd(AdRequest request)
	{
		Debug.Log((object)"Dummy LoadAd");
	}

	public void ShowBannerView()
	{
		Debug.Log((object)"Dummy ShowBannerView");
	}

	public void HideBannerView()
	{
		Debug.Log((object)"Dummy HideBannerView");
	}

	public void DestroyBannerView()
	{
		Debug.Log((object)"Dummy DestroyBannerView");
	}

	public void CreateInterstitialAd(string adUnitId)
	{
		Debug.Log((object)"Dummy CreateIntersitialAd");
	}

	public bool IsLoaded()
	{
		Debug.Log((object)"Dummy IsLoaded");
		return true;
	}

	public void ShowInterstitial()
	{
		Debug.Log((object)"Dummy ShowInterstitial");
	}

	public void DestroyInterstitial()
	{
		Debug.Log((object)"Dummy DestroyInterstitial");
	}

	public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey)
	{
		Debug.Log((object)"Dummy SetInAppPurchaseParams");
	}
}
