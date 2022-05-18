using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000056 RID: 86
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x0600048F RID: 1167 RVA: 0x00008016 File Offset: 0x00006216
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture component = base.GetComponent<UITexture>();
			component.mainTexture = this.mTex;
			component.MakePixelPerfect();
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00008025 File Offset: 0x00006225
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			Object.Destroy(this.mTex);
		}
	}

	// Token: 0x040002D8 RID: 728
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x040002D9 RID: 729
	private Texture2D mTex;
}
