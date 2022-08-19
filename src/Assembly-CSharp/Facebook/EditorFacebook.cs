using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook
{
	// Token: 0x02000B1C RID: 2844
	internal class EditorFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x0000280F File Offset: 0x00000A0F
		// (set) Token: 0x06004F40 RID: 20288 RVA: 0x00004095 File Offset: 0x00002295
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x00218458 File Offset: 0x00216658
		// (set) Token: 0x06004F42 RID: 20290 RVA: 0x00218E52 File Offset: 0x00217052
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

		// Token: 0x06004F43 RID: 20291 RVA: 0x00218E5B File Offset: 0x0021705B
		protected override void OnAwake()
		{
			base.StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", new FB.RemoteFacebookLoader.LoadedDllCallback(this.OnDllLoaded)));
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x00218E7C File Offset: 0x0021707C
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			base.StartCoroutine(this.OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x00218EA8 File Offset: 0x002170A8
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

		// Token: 0x06004F46 RID: 20294 RVA: 0x00218F0F File Offset: 0x0021710F
		private void OnDllLoaded(IFacebook fb)
		{
			this.fb = (AbstractFacebook)fb;
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00218F1D File Offset: 0x0021711D
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(0);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x00218F2D File Offset: 0x0021712D
		public override void Logout()
		{
			this.isLoggedIn = false;
			this.userId = "";
			this.accessToken = "";
			this.fb.UserId = "";
			this.fb.AccessToken = "";
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x00218F6C File Offset: 0x0021716C
		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.fb.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x00218F94 File Offset: 0x00217194
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x00218FC1 File Offset: 0x002171C1
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x00218FCD File Offset: 0x002171CD
		public override void GetAuthResponse(FacebookDelegate callback = null)
		{
			this.fb.GetAuthResponse(callback);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x00004095 File Offset: 0x00002295
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x00218FDB File Offset: 0x002171DB
		public override void GetDeepLink(FacebookDelegate callback)
		{
			FbDebug.Info("No Deep Linking in the Editor");
			if (callback != null)
			{
				callback.Invoke(new FBResult("<platform dependent>", null));
			}
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00218FFB File Offset: 0x002171FB
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x00218FFB File Offset: 0x002171FB
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x00219008 File Offset: 0x00217208
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

		// Token: 0x06004F52 RID: 20306 RVA: 0x002191A8 File Offset: 0x002173A8
		public void MockCancelledLoginCallback()
		{
			base.OnAuthResponse(new FBResult("", null));
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x002191BC File Offset: 0x002173BC
		private void BadAccessToken(string error)
		{
			FbDebug.Error(error);
			this.userId = "";
			this.fb.UserId = "";
			this.accessToken = "";
			this.fb.AccessToken = "";
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(0);
		}

		// Token: 0x04004E67 RID: 20071
		private AbstractFacebook fb;

		// Token: 0x04004E68 RID: 20072
		private FacebookDelegate loginCallback;
	}
}
