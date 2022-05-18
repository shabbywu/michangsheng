using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000459 RID: 1113
public class NomalSeceltLingWenCell : MonoBehaviour
{
	// Token: 0x06001DCD RID: 7629 RVA: 0x00018CE5 File Offset: 0x00016EE5
	public void showSelect()
	{
		base.gameObject.SetActive(true);
		this.lingWenType = -1;
		this.updateSelect();
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x00103AEC File Offset: 0x00101CEC
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

	// Token: 0x06001DCF RID: 7631 RVA: 0x00018D00 File Offset: 0x00016F00
	public void setCurSelectContent(string str)
	{
		this.showDesc.text = str;
	}

	// Token: 0x04001977 RID: 6519
	[SerializeField]
	private Text showDesc;

	// Token: 0x04001978 RID: 6520
	private int lingWenType;

	// Token: 0x04001979 RID: 6521
	[SerializeField]
	private GameObject LingWenCell;
}
