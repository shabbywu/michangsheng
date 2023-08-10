using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	public string url = "http://www.yourwebsite.com/logo.png";

	private Texture2D mTex;

	private IEnumerator Start()
	{
		WWW www = new WWW(url);
		yield return www;
		mTex = www.texture;
		if ((Object)(object)mTex != (Object)null)
		{
			UITexture component = ((Component)this).GetComponent<UITexture>();
			component.mainTexture = (Texture)(object)mTex;
			component.MakePixelPerfect();
		}
		www.Dispose();
	}

	private void OnDestroy()
	{
		if ((Object)(object)mTex != (Object)null)
		{
			Object.Destroy((Object)(object)mTex);
		}
	}
}
