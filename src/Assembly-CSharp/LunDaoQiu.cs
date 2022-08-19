using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000314 RID: 788
public class LunDaoQiu : MonoBehaviour
{
	// Token: 0x06001B6A RID: 7018 RVA: 0x000C386C File Offset: 0x000C1A6C
	public void SetNull()
	{
		this.lunDaoQiuImage.gameObject.SetActive(false);
		this.wudaoId = -1;
		this.isNull = true;
		this.level = 0;
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x000C3894 File Offset: 0x000C1A94
	public void SetData(int id, int curLevel)
	{
		this.isNull = false;
		this.wudaoId = id;
		this.level = curLevel;
		this.curLevel.text = curLevel.ToString();
		this.lunDaoQiuImage.sprite = LunDaoManager.inst.lunDaoPanel.wuDaoQiuSpriteList[id];
		this.lunDaoQiuImage.gameObject.SetActive(true);
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x000C38F9 File Offset: 0x000C1AF9
	public LunDaoQiu LevelUp()
	{
		this.level++;
		this.curLevel.text = this.level.ToString();
		return this;
	}

	// Token: 0x040015E8 RID: 5608
	public Image lunDaoQiuImage;

	// Token: 0x040015E9 RID: 5609
	[SerializeField]
	public Text curLevel;

	// Token: 0x040015EA RID: 5610
	public int wudaoId;

	// Token: 0x040015EB RID: 5611
	public int level;

	// Token: 0x040015EC RID: 5612
	public bool isNull;
}
