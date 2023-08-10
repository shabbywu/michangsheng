using System;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook;

internal sealed class AndroidFacebook : AbstractFacebook, IFacebook
{
	public const int BrowserDialogMode = 0;

	private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

	private const string CallbackIdKey = "callback_id";

	private string keyHash;

	private FacebookDelegate deepLinkDelegate;

	private InitDelegate onInitComplete;

	public string KeyHash => keyHash;

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

	public override bool LimitEventUsage
	{
		get
		{
			return base.limitEventUsage;
		}
		set
		{
			base.limitEventUsage = value;
			CallFB("SetLimitEventUsage", value.ToString());
		}
	}

	private void CallFB(string method, string args)
	{
		FbDebug.Error("Using Android when not on an Android build!  Doesn't Work!");
	}

	protected override void OnAwake()
	{
		keyHash = "";
	}

	private bool IsErrorResponse(string response)
	{
		return false;
	}

	public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
	{
		if (string.IsNullOrEmpty(appId))
		{
			throw new ArgumentException("appId cannot be null or empty!");
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("appId", appId);
		if (cookie)
		{
			dictionary.Add("cookie", true);
		}
		if (!logging)
		{
			dictionary.Add("logging", false);
		}
		if (!status)
		{
			dictionary.Add("status", false);
		}
		if (xfbml)
		{
			dictionary.Add("xfbml", true);
		}
		if (!string.IsNullOrEmpty(channelUrl))
		{
			dictionary.Add("channelUrl", channelUrl);
		}
		if (!string.IsNullOrEmpty(authResponse))
		{
			dictionary.Add("authResponse", authResponse);
		}
		if (frictionlessRequests)
		{
			dictionary.Add("frictionlessRequests", true);
		}
		string text = Json.Serialize((object)dictionary);
		this.onInitComplete = onInitComplete;
		CallFB("Init", text.ToString());
	}

	public void OnInitComplete(string message)
	{
		OnLoginComplete(message);
		if (onInitComplete != null)
		{
			onInitComplete.Invoke();
		}
	}

	public override void Login(string scope = "", FacebookDelegate callback = null)
	{
		string args = Json.Serialize((object)new Dictionary<string, object> { { "scope", scope } });
		((AbstractFacebook)this).AddAuthDelegate(callback);
		CallFB("Login", args);
	}

	public void OnLoginComplete(string message)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
		if (dictionary.ContainsKey("user_id"))
		{
			base.isLoggedIn = true;
			base.userId = (string)dictionary["user_id"];
			base.accessToken = (string)dictionary["access_token"];
			base.accessTokenExpiresAt = FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
		}
		if (dictionary.ContainsKey("key_hash"))
		{
			keyHash = (string)dictionary["key_hash"];
			Debug.Log((object)("proper keyhash : " + keyHash));
		}
		((AbstractFacebook)this).OnAuthResponse(new FBResult(message, (string)null));
	}

