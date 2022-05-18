using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class Fader : MonoBehaviour
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0000EA3E File Offset: 0x0000CC3E
	public void FadeIntoLevel(string sceneName)
	{
		this.sceneToLoad = sceneName;
		base.GetComponent<Animator>().Play("Fader In");
		this.load();
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0000EA5D File Offset: 0x0000CC5D
	public void setCanClick()
	{
		Tools.canClickFlag = true;
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0000EA65 File Offset: 0x0000CC65
	public void setCanNotClick()
	{
		Tools.canClickFlag = false;
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0000EA6D File Offset: 0x0000CC6D
	private void load()
	{
		Tools.instance.loadOtherScenes(this.sceneToLoad);
	}

	// Token: 0x04000A0A RID: 2570
	private string sceneToLoad;
}
