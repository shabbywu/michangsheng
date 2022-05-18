using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook
{
	// Token: 0x02000E8B RID: 3723
	internal class EditorFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x00004050 File Offset: 0x00002250
		// (set) Token: 0x06005952 RID: 22866 RVA: 0x000042DD File Offset: 0x000024DD
		public override int DialogMode
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x0003F64A File Offset: 0x0003D84A
		// (set) Token: 0x06005954 RID: 22868 RVA: 0x0003F722 File Offset: 0x0003D922
		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
			}
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x0003F72B File Offset: 0x0003D92B
		protected override void OnAwake()
		{
			base.StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", new FB.RemoteFacebookLoader.LoadedDllCallback(this.OnDllLoaded)));
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x00248E10 File Offset: 0x00247010
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			base.StartCoroutine(this.OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x00248E3C File Offset: 0x0024703C
		private IEnumerator OnInit(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			while (this.fb == null)
			{
				yield return null;
			}
			this.fb.Init(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate);
			if (onInitComplete != null)
			{
				onInitComplete.Invoke();
			}
			yield break;
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x0003F74A File Offset: 0x0003D94A
		private void OnDllLoaded(IFacebook fb)
		{
			this.fb = (AbstractFacebook)fb;
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0003F758 File Offset: 0x0003D958
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(0);
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x0003F768 File Offset: 0x0003D968
		public override void Logout()
		{
			this.isLoggedIn = false;
			this.userId = "";
			this.accessToken = "";
			this.fb.UserId = "";
			this.fb.AccessToken = "";
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00248EA4 File Offset: 0x002470A4
		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.fb.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00248ECC File Offset: 0x002470CC
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x0003F7A7 File Offset: 0x0003D9A7
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x0003F7B3 File Offset: 0x0003D9B3
		public override void GetAuthResponse(FacebookDelegate callback = null)
		{
			this.fb.GetAuthResponse(callback);
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x000042DD File Offset: 0x000024DD
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x0003F7C1 File Offset: 0x0003D9C1
		public override void GetDeepLink(FacebookDelegate callback)
		{
			FbDebug.Info("No Deep Linking in the Editor");
			if (callback != null)
			{
				callback.Invoke(new FBResult("<platform dependent>", null));
			}
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x0003F7E1 File Offset: 0x0003D9E1
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x0003F7E1 File Offset: 0x0003D9E1
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00248EFC File Offset: 0x002470FC
		public void MockLoginCallback(FBResult result)
		{
			Object.Destroy(FBComponentFactory.GetComponent<EditorFacebookAccessToken>(0));
			if (result.Error != null)
			{
				this.BadAccessToken(result.Error);
				return;
			}
			try
			{
				List<object> list = (List<object>)Json.Deserialize(result.Text);
				List<string> list2 = new List<string>();
				foreach (object obj in list)
				{
					if (obj is Dictionary<string, object>)
					{
						Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
						if (dictionary.ContainsKey("body"))
						{
							list2.Add((string)dictionary["body"]);
						}
					}
				}
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)Json.Deserialize(list2[0]);
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)Json.Deserialize(list2[1]);
				if (FB.AppId != (string)dictionary3["id"])
				{
					this.BadAccessToken("Access token is not for current app id: " + FB.AppId);
				}
				else
				{
					this.userId = (string)dictionary2["id"];
					this.fb.UserId = this.userId;
					this.fb.AccessToken = this.accessToken;
					this.isLoggedIn = true;
					base.OnAuthResponse(new FBResult("", null));
				}
			}
			catch (Exception ex)
			{
				this.BadAccessToken("Could not get data from access token: " + ex.Message);
			}
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x0003F7ED File Offset: 0x0003D9ED
		public void MockCancelledLoginCallback()
		{
			base.OnAuthResponse(new FBResult("", null));
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x0024909C File Offset: 0x0024729C
		private void BadAccessToken(string error)
		{
			FbDebug.Error(error);
			this.userId = "";
			this.fb.UserId = "";
			this.accessToken = "";
			this.fb.AccessToken = "";
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(0);
		}

		// Token: 0x040058C4 RID: 22724
		private AbstractFacebook fb;

		// Token: 0x040058C5 RID: 22725
		private FacebookDelegate loginCallback;
	}
}
