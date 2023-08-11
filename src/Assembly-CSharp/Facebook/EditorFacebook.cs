using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook;

internal class EditorFacebook : AbstractFacebook, IFacebook
{
	private AbstractFacebook fb;

	private FacebookDelegate loginCallback;

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
		}
	}

	protected override void OnAwake()
	{
		((MonoBehaviour)this).StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", OnDllLoaded));
	}

	public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
	{
		((MonoBehaviour)this).StartCoroutine(OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
	}

	private IEnumerator OnInit(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
	{
		while ((Object)(object)fb == (Object)null)
		{
			yield return null;
		}
		fb.Init(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate);
		if (onInitComplete != null)
		{
			onInitComplete.Invoke();
		}
	}

	private void OnDllLoaded(IFacebook fb)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		this.fb = (AbstractFacebook)fb;
	}

	public override void Login(string scope = "", FacebookDelegate callback = null)
	{
		((AbstractFacebook)this).AddAuthDelegate(callback);
		FBComponentFactory.GetComponent<EditorFacebookAccessToken>((IfNotExist)0);
	}

	public override void Logout()
	{
		base.isLoggedIn = false;
		base.userId = "";
		base.accessToken = "";
		fb.UserId = "";
		fb.AccessToken = "";
	}

	public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		fb.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
	}

	public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
	}

	public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
	{
		FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
	}

	public override void GetAuthResponse(FacebookDelegate callback = null)
	{
		fb.GetAuthResponse(callback);
	}

	public override void PublishInstall(string appId, FacebookDelegate callback = null)
	{
	}

	public override void GetDeepLink(FacebookDelegate callback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		FbDebug.Info("No Deep Linking in the Editor");
		if (callback != null)
		{
			callback.Invoke(new FBResult("<platform dependent>", (string)null));
		}
	}

	public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
	{
		FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
	}

	public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
	{
		FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
	}

	public void MockLoginCallback(FBResult result)
	{
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		Object.Destroy((Object)(object)FBComponentFactory.GetComponent<EditorFacebookAccessToken>((IfNotExist)0));
		if (result.Error != null)
		{
			BadAccessToken(result.Error);
			return;
		}
		try
		{
			List<object> obj = (List<object>)Json.Deserialize(result.Text);
			List<string> list = new List<string>();
			foreach (object item in obj)
			{
				if (item is Dictionary<string, object>)
				{
					Dictionary<string, object> dictionary = (Dictionary<string, object>)item;
					if (dictionary.ContainsKey("body"))
					{
						list.Add((string)dictionary["body"]);
					}
				}
			}
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)Json.Deserialize(list[0]);
			Dictionary<string, object> dictionary3 = (Dictionary<string, object>)Json.Deserialize(list[1]);
			if (FB.AppId != (string)dictionary3["id"])
			{
				BadAccessToken("Access token is not for current app id: " + FB.AppId);
				return;
			}
			base.userId = (string)dictionary2["id"];
			fb.UserId = base.userId;
			fb.AccessToken = base.accessToken;
			base.isLoggedIn = true;
			((AbstractFacebook)this).OnAuthResponse(new FBResult("", (string)null));
		}
		catch (Exception ex)
		{
			BadAccessToken("Could not get data from access token: " + ex.Message);
		}
	}

	public void MockCancelledLoginCallback()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		((AbstractFacebook)this).OnAuthResponse(new FBResult("", (string)null));
	}

	private void BadAccessToken(string error)
	{
		FbDebug.Error(error);
		base.userId = "";
		fb.UserId = "";
		base.accessToken = "";
		fb.AccessToken = "";
		FBComponentFactory.GetComponent<EditorFacebookAccessToken>((IfNotExist)0);
	}
}
