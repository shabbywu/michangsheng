using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000448 RID: 1096
public class SelectDanFangPingJie : MonoBehaviour
{
	// Token: 0x06001D32 RID: 7474 RVA: 0x00018555 File Offset: 0x00016755
	public void openSelectPanel()
	{
		this.Content.SetActive(true);
		this.Selected.SetActive(false);
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x0010108C File Offset: 0x000FF28C
	public void selectPingJie(int pinjie)
	{
		LianDanSystemManager.inst.DanFangPageManager.setPingJie((DanFangPageManager.DanFangPingJie)pinjie);
		this.SelectedImage.sprite = LianDanSystemManager.inst.DanFangPageManager.pingJieSprites[pinjie];
		this.Content.SetActive(false);
		this.Selected.SetActive(true);
	}

	// Token: 0x04001924 RID: 6436
	[SerializeField]
	private GameObject Selected;

	// Token: 0x04001925 RID: 6437
	[SerializeField]
	private GameObject Content;

	// Token: 0x04001926 RID: 6438
	[SerializeField]
	private Image SelectedImage;
}
