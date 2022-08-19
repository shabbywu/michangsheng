using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class LunDaoPanel : MonoBehaviour
{
	// Token: 0x06001B5B RID: 7003 RVA: 0x000C2F18 File Offset: 0x000C1118
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

	// Token: 0x06001B5C RID: 7004 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x000C311C File Offset: 0x000C131C
	public void CloseMoreLunTiPanel()
	{
		this.moreLunTiPanel.SetActive(false);
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x000C312C File Offset: 0x000C132C
	public void AddNullSlot()
	{
		LunDaoQiu component = Object.Instantiate<GameObject>(this.lunDaoQiuSlot, this.lunDaoQiuSlot.transform.parent).GetComponent<LunDaoQiu>();
		component.SetNull();
		component.gameObject.SetActive(true);
		LunDaoManager.inst.lunTiMag.curLunDianList.Add(component);
	}

	// Token: 0x040015DB RID: 5595
	[SerializeField]
	private List<Sprite> lunTiNameSpriteList;

	// Token: 0x040015DC RID: 5596
	[SerializeField]
	public List<Sprite> wuDaoQiuSpriteList;

	// Token: 0x040015DD RID: 5597
	[SerializeField]
	private GameObject startLunTiDaoCell;

	// Token: 0x040015DE RID: 5598
	[SerializeField]
	private GameObject wuDaoQiuCell;

	// Token: 0x040015DF RID: 5599
	[SerializeField]
	private GameObject moreLunTiDaoCell;

	// Token: 0x040015E0 RID: 5600
	[SerializeField]
	private GameObject moreWuDaoQiuCell;

	// Token: 0x040015E1 RID: 5601
	[SerializeField]
	private GameObject moreLunTiPanel;

	// Token: 0x040015E2 RID: 5602
	[SerializeField]
	private GameObject lunDaoQiuSlot;

	// Token: 0x040015E3 RID: 5603
	[SerializeField]
	private BtnCell moreWuDaoBtn;

	// Token: 0x040015E4 RID: 5604
	private Dictionary<int, List<int>> targetLunTiDictionary;

	// Token: 0x040015E5 RID: 5605
	public Dictionary<int, StartLunTiCell> lunTiCtrDictionary;
}
