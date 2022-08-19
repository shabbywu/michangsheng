using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

// Token: 0x02000121 RID: 289
public sealed class FB : ScriptableObject
{
	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00052696 File Offset: 0x00050896
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

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000526AF File Offset: 0x000508AF
	public static string AppId
	{
		get
		{
			return FB.appId;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000526B6 File Offset: 0x000508B6
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

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000526CF File Offset: 0x000508CF
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

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000DCC RID: 3532 RVA: 0x000526E8 File Offset: 0x000508E8
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

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00052701 File Offset: 0x00050901
	public static bool IsLoggedIn
	{
		get
		{
			return FB.facebook != null && FB.facebook.IsLoggedIn;
		}
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00052718 File Offset: 0x00050918
	public static void Init(InitDelegate onInitComplete, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.Init(onInitComplete, FBSettings.AppId, FBSettings.Cookie, FBSettings.Logging, FBSettings.Status, FBSettings.Xfbml, FBSettings.FrictionlessRequests, onHideUnity, authResponse);
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0005274C File Offset: 0x0005094C
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

	// Token: 0x06000DD0 RID: 3536 RVA: 0x000527F8 File Offset: 0x000509F8
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

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0005287B File Offset: 0x00050A7B
	public static void Login(string scope = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.Login(scope, callback);
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00052889 File Offset: 0x00050A89
	public static void Logout()
	{
		FB.FacebookImpl.Logout();
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00052898 File Offset: 0x00050A98
	public static void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x000528BC File Offset: 0x00050ABC
	public static void Feed(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		FB.FacebookImpl.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000528E7 File Offset: 0x00050AE7
	public static void API(string query, HttpMethod method, FacebookDelegate callback = null, Dictionary<string, string> formData = null)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000528F7 File Offset: 0x00050AF7
	public static void API(string query, HttpMethod method, FacebookDelegate callback, WWWForm formData)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00052907 File Offset: 0x00050B07
	public static void PublishInstall(FacebookDelegate callback = null)
	{
		FB.FacebookImpl.PublishInstall(FB.AppId, callback);
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x00052919 File Offset: 0x00050B19
	public static void GetDeepLink(FacebookDelegate callback)
	{
		FB.FacebookImpl.GetDeepLink(callback);
	}

	// Token: 0x040009D0 RID: 2512
	public static InitDelegate OnInitComplete;

	// Token: 0x040009D1 RID: 2513
	public static HideUnityDelegate OnHideUnity;

	// Token: 0x040009D2 RID: 2514
	private static IFacebook facebook;

	// Token: 0x040009D3 RID: 2515
	private static string authResponse;

	// Token: 0x040009D4 RID: 2516
	private static bool isInitCalled;

	// Token: 0x040009D5 RID: 2517
	private static string appId;

	// Token: 0x040009D6 RID: 2518
	private static bool cookie;

	// Token: 0x040009D7 RID: 2519
	private static bool logging;

	// Token: 0x040009D8 RID: 2520
	private static bool status;

	// Token: 0x040009D9 RID: 2521
	private static bool xfbml;

	// Token: 0x040009DA RID: 2522
	private static bool frictionlessRequests;

	// Token: 0x02001288 RID: 4744
	public sealed class AppEvents
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600799B RID: 31131 RVA: 0x002BBAA2 File Offset: 0x002B9CA2
		// (set) Token: 0x0600799C RID: 31132 RVA: 0x002BBAB7 File Offset: 0x002B9CB7
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

		// Token: 0x0600799D RID: 31133 RVA: 0x002BBAC4 File Offset: 0x002B9CC4
		public static void LogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x002BBAD3 File Offset: 0x002B9CD3
		public static void LogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
		}
	}

	// Token: 0x02001289 RID: 4745
	public sealed class Canvas
	{
		// Token: 0x060079A0 RID: 31136 RVA: 0x002BBAE4 File Offset: 0x002B9CE4
		public static void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FB.FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x002BBB09 File Offset: 0x002B9D09
		public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetResolution(width, height, fullscreen, preferredRefreshRate, layoutParams);
		}

		// Token: 0x060079A2 RID: 31138 RVA: 0x002BBB16 File Offset: 0x002B9D16
		public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetAspectRatio(width, height, layoutParams);
		}
	}

	// Token: 0x0200128A RID: 4746
	public sealed class Android
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060079A4 RID: 31140 RVA: 0x002BBB20 File Offset: 0x002B9D20
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

	// Token: 0x0200128B RID: 4747
	public abstract class RemoteFacebookLoader : MonoBehaviour
	{
		// Token: 0x060079A6 RID: 31142 RVA: 0x002BBB4D File Offset: 0x002B9D4D
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

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060079A7 RID: 31143
		protected abstract string className { get; }

		// Token: 0x060079A8 RID: 31144 RVA: 0x002BBB5C File Offset: 0x002B9D5C
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

		// Token: 0x060079A9 RID: 31145 RVA: 0x002BBB6B File Offset: 0x002B9D6B
		private void OnDllLoaded(IFacebook fb)
		{
			FB.facebook = fb;
			FB.OnDllLoaded();
		}

		// Token: 0x040065D9 RID: 26073
		private const string facebookNamespace = "Facebook.";

		// Token: 0x040065DA RID: 26074
		private const int maxRetryLoadCount = 3;

		// Token: 0x040065DB RID: 26075
		private static int retryLoadCount;

		// Token: 0x0200174E RID: 5966
		// (Invoke) Token: 0x06008977 RID: 35191
		public delegate void LoadedDllCallback(IFacebook fb);
	}

	// Token: 0x0200128C RID: 4748
	public abstract class CompiledFacebookLoader : MonoBehaviour
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060079AC RID: 31148
		protected abstract IFacebook fb { get; }

		// Token: 0x060079AD RID: 31149 RVA: 0x002BBB78 File Offset: 0x002B9D78
		private void Start()
		{
			FB.facebook = this.fb;
			FB.OnDllLoaded();
			Object.Destroy(this);
		}
	}
}
