using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000B13 RID: 2835
	public class BannerView : IAdListener
	{
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06004EE8 RID: 20200 RVA: 0x00217B38 File Offset: 0x00215D38
		// (remove) Token: 0x06004EE9 RID: 20201 RVA: 0x00217B70 File Offset: 0x00215D70
		public event EventHandler<EventArgs> AdLoaded = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06004EEA RID: 20202 RVA: 0x00217BA8 File Offset: 0x00215DA8
		// (remove) Token: 0x06004EEB RID: 20203 RVA: 0x00217BE0 File Offset: 0x00215DE0
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate(object <p0>, AdFailedToLoadEventArgs <p1>)
		{
		};

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06004EEC RID: 20204 RVA: 0x00217C18 File Offset: 0x00215E18
		// (remove) Token: 0x06004EED RID: 20205 RVA: 0x00217C50 File Offset: 0x00215E50
		public event EventHandler<EventArgs> AdOpened = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06004EEE RID: 20206 RVA: 0x00217C88 File Offset: 0x00215E88
		// (remove) Token: 0x06004EEF RID: 20207 RVA: 0x00217CC0 File Offset: 0x00215EC0
		public event EventHandler<EventArgs> AdClosing = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06004EF0 RID: 20208 RVA: 0x00217CF8 File Offset: 0x00215EF8
		// (remove) Token: 0x06004EF1 RID: 20209 RVA: 0x00217D30 File Offset: 0x00215F30
		public event EventHandler<EventArgs> AdClosed = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06004EF2 RID: 20210 RVA: 0x00217D68 File Offset: 0x00215F68
		// (remove) Token: 0x06004EF3 RID: 20211 RVA: 0x00217DA0 File Offset: 0x00215FA0
		public event EventHandler<EventArgs> AdLeftApplication = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x06004EF4 RID: 20212 RVA: 0x00217DD8 File Offset: 0x00215FD8
		public BannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsBannerClient(this);
			this.client.CreateBannerView(adUnitId, adSize, position);
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x00217EE3 File Offset: 0x002160E3
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x00217EF1 File Offset: 0x002160F1
		public void Hide()
		{
			this.client.HideBannerView();
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x00217EFE File Offset: 0x002160FE
		public void Show()
		{
			this.client.ShowBannerView();
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x00217F0B File Offset: 0x0021610B
		public void Destroy()
		{
			this.client.DestroyBannerView();
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x00217F18 File Offset: 0x00216118
		void IAdListener.FireAdLoaded()
		{
			this.AdLoaded(this, EventArgs.Empty);
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x00217F2C File Offset: 0x0021612C
		void IAdListener.FireAdFailedToLoad(string message)
		{
			AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
			adFailedToLoadEventArgs.Message = message;
			this.AdFailedToLoad(this, adFailedToLoadEventArgs);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x00217F53 File Offset: 0x00216153
		void IAdListener.FireAdOpened()
		{
			this.AdOpened(this, EventArgs.Empty);
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x00217F66 File Offset: 0x00216166
		void IAdListener.FireAdClosing()
		{
			this.AdClosing(this, EventArgs.Empty);
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x00217F79 File Offset: 0x00216179
		void IAdListener.FireAdClosed()
		{
			this.AdClosed(this, EventArgs.Empty);
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x00217F8C File Offset: 0x0021618C
		void IAdListener.FireAdLeftApplication()
		{
			this.AdLeftApplication(this, EventArgs.Empty);
		}

		// Token: 0x04004E4B RID: 20043
		private IGoogleMobileAdsBannerClient client;
	}
}
