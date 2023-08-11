using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api;

public class BannerView : IAdListener
{
	private IGoogleMobileAdsBannerClient client;

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

	public BannerView(string adUnitId, AdSize adSize, AdPosition position)
	{
		client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsBannerClient(this);
		client.CreateBannerView(adUnitId, adSize, position);
	}

	public void LoadAd(AdRequest request)
	{
		client.LoadAd(request);
	}

	public void Hide()
	{
		client.HideBannerView();
	}

	public void Show()
	{
		client.ShowBannerView();
	}

	public void Destroy()
	{
		client.DestroyBannerView();
	}

	void IAdListener.FireAdLoaded()
	{
		this.AdLoaded(this, EventArgs.Empty);
	}

	void IAdListener.FireAdFailedToLoad(string message)
	{
		AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
		adFailedToLoadEventArgs.Message = message;
		this.AdFailedToLoad(this, adFailedToLoadEventArgs);
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
}
