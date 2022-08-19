using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000316 RID: 790
public class WuDaoQiu : MonoBehaviour
{
	// Token: 0x06001B70 RID: 7024 RVA: 0x000C3941 File Offset: 0x000C1B41
	public void Init(Sprite sprite, int level)
	{
		base.gameObject.SetActive(true);
		this.wuDaoQiuImage.sprite = sprite;
		this.wuDaoQiuLevel.text = level.ToString();
	}

	// Token: 0x040015F2 RID: 5618
	[SerializeField]
	private Image wuDaoQiuImage;

	// Token: 0x040015F3 RID: 5619
	[SerializeField]
	private Text wuDaoQiuLevel;
}
