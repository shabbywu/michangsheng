using System;
using System.Collections.Generic;
using Facebook.MiniJSON;

namespace Facebook;

internal class IOSFacebook : AbstractFacebook, IFacebook
{
	private class NativeDict
	{
		public int numEntries;

		public string[] keys;

		public string[] vals;

		public NativeDict()
		{
			numEntries = 0;
			keys = null;
			vals = null;
		}
	}

	public enum FBInsightsFlushBehavior
	{
		FBInsightsFlushBehaviorAuto,
		FBInsightsFlushBehaviorExplicitOnly
	}

	private const string CancelledResponse = "{\"cancelled\":true}";

	private int dialogMode = 1;

	private InitDelegate externalInitDelegate;

	private FacebookDelegate deepLinkDelegate;

	public override int DialogMode
	{
		get
		{
			return dialogMode;
		}
		set
		{
			dialogMode = value;
			iosSetShareDialogMode(dialogMode);
		}
	}

	public override bool LimitEventUsage
	{
		get
		{
			return base.limitEventUsage;
		}
		set
		{
			base.limitEventUsage = value;
			iosFBAppEventsSetLimitEventUsage(value);
		}
	}

	private void iosInit(bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix)
	{
	}

	private void iosLogin(string scope)
	{
	}

	private void iosLogout()
	{
	}

	private void iosSetShareDialogMode(int mode)
	{
	}

	private void iosFeedRequest(int requestId, string toId, string link, string linkName, string linkCaption, string linkDescription, string picture, string mediaSource, string actionName, string actionLink, string reference)
	{
	}

	private void iosAppRequest(int requestId, string message, string[] to = null, int toLength = 0, string filters = "", string[] excludeIds = null, int excludeIdsLength = 0, bool hasMaxRecipients = false, int maxRecipients = 0, string data = "", string title = "")
	{
	}

	private void iosFBSettingsPublishInstall(int requestId, string appId)
	{
	}

	private void iosFBAppEventsLogEvent(string logEvent, double valueToSum, int numParams, string[] paramKeys, string[] paramVals)
	{
	}

	private void iosFBAppEventsLogPurchase(double logPurchase, string currency, int numParams, string[] paramKeys, string[] paramVals)
	{
	}

	private void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage)
	{
	}

	private void iosGetDeepLink()
	{
	}

	protected override void OnAwake()
	{
		base.accessToken = "NOT_USED_ON_IOS_FACEBOOK";
	}

	public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
	{
		iosInit(cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
		externalInitDelegate = onInitComplete;
	}

	public override void Login(string scope = "", FacebookDelegate callback = null)
	{
		((AbstractFacebook)this).AddAuthDelegate(callback);
		iosLogin(scope);
	}

	public override void Logout()
	{
		iosLogout();
		base.isLoggedIn = false;
	}

	public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		iosAppRequest(Convert.ToInt32(((AbstractFacebook)this).AddFacebookDelegate(callback)), message, to, (to != null) ? to.Length : 0, filters, excludeIds, (excludeIds != null) ? excludeIds.Length : 0, maxRecipients.HasValue, maxRecipients.HasValue ? maxRecipients.Value : 0, data, title);
	}

	public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		iosFeedRequest(Convert.ToInt32(((AbstractFacebook)this).AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
	}

	public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
	{
		throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
	}

	public override void GetDeepLink(FacebookDelegate callback)
	{
		if (callback != null)
		{
			deepLinkDelegate = callback;
			iosGetDeepLink();
		}
	}

	public void OnGetDeepLinkComplete(string message)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
		if (deepLinkDelegate != null)
		{
			object value = "";
			dictionary.TryGetValue("deep_link", out value);
			deepLinkDelegate.Invoke(new FBResult(value.ToString(), (string)null));
		}
	}

	public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
	{
		NativeDict nativeDict = MarshallDict(parameters);
		if (valueToSum.HasValue)
		{
			iosFBAppEventsLogEvent(logEvent, valueToSum.Value, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}
		else
		{
			iosFBAppEventsLogEvent(logEvent, 0.0, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}
	}

	public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
	{
		NativeDict nativeDict = MarshallDict(parameters);
		if (string.IsNullOrEmpty(currency))
		{
			currency = "USD";
		}
		iosFBAppEventsLogPurchase(logPurchase, currency, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
	}

	public override void PublishInstall(string appId, FacebookDelegate callback = null)
	{
		iosFBSettingsPublishInstall(Convert.ToInt32(((AbstractFacebook)this).AddFacebookDelegate(callback)), appId);
	}

	private NativeDict MarshallDict(Dictionary<string, object> dict)
	{
		NativeDict nativeDict = new NativeDict();
		if (dict != null && dict.Count > 0)
		{
			nativeDict.keys = new string[dict.Count];
			nativeDict.vals = new string[dict.Count];
			nativeDict.numEntries = 0;
			foreach (KeyValuePair<string, object> item in dict)
			{
				nativeDict.keys[nativeDict.numEntries] = item.Key;
				nativeDict.vals[nativeDict.numEntries] = item.Value.ToString();
				nativeDict.numEntries++;
			}
		}
		return nativeDict;
	}

	private NativeDict MarshallDict(Dictionary<string, string> dict)
	{
		NativeDict nativeDict = new NativeDict();
		if (dict != null && dict.Count > 0)
		{
			nativeDict.keys = new string[dict.Count];
			nativeDict.vals = new string[dict.Count];
			nativeDict.numEntries = 0;
			foreach (KeyValuePair<string, string> item in dict)
			{
				nativeDict.keys[nativeDict.numEntries] = item.Key;
				nativeDict.vals[nativeDict.numEntries] = item.Value;
				nativeDict.numEntries++;
			}
		}
		return nativeDict;
	}

	private void OnInitComplete(string msg)
	{
		if (!string.IsNullOrEmpty(msg))
		{
			OnLogin(msg);
		}
		externalInitDelegate.Invoke();
	}

	public void OnLogin(string msg)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		if (string.IsNullOrEmpty(msg))
		{
			((AbstractFacebook)this).OnAuthResponse(new FBResult("{\"cancelled\":true}", (string)null));
			return;
		}
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(msg);
		if (dictionary.ContainsKey("user_id"))
		{
			base.isLoggedIn = true;
		}
		ParseLoginDict(dictionary);
		((AbstractFacebook)this).OnAuthResponse(new FBResult(msg, (string)null));
	}

	public void ParseLoginDict(Dictionary<string, object> parameters)
	{
		if (parameters.ContainsKey("user_id"))
		{
			base.userId = (string)parameters["user_id"];
		}
		if (parameters.ContainsKey("access_token"))
		{
			base.accessToken = (string)parameters["access_token"];
		}
		if (parameters.ContainsKey("expiration_timestamp"))
		{
			base.accessTokenExpiresAt = FromTimestamp(int.Parse((string)parameters["expiration_timestamp"]));
		}
	}

	public void OnAccessTokenRefresh(string message)
	{
		Dictionary<string, object> parameters = (Dictionary<string, object>)Json.Deserialize(message);
		ParseLoginDict(parameters);
	}

	private DateTime FromTimestamp(int timestamp)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
	}

	public void OnLogout(string msg)
	{
		base.isLoggedIn = false;
	}

	public void OnRequestComplete(string msg)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
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
		((AbstractFacebook)this).OnFacebookResponse(text, new FBResult(text2, (string)null));
	}
}
