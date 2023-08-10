using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

public sealed class InteractiveConsole : MonoBehaviour
{
	private bool isInit;

	public string FriendSelectorTitle = "";

	public string FriendSelectorMessage = "Derp";

	public string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	public string FriendSelectorData = "{}";

	public string FriendSelectorExcludeIds = "";

	public string FriendSelectorMax = "";

	public string DirectRequestTitle = "";

	public string DirectRequestMessage = "Herp";

	private string DirectRequestTo = "";

	public string FeedToId = "";

	public string FeedLink = "";

	public string FeedLinkName = "";

	public string FeedLinkCaption = "";

	public string FeedLinkDescription = "";

	public string FeedPicture = "";

	public string FeedMediaSource = "";

	public string FeedActionName = "";

	public string FeedActionLink = "";

	public string FeedReference = "";

	public bool IncludeFeedProperties;

	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	public string PayProduct = "";

	public string ApiQuery = "";

	public float PlayerLevel = 1f;

	public string Width = "800";

	public string Height = "600";

	public bool CenterHorizontal = true;

	public bool CenterVertical;

	public string Top = "10";

	public string Left = "10";

	private string status = "Ready";

	private string lastResponse = "";

	public GUIStyle textStyle = new GUIStyle();

	private Texture2D lastResponseTexture;

	private Vector2 scrollPosition = Vector2.zero;

	private int buttonHeight = 24;

	private int mainWindowWidth = 500;

	private int mainWindowFullWidth = 530;

	private int TextWindowHeight => Screen.height;

