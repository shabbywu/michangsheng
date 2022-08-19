using System;
using System.Collections.Generic;
using Facebook.MiniJSON;

namespace Facebook
{
	// Token: 0x02000B1E RID: 2846
	internal class IOSFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x06004F57 RID: 20311 RVA: 0x00004095 File Offset: 0x00002295
		private void iosInit(bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix)
		{
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x00004095 File Offset: 0x00002295
		private void iosLogin(string scope)
		{
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x00004095 File Offset: 0x00002295
		private void iosLogout()
		{
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00004095 File Offset: 0x00002295
		private void iosSetShareDialogMode(int mode)
		{
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00004095 File Offset: 0x00002295
		private void iosFeedRequest(int requestId, string toId, string link, string linkName, string linkCaption, string linkDescription, string picture, string mediaSource, string actionName, string actionLink, string reference)
		{
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x00004095 File Offset: 0x00002295
		private void iosAppRequest(int requestId, string message, string[] to = null, int toLength = 0, string filters = "", string[] excludeIds = null, int excludeIdsLength = 0, bool hasMaxRecipients = false, int maxRecipients = 0, string data = "", string title = "")
		{
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00004095 File Offset: 0x00002295
		private void iosFBSettingsPublishInstall(int requestId, string appId)
		{
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00004095 File Offset: 0x00002295
		private void iosFBAppEventsLogEvent(string logEvent, double valueToSum, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00004095 File Offset: 0x00002295
		private void iosFBAppEventsLogPurchase(double logPurchase, string currency, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x00004095 File Offset: 0x00002295
		private void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage)
		{
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x00004095 File Offset: 0x00002295
		private void iosGetDeepLink()
		{
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x00219214 File Offset: 0x00217414
		// (set) Token: 0x06004F63 RID: 20323 RVA: 0x0021921C File Offset: 0x0021741C
		public override int DialogMode
		{
			get
			{
				return this.dialogMode;
			}
			set
			{
				this.dialogMode = value;
				this.iosSetShareDialogMode(this.dialogMode);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06004F64 RID: 20324 RVA: 0x00218458 File Offset: 0x00216658
		// (set) Token: 0x06004F65 RID: 20325 RVA: 0x00219231 File Offset: 0x00217431
		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
				this.iosFBAppEventsSetLimitEventUsage(value);
			}
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00219241 File Offset: 0x00217441
		protected override void OnAwake()
		{
			this.accessToken = "NOT_USED_ON_IOS_FACEBOOK";
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x0021924E File Offset: 0x0021744E
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			this.iosInit(cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
			this.externalInitDelegate = onInitComplete;
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00219269 File Offset: 0x00217469
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			this.iosLogin(scope);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00219279 File Offset: 0x00217479
		public override void Logout()
		{
			this.iosLogout();
			this.isLoggedIn = false;
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00219288 File Offset: 0x00217488
		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.iosAppRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), message, to, (to != null) ? to.Length : 0, filters, excludeIds, (excludeIds != null) ? excludeIds.Length : 0, maxRecipients != null, (maxRecipients != null) ? maxRecipients.Value : 0, data, title);
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x002192E0 File Offset: 0x002174E0
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.iosFeedRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00219311 File Offset: 0x00217511
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0021931D File Offset: 0x0021751D
		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback == null)
			{
				return;
			}
			this.deepLinkDelegate = callback;
			this.iosGetDeepLink();
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00219330 File Offset: 0x00217530
		public void OnGetDeepLinkComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (this.deepLinkDelegate == null)
			{
				return;
			}
			object obj = "";
			dictionary.TryGetValue("deep_link", out obj);
			this.deepLinkDelegate.Invoke(new FBResult(obj.ToString(), null));
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x00219380 File Offset: 0x00217580
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (valueToSum != null)
			{
				this.iosFBAppEventsLogEvent(logEvent, (double)valueToSum.Value, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
				return;
			}
			this.iosFBAppEventsLogEvent(logEvent, 0.0, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x002193E4 File Offset: 0x002175E4
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (string.IsNullOrEmpty(currency))
			{
				currency = "USD";
			}
			this.iosFBAppEventsLogPurchase((double)logPurchase, currency, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00219423 File Offset: 0x00217623
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			this.iosFBSettingsPublishInstall(Convert.ToInt32(base.AddFacebookDelegate(callback)), appId);
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00219438 File Offset: 0x00217638
		private IOSFacebook.NativeDict MarshallDict(Dictionary<string, object> dict)
		{
			IOSFacebook.NativeDict nativeDict = new IOSFacebook.NativeDict();
			if (dict != null && dict.Count > 0)
			{
				nativeDict.keys = new string[dict.Count];
				nativeDict.vals = new string[dict.Count];
				nativeDict.numEntries = 0;
				foreach (KeyValuePair<string, object> keyValuePair in dict)
				{
					nativeDict.keys[nativeDict.numEntries] = keyValuePair.Key;
					nativeDict.vals[nativeDict.numEntries] = keyValuePair.Value.ToString();
					nativeDict.numEntries++;
				}
			}
			return nativeDict;
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x002194FC File Offset: 0x002176FC
		private IOSFacebook.NativeDict MarshallDict(Dictionary<string, string> dict)
		{
			IOSFacebook.NativeDict nativeDict = new IOSFacebook.NativeDict();
			if (dict != null && dict.Count > 0)
			{
				nativeDict.keys = new string[dict.Count];
				nativeDict.vals = new string[dict.Count];
				nativeDict.numEntries = 0;
				foreach (KeyValuePair<string, string> keyValuePair in dict)
				{
					nativeDict.keys[nativeDict.numEntries] = keyValuePair.Key;
					nativeDict.vals[nativeDict.numEntries] = keyValuePair.Value;
					nativeDict.numEntries++;
				}
			}
			return nativeDict;
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x002195BC File Offset: 0x002177BC
		private void OnInitComplete(string msg)
		{
			if (!string.IsNullOrEmpty(msg))
			{
				this.OnLogin(msg);
			}
			this.externalInitDelegate.Invoke();
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x002195D8 File Offset: 0x002177D8
		public void OnLogin(string msg)
		{
			if (string.IsNullOrEmpty(msg))
			{
				base.OnAuthResponse(new FBResult("{\"cancelled\":true}", null));
				return;
			}
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(msg);
			if (dictionary.ContainsKey("user_id"))
			{
				this.isLoggedIn = true;
			}
			this.ParseLoginDict(dictionary);
			base.OnAuthResponse(new FBResult(msg, null));
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00219634 File Offset: 0x00217834
		public void ParseLoginDict(Dictionary<string, object> parameters)
		{
			if (parameters.ContainsKey("user_id"))
			{
				this.userId = (string)parameters["user_id"];
			}
			if (parameters.ContainsKey("access_token"))
			{
				this.accessToken = (string)parameters["access_token"];
			}
			if (parameters.ContainsKey("expiration_timestamp"))
			{
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)parameters["expiration_timestamp"]));
			}
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x002196B8 File Offset: 0x002178B8
		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> parameters = (Dictionary<string, object>)Json.Deserialize(message);
			this.ParseLoginDict(parameters);
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x002196D8 File Offset: 0x002178D8
		private DateTime FromTimestamp(int timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)timestamp);
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x002196FF File Offset: 0x002178FF
		public void OnLogout(string msg)
		{
			this.isLoggedIn = false;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00219708 File Offset: 0x00217908
		public void OnRequestComplete(string msg)
		{
			int num = msg.IndexOf(":");
			if (num <= 0)
			{
				FbDebug.Error("Malformed callback from ios.  I expected the form id:message but couldn't find either the ':' character or the id.");
				FbDebug.Error("Here's the message that errored: " + msg);
				return;
			}
			string text = msg.Substring(0, num);
			string text2 = msg.Substring(num + 1);
			FbDebug.Info("id:" + text + " msg:" + text2);
			base.OnFacebookResponse(text, new FBResult(text2, null));
		}

		// Token: 0x04004E69 RID: 20073
		private const string CancelledResponse = "{\"cancelled\":true}";

		// Token: 0x04004E6A RID: 20074
		private int dialogMode = 1;

		// Token: 0x04004E6B RID: 20075
		private InitDelegate externalInitDelegate;

		// Token: 0x04004E6C RID: 20076
		private FacebookDelegate deepLinkDelegate;

		// Token: 0x020015E6 RID: 5606
		private class NativeDict
		{
			// Token: 0x0600857E RID: 34174 RVA: 0x002E4579 File Offset: 0x002E2779
			public NativeDict()
			{
				this.numEntries = 0;
				this.keys = null;
				this.vals = null;
			}

			// Token: 0x040070C1 RID: 28865
			public int numEntries;

			// Token: 0x040070C2 RID: 28866
			public string[] keys;

			// Token: 0x040070C3 RID: 28867
			public string[] vals;
		}

		// Token: 0x020015E7 RID: 5607
		public enum FBInsightsFlushBehavior
		{
			// Token: 0x040070C5 RID: 28869
			FBInsightsFlushBehaviorAuto,
			// Token: 0x040070C6 RID: 28870
			FBInsightsFlushBehaviorExplicitOnly
		}
	}
}
