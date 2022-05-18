using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class LunDaoPanel : MonoBehaviour
{
	// Token: 0x06001E8C RID: 7820 RVA: 0x00108460 File Offset: 0x00106660
	public void Init()
	{
		this.targetLunTiDictionary = LunDaoManager.inst.lunTiMag.targetLunTiDictionary;
		this.lunTiCtrDictionary = new Dictionary<int, StartLunTiCell>();
		int num = 0;
		foreach (int num2 in this.targetLunTiDictionary.Keys)
		{
			num++;
			if (num > 5)
			{
				StartLunTiCell component = Object.Instantiate<GameObject>(this.moreLunTiDaoCell, this.moreLunTiDaoCell.transform.parent).GetComponent<StartLunTiCell>();
				component.Init(this.lunTiNameSpriteList[num2 - 1], num2);
				for (int i = 0; i < this.targetLunTiDictionary[num2].Count; i++)
				{
					Object.Instantiate<GameObject>(this.moreWuDaoQiuCell, component.wuDaoParent).GetComponent<WuDaoQiu>().Init(this.wuDaoQiuSpriteList[num2], this.targetLunTiDictionary[num2][i]);
				}
				this.lunTiCtrDictionary.Add(num2, component);
			}
			else
			{
				StartLunTiCell component2 = Object.Instantiate<GameObject>(this.startLunTiDaoCell, this.startLunTiDaoCell.transform.parent).GetComponent<StartLunTiCell>();
				component2.Init(this.lunTiNameSpriteList[num2 - 1], num2);
				for (int j = 0; j < this.targetLunTiDictionary[num2].Count; j++)
				{
					Object.Instantiate<GameObject>(this.wuDaoQiuCell, component2.wuDaoParent).GetComponent<WuDaoQiu>().Init(this.wuDaoQiuSpriteList[num2], this.targetLunTiDictionary[num2][j]);
				}
				this.lunTiCtrDictionary.Add(num2, component2);
			}
		}
		if (num > 5)
		{
			this.moreWuDaoBtn.gameObject.SetActive(true);
			this.moreWuDaoBtn.mouseUp.AddListener(delegate()
			{
				this.moreLunTiPanel.SetActive(true);
			});
		}
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x00019570 File Offset: 0x00017770
	public void CloseMoreLunTiPanel()
	{
		this.moreLunTiPanel.SetActive(false);
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x00108664 File Offset: 0x00106864
	public void AddNullSlot()
	{
		LunDaoQiu component = Object.Instantiate<GameObject>(this.lunDaoQiuSlot, this.lunDaoQiuSlot.transform.parent).GetComponent<LunDaoQiu>();
		component.SetNull();
		component.gameObject.SetActive(true);
		LunDaoManager.inst.lunTiMag.curLunDianList.Add(component);
	}

	// Token: 0x040019F5 RID: 6645
	[SerializeField]
	private List<Sprite> lunTiNameSpriteList;

	// Token: 0x040019F6 RID: 6646
	[SerializeField]
	public List<Sprite> wuDaoQiuSpriteList;

	// Token: 0x040019F7 RID: 6647
	[SerializeField]
	private GameObject startLunTiDaoCell;

	// Token: 0x040019F8 RID: 6648
	[SerializeField]
	private GameObject wuDaoQiuCell;

	// Token: 0x040019F9 RID: 6649
	[SerializeField]
	private GameObject moreLunTiDaoCell;

	// Token: 0x040019FA RID: 6650
	[SerializeField]
	private GameObject moreWuDaoQiuCell;

	// Token: 0x040019FB RID: 6651
	[SerializeField]
	private GameObject moreLunTiPanel;

	// Token: 0x040019FC RID: 6652
	[SerializeField]
	private GameObject lunDaoQiuSlot;

	// Token: 0x040019FD RID: 6653
	[SerializeField]
	private BtnCell moreWuDaoBtn;

	// Token: 0x040019FE RID: 6654
	private Dictionary<int, List<int>> targetLunTiDictionary;

	// Token: 0x040019FF RID: 6655
	public Dictionary<int, StartLunTiCell> lunTiCtrDictionary;
}
