using System;
using System.Collections.Generic;
using Facebook.MiniJSON;

namespace Facebook
{
	// Token: 0x02000E8E RID: 3726
	internal class IOSFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x0600596F RID: 22895 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosInit(bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix)
		{
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosLogin(string scope)
		{
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosLogout()
		{
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosSetShareDialogMode(int mode)
		{
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosFeedRequest(int requestId, string toId, string link, string linkName, string linkCaption, string linkDescription, string picture, string mediaSource, string actionName, string actionLink, string reference)
		{
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosAppRequest(int requestId, string message, string[] to = null, int toLength = 0, string filters = "", string[] excludeIds = null, int excludeIdsLength = 0, bool hasMaxRecipients = false, int maxRecipients = 0, string data = "", string title = "")
		{
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosFBSettingsPublishInstall(int requestId, string appId)
		{
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosFBAppEventsLogEvent(string logEvent, double valueToSum, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosFBAppEventsLogPurchase(double logPurchase, string currency, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage)
		{
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x000042DD File Offset: 0x000024DD
		private void iosGetDeepLink()
		{
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x0003F81F File Offset: 0x0003DA1F
		// (set) Token: 0x0600597B RID: 22907 RVA: 0x0003F827 File Offset: 0x0003DA27
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

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x0003F64A File Offset: 0x0003D84A
		// (set) Token: 0x0600597D RID: 22909 RVA: 0x0003F83C File Offset: 0x0003DA3C
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

		// Token: 0x0600597E RID: 22910 RVA: 0x0003F84C File Offset: 0x0003DA4C
		protected override void OnAwake()
		{
			this.accessToken = "NOT_USED_ON_IOS_FACEBOOK";
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x0003F859 File Offset: 0x0003DA59
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			this.iosInit(cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
			this.externalInitDelegate = onInitComplete;
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0003F874 File Offset: 0x0003DA74
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			this.iosLogin(scope);
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0003F884 File Offset: 0x0003DA84
		public override void Logout()
		{
			this.iosLogout();
			this.isLoggedIn = false;
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x0024919C File Offset: 0x0024739C
		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.iosAppRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), message, to, (to != null) ? to.Length : 0, filters, excludeIds, (excludeIds != null) ? excludeIds.Length : 0, maxRecipients != null, (maxRecipients != null) ? maxRecipients.Value : 0, data, title);
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x002491F4 File Offset: 0x002473F4
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.iosFeedRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x0003F893 File Offset: 0x0003DA93
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x0003F89F File Offset: 0x0003DA9F
		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback == null)
			{
				return;
			}
			this.deepLinkDelegate = callback;
			this.iosGetDeepLink();
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x00249228 File Offset: 0x00247428
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

		// Token: 0x06005987 RID: 22919 RVA: 0x00249278 File Offset: 0x00247478
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

		// Token: 0x06005988 RID: 22920 RVA: 0x002492DC File Offset: 0x002474DC
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (string.IsNullOrEmpty(currency))
			{
				currency = "USD";
			}
			this.iosFBAppEventsLogPurchase((double)logPurchase, currency, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x0003F8B2 File Offset: 0x0003DAB2
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			this.iosFBSettingsPublishInstall(Convert.ToInt32(base.AddFacebookDelegate(callback)), appId);
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x0024931C File Offset: 0x0024751C
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

		// Token: 0x0600598B RID: 22923 RVA: 0x002493E0 File Offset: 0x002475E0
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

		// Token: 0x0600598C RID: 22924 RVA: 0x0003F8C7 File Offset: 0x0003DAC7
		private void OnInitComplete(string msg)
		{
			if (!string.IsNullOrEmpty(msg))
			{
				this.OnLogin(msg);
			}
			this.externalInitDelegate.Invoke();
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x002494A0 File Offset: 0x002476A0
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

		// Token: 0x0600598E RID: 22926 RVA: 0x002494FC File Offset: 0x002476FC
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

		// Token: 0x0600598F RID: 22927 RVA: 0x00249580 File Offset: 0x00247780
		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> parameters = (Dictionary<string, object>)Json.Deserialize(message);
			this.ParseLoginDict(parameters);
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x00248DE8 File Offset: 0x00246FE8
		private DateTime FromTimestamp(int timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)timestamp);
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x0003F8E3 File Offset: 0x0003DAE3
		public void OnLogout(string msg)
		{
			this.isLoggedIn = false;
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x002495A0 File Offset: 0x002477A0
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

		// Token: 0x040058D3 RID: 22739
		private const string CancelledResponse = "{\"cancelled\":true}";

		// Token: 0x040058D4 RID: 22740
		private int dialogMode = 1;

		// Token: 0x040058D5 RID: 22741
		private InitDelegate externalInitDelegate;

		// Token: 0x040058D6 RID: 22742
		private FacebookDelegate deepLinkDelegate;

		// Token: 0x02000E8F RID: 3727
		private class NativeDict
		{
			// Token: 0x06005994 RID: 22932 RVA: 0x0003F8FB File Offset: 0x0003DAFB
			public NativeDict()
			{
				this.numEntries = 0;
				this.keys = null;
				this.vals = null;
			}

			// Token: 0x040058D7 RID: 22743
			public int numEntries;

			// Token: 0x040058D8 RID: 22744
			public string[] keys;

			// Token: 0x040058D9 RID: 22745
			public string[] vals;
		}

		// Token: 0x02000E90 RID: 3728
		public enum FBInsightsFlushBehavior
		{
			// Token: 0x040058DB RID: 22747
			FBInsightsFlushBehaviorAuto,
			// Token: 0x040058DC RID: 22748
			FBInsightsFlushBehaviorExplicitOnly
		}
	}
}
