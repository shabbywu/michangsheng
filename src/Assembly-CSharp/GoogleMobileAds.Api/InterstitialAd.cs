using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api;

public class InterstitialAd : IAdListener, IInAppPurchaseListener
{
	private IGoogleMobileAdsInterstitialClient client;

	private IInAppPurchaseHandler handler;

	public event EventHandler<EventArgs> AdLoaded = delegate
	{
	};

	public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate
	{
	};

	public event EventHandler<EventArgs> AdOpened = delegate
	{
	};

	public event EventHandler<EventArgs> AdClosing = delegate
	{
	};

	public event EventHandler<EventArgs> AdClosed = delegate
	{
	};

	public event EventHandler<EventArgs> AdLeftApplication = delegate
	{
	};

	public InterstitialAd(string adUnitId)
	{
		client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsInterstitialClient(this);
		client.CreateInterstitialAd(adUnitId);
	}

	public void LoadAd(AdRequest request)
	{
		client.LoadAd(request);
	}

	public bool IsLoaded()
	{
		return client.IsLoaded();
	}

	public void Show()
	{
		client.ShowInterstitial();
	}

	public void Destroy()
	{
		client.DestroyInterstitial();
	}

	void IAdListener.FireAdLoaded()
	{
		this.AdLoaded(this, EventArgs.Empty);
	}

	void IAdListener.FireAdFailedToLoad(string message)
	{
		AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
		{
			Message = message
		};
		this.AdFailedToLoad(this, e);
	}

	void IAdListener.FireAdOpened()
	{
		this.AdOpened(this, EventArgs.Empty);
	}

	void IAdListener.FireAdClosing()
	{
		this.AdClosing(this, EventArgs.Empty);
	}

	void IAdListener.FireAdClosed()
	{
		this.AdClosed(this, EventArgs.Empty);
	}

	void IAdListener.FireAdLeftApplication()
	{
		this.AdLeftApplication(this, EventArgs.Empty);
	}

	bool IInAppPurchaseListener.FireIsValidPurchase(string sku)
	{
		if (handler != null)
		{
			return handler.IsValidPurchase(sku);
		}
		return false;
	}

	void IInAppPurchaseListener.FireOnInAppPurchaseFinished(IInAppPurchaseResult result)
	{
		if (handler != null)
		{
			handler.OnInAppPurchaseFinished(result);
		}
	}

	public void SetInAppPurchaseHandler(IInAppPurchaseHandler handler)
	{
		this.handler = handler;
		client.SetInAppPurchaseParams(this, handler.AndroidPublicKey);
	}
}
