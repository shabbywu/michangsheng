using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003F RID: 63
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x06000447 RID: 1095 RVA: 0x00017B4C File Offset: 0x00015D4C
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

	// Token: 0x06000448 RID: 1096 RVA: 0x00017B5B File Offset: 0x00015D5B
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			Object.Destroy(this.mTex);
		}
	}

	// Token: 0x0400026C RID: 620
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x0400026D RID: 621
	private Texture2D mTex;
}
