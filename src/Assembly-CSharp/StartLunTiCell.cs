using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000477 RID: 1143
public class StartLunTiCell : MonoBehaviour
{
	// Token: 0x06001E9F RID: 7839 RVA: 0x000195EE File Offset: 0x000177EE
	public void Init(Sprite sprite, int id)
	{
		base.gameObject.SetActive(true);
		this.lunTiName.sprite = sprite;
		this.lunTiId = id;
	}

	// Token: 0x04001A07 RID: 6663
	[SerializeField]
	private Image lunTiName;

	// Token: 0x04001A08 RID: 6664
	public int lunTiId;

	// Token: 0x04001A09 RID: 6665
	public Transform wuDaoParent;

	// Token: 0x04001A0A RID: 6666
	public Image finshIBg;

	// Token: 0x04001A0B RID: 6667
	public Image finshImage;
}
