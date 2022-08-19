using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class EditorFacebookAccessToken : MonoBehaviour
{
	// Token: 0x06000DC4 RID: 3524 RVA: 0x000523F5 File Offset: 0x000505F5
	private IEnumerator Start()
	{
		if (EditorFacebookAccessToken.fbSkin != null)
		{
			yield break;
		}
		string fbSkinUrl = IntegratedPluginCanvasLocation.FbSkinUrl;
		WWW www = new WWW(fbSkinUrl);
		yield return www;
		if (www.error != null)
		{
			FbDebug.Error("Could not find the Facebook Skin: " + www.error);
			yield break;
		}
		EditorFacebookAccessToken.fbSkin = (www.assetBundle.mainAsset as GUISkin);
		www.assetBundle.Unload(false);
		yield break;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00052400 File Offset: 0x00050600
	private void OnGUI()
	{
		float num = (float)(Screen.height / 2) - this.windowHeight / 2f;
		float num2 = (float)(Screen.width / 2) - 296f;
		if (EditorFacebookAccessToken.fbSkin != null)
		{
			GUI.skin = EditorFacebookAccessToken.fbSkin;
			this.greyButton = EditorFacebookAccessToken.fbSkin.GetStyle("greyButton");
		}
		else
		{
			this.greyButton = GUI.skin.button;
		}
		GUI.ModalWindow(this.GetHashCode(), new Rect(num2, num, 592f, this.windowHeight), new GUI.WindowFunction(this.OnGUIDialog), "Unity Editor Facebook Login");
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x000524A0 File Offset: 0x000506A0
	private void OnGUIDialog(int windowId)
	{
		GUI.enabled = !this.isLoggingIn;
		GUILayout.Space(10f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(10f);
		GUILayout.Label("User Access Token:", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		this.accessToken = GUILayout.TextField(this.accessToken, GUI.skin.textArea, new GUILayoutOption[]
		{
			GUILayout.MinWidth(400f)
		});
		GUILayout.EndHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Find Access Token", Array.Empty<GUILayoutOption>()))
		{
			Application.OpenURL(string.Format("https://developers.facebook.com/tools/accesstoken/?app_id={0}", FB.AppId));
		}
		GUILayout.FlexibleSpace();
		GUIContent guicontent = new GUIContent("Login");
		if (GUI.Button(GUILayoutUtility.GetRect(guicontent, GUI.skin.button), guicontent))
		{
			EditorFacebook component = FBComponentFactory.GetComponent<EditorFacebook>(0);
			component.AccessToken = this.accessToken;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["batch"] = "[{\"method\":\"GET\", \"relative_url\":\"me?fields=id\"},{\"method\":\"GET\", \"relative_url\":\"app?fields=id\"}]";
			dictionary["method"] = "POST";
			dictionary["access_token"] = this.accessToken;
			FB.API("/", HttpMethod.GET, new FacebookDelegate(component.MockLoginCallback), dictionary);
			this.isLoggingIn = true;
		}
		GUI.enabled = true;
		GUIContent guicontent2 = new GUIContent("Cancel");
		Rect rect = GUILayoutUtility.GetRect(guicontent2, this.greyButton);
		if (GUI.Button(rect, guicontent2, this.greyButton))
		{
			FBComponentFactory.GetComponent<EditorFacebook>(0).MockCancelledLoginCallback();
			Object.Destroy(this);
		}
		GUILayout.EndHorizontal();
		if (Event.current.type == 7)
		{
			this.windowHeight = rect.y + rect.height + (float)GUI.skin.window.padding.bottom;
		}
	}

	// Token: 0x040009CA RID: 2506
	private const float windowWidth = 592f;

	// Token: 0x040009CB RID: 2507
	private float windowHeight = 200f;

	// Token: 0x040009CC RID: 2508
	private string accessToken = "";

	// Token: 0x040009CD RID: 2509
	private bool isLoggingIn;

	// Token: 0x040009CE RID: 2510
	private static GUISkin fbSkin;

	// Token: 0x040009CF RID: 2511
	private GUIStyle greyButton;
}
