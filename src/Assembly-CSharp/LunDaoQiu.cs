using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000476 RID: 1142
public class LunDaoQiu : MonoBehaviour
{
	// Token: 0x06001E9B RID: 7835 RVA: 0x0001959F File Offset: 0x0001779F
	public void SetNull()
	{
		this.lunDaoQiuImage.gameObject.SetActive(false);
		this.wudaoId = -1;
		this.isNull = true;
		this.level = 0;
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x00108D84 File Offset: 0x00106F84
	public void SetData(int id, int curLevel)
	{
		this.isNull = false;
		this.wudaoId = id;
		this.level = curLevel;
		this.curLevel.text = curLevel.ToString();
		this.lunDaoQiuImage.sprite = LunDaoManager.inst.lunDaoPanel.wuDaoQiuSpriteList[id];
		this.lunDaoQiuImage.gameObject.SetActive(true);
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x000195C7 File Offset: 0x000177C7
	public LunDaoQiu LevelUp()
	{
		this.level++;
		this.curLevel.text = this.level.ToString();
		return this;
	}

	// Token: 0x04001A02 RID: 6658
	public Image lunDaoQiuImage;

	// Token: 0x04001A03 RID: 6659
	[SerializeField]
	public Text curLevel;

	// Token: 0x04001A04 RID: 6660
	public int wudaoId;

	// Token: 0x04001A05 RID: 6661
	public int level;

	// Token: 0x04001A06 RID: 6662
	public bool isNull;
}
