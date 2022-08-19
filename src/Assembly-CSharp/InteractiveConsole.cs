using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

// Token: 0x0200011F RID: 287
public sealed class InteractiveConsole : MonoBehaviour
{
	// Token: 0x06000DAB RID: 3499 RVA: 0x00051788 File Offset: 0x0004F988
	private void CallFBInit()
	{
		FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000517A8 File Offset: 0x0004F9A8
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn.ToString());
		this.isInit = true;
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000517D8 File Offset: 0x0004F9D8
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown.ToString());
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x000517F0 File Offset: 0x0004F9F0
	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", new FacebookDelegate(this.LoginCallback));
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00051808 File Offset: 0x0004FA08
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

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00051847 File Offset: 0x0004FA47
	private void CallFBLogout()
	{
		FB.Logout();
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0005184E File Offset: 0x0004FA4E
	private void CallFBPublishInstall()
	{
		FB.PublishInstall(new FacebookDelegate(this.PublishComplete));
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00051861 File Offset: 0x0004FA61
	private void PublishComplete(FBResult result)
	{
		Debug.Log("publish response: " + result.Text);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00051878 File Offset: 0x0004FA78
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

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0005192C File Offset: 0x0004FB2C
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

	// Token: 0x06000DB5 RID: 3509 RVA: 0x000519A4 File Offset: 0x0004FBA4
	private void CallFBFeed()
	{
		Dictionary<string, string[]> properties = null;
		if (this.IncludeFeedProperties)
		{
			properties = this.FeedProperties;
		}
		FB.Feed(this.FeedToId, this.FeedLink, this.FeedLinkName, this.FeedLinkCaption, this.FeedLinkDescription, this.FeedPicture, this.FeedMediaSource, this.FeedActionName, this.FeedActionLink, this.FeedReference, properties, new FacebookDelegate(this.Callback));
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00051A10 File Offset: 0x0004FC10
	private void CallFBPay()
	{
		FB.Canvas.Pay(this.PayProduct, "purchaseitem", 1, null, null, null, null, null, null);
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00051A44 File Offset: 0x0004FC44
	private void CallFBAPI()
	{
		FB.API(this.ApiQuery, HttpMethod.GET, new FacebookDelegate(this.Callback), null);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00051A63 File Offset: 0x0004FC63
	private void CallFBGetDeepLink()
	{
		FB.GetDeepLink(new FacebookDelegate(this.Callback));
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00051A78 File Offset: 0x0004FC78
	public void CallAppEventLogEvent()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_level"] = "Player Level";
		FB.AppEvents.LogEvent("fb_mobile_level_achieved", new float?(this.PlayerLevel), dictionary);
		this.PlayerLevel += 1f;
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00051AC4 File Offset: 0x0004FCC4
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

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00051BC8 File Offset: 0x0004FDC8
	private int TextWindowHeight
	{
		get
		{
			return Screen.height;
		}
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00051BD0 File Offset: 0x0004FDD0
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

	// Token: 0x06000DBD RID: 3517 RVA: 0x00051C6C File Offset: 0x0004FE6C
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

	// Token: 0x06000DBE RID: 3518 RVA: 0x0005217C File Offset: 0x0005037C
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

	// Token: 0x06000DBF RID: 3519 RVA: 0x000521F4 File Offset: 0x000503F4
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

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00052203 File Offset: 0x00050403
	private bool Button(string label)
	{
		return GUILayout.Button(label, new GUILayoutOption[]
		{
			GUILayout.MinHeight((float)this.buttonHeight),
			GUILayout.MaxWidth((float)this.mainWindowWidth)
		});
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0005222F File Offset: 0x0005042F
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

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00024C5F File Offset: 0x00022E5F
	private bool IsHorizontalLayout()
	{
		return true;
	}

	// Token: 0x040009A3 RID: 2467
	private bool isInit;

	// Token: 0x040009A4 RID: 2468
	public string FriendSelectorTitle = "";

	// Token: 0x040009A5 RID: 2469
	public string FriendSelectorMessage = "Derp";

	// Token: 0x040009A6 RID: 2470
	public string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	// Token: 0x040009A7 RID: 2471
	public string FriendSelectorData = "{}";

	// Token: 0x040009A8 RID: 2472
	public string FriendSelectorExcludeIds = "";

	// Token: 0x040009A9 RID: 2473
	public string FriendSelectorMax = "";

	// Token: 0x040009AA RID: 2474
	public string DirectRequestTitle = "";

	// Token: 0x040009AB RID: 2475
	public string DirectRequestMessage = "Herp";

	// Token: 0x040009AC RID: 2476
	private string DirectRequestTo = "";

	// Token: 0x040009AD RID: 2477
	public string FeedToId = "";

	// Token: 0x040009AE RID: 2478
	public string FeedLink = "";

	// Token: 0x040009AF RID: 2479
	public string FeedLinkName = "";

	// Token: 0x040009B0 RID: 2480
	public string FeedLinkCaption = "";

	// Token: 0x040009B1 RID: 2481
	public string FeedLinkDescription = "";

	// Token: 0x040009B2 RID: 2482
	public string FeedPicture = "";

	// Token: 0x040009B3 RID: 2483
	public string FeedMediaSource = "";

	// Token: 0x040009B4 RID: 2484
	public string FeedActionName = "";

	// Token: 0x040009B5 RID: 2485
	public string FeedActionLink = "";

	// Token: 0x040009B6 RID: 2486
	public string FeedReference = "";

	// Token: 0x040009B7 RID: 2487
	public bool IncludeFeedProperties;

	// Token: 0x040009B8 RID: 2488
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	// Token: 0x040009B9 RID: 2489
	public string PayProduct = "";

	// Token: 0x040009BA RID: 2490
	public string ApiQuery = "";

	// Token: 0x040009BB RID: 2491
	public float PlayerLevel = 1f;

	// Token: 0x040009BC RID: 2492
	public string Width = "800";

	// Token: 0x040009BD RID: 2493
	public string Height = "600";

	// Token: 0x040009BE RID: 2494
	public bool CenterHorizontal = true;

	// Token: 0x040009BF RID: 2495
	public bool CenterVertical;

	// Token: 0x040009C0 RID: 2496
	public string Top = "10";

	// Token: 0x040009C1 RID: 2497
	public string Left = "10";

	// Token: 0x040009C2 RID: 2498
	private string status = "Ready";

	// Token: 0x040009C3 RID: 2499
	private string lastResponse = "";

	// Token: 0x040009C4 RID: 2500
	public GUIStyle textStyle = new GUIStyle();

	// Token: 0x040009C5 RID: 2501
	private Texture2D lastResponseTexture;

	// Token: 0x040009C6 RID: 2502
	private Vector2 scrollPosition = Vector2.zero;

	// Token: 0x040009C7 RID: 2503
	private int buttonHeight = 24;

	// Token: 0x040009C8 RID: 2504
	private int mainWindowWidth = 500;

	// Token: 0x040009C9 RID: 2505
	private int mainWindowFullWidth = 530;
}
