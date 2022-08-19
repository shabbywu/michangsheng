using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FC RID: 764
public class NomalSeceltLingWenCell : MonoBehaviour
{
	// Token: 0x06001AA7 RID: 6823 RVA: 0x000BDC10 File Offset: 0x000BBE10
	public void showSelect()
	{
		base.gameObject.SetActive(true);
		this.lingWenType = -1;
		this.updateSelect();
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000BDC2C File Offset: 0x000BBE2C
	public void updateSelect()
	{
		this.lingWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		Tools.ClearObj(this.LingWenCell.transform);
		LingWenCell lingWenCell = null;
		List<JSONObject> list = jsonData.instance.LianQiLingWenBiao.list;
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["type"].I == this.lingWenType)
			{
				LingWenCell component = Tools.InstantiateGameObject(this.LingWenCell, this.LingWenCell.transform.parent).GetComponent<LingWenCell>();
				lingWenCell = component;
				component.lingWenID = list[i]["id"].I;
				if (num == 0)
				{
					LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(component.lingWenID);
					this.setCurSelectContent(Tools.Code64(LianQiTotalManager.inst.buildNomalLingWenDesc(list[i])));
					component.showDaoSanJiao();
				}
				component.setDesc(LianQiTotalManager.inst.buildNomalLingWenDesc(list[i]));
				component.clickCallBack = new Action<string>(this.setCurSelectContent);
				num++;
			}
		}
		lingWenCell.hideFenGeXian();
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x000BDD67 File Offset: 0x000BBF67
	public void setCurSelectContent(string str)
	{
		this.showDesc.text = str;
	}

	// Token: 0x0400156A RID: 5482
	[SerializeField]
	private Text showDesc;

	// Token: 0x0400156B RID: 5483
	private int lingWenType;

	// Token: 0x0400156C RID: 5484
	[SerializeField]
	private GameObject LingWenCell;
}
