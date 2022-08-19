using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class CyUIIconShow : UIIconShow
{
	// Token: 0x060017FF RID: 6143 RVA: 0x000A77AF File Offset: 0x000A59AF
	public void Init()
	{
		this.hasTiJiaoImage.SetActive(false);
		this.hasGetImage.SetActive(false);
		base.transform.parent.gameObject.SetActive(true);
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x000A77DF File Offset: 0x000A59DF
	public void ShowHasTiJiao()
	{
		this.hasTiJiaoImage.SetActive(true);
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x000A77ED File Offset: 0x000A59ED
	public void ShowHasGet()
	{
		this.hasGetImage.SetActive(true);
	}

	// Token: 0x040012E6 RID: 4838
	[SerializeField]
	private GameObject hasTiJiaoImage;

	// Token: 0x040012E7 RID: 4839
	[SerializeField]
	private GameObject hasGetImage;
}
