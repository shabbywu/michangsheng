using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000E80 RID: 3712
	public class BannerView : IAdListener
	{
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060058EA RID: 22762 RVA: 0x00247D44 File Offset: 0x00245F44
		// (remove) Token: 0x060058EB RID: 22763 RVA: 0x00247D7C File Offset: 0x00245F7C
		public event EventHandler<EventArgs> AdLoaded = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060058EC RID: 22764 RVA: 0x00247DB4 File Offset: 0x00245FB4
		// (remove) Token: 0x060058ED RID: 22765 RVA: 0x00247DEC File Offset: 0x00245FEC
		public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate(object <p0>, AdFailedToLoadEventArgs <p1>)
		{
		};

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060058EE RID: 22766 RVA: 0x00247E24 File Offset: 0x00246024
		// (remove) Token: 0x060058EF RID: 22767 RVA: 0x00247E5C File Offset: 0x0024605C
		public event EventHandler<EventArgs> AdOpened = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060058F0 RID: 22768 RVA: 0x00247E94 File Offset: 0x00246094
		// (remove) Token: 0x060058F1 RID: 22769 RVA: 0x00247ECC File Offset: 0x002460CC
		public event EventHandler<EventArgs> AdClosing = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060058F2 RID: 22770 RVA: 0x00247F04 File Offset: 0x00246104
		// (remove) Token: 0x060058F3 RID: 22771 RVA: 0x00247F3C File Offset: 0x0024613C
		public event EventHandler<EventArgs> AdClosed = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060058F4 RID: 22772 RVA: 0x00247F74 File Offset: 0x00246174
		// (remove) Token: 0x060058F5 RID: 22773 RVA: 0x00247FAC File Offset: 0x002461AC
		public event EventHandler<EventArgs> AdLeftApplication = delegate(object <p0>, EventArgs <p1>)
		{
		};

		// Token: 0x060058F6 RID: 22774 RVA: 0x00247FE4 File Offset: 0x002461E4
		public BannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsBannerClient(this);
			this.client.CreateBannerView(adUnitId, adSize, position);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x0003F4B9 File Offset: 0x0003D6B9
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0003F4C7 File Offset: 0x0003D6C7
		public void Hide()
		{
			this.client.HideBannerView();
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x0003F4D4 File Offset: 0x0003D6D4
		public void Show()
		{
			this.client.ShowBannerView();
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x0003F4E1 File Offset: 0x0003D6E1
		public void Destroy()
		{
			this.client.DestroyBannerView();
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0003F4EE File Offset: 0x0003D6EE
		void IAdListener.FireAdLoaded()
		{
			this.AdLoaded(this, EventArgs.Empty);
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x002480F0 File Offset: 0x002462F0
		void IAdListener.FireAdFailedToLoad(string message)
		{
			AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
			adFailedToLoadEventArgs.Message = message;
			this.AdFailedToLoad(this, adFailedToLoadEventArgs);
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0003F501 File Offset: 0x0003D701
		void IAdListener.FireAdOpened()
		{
			this.AdOpened(this, EventArgs.Empty);
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x0003F514 File Offset: 0x0003D714
		void IAdListener.FireAdClosing()
		{
			this.AdClosing(this, EventArgs.Empty);
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x0003F527 File Offset: 0x0003D727
		void IAdListener.FireAdClosed()
		{
			this.AdClosed(this, EventArgs.Empty);
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x0003F53A File Offset: 0x0003D73A
		void IAdListener.FireAdLeftApplication()
		{
			this.AdLeftApplication(this, EventArgs.Empty);
		}

		// Token: 0x0400589A RID: 22682
		private IGoogleMobileAdsBannerClient client;
	}
}
