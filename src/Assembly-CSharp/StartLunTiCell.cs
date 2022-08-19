using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000315 RID: 789
public class StartLunTiCell : MonoBehaviour
{
	// Token: 0x06001B6E RID: 7022 RVA: 0x000C3920 File Offset: 0x000C1B20
	public void Init(Sprite sprite, int id)
	{
		base.gameObject.SetActive(true);
		this.lunTiName.sprite = sprite;
		this.lunTiId = id;
	}

	// Token: 0x040015ED RID: 5613
	[SerializeField]
	private Image lunTiName;

	// Token: 0x040015EE RID: 5614
	public int lunTiId;

	// Token: 0x040015EF RID: 5615
	public Transform wuDaoParent;

	// Token: 0x040015F0 RID: 5616
	public Image finshIBg;

	// Token: 0x040015F1 RID: 5617
	public Image finshImage;
}