	public void OnAccessTokenRefresh(string message)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
		if (dictionary.ContainsKey("access_token"))
		{
			base.accessToken = (string)dictionary["access_token"];
			base.accessTokenExpiresAt = FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
		}
	}

	public override void Logout()
	{
		CallFB("Logout", "");
	}

	public void OnLogoutComplete(string message)
	{
		base.isLoggedIn = false;
		base.userId = "";
		base.accessToken = "";
	}

	public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["message"] = message;
		if (callback != null)
		{
			dictionary["callback_id"] = ((AbstractFacebook)this).AddFacebookDelegate(callback);
		}
		if (to != null)
		{
			dictionary["to"] = string.Join(",", to);
		}
		if (!string.IsNullOrEmpty(filters))
		{
			dictionary["filters"] = filters;
		}
		if (maxRecipients.HasValue)
		{
			dictionary["max_recipients"] = maxRecipients.Value;
		}
		if (!string.IsNullOrEmpty(data))
		{
			dictionary["data"] = data;
		}
		if (!string.IsNullOrEmpty(title))
		{
			dictionary["title"] = title;
		}
		CallFB("AppRequest", Json.Serialize((object)dictionary));
	}

	public void OnAppRequestsComplete(string message)
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
		if (!dictionary.ContainsKey("callback_id"))
		{
			return;
		}
		Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
		string text = (string)dictionary["callback_id"];
		dictionary.Remove("callback_id");
		if (dictionary.Count > 0)
		{
			List<string> list = new List<string>(dictionary.Count - 1);
			foreach (string key in dictionary.Keys)
			{
				if (!key.StartsWith("to"))
				{
					dictionary2[key] = dictionary[key];
				}
				else
				{
					list.Add((string)dictionary[key]);
				}
			}
			dictionary2.Add("to", list);
			dictionary.Clear();
			((AbstractFacebook)this).OnFacebookResponse(text, new FBResult(Json.Serialize((object)dictionary2), (string)null));
		}
		else
		{
			((AbstractFacebook)this).OnFacebookResponse(text, new FBResult(Json.Serialize((object)dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
		}
	}

	public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (callback != null)
		{
			dictionary["callback_id"] = ((AbstractFacebook)this).AddFacebookDelegate(callback);
		}
		if (!string.IsNullOrEmpty(toId))
		{
			dictionary.Add("to", toId);
		}
		if (!string.IsNullOrEmpty(link))
		{
			dictionary.Add("link", link);
		}
		if (!string.IsNullOrEmpty(linkName))
		{
			dictionary.Add("name", linkName);
		}
		if (!string.IsNullOrEmpty(linkCaption))
		{
			dictionary.Add("caption", linkCaption);
		}
		if (!string.IsNullOrEmpty(linkDescription))
		{
			dictionary.Add("description", linkDescription);
		}
		if (!string.IsNullOrEmpty(picture))
		{
			dictionary.Add("picture", picture);
		}
		if (!string.IsNullOrEmpty(mediaSource))
		{
			dictionary.Add("source", mediaSource);
		}
		if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(actionLink))
		{
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2.Add("name", actionName);
			dictionary2.Add("link", actionLink);
			dictionary.Add("actions", new Dictionary<string, object>[1] { dictionary2 });
		}
		if (!string.IsNullOrEmpty(reference))
		{
			dictionary.Add("ref", reference);
		}
		if (properties != null)
		{
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			foreach (KeyValuePair<string, string[]> property in properties)
			{
				if (property.Value.Length >= 1)
				{
					if (property.Value.Length == 1)
					{
						dictionary3.Add(property.Key, property.Value[0]);
						continue;
					}
					Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
					dictionary4.Add("text", property.Value[0]);
					dictionary4.Add("href", property.Value[1]);
					dictionary3.Add(property.Key, dictionary4);
				}
			}
			dictionary.Add("properties", dictionary3);
		}
		CallFB("FeedRequest", Json.Serialize((object)dictionary));
	}

	public void OnFeedRequestComplete(string message)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
		if (!dictionary.ContainsKey("callback_id"))
		{
			return;
		}
		Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
		string text = (string)dictionary["callback_id"];
		dictionary.Remove("callback_id");
		if (dictionary.Count > 0)
		{
			foreach (string key in dictionary.Keys)
			{
				dictionary2[key] = dictionary[key];
			}
			dictionary.Clear();
			((AbstractFacebook)this).OnFacebookResponse(text, new FBResult(Json.Serialize((object)dictionary2), (string)null));
		}
		else
		{
			((AbstractFacebook)this).OnFacebookResponse(text, new FBResult(Json.Serialize((object)dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
		}
	}

	public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
	{
		throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
	}

	public override void GetDeepLink(FacebookDelegate callback)
	{
		if (callback != null)
		{
			deepLinkDelegate = callback;
			CallFB("GetDeepLink", "");
		}
	}

	public void OnGetDeepLinkComplete(string message)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
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
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["logEvent"] = logEvent;
		if (valueToSum.HasValue)
		{
			dictionary["valueToSum"] = valueToSum.Value;
		}
		if (parameters != null)
		{
			dictionary["parameters"] = ToStringDict(parameters);
		}
		CallFB("AppEvents", Json.Serialize((object)dictionary));
	}

	public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["logPurchase"] = logPurchase;
		dictionary["currency"] = ((!string.IsNullOrEmpty(currency)) ? currency : "USD");
		if (parameters != null)
		{
			dictionary["parameters"] = ToStringDict(parameters);
		}
		CallFB("AppEvents", Json.Serialize((object)dictionary));
	}

	public override void PublishInstall(string appId, FacebookDelegate callback = null)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
		dictionary["app_id"] = appId;
		if (callback != null)
		{
			dictionary["callback_id"] = ((AbstractFacebook)this).AddFacebookDelegate(callback);
		}
		CallFB("PublishInstall", Json.Serialize((object)dictionary));
	}

	public void OnPublishInstallComplete(string message)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
		if (dictionary.ContainsKey("callback_id"))
		{
			((AbstractFacebook)this).OnFacebookResponse((string)dictionary["callback_id"], new FBResult("", (string)null));
		}
	}

	private Dictionary<string, string> ToStringDict(Dictionary<string, object> dict)
	{
		if (dict == null)
		{
			return null;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (KeyValuePair<string, object> item in dict)
		{
			dictionary[item.Key] = item.Value.ToString();
		}
		return dictionary;
	}

	private DateTime FromTimestamp(int timestamp)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
	}
}
