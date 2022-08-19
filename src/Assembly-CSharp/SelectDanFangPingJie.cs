using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002ED RID: 749
public class SelectDanFangPingJie : MonoBehaviour
{
	// Token: 0x06001A0E RID: 6670 RVA: 0x000BA9A6 File Offset: 0x000B8BA6
	public void openSelectPanel()
	{
		this.Content.SetActive(true);
		this.Selected.SetActive(false);
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000BA9C0 File Offset: 0x000B8BC0
	public void selectPingJie(int pinjie)
	{
		LianDanSystemManager.inst.DanFangPageManager.setPingJie((DanFangPageManager.DanFangPingJie)pinjie);
		this.SelectedImage.sprite = LianDanSystemManager.inst.DanFangPageManager.pingJieSprites[pinjie];
		this.Content.SetActive(false);
		this.Selected.SetActive(true);
	}

	// Token: 0x0400151E RID: 5406
	[SerializeField]
	private GameObject Selected;

	// Token: 0x0400151F RID: 5407
	[SerializeField]
	private GameObject Content;

	// Token: 0x04001520 RID: 5408
	[SerializeField]
	private Image SelectedImage;
}
