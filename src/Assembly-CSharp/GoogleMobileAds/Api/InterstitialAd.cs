using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000E85 RID: 3717
	public class InterstitialAd : IAdListener, IInAppPurchaseListener
	{
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x0600590E RID: 22798 RVA: 0x00248118 File Offset: 0x00246318
		// (remove) Token: 0x0600590F RID: 22799 RVA: 0x00248150 File Offset: 0x00246350
		public event EventHandler<EventArgs> AdLoaded = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06005910 RID: 22800 RVA: 0x00248188 File Offset: 0x00246388
		// (remove) Token: 0x06005911 RID: 22801 RVA: 0x002481C0 File Offset: 0x002463C0
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate(object <p0>, AdFailedToLoadEventArgs <p1>)
		{
		};

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06005912 RID: 22802 RVA: 0x002481F8 File Offset: 0x002463F8
		// (remove) Token: 0x06005913 RID: 22803 RVA: 0x00248230 File Offset: 0x00246430
		public event EventHandler<EventArgs> AdOpened = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06005914 RID: 22804 RVA: 0x00248268 File Offset: 0x00246468
		// (remove) Token: 0x06005915 RID: 22805 RVA: 0x002482A0 File Offset: 0x002464A0
		public event EventHandler<EventArgs> AdClosing = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06005916 RID: 22806 RVA: 0x002482D8 File Offset: 0x002464D8
		// (remove) Token: 0x06005917 RID: 22807 RVA: 0x00248310 File Offset: 0x00246510
		public event EventHandler<EventArgs> AdClosed = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06005918 RID: 22808 RVA: 0x00248348 File Offset: 0x00246548
		// (remove) Token: 0x06005919 RID: 22809 RVA: 0x00248380 File Offset: 0x00246580
		public event EventHandler<EventArgs> AdLeftApplication = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x0600591A RID: 22810 RVA: 0x002483B8 File Offset: 0x002465B8
		public InterstitialAd(string adUnitId)
		{
			this.client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsInterstitialClient(this);
			this.client.CreateInterstitialAd(adUnitId);
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x0003F559 File Offset: 0x0003D759
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x0003F567 File Offset: 0x0003D767
		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x0003F574 File Offset: 0x0003D774
		public void Show()
		{
			this.client.ShowInterstitial();
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x0003F581 File Offset: 0x0003D781
		public void Destroy()
		{
			this.client.DestroyInterstitial();
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x0003F58E File Offset: 0x0003D78E
		void IAdListener.FireAdLoaded()
		{
			this.AdLoaded(this, EventArgs.Empty);
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x002484C4 File Offset: 0x002466C4
		void IAdListener.FireAdFailedToLoad(string message)
		{
			AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
			{
				Message = message
			};
			this.AdFailedToLoad(this, e);
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x0003F5A1 File Offset: 0x0003D7A1
		void IAdListener.FireAdOpened()
		{
			this.AdOpened(this, EventArgs.Empty);
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x0003F5B4 File Offset: 0x0003D7B4
		void IAdListener.FireAdClosing()
		{
			this.AdClosing(this, EventArgs.Empty);
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0003F5C7 File Offset: 0x0003D7C7
		void IAdListener.FireAdClosed()
		{
			this.AdClosed(this, EventArgs.Empty);
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x0003F5DA File Offset: 0x0003D7DA
		void IAdListener.FireAdLeftApplication()
		{
			this.AdLeftApplication(this, EventArgs.Empty);
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x0003F5ED File Offset: 0x0003D7ED
		bool IInAppPurchaseListener.FireIsValidPurchase(string sku)
		{
			return this.handler != null && this.handler.IsValidPurchase(sku);
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x0003F605 File Offset: 0x0003D805
		void IInAppPurchaseListener.FireOnInAppPurchaseFinished(IInAppPurchaseResult result)
		{
			if (this.handler != null)
			{
				this.handler.OnInAppPurchaseFinished(result);
			}
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x0003F61B File Offset: 0x0003D81B
		public void SetInAppPurchaseHandler(IInAppPurchaseHandler handler)
		{
			this.handler = handler;
			this.client.SetInAppPurchaseParams(this, handler.AndroidPublicKey);
		}

		// Token: 0x040058AC RID: 22700
		private IGoogleMobileAdsInterstitialClient client;

		// Token: 0x040058AD RID: 22701
		private IInAppPurchaseHandler handler;
	}
}
