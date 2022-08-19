using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class Fader : MonoBehaviour
{
	// Token: 0x06000BD3 RID: 3027 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00047BB3 File Offset: 0x00045DB3
	public void FadeIntoLevel(string sceneName)
	{
		this.sceneToLoad = sceneName;
		base.GetComponent<Animator>().Play("Fader In");
		this.load();
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00047BD2 File Offset: 0x00045DD2
	public void setCanClick()
	{
		Tools.canClickFlag = true;
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00047BDA File Offset: 0x00045DDA
	public void setCanNotClick()
	{
		Tools.canClickFlag = false;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00047BE2 File Offset: 0x00045DE2
	private void load()
	{
		Tools.instance.loadOtherScenes(this.sceneToLoad);
	}

	// Token: 0x0400081B RID: 2075
	private string sceneToLoad;
}
