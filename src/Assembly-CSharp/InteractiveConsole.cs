using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public sealed class InteractiveConsole : MonoBehaviour
{
	// Token: 0x06000F84 RID: 3972 RVA: 0x0000FC1C File Offset: 0x0000DE1C
	private void CallFBInit()
	{
		FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000A21B8 File Offset: 0x000A03B8
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn.ToString());
		this.isInit = true;
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x0000FC3C File Offset: 0x0000DE3C
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown.ToString());
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x0000FC54 File Offset: 0x0000DE54
	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", new FacebookDelegate(this.LoginCallback));
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0000FC6C File Offset: 0x0000DE6C
	private void LoginCallback(FBResult result)
	{
		if (result.Error != null)
		{
			this.lastResponse = "Error Response:\n" + result.Error;
			return;
		}
		if (!FB.IsLoggedIn)
		{
			this.lastResponse = "Login cancelled by Player";
			return;
		}
		this.lastResponse = "Login was successful!";
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0000FCAB File Offset: 0x0000DEAB
	private void CallFBLogout()
	{
		FB.Logout();
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
	private void CallFBPublishInstall()
	{
		FB.PublishInstall(new FacebookDelegate(this.PublishComplete));
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x0000FCC5 File Offset: 0x0000DEC5
	private void PublishComplete(FBResult result)
	{
		Debug.Log("publish response: " + result.Text);
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x000A21E8 File Offset: 0x000A03E8
	private void CallAppRequestAsFriendSelector()
	{
		int? maxRecipients = null;
		if (this.FriendSelectorMax != "")
		{
			try
			{
				maxRecipients = new int?(int.Parse(this.FriendSelectorMax));
			}
			catch (Exception ex)
			{
				this.status = ex.Message;
			}
		}
		string[] excludeIds = (this.FriendSelectorExcludeIds == "") ? null : this.FriendSelectorExcludeIds.Split(new char[]
		{
			','
		});
		FB.AppRequest(this.FriendSelectorMessage, null, this.FriendSelectorFilters, excludeIds, maxRecipients, this.FriendSelectorData, this.FriendSelectorTitle, new FacebookDelegate(this.Callback));
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x000A229C File Offset: 0x000A049C
	private void CallAppRequestAsDirectRequest()
	{
		if (this.DirectRequestTo == "")
		{
			throw new ArgumentException("\"To Comma Ids\" must be specificed", "to");
		}
		FB.AppRequest(this.DirectRequestMessage, this.DirectRequestTo.Split(new char[]
		{
			','
		}), "", null, null, "", this.DirectRequestTitle, new FacebookDelegate(this.Callback));
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x000A2314 File Offset: 0x000A0514
	private void CallFBFeed()
	{
		Dictionary<string, string[]> properties = null;
		if (this.IncludeFeedProperties)
		{
			properties = this.FeedProperties;
		}
		FB.Feed(this.FeedToId, this.FeedLink, this.FeedLinkName, this.FeedLinkCaption, this.FeedLinkDescription, this.FeedPicture, this.FeedMediaSource, this.FeedActionName, this.FeedActionLink, this.FeedReference, properties, new FacebookDelegate(this.Callback));
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x000A2380 File Offset: 0x000A0580
	private void CallFBPay()
	{
		FB.Canvas.Pay(this.PayProduct, "purchaseitem", 1, null, null, null, null, null, null);
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x0000FCDC File Offset: 0x0000DEDC
	private void CallFBAPI()
	{
		FB.API(this.ApiQuery, HttpMethod.GET, new FacebookDelegate(this.Callback), null);
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x0000FCFB File Offset: 0x0000DEFB
	private void CallFBGetDeepLink()
	{
		FB.GetDeepLink(new FacebookDelegate(this.Callback));
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x000A23B4 File Offset: 0x000A05B4
	public void CallAppEventLogEvent()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_level"] = "Player Level";
		FB.AppEvents.LogEvent("fb_mobile_level_achieved", new float?(this.PlayerLevel), dictionary);
		this.PlayerLevel += 1f;
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x000A2400 File Offset: 0x000A0600
	public void CallCanvasSetResolution()
	{
		int width;
		if (!int.TryParse(this.Width, out width))
		{
			width = 800;
		}
		int height;
		if (!int.TryParse(this.Height, out height))
		{
			height = 600;
		}
		float amount;
		if (!float.TryParse(this.Top, out amount))
		{
			amount = 0f;
		}
		float amount2;
		if (!float.TryParse(this.Left, out amount2))
		{
			amount2 = 0f;
		}
		if (this.CenterHorizontal && this.CenterVertical)
		{
			FB.Canvas.SetResolution(width, height, false, 0, new FBScreen.Layout[]
			{
				FBScreen.CenterVertical(),
				FBScreen.CenterHorizontal()
			});
			return;
		}
		if (this.CenterHorizontal)
		{
			FB.Canvas.SetResolution(width, height, false, 0, new FBScreen.Layout[]
			{
				FBScreen.Top(amount),
				FBScreen.CenterHorizontal()
			});
			return;
		}
		if (this.CenterVertical)
		{
			FB.Canvas.SetResolution(width, height, false, 0, new FBScreen.Layout[]
			{
				FBScreen.CenterVertical(),
				FBScreen.Left(amount2)
			});
			return;
		}
		FB.Canvas.SetResolution(width, height, false, 0, new FBScreen.Layout[]
		{
			FBScreen.Top(amount),
			FBScreen.Left(amount2)
		});
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0000FD0E File Offset: 0x0000DF0E
	private int TextWindowHeight
	{
		get
		{
			return Screen.height;
		}
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x000A2504 File Offset: 0x000A0704
	private void Awake()
	{
		this.textStyle.alignment = 0;
		this.textStyle.wordWrap = true;
		this.textStyle.padding = new RectOffset(10, 10, 10, 10);
		this.textStyle.stretchHeight = true;
		this.textStyle.stretchWidth = false;
		this.FeedProperties.Add("key1", new string[]
		{
			"valueString1"
		});
		this.FeedProperties.Add("key2", new string[]
		{
			"valueString2",
			"http://www.facebook.com"
		});
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x000A25A0 File Offset: 0x000A07A0
	private void OnGUI()
	{
		if (this.IsHorizontalLayout())
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		}
		GUILayout.Space(5f);
		GUILayout.Box("Status: " + this.status, new GUILayoutOption[]
		{
			GUILayout.MinWidth((float)this.mainWindowWidth)
		});
		this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[]
		{
			GUILayout.MinWidth((float)this.mainWindowFullWidth)
		});
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUI.enabled = !this.isInit;
		if (this.Button("FB.Init"))
		{
			this.CallFBInit();
			this.status = "FB.Init() called with " + FB.AppId;
		}
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUI.enabled = this.isInit;
		if (this.Button("Login"))
		{
			this.CallFBLogin();
			this.status = "Login called";
		}
		GUILayout.EndHorizontal();
		GUI.enabled = FB.IsLoggedIn;
		GUILayout.Space(10f);
		this.LabelAndTextField("Title (optional): ", ref this.FriendSelectorTitle);
		this.LabelAndTextField("Message: ", ref this.FriendSelectorMessage);
		this.LabelAndTextField("Exclude Ids (optional): ", ref this.FriendSelectorExcludeIds);
		this.LabelAndTextField("Filters (optional): ", ref this.FriendSelectorFilters);
		this.LabelAndTextField("Max Recipients (optional): ", ref this.FriendSelectorMax);
		this.LabelAndTextField("Data (optional): ", ref this.FriendSelectorData);
		if (this.Button("Open Friend Selector"))
		{
			try
			{
				this.CallAppRequestAsFriendSelector();
				this.status = "Friend Selector called";
			}
			catch (Exception ex)
			{
				this.status = ex.Message;
			}
		}
		GUILayout.Space(10f);
		this.LabelAndTextField("Title (optional): ", ref this.DirectRequestTitle);
		this.LabelAndTextField("Message: ", ref this.DirectRequestMessage);
		this.LabelAndTextField("To Comma Ids: ", ref this.DirectRequestTo);
		if (this.Button("Open Direct Request"))
		{
			try
			{
				this.CallAppRequestAsDirectRequest();
				this.status = "Direct Request called";
			}
			catch (Exception ex2)
			{
				this.status = ex2.Message;
			}
		}
		GUILayout.Space(10f);
		this.LabelAndTextField("To Id (optional): ", ref this.FeedToId);
		this.LabelAndTextField("Link (optional): ", ref this.FeedLink);
		this.LabelAndTextField("Link Name (optional): ", ref this.FeedLinkName);
		this.LabelAndTextField("Link Desc (optional): ", ref this.FeedLinkDescription);
		this.LabelAndTextField("Link Caption (optional): ", ref this.FeedLinkCaption);
		this.LabelAndTextField("Picture (optional): ", ref this.FeedPicture);
		this.LabelAndTextField("Media Source (optional): ", ref this.FeedMediaSource);
		this.LabelAndTextField("Action Name (optional): ", ref this.FeedActionName);
		this.LabelAndTextField("Action Link (optional): ", ref this.FeedActionLink);
		this.LabelAndTextField("Reference (optional): ", ref this.FeedReference);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label("Properties (optional)", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		});
		this.IncludeFeedProperties = GUILayout.Toggle(this.IncludeFeedProperties, "Include", Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
		if (this.Button("Open Feed Dialog"))
		{
			try
			{
				this.CallFBFeed();
				this.status = "Feed dialog called";
			}
			catch (Exception ex3)
			{
				this.status = ex3.Message;
			}
		}
		GUILayout.Space(10f);
		this.LabelAndTextField("API: ", ref this.ApiQuery);
		if (this.Button("Call API"))
		{
			this.status = "API called";
			this.CallFBAPI();
		}
		GUILayout.Space(10f);
		if (this.Button("Take & upload screenshot"))
		{
			this.status = "Take screenshot";
			base.StartCoroutine(this.TakeScreenshot());
		}
		if (this.Button("Get Deep Link"))
		{
			this.CallFBGetDeepLink();
		}
		GUILayout.Space(10f);
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		if (this.IsHorizontalLayout())
		{
			GUILayout.EndVertical();
		}
		GUI.enabled = true;
		Rect rect = GUILayoutUtility.GetRect(640f, (float)this.TextWindowHeight);
		GUI.TextArea(rect, string.Format(" AppId: {0} \n Facebook Dll: {1} \n UserId: {2}\n IsLoggedIn: {3}\n AccessToken: {4}\n AccessTokenExpiresAt: {5}\n {6}", new object[]
		{
			FB.AppId,
			this.isInit ? "Loaded Successfully" : "Not Loaded",
			FB.UserId,
			FB.IsLoggedIn,
			FB.AccessToken,
			FB.AccessTokenExpiresAt,
			this.lastResponse
		}), this.textStyle);
		if (this.lastResponseTexture != null)
		{
			float num = rect.y + 200f;
			if ((float)(Screen.height - this.lastResponseTexture.height) < num)
			{
				num = (float)(Screen.height - this.lastResponseTexture.height);
			}
			GUI.Label(new Rect(rect.x + 5f, num, (float)this.lastResponseTexture.width, (float)this.lastResponseTexture.height), this.lastResponseTexture);
		}
		if (this.IsHorizontalLayout())
		{
			GUILayout.EndHorizontal();
		}
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x000A2AB0 File Offset: 0x000A0CB0
	private void Callback(FBResult result)
	{
		this.lastResponseTexture = null;
		if (!string.IsNullOrEmpty(result.Error))
		{
			this.lastResponse = "Error Response:\n" + result.Error;
			return;
		}
		if (!this.ApiQuery.Contains("/picture"))
		{
			this.lastResponse = "Success Response:\n" + result.Text;
			return;
		}
		this.lastResponseTexture = result.Texture;
		this.lastResponse = "Success Response:\n";
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x0000FD15 File Offset: 0x0000DF15
	private IEnumerator TakeScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D texture2D = new Texture2D(width, height, 3, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		texture2D.Apply();
		byte[] array = ImageConversion.EncodeToPNG(texture2D);
		WWWForm wwwform = new WWWForm();
		wwwform.AddBinaryData("image", array, "InteractiveConsole.png");
		wwwform.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
		FB.API("me/photos", HttpMethod.POST, new FacebookDelegate(this.Callback), wwwform);
		yield break;
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0000FD24 File Offset: 0x0000DF24
	private bool Button(string label)
	{
		return GUILayout.Button(label, new GUILayoutOption[]
		{
			GUILayout.MinHeight((float)this.buttonHeight),
			GUILayout.MaxWidth((float)this.mainWindowWidth)
		});
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0000FD50 File Offset: 0x0000DF50
	private void LabelAndTextField(string label, ref string text)
	{
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Label(label, new GUILayoutOption[]
		{
			GUILayout.MaxWidth(150f)
		});
		text = GUILayout.TextField(text, Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x0000A093 File Offset: 0x00008293
	private bool IsHorizontalLayout()
	{
		return true;
	}

	// Token: 0x04000C27 RID: 3111
	private bool isInit;

	// Token: 0x04000C28 RID: 3112
	public string FriendSelectorTitle = "";

	// Token: 0x04000C29 RID: 3113
	public string FriendSelectorMessage = "Derp";

	// Token: 0x04000C2A RID: 3114
	public string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	// Token: 0x04000C2B RID: 3115
	public string FriendSelectorData = "{}";

	// Token: 0x04000C2C RID: 3116
	public string FriendSelectorExcludeIds = "";

	// Token: 0x04000C2D RID: 3117
	public string FriendSelectorMax = "";

	// Token: 0x04000C2E RID: 3118
	public string DirectRequestTitle = "";

	// Token: 0x04000C2F RID: 3119
	public string DirectRequestMessage = "Herp";

	// Token: 0x04000C30 RID: 3120
	private string DirectRequestTo = "";

	// Token: 0x04000C31 RID: 3121
	public string FeedToId = "";

	// Token: 0x04000C32 RID: 3122
	public string FeedLink = "";

	// Token: 0x04000C33 RID: 3123
	public string FeedLinkName = "";

	// Token: 0x04000C34 RID: 3124
	public string FeedLinkCaption = "";

	// Token: 0x04000C35 RID: 3125
	public string FeedLinkDescription = "";

	// Token: 0x04000C36 RID: 3126
	public string FeedPicture = "";

	// Token: 0x04000C37 RID: 3127
	public string FeedMediaSource = "";

	// Token: 0x04000C38 RID: 3128
	public string FeedActionName = "";

	// Token: 0x04000C39 RID: 3129
	public string FeedActionLink = "";

	// Token: 0x04000C3A RID: 3130
	public string FeedReference = "";

	// Token: 0x04000C3B RID: 3131
	public bool IncludeFeedProperties;

	// Token: 0x04000C3C RID: 3132
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	// Token: 0x04000C3D RID: 3133
	public string PayProduct = "";

	// Token: 0x04000C3E RID: 3134
	public string ApiQuery = "";

	// Token: 0x04000C3F RID: 3135
	public float PlayerLevel = 1f;

	// Token: 0x04000C40 RID: 3136
	public string Width = "800";

	// Token: 0x04000C41 RID: 3137
	public string Height = "600";

	// Token: 0x04000C42 RID: 3138
	public bool CenterHorizontal = true;

	// Token: 0x04000C43 RID: 3139
	public bool CenterVertical;

	// Token: 0x04000C44 RID: 3140
	public string Top = "10";

	// Token: 0x04000C45 RID: 3141
	public string Left = "10";

	// Token: 0x04000C46 RID: 3142
	private string status = "Ready";

	// Token: 0x04000C47 RID: 3143
	private string lastResponse = "";

	// Token: 0x04000C48 RID: 3144
	public GUIStyle textStyle = new GUIStyle();

	// Token: 0x04000C49 RID: 3145
	private Texture2D lastResponseTexture;

	// Token: 0x04000C4A RID: 3146
	private Vector2 scrollPosition = Vector2.zero;

	// Token: 0x04000C4B RID: 3147
	private int buttonHeight = 24;

	// Token: 0x04000C4C RID: 3148
	private int mainWindowWidth = 500;

	// Token: 0x04000C4D RID: 3149
	private int mainWindowFullWidth = 530;
}
