using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000B17 RID: 2839
	public class InterstitialAd : IAdListener, IInAppPurchaseListener
	{
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06004F04 RID: 20228 RVA: 0x00217FA0 File Offset: 0x002161A0
		// (remove) Token: 0x06004F05 RID: 20229 RVA: 0x00217FD8 File Offset: 0x002161D8
		public event EventHandler<EventArgs> AdLoaded = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06004F06 RID: 20230 RVA: 0x00218010 File Offset: 0x00216210
		// (remove) Token: 0x06004F07 RID: 20231 RVA: 0x00218048 File Offset: 0x00216248
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate(object <p0>, AdFailedToLoadEventArgs <p1>)
		{
		};

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06004F08 RID: 20232 RVA: 0x00218080 File Offset: 0x00216280
		// (remove) Token: 0x06004F09 RID: 20233 RVA: 0x002180B8 File Offset: 0x002162B8
		public event EventHandler<EventArgs> AdOpened = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06004F0A RID: 20234 RVA: 0x002180F0 File Offset: 0x002162F0
		// (remove) Token: 0x06004F0B RID: 20235 RVA: 0x00218128 File Offset: 0x00216328
		public event EventHandler<EventArgs> AdClosing = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06004F0C RID: 20236 RVA: 0x00218160 File Offset: 0x00216360
		// (remove) Token: 0x06004F0D RID: 20237 RVA: 0x00218198 File Offset: 0x00216398
		public event EventHandler<EventArgs> AdClosed = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06004F0E RID: 20238 RVA: 0x002181D0 File Offset: 0x002163D0
		// (remove) Token: 0x06004F0F RID: 20239 RVA: 0x00218208 File Offset: 0x00216408
		public event EventHandler<EventArgs> AdLeftApplication = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x06004F10 RID: 20240 RVA: 0x00218240 File Offset: 0x00216440
		public InterstitialAd(string adUnitId)
		{
			this.client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsInterstitialClient(this);
			this.client.CreateInterstitialAd(adUnitId);
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x00218349 File Offset: 0x00216549
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x00218357 File Offset: 0x00216557
		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x00218364 File Offset: 0x00216564
		public void Show()
		{
			this.client.ShowInterstitial();
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x00218371 File Offset: 0x00216571
		public void Destroy()
		{
			this.client.DestroyInterstitial();
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x0021837E File Offset: 0x0021657E
		void IAdListener.FireAdLoaded()
		{
			this.AdLoaded(this, EventArgs.Empty);
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00218394 File Offset: 0x00216594
		void IAdListener.FireAdFailedToLoad(string message)
		{
			AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
			{
				Message = message
			};
			this.AdFailedToLoad(this, e);
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x002183BB File Offset: 0x002165BB
		void IAdListener.FireAdOpened()
		{
			this.AdOpened(this, EventArgs.Empty);
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x002183CE File Offset: 0x002165CE
		void IAdListener.FireAdClosing()
		{
			this.AdClosing(this, EventArgs.Empty);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x002183E1 File Offset: 0x002165E1
		void IAdListener.FireAdClosed()
		{
			this.AdClosed(this, EventArgs.Empty);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x002183F4 File Offset: 0x002165F4
		void IAdListener.FireAdLeftApplication()
		{
			this.AdLeftApplication(this, EventArgs.Empty);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x00218407 File Offset: 0x00216607
		bool IInAppPurchaseListener.FireIsValidPurchase(string sku)
		{
			return this.handler != null && this.handler.IsValidPurchase(sku);
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x0021841F File Offset: 0x0021661F
		void IInAppPurchaseListener.FireOnInAppPurchaseFinished(IInAppPurchaseResult result)
		{
			if (this.handler != null)
			{
				this.handler.OnInAppPurchaseFinished(result);
			}
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00218435 File Offset: 0x00216635
		public void SetInAppPurchaseHandler(IInAppPurchaseHandler handler)
		{
			this.handler = handler;
			this.client.SetInAppPurchaseParams(this, handler.AndroidPublicKey);
		}

		// Token: 0x04004E56 RID: 20054
		private IGoogleMobileAdsInterstitialClient client;

		// Token: 0x04004E57 RID: 20055
		private IInAppPurchaseHandler handler;
	}
}