	private void CallFBInit()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_001e: Expected O, but got Unknown
		FB.Init(new InitDelegate(OnInitComplete), new HideUnityDelegate(OnHideUnity));
	}

	private void OnInitComplete()
	{
		Debug.Log((object)("FB.Init completed: Is user logged in? " + FB.IsLoggedIn));
		isInit = true;
	}

	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log((object)("Is game showing? " + isGameShown));
	}

	private void CallFBLogin()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		FB.Login("email,publish_actions", new FacebookDelegate(LoginCallback));
	}

	private void LoginCallback(FBResult result)
	{
		if (result.Error != null)
		{
			lastResponse = "Error Response:\n" + result.Error;
		}
		else if (!FB.IsLoggedIn)
		{
			lastResponse = "Login cancelled by Player";
		}
		else
		{
			lastResponse = "Login was successful!";
		}
	}

	private void CallFBLogout()
	{
		FB.Logout();
	}

	private void CallFBPublishInstall()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		FB.PublishInstall(new FacebookDelegate(PublishComplete));
	}

	private void PublishComplete(FBResult result)
	{
		Debug.Log((object)("publish response: " + result.Text));
	}

	private void CallAppRequestAsFriendSelector()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		int? maxRecipients = null;
		if (FriendSelectorMax != "")
		{
			try
			{
				maxRecipients = int.Parse(FriendSelectorMax);
			}
			catch (Exception ex)
			{
				status = ex.Message;
			}
		}
		string[] excludeIds = ((FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(new char[1] { ',' }));
		FB.AppRequest(FriendSelectorMessage, null, FriendSelectorFilters, excludeIds, maxRecipients, FriendSelectorData, FriendSelectorTitle, new FacebookDelegate(Callback));
	}

	private void CallAppRequestAsDirectRequest()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		if (DirectRequestTo == "")
		{
			throw new ArgumentException("\"To Comma Ids\" must be specificed", "to");
		}
		FB.AppRequest(DirectRequestMessage, DirectRequestTo.Split(new char[1] { ',' }), "", null, null, "", DirectRequestTitle, new FacebookDelegate(Callback));
	}

	private void CallFBFeed()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		Dictionary<string, string[]> properties = null;
		if (IncludeFeedProperties)
		{
			properties = FeedProperties;
		}
		FB.Feed(FeedToId, FeedLink, FeedLinkName, FeedLinkCaption, FeedLinkDescription, FeedPicture, FeedMediaSource, FeedActionName, FeedActionLink, FeedReference, properties, new FacebookDelegate(Callback));
	}

	private void CallFBPay()
	{
		FB.Canvas.Pay(PayProduct);
	}

	private void CallFBAPI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		FB.API(ApiQuery, HttpMethod.GET, new FacebookDelegate(Callback));
	}

	private void CallFBGetDeepLink()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		FB.GetDeepLink(new FacebookDelegate(Callback));
	}

	public void CallAppEventLogEvent()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_level"] = "Player Level";
		FB.AppEvents.LogEvent("fb_mobile_level_achieved", PlayerLevel, dictionary);
		PlayerLevel += 1f;
	}

	public void CallCanvasSetResolution()
	{
		if (!int.TryParse(Width, out var result))
		{
			result = 800;
		}
		if (!int.TryParse(Height, out var result2))
		{
			result2 = 600;
		}
		if (!float.TryParse(Top, out var result3))
		{
			result3 = 0f;
		}
		if (!float.TryParse(Left, out var result4))
		{
			result4 = 0f;
		}
		if (CenterHorizontal && CenterVertical)
		{
			FB.Canvas.SetResolution(result, result2, false, 0, FBScreen.CenterVertical(), FBScreen.CenterHorizontal());
		}
		else if (CenterHorizontal)
		{
			FB.Canvas.SetResolution(result, result2, false, 0, FBScreen.Top(result3), FBScreen.CenterHorizontal());
		}
		else if (CenterVertical)
		{
			FB.Canvas.SetResolution(result, result2, false, 0, FBScreen.CenterVertical(), FBScreen.Left(result4));
		}
		else
		{
			FB.Canvas.SetResolution(result, result2, false, 0, FBScreen.Top(result3), FBScreen.Left(result4));
		}
	}

	private void Awake()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		textStyle.alignment = (TextAnchor)0;
		textStyle.wordWrap = true;
		textStyle.padding = new RectOffset(10, 10, 10, 10);
		textStyle.stretchHeight = true;
		textStyle.stretchWidth = false;
		FeedProperties.Add("key1", new string[1] { "valueString1" });
		FeedProperties.Add("key2", new string[2] { "valueString2", "http://www.facebook.com" });
	}

	private void OnGUI()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		if (IsHorizontalLayout())
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		}
		GUILayout.Space(5f);
		GUILayout.Box("Status: " + status, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.MinWidth((float)mainWindowWidth) });
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.MinWidth((float)mainWindowFullWidth) });
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUI.enabled = !isInit;
		if (Button("FB.Init"))
		{
			CallFBInit();
			status = "FB.Init() called with " + FB.AppId;
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUI.enabled = isInit;
		if (Button("Login"))
		{
			CallFBLogin();
			status = "Login called";
		}
		GUILayout.EndHorizontal();
		GUI.enabled = FB.IsLoggedIn;
		GUILayout.Space(10f);
		LabelAndTextField("Title (optional): ", ref FriendSelectorTitle);
		LabelAndTextField("Message: ", ref FriendSelectorMessage);
		LabelAndTextField("Exclude Ids (optional): ", ref FriendSelectorExcludeIds);
		LabelAndTextField("Filters (optional): ", ref FriendSelectorFilters);
		LabelAndTextField("Max Recipients (optional): ", ref FriendSelectorMax);
		LabelAndTextField("Data (optional): ", ref FriendSelectorData);
		if (Button("Open Friend Selector"))
		{
			try
			{
				CallAppRequestAsFriendSelector();
				status = "Friend Selector called";
			}
			catch (Exception ex)
			{
				status = ex.Message;
			}
		}
		GUILayout.Space(10f);
		LabelAndTextField("Title (optional): ", ref DirectRequestTitle);
		LabelAndTextField("Message: ", ref DirectRequestMessage);
		LabelAndTextField("To Comma Ids: ", ref DirectRequestTo);
		if (Button("Open Direct Request"))
		{
			try
			{
				CallAppRequestAsDirectRequest();
				status = "Direct Request called";
			}
			catch (Exception ex2)
			{
				status = ex2.Message;
			}
		}
		GUILayout.Space(10f);
		LabelAndTextField("To Id (optional): ", ref FeedToId);
		LabelAndTextField("Link (optional): ", ref FeedLink);
		LabelAndTextField("Link Name (optional): ", ref FeedLinkName);
		LabelAndTextField("Link Desc (optional): ", ref FeedLinkDescription);
		LabelAndTextField("Link Caption (optional): ", ref FeedLinkCaption);
		LabelAndTextField("Picture (optional): ", ref FeedPicture);
		LabelAndTextField("Media Source (optional): ", ref FeedMediaSource);
		LabelAndTextField("Action Name (optional): ", ref FeedActionName);
		LabelAndTextField("Action Link (optional): ", ref FeedActionLink);
		LabelAndTextField("Reference (optional): ", ref FeedReference);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Properties (optional)", (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(150f) });
		IncludeFeedProperties = GUILayout.Toggle(IncludeFeedProperties, "Include", Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
		if (Button("Open Feed Dialog"))
		{
			try
			{
				CallFBFeed();
				status = "Feed dialog called";
			}
			catch (Exception ex3)
			{
				status = ex3.Message;
			}
		}
		GUILayout.Space(10f);
		LabelAndTextField("API: ", ref ApiQuery);
		if (Button("Call API"))
		{
			status = "API called";
			CallFBAPI();
		}
		GUILayout.Space(10f);
		if (Button("Take & upload screenshot"))
		{
			status = "Take screenshot";
			((MonoBehaviour)this).StartCoroutine(TakeScreenshot());
		}
		if (Button("Get Deep Link"))
		{
			CallFBGetDeepLink();
		}
		GUILayout.Space(10f);
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		if (IsHorizontalLayout())
		{
			GUILayout.EndVertical();
		}
		GUI.enabled = true;
		Rect rect = GUILayoutUtility.GetRect(640f, (float)TextWindowHeight);
		GUI.TextArea(rect, string.Format(" AppId: {0} \n Facebook Dll: {1} \n UserId: {2}\n IsLoggedIn: {3}\n AccessToken: {4}\n AccessTokenExpiresAt: {5}\n {6}", FB.AppId, isInit ? "Loaded Successfully" : "Not Loaded", FB.UserId, FB.IsLoggedIn, FB.AccessToken, FB.AccessTokenExpiresAt, lastResponse), textStyle);
		if ((Object)(object)lastResponseTexture != (Object)null)
		{
			float num = ((Rect)(ref rect)).y + 200f;
			if ((float)(Screen.height - ((Texture)lastResponseTexture).height) < num)
			{
				num = Screen.height - ((Texture)lastResponseTexture).height;
			}
			GUI.Label(new Rect(((Rect)(ref rect)).x + 5f, num, (float)((Texture)lastResponseTexture).width, (float)((Texture)lastResponseTexture).height), (Texture)(object)lastResponseTexture);
		}
		if (IsHorizontalLayout())
		{
			GUILayout.EndHorizontal();
		}
	}

	private void Callback(FBResult result)
	{
		lastResponseTexture = null;
		if (!string.IsNullOrEmpty(result.Error))
		{
			lastResponse = "Error Response:\n" + result.Error;
			return;
		}
		if (!ApiQuery.Contains("/picture"))
		{
			lastResponse = "Success Response:\n" + result.Text;
			return;
		}
		lastResponseTexture = result.Texture;
		lastResponse = "Success Response:\n";
	}

	private IEnumerator TakeScreenshot()
	{
		yield return (object)new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D val = new Texture2D(width, height, (TextureFormat)3, false);
		val.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		val.Apply();
		byte[] array = ImageConversion.EncodeToPNG(val);
		WWWForm val2 = new WWWForm();
		val2.AddBinaryData("image", array, "InteractiveConsole.png");
		val2.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
		FB.API("me/photos", HttpMethod.POST, new FacebookDelegate(Callback), val2);
	}

	private bool Button(string label)
	{
		return GUILayout.Button(label, (GUILayoutOption[])(object)new GUILayoutOption[2]
		{
			GUILayout.MinHeight((float)buttonHeight),
			GUILayout.MaxWidth((float)mainWindowWidth)
		});
	}

	private void LabelAndTextField(string label, ref string text)
	{
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(label, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.MaxWidth(150f) });
		text = GUILayout.TextField(text, Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
	}

	private bool IsHorizontalLayout()
	{
		return true;
	}
}
