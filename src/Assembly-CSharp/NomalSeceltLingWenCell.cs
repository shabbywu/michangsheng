using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NomalSeceltLingWenCell : MonoBehaviour
{
	[SerializeField]
	private Text showDesc;

	private int lingWenType;

	[SerializeField]
	private GameObject LingWenCell;

	public void showSelect()
	{
		((Component)this).gameObject.SetActive(true);
		lingWenType = -1;
		updateSelect();
	}

	public void updateSelect()
	{
		lingWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		Tools.ClearObj(LingWenCell.transform);
		LingWenCell lingWenCell = null;
		List<JSONObject> list = jsonData.instance.LianQiLingWenBiao.list;
		int num = 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["type"].I == lingWenType)
			{
				LingWenCell component = Tools.InstantiateGameObject(LingWenCell, LingWenCell.transform.parent).GetComponent<LingWenCell>();
				lingWenCell = component;
				component.lingWenID = list[i]["id"].I;
				if (num == 0)
				{
					LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(component.lingWenID);
					setCurSelectContent(Tools.Code64(LianQiTotalManager.inst.buildNomalLingWenDesc(list[i])));
					component.showDaoSanJiao();
				}
				component.setDesc(LianQiTotalManager.inst.buildNomalLingWenDesc(list[i]));
				component.clickCallBack = setCurSelectContent;
				num++;
			}
		}
		lingWenCell.hideFenGeXian();
	}

	public void setCurSelectContent(string str)
	{
		showDesc.text = str;
	}
}
