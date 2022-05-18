using System;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook
{
	// Token: 0x02000E88 RID: 3720
	internal sealed class AndroidFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06005930 RID: 22832 RVA: 0x0003F642 File Offset: 0x0003D842
		public string KeyHash
		{
			get
			{
				return this.keyHash;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06005931 RID: 22833 RVA: 0x00004050 File Offset: 0x00002250
		// (set) Token: 0x06005932 RID: 22834 RVA: 0x000042DD File Offset: 0x000024DD
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

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x0003F64A File Offset: 0x0003D84A
		// (set) Token: 0x06005934 RID: 22836 RVA: 0x0003F652 File Offset: 0x0003D852
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

		// Token: 0x06005935 RID: 22837 RVA: 0x0003F66D File Offset: 0x0003D86D
		private void CallFB(string method, string args)
		{
			FbDebug.Error("Using Android when not on an Android build!  Doesn't Work!");
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0003F679 File Offset: 0x0003D879
		protected override void OnAwake()
		{
			this.keyHash = "";
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x00004050 File Offset: 0x00002250
		private bool IsErrorResponse(string response)
		{
			return false;
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x002484EC File Offset: 0x002466EC
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

		// Token: 0x06005939 RID: 22841 RVA: 0x0003F686 File Offset: 0x0003D886
		public void OnInitComplete(string message)
		{
			this.OnLoginComplete(message);
			if (this.onInitComplete != null)
			{
				this.onInitComplete.Invoke();
			}
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x002485D4 File Offset: 0x002467D4
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

		// Token: 0x0600593B RID: 22843 RVA: 0x0024860C File Offset: 0x0024680C
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

		// Token: 0x0600593C RID: 22844 RVA: 0x002486CC File Offset: 0x002468CC
		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("access_token"))
			{
				this.accessToken = (string)dictionary["access_token"];
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x0003F6A2 File Offset: 0x0003D8A2
		public override void Logout()
		{
			this.CallFB("Logout", "");
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x0003F6B4 File Offset: 0x0003D8B4
		public void OnLogoutComplete(string message)
		{
			this.isLoggedIn = false;
			this.userId = "";
			this.accessToken = "";
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0024872C File Offset: 0x0024692C
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

		// Token: 0x06005940 RID: 22848 RVA: 0x002487EC File Offset: 0x002469EC
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

		// Token: 0x06005941 RID: 22849 RVA: 0x00248904 File Offset: 0x00246B04
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

		// Token: 0x06005942 RID: 22850 RVA: 0x00248AF8 File Offset: 0x00246CF8
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

		// Token: 0x06005943 RID: 22851 RVA: 0x0003F6D3 File Offset: 0x0003D8D3
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x0003F6DF File Offset: 0x0003D8DF
		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback != null)
			{
				this.deepLinkDelegate = callback;
				this.CallFB("GetDeepLink", "");
			}
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x00248BD0 File Offset: 0x00246DD0
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

		// Token: 0x06005946 RID: 22854 RVA: 0x00248C1C File Offset: 0x00246E1C
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

		// Token: 0x06005947 RID: 22855 RVA: 0x00248C84 File Offset: 0x00246E84
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

		// Token: 0x06005948 RID: 22856 RVA: 0x00248CEC File Offset: 0x00246EEC
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

		// Token: 0x06005949 RID: 22857 RVA: 0x00248D34 File Offset: 0x00246F34
		public void OnPublishInstallComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				base.OnFacebookResponse((string)dictionary["callback_id"], new FBResult("", null));
			}
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x00248D7C File Offset: 0x00246F7C
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

		// Token: 0x0600594B RID: 22859 RVA: 0x00248DE8 File Offset: 0x00246FE8
		private DateTime FromTimestamp(int timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)timestamp);
		}

		// Token: 0x040058BE RID: 22718
		public const int BrowserDialogMode = 0;

		// Token: 0x040058BF RID: 22719
		private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

		// Token: 0x040058C0 RID: 22720
		private const string CallbackIdKey = "callback_id";

		// Token: 0x040058C1 RID: 22721
		private string keyHash;

		// Token: 0x040058C2 RID: 22722
		private FacebookDelegate deepLinkDelegate;

		// Token: 0x040058C3 RID: 22723
		private InitDelegate onInitComplete;
	}
}
