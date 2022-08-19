using System;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook
{
	// Token: 0x02000B19 RID: 2841
	internal sealed class AndroidFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x00218450 File Offset: 0x00216650
		public string KeyHash
		{
			get
			{
				return this.keyHash;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0000280F File Offset: 0x00000A0F
		// (set) Token: 0x06004F20 RID: 20256 RVA: 0x00004095 File Offset: 0x00002295
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

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x00218458 File Offset: 0x00216658
		// (set) Token: 0x06004F22 RID: 20258 RVA: 0x00218460 File Offset: 0x00216660
		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
				this.CallFB("SetLimitEventUsage", value.ToString());
			}
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x0021847B File Offset: 0x0021667B
		private void CallFB(string method, string args)
		{
			FbDebug.Error("Using Android when not on an Android build!  Doesn't Work!");
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x00218487 File Offset: 0x00216687
		protected override void OnAwake()
		{
			this.keyHash = "";
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0000280F File Offset: 0x00000A0F
		private bool IsErrorResponse(string response)
		{
			return false;
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x00218494 File Offset: 0x00216694
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
			string text = Json.Serialize(dictionary);
			this.onInitComplete = onInitComplete;
			this.CallFB("Init", text.ToString());
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x00218579 File Offset: 0x00216779
		public void OnInitComplete(string message)
		{
			this.OnLoginComplete(message);
			if (this.onInitComplete != null)
			{
				this.onInitComplete.Invoke();
			}
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x00218598 File Offset: 0x00216798
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			string args = Json.Serialize(new Dictionary<string, object>
			{
				{
					"scope",
					scope
				}
			});
			base.AddAuthDelegate(callback);
			this.CallFB("Login", args);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x002185D0 File Offset: 0x002167D0
		public void OnLoginComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("user_id"))
			{
				this.isLoggedIn = true;
				this.userId = (string)dictionary["user_id"];
				this.accessToken = (string)dictionary["access_token"];
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
			if (dictionary.ContainsKey("key_hash"))
			{
				this.keyHash = (string)dictionary["key_hash"];
				Debug.Log("proper keyhash : " + this.keyHash);
			}
			base.OnAuthResponse(new FBResult(message, null));
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x00218690 File Offset: 0x00216890
		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("access_token"))
			{
				this.accessToken = (string)dictionary["access_token"];
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x002186ED File Offset: 0x002168ED
		public override void Logout()
		{
			this.CallFB("Logout", "");
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x002186FF File Offset: 0x002168FF
		public void OnLogoutComplete(string message)
		{
			this.isLoggedIn = false;
			this.userId = "";
			this.accessToken = "";
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x00218720 File Offset: 0x00216920
		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["message"] = message;
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			if (to != null)
			{
				dictionary["to"] = string.Join(",", to);
			}
			if (!string.IsNullOrEmpty(filters))
			{
				dictionary["filters"] = filters;
			}
			if (maxRecipients != null)
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
			this.CallFB("AppRequest", Json.Serialize(dictionary));
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x002187E0 File Offset: 0x002169E0
		public void OnAppRequestsComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				string text = (string)dictionary["callback_id"];
				dictionary.Remove("callback_id");
				if (dictionary.Count > 0)
				{
					List<string> list = new List<string>(dictionary.Count - 1);
					foreach (string text2 in dictionary.Keys)
					{
						if (!text2.StartsWith("to"))
						{
							dictionary2[text2] = dictionary[text2];
						}
						else
						{
							list.Add((string)dictionary[text2]);
						}
					}
					dictionary2.Add("to", list);
					dictionary.Clear();
					base.OnFacebookResponse(text, new FBResult(Json.Serialize(dictionary2), null));
					return;
				}
				base.OnFacebookResponse(text, new FBResult(Json.Serialize(dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
			}
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x002188F8 File Offset: 0x00216AF8
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
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
				dictionary.Add("actions", new Dictionary<string, object>[]
				{
					new Dictionary<string, object>
					{
						{
							"name",
							actionName
						},
						{
							"link",
							actionLink
						}
					}
				});
			}
			if (!string.IsNullOrEmpty(reference))
			{
				dictionary.Add("ref", reference);
			}
			if (properties != null)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				foreach (KeyValuePair<string, string[]> keyValuePair in properties)
				{
					if (keyValuePair.Value.Length >= 1)
					{
						if (keyValuePair.Value.Length == 1)
						{
							dictionary2.Add(keyValuePair.Key, keyValuePair.Value[0]);
						}
						else
						{
							Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
							dictionary3.Add("text", keyValuePair.Value[0]);
							dictionary3.Add("href", keyValuePair.Value[1]);
							dictionary2.Add(keyValuePair.Key, dictionary3);
						}
					}
				}
				dictionary.Add("properties", dictionary2);
			}
			this.CallFB("FeedRequest", Json.Serialize(dictionary));
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x00218AEC File Offset: 0x00216CEC
		public void OnFeedRequestComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
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
					base.OnFacebookResponse(text, new FBResult(Json.Serialize(dictionary2), null));
					return;
				}
				base.OnFacebookResponse(text, new FBResult(Json.Serialize(dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
			}
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x00218BC4 File Offset: 0x00216DC4
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x00218BD0 File Offset: 0x00216DD0
		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback != null)
			{
				this.deepLinkDelegate = callback;
				this.CallFB("GetDeepLink", "");
			}
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x00218BEC File Offset: 0x00216DEC
		public void OnGetDeepLinkComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (this.deepLinkDelegate != null)
			{
				object obj = "";
				dictionary.TryGetValue("deep_link", out obj);
				this.deepLinkDelegate.Invoke(new FBResult(obj.ToString(), null));
			}
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00218C38 File Offset: 0x00216E38
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["logEvent"] = logEvent;
			if (valueToSum != null)
			{
				dictionary["valueToSum"] = valueToSum.Value;
			}
			if (parameters != null)
			{
				dictionary["parameters"] = this.ToStringDict(parameters);
			}
			this.CallFB("AppEvents", Json.Serialize(dictionary));
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00218CA0 File Offset: 0x00216EA0
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["logPurchase"] = logPurchase;
			dictionary["currency"] = ((!string.IsNullOrEmpty(currency)) ? currency : "USD");
			if (parameters != null)
			{
				dictionary["parameters"] = this.ToStringDict(parameters);
			}
			this.CallFB("AppEvents", Json.Serialize(dictionary));
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x00218D08 File Offset: 0x00216F08
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
			dictionary["app_id"] = appId;
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			this.CallFB("PublishInstall", Json.Serialize(dictionary));
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x00218D50 File Offset: 0x00216F50
		public void OnPublishInstallComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				base.OnFacebookResponse((string)dictionary["callback_id"], new FBResult("", null));
			}
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00218D98 File Offset: 0x00216F98
		private Dictionary<string, string> ToStringDict(Dictionary<string, object> dict)
		{
			if (dict == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in dict)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value.ToString();
			}
			return dictionary;
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00218E04 File Offset: 0x00217004
		private DateTime FromTimestamp(int timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)timestamp);
		}

		// Token: 0x04004E61 RID: 20065
		public const int BrowserDialogMode = 0;

		// Token: 0x04004E62 RID: 20066
		private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

		// Token: 0x04004E63 RID: 20067
		private const string CallbackIdKey = "callback_id";

		// Token: 0x04004E64 RID: 20068
		private string keyHash;

		// Token: 0x04004E65 RID: 20069
		private FacebookDelegate deepLinkDelegate;

		// Token: 0x04004E66 RID: 20070
		private InitDelegate onInitComplete;
	}
}
