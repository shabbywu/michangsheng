using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DanFangChildCell : MonoBehaviour
{
	public JSONObject danFang;

	private JSONObject yaoCaiIDList;

	private JSONObject yaoCaiSumList;

	[SerializeField]
	private Text ZhuYao;

	[SerializeField]
	private Text FuYao;

	[SerializeField]
	private Text YaoYing;

	[SerializeField]
	private GameObject CanLianZhiImage;

	[SerializeField]
	private GameObject Line;

	[SerializeField]
	private GameObject HightImage;

	public void init()
	{
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		if (danFang == null)
		{
			return;
		}
		yaoCaiIDList = danFang["Type"];
		yaoCaiSumList = danFang["Num"];
		int num = 0;
		int num2 = 0;
		YaoYing.text = "";
		ZhuYao.text = "";
		FuYao.text = "";
		for (int i = 0; i < yaoCaiIDList.Count; i++)
		{
			if (yaoCaiIDList[i].I <= 0)
			{
				continue;
			}
			string name = _ItemJsonData.DataDict[yaoCaiIDList[i].I].name;
			int i2 = yaoCaiSumList[i].I;
			string text = $"{name}x{i2}";
			bool flag = PlayerEx.Player.getItemNum(yaoCaiIDList[i].I) < i2;
			switch (i)
			{
			case 0:
				if (flag)
				{
					Text yaoYing = YaoYing;
					yaoYing.text = yaoYing.text + "<color=#888888>" + text + "</color>。";
				}
				else
				{
					Text yaoYing2 = YaoYing;
					yaoYing2.text = yaoYing2.text + text + "。";
				}
				break;
			case 1:
			case 2:
				if (num == 1)
				{
					Text zhuYao = ZhuYao;
					zhuYao.text += "、";
				}
				if (flag)
				{
					Text zhuYao2 = ZhuYao;
					zhuYao2.text = zhuYao2.text + "<color=#888888>" + text + "</color>";
				}
				else
				{
					Text zhuYao3 = ZhuYao;
					zhuYao3.text += text;
				}
				num++;
				break;
			case 3:
			case 4:
				if (num2 == 1)
				{
					Text fuYao = FuYao;
					fuYao.text += "、";
				}
				if (flag)
				{
					Text fuYao2 = FuYao;
					fuYao2.text = fuYao2.text + "<color=#888888>" + text + "</color>";
				}
				else
				{
					Text fuYao3 = FuYao;
					fuYao3.text += text;
				}
				num2++;
				break;
			}
		}
		Text zhuYao4 = ZhuYao;
		zhuYao4.text += ((num >= 1) ? "。" : "无");
		Text fuYao4 = FuYao;
		fuYao4.text += ((num2 >= 1) ? "。" : "无");
		updateState();
		((UnityEvent)((Component)this).GetComponent<Button>().onClick).AddListener(new UnityAction(clickDanFang));
	}

	public void updateState()
	{
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(danFang))
		{
			CanLianZhiImage.SetActive(true);
		}
		else
		{
			CanLianZhiImage.SetActive(false);
		}
	}

	public void hideLine()
	{
		Line.SetActive(false);
	}

	public void showLine()
	{
		Line.SetActive(true);
	}

	private void clickDanFang()
	{
		if (LianDanSystemManager.inst.inventory.inventory[30].itemID == -1)
		{
			LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
			UIPopTip.Inst.Pop("请先放入丹炉");
			return;
		}
		LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFang = ((Component)this).gameObject;
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFanParent = ((Component)this).gameObject.GetComponentInParent<DanFangParentCell>();
		LianDanSystemManager.inst.DanFangPageManager.curSelectJSONObject = danFang;
		if ((Object)(object)LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg != (Object)null)
		{
			LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg.SetActive(false);
		}
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg = HightImage;
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg.SetActive(true);
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(danFang))
		{
			yaoCaiIDList = danFang["Type"];
			yaoCaiSumList = danFang["Num"];
			ItemDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
			for (int i = 0; i < yaoCaiIDList.Count; i++)
			{
				if (yaoCaiIDList[i].I > 0)
				{
					LianDanSystemManager.inst.inventory.inventory[i + 25] = component.items[yaoCaiIDList[i].I].Clone();
					LianDanSystemManager.inst.inventory.inventory[i + 25].itemNum = yaoCaiSumList[i].I;
					LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[i].updateItem();
					LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
				}
			}
		}
		else
		{
			UIPopTip.Inst.Pop("材料不足");
		}
	}
}
