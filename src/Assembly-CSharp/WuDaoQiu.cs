using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000478 RID: 1144
public class WuDaoQiu : MonoBehaviour
{
	// Token: 0x06001EA1 RID: 7841 RVA: 0x0001960F File Offset: 0x0001780F
	public void Init(Sprite sprite, int level)
	{
		base.gameObject.SetActive(true);
		this.wuDaoQiuImage.sprite = sprite;
		this.wuDaoQiuLevel.text = level.ToString();
	}

	// Token: 0x04001A0C RID: 6668
	[SerializeField]
	private Image wuDaoQiuImage;

	// Token: 0x04001A0D RID: 6669
	[SerializeField]
	private Text wuDaoQiuLevel;
}
