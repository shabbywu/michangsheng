using System;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class CyUIIconShow : UIIconShow
{
	// Token: 0x06001AF1 RID: 6897 RVA: 0x00016D2E File Offset: 0x00014F2E
	public void Init()
	{
		this.hasTiJiaoImage.SetActive(false);
		this.hasGetImage.SetActive(false);
		base.transform.parent.gameObject.SetActive(true);
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x00016D5E File Offset: 0x00014F5E
	public void ShowHasTiJiao()
	{
		this.hasTiJiaoImage.SetActive(true);
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x00016D6C File Offset: 0x00014F6C
	public void ShowHasGet()
	{
		this.hasGetImage.SetActive(true);
	}

	// Token: 0x04001682 RID: 5762
	[SerializeField]
	private GameObject hasTiJiaoImage;

	// Token: 0x04001683 RID: 5763
	[SerializeField]
	private GameObject hasGetImage;
}
