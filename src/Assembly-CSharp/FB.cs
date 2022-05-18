using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public sealed class FB : ScriptableObject
{
	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0000FDDC File Offset: 0x0000DFDC
	private static IFacebook FacebookImpl
	{
		get
		{
			if (FB.facebook == null)
			{
				throw new NullReferenceException("Facebook object is not yet loaded.  Did you call FB.Init()?");
			}
			return FB.facebook;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0000FDF5 File Offset: 0x0000DFF5
	public static string AppId
	{
		get
		{
			return FB.appId;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0000FDFC File Offset: 0x0000DFFC
	public static string UserId
	{
		get
		{
			if (FB.facebook == null)
			{
				return "";
			}
			return FB.facebook.UserId;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0000FE15 File Offset: 0x0000E015
	public static string AccessToken
	{
		get
		{
			if (FB.facebook == null)
			{
				return "";
			}
			return FB.facebook.AccessToken;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0000FE2E File Offset: 0x0000E02E
	public static DateTime AccessTokenExpiresAt
	{
		get
		{
			if (FB.facebook == null)
			{
				return DateTime.MinValue;
			}
			return FB.facebook.AccessTokenExpiresAt;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0000FE47 File Offset: 0x0000E047
	public static bool IsLoggedIn
	{
		get
		{
			return FB.facebook != null && FB.facebook.IsLoggedIn;
		}
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x000A30B4 File Offset: 0x000A12B4
	public static void Init(InitDelegate onInitComplete, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.Init(onInitComplete, FBSettings.AppId, FBSettings.Cookie, FBSettings.Logging, FBSettings.Status, FBSettings.Xfbml, FBSettings.FrictionlessRequests, onHideUnity, authResponse);
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x000A30E8 File Offset: 0x000A12E8
	public static void Init(InitDelegate onInitComplete, string appId, bool cookie = true, bool logging = true, bool status = true, bool xfbml = false, bool frictionlessRequests = true, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.appId = appId;
		FB.cookie = cookie;
		FB.logging = logging;
		FB.status = status;
		FB.xfbml = xfbml;
		FB.frictionlessRequests = frictionlessRequests;
		FB.authResponse = authResponse;
		FB.OnInitComplete = onInitComplete;
		FB.OnHideUnity = onHideUnity;
		if (!FB.isInitCalled)
		{
			FBBuildVersionAttribute versionAttributeOfType = FBBuildVersionAttribute.GetVersionAttributeOfType(typeof(IFacebook));
			if (versionAttributeOfType == null)
			{
				FbDebug.Warn("Cannot find Facebook SDK Version");
			}
			else
			{
				FbDebug.Info(string.Format("Using SDK {0}, Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
			}
			throw new NotImplementedException("Facebook API does not yet support this platform");
		}
		FbDebug.Warn("FB.Init() has already been called.  You only need to call this once and only once.");
		if (FB.FacebookImpl != null)
		{
			FB.OnDllLoaded();
		}
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x000A3194 File Offset: 0x000A1394
	private static void OnDllLoaded()
	{
		FBBuildVersionAttribute versionAttributeOfType = FBBuildVersionAttribute.GetVersionAttributeOfType(FB.FacebookImpl.GetType());
		if (versionAttributeOfType == null)
		{
			FbDebug.Warn("Finished loading Facebook dll, but could not find version info");
		}
		else
		{
			FbDebug.Log(string.Format("Finished loading Facebook dll. Version {0} Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
		}
		FB.FacebookImpl.Init(FB.OnInitComplete, FB.appId, FB.cookie, FB.logging, FB.status, FB.xfbml, FBSettings.ChannelUrl, FB.authResponse, FB.frictionlessRequests, FB.OnHideUnity);
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0000FE5C File Offset: 0x0000E05C
	public static void Login(string scope = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.Login(scope, callback);
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0000FE6A File Offset: 0x0000E06A
	public static void Logout()
	{
		FB.FacebookImpl.Logout();
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000A3218 File Offset: 0x000A1418
	public static void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x000A323C File Offset: 0x000A143C
	public static void Feed(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		FB.FacebookImpl.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0000FE76 File Offset: 0x0000E076
	public static void API(string query, HttpMethod method, FacebookDelegate callback = null, Dictionary<string, string> formData = null)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0000FE86 File Offset: 0x0000E086
	public static void API(string query, HttpMethod method, FacebookDelegate callback, WWWForm formData)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0000FE96 File Offset: 0x0000E096
	public static void PublishInstall(FacebookDelegate callback = null)
	{
		FB.FacebookImpl.PublishInstall(FB.AppId, callback);
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
	public static void GetDeepLink(FacebookDelegate callback)
	{
		FB.FacebookImpl.GetDeepLink(callback);
	}

	// Token: 0x04000C5A RID: 3162
	public static InitDelegate OnInitComplete;

	// Token: 0x04000C5B RID: 3163
	public static HideUnityDelegate OnHideUnity;

	// Token: 0x04000C5C RID: 3164
	private static IFacebook facebook;

	// Token: 0x04000C5D RID: 3165
	private static string authResponse;

	// Token: 0x04000C5E RID: 3166
	private static bool isInitCalled;

	// Token: 0x04000C5F RID: 3167
	private static string appId;

	// Token: 0x04000C60 RID: 3168
	private static bool cookie;

	// Token: 0x04000C61 RID: 3169
	private static bool logging;

	// Token: 0x04000C62 RID: 3170
	private static bool status;

	// Token: 0x04000C63 RID: 3171
	private static bool xfbml;

	// Token: 0x04000C64 RID: 3172
	private static bool frictionlessRequests;

	// Token: 0x020001E6 RID: 486
	public sealed class AppEvents
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0000FEBD File Offset: 0x0000E0BD
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x0000FED2 File Offset: 0x0000E0D2
		public static bool LimitEventUsage
		{
			get
			{
				return FB.facebook != null && FB.facebook.LimitEventUsage;
			}
			set
			{
				FB.facebook.LimitEventUsage = value;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0000FEDF File Offset: 0x0000E0DF
		public static void LogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0000FEEE File Offset: 0x0000E0EE
		public static void LogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
		}
	}

	// Token: 0x020001E7 RID: 487
	public sealed class Canvas
	{
		// Token: 0x06000FC5 RID: 4037 RVA: 0x000A3268 File Offset: 0x000A1468
		public static void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FB.FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0000FEFD File Offset: 0x0000E0FD
		public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetResolution(width, height, fullscreen, preferredRefreshRate, layoutParams);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0000FF0A File Offset: 0x0000E10A
		public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetAspectRatio(width, height, layoutParams);
		}
	}

	// Token: 0x020001E8 RID: 488
	public sealed class Android
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000A3290 File Offset: 0x000A1490
		public static string KeyHash
		{
			get
			{
				AndroidFacebook androidFacebook = FB.facebook as AndroidFacebook;
				if (!(androidFacebook != null))
				{
					return "";
				}
				return androidFacebook.KeyHash;
			}
		}
	}

	// Token: 0x020001E9 RID: 489
	public abstract class RemoteFacebookLoader : MonoBehaviour
	{
		// Token: 0x06000FCB RID: 4043 RVA: 0x0000FF14 File Offset: 0x0000E114
		public static IEnumerator LoadFacebookClass(string className, FB.RemoteFacebookLoader.LoadedDllCallback callback)
		{
			string url = string.Format(IntegratedPluginCanvasLocation.DllUrl, className);
			WWW www = new WWW(url);
			FbDebug.Log("loading dll: " + url);
			yield return www;
			if (www.error != null)
			{
				FbDebug.Error(www.error);
				if (FB.RemoteFacebookLoader.retryLoadCount < 3)
				{
					FB.RemoteFacebookLoader.retryLoadCount++;
				}
				www.Dispose();
				yield break;
			}
			if ("" == null)
			{
				FbDebug.Error("Could not securely load assembly from " + url);
				www.Dispose();
				yield break;
			}
			www.Dispose();
			yield break;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000FCC RID: 4044
		protected abstract string className { get; }

		// Token: 0x06000FCD RID: 4045 RVA: 0x0000FF23 File Offset: 0x0000E123
		private IEnumerator Start()
		{
			IEnumerator loader = FB.RemoteFacebookLoader.LoadFacebookClass(this.className, new FB.RemoteFacebookLoader.LoadedDllCallback(this.OnDllLoaded));
			while (loader.MoveNext())
			{
				object obj = loader.Current;
				yield return obj;
			}
			Object.Destroy(this);
			yield break;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0000FF32 File Offset: 0x0000E132
		private void OnDllLoaded(IFacebook fb)
		{
			FB.facebook = fb;
			FB.OnDllLoaded();
		}

		// Token: 0x04000C65 RID: 3173
		private const string facebookNamespace = "Facebook.";

		// Token: 0x04000C66 RID: 3174
		private const int maxRetryLoadCount = 3;

		// Token: 0x04000C67 RID: 3175
		private static int retryLoadCount;

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x06000FD2 RID: 4050
		public delegate void LoadedDllCallback(IFacebook fb);
	}

	// Token: 0x020001ED RID: 493
	public abstract class CompiledFacebookLoader : MonoBehaviour
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000FE1 RID: 4065
		protected abstract IFacebook fb { get; }

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0000FF6D File Offset: 0x0000E16D
		private void Start()
		{
			FB.facebook = this.fb;
			FB.OnDllLoaded();
			Object.Destroy(this);
		}
	}
}
