using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

public class EditorFacebookAccessToken : MonoBehaviour
{
	private const float windowWidth = 592f;

	private float windowHeight = 200f;

	private string accessToken = "";

	private bool isLoggingIn;

	private static GUISkin fbSkin;

	private GUIStyle greyButton;

	private IEnumerator Start()
	{
		if (!((Object)(object)fbSkin != (Object)null))
		{
			string fbSkinUrl = IntegratedPluginCanvasLocation.FbSkinUrl;
			WWW www = new WWW(fbSkinUrl);
			yield return www;
			if (www.error != null)
			{
				FbDebug.Error("Could not find the Facebook Skin: " + www.error);
				yield break;
			}
			Object mainAsset = www.assetBundle.mainAsset;
			fbSkin = (GUISkin)(object)((mainAsset is GUISkin) ? mainAsset : null);
			www.assetBundle.Unload(false);
		}
	}

	private void OnGUI()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)(Screen.height / 2) - windowHeight / 2f;
		float num2 = (float)(Screen.width / 2) - 296f;
		if ((Object)(object)fbSkin != (Object)null)
		{
			GUI.skin = fbSkin;
			greyButton = fbSkin.GetStyle("greyButton");
		}
		else
		{
			greyButton = GUI.skin.button;
		}
		GUI.ModalWindow(((object)this).GetHashCode(), new Rect(num2, num, 592f, windowHeight), new WindowFunction(OnGUIDialog), "Unity Editor Facebook Login");
	}

	private void OnGUIDialog(int windowId)
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Invalid comparison between Unknown and I4
		GUI.enabled = !isLoggingIn;
		GUILayout.Space(10f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(10f);
		GUILayout.Label("User Access Token:", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		accessToken = GUILayout.TextField(accessToken, GUI.skin.textArea, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.MinWidth(400f) });
		GUILayout.EndHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Find Access Token", Array.Empty<GUILayoutOption>()))
		{
			Application.OpenURL($"https://developers.facebook.com/tools/accesstoken/?app_id={FB.AppId}");
		}
		GUILayout.FlexibleSpace();
		GUIContent val = new GUIContent("Login");
		if (GUI.Button(GUILayoutUtility.GetRect(val, GUI.skin.button), val))
		{
			EditorFacebook component = FBComponentFactory.GetComponent<EditorFacebook>((IfNotExist)0);
			((AbstractFacebook)component).AccessToken = accessToken;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["batch"] = "[{\"method\":\"GET\", \"relative_url\":\"me?fields=id\"},{\"method\":\"GET\", \"relative_url\":\"app?fields=id\"}]";
			dictionary["method"] = "POST";
			dictionary["access_token"] = accessToken;
			FB.API("/", HttpMethod.GET, new FacebookDelegate(component.MockLoginCallback), dictionary);
			isLoggingIn = true;
		}
		GUI.enabled = true;
		GUIContent val2 = new GUIContent("Cancel");
		Rect rect = GUILayoutUtility.GetRect(val2, greyButton);
		if (GUI.Button(rect, val2, greyButton))
		{
			FBComponentFactory.GetComponent<EditorFacebook>((IfNotExist)0).MockCancelledLoginCallback();
			Object.Destroy((Object)(object)this);
		}
		GUILayout.EndHorizontal();
		if ((int)Event.current.type == 7)
		{
			windowHeight = ((Rect)(ref rect)).y + ((Rect)(ref rect)).height + (float)GUI.skin.window.padding.bottom;
		}
	}
}
