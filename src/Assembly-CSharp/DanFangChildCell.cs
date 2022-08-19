using System;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E7 RID: 743
public class DanFangChildCell : MonoBehaviour
{
	// Token: 0x060019D8 RID: 6616 RVA: 0x000B8EB0 File Offset: 0x000B70B0
	public void init()
	{
		if (this.danFang != null)
		{
			this.yaoCaiIDList = this.danFang["Type"];
			this.yaoCaiSumList = this.danFang["Num"];
			int num = 0;
			int num2 = 0;
			this.YaoYing.text = "";
			this.ZhuYao.text = "";
			this.FuYao.text = "";
			for (int i = 0; i < this.yaoCaiIDList.Count; i++)
			{
				if (this.yaoCaiIDList[i].I > 0)
				{
					string name = _ItemJsonData.DataDict[this.yaoCaiIDList[i].I].name;
					int i2 = this.yaoCaiSumList[i].I;
					string text = string.Format("{0}x{1}", name, i2);
					bool flag = PlayerEx.Player.getItemNum(this.yaoCaiIDList[i].I) < i2;
					if (i == 0)
					{
						if (flag)
						{
							Text yaoYing = this.YaoYing;
							yaoYing.text = yaoYing.text + "<color=#888888>" + text + "</color>。";
						}
						else
						{
							Text yaoYing2 = this.YaoYing;
							yaoYing2.text = yaoYing2.text + text + "。";
						}
					}
					else if (i == 1 || i == 2)
					{
						if (num == 1)
						{
							Text zhuYao = this.ZhuYao;
							zhuYao.text += "、";
						}
						if (flag)
						{
							Text zhuYao2 = this.ZhuYao;
							zhuYao2.text = zhuYao2.text + "<color=#888888>" + text + "</color>";
						}
						else
						{
							Text zhuYao3 = this.ZhuYao;
							zhuYao3.text += text;
						}
						num++;
					}
					else if (i == 3 || i == 4)
					{
						if (num2 == 1)
						{
							Text fuYao = this.FuYao;
							fuYao.text += "、";
						}
						if (flag)
						{
							Text fuYao2 = this.FuYao;
							fuYao2.text = fuYao2.text + "<color=#888888>" + text + "</color>";
						}
						else
						{
							Text fuYao3 = this.FuYao;
							fuYao3.text += text;
						}
						num2++;
					}
				}
			}
			Text zhuYao4 = this.ZhuYao;
			zhuYao4.text += ((num >= 1) ? "。" : "无");
			Text fuYao4 = this.FuYao;
			fuYao4.text += ((num2 >= 1) ? "。" : "无");
			this.updateState();
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.clickDanFang));
		}
	}

	// Token: 0x060019D9 RID: 6617 RVA: 0x000B915A File Offset: 0x000B735A
	public void updateState()
	{
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(this.danFang))
		{
			this.CanLianZhiImage.SetActive(true);
			return;
		}
		this.CanLianZhiImage.SetActive(false);
	}

	// Token: 0x060019DA RID: 6618 RVA: 0x000B918C File Offset: 0x000B738C
	public void hideLine()
	{
		this.Line.SetActive(false);
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x000B919A File Offset: 0x000B739A
	public void showLine()
	{
		this.Line.SetActive(true);
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x000B91A8 File Offset: 0x000B73A8
	private void clickDanFang()
	{
		if (LianDanSystemManager.inst.inventory.inventory[30].itemID == -1)
		{
			LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
			UIPopTip.Inst.Pop("请先放入丹炉", PopTipIconType.叹号);
			return;
		}
		LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFang = base.gameObject;
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFanParent = base.gameObject.GetComponentInParent<DanFangParentCell>();
		LianDanSystemManager.inst.DanFangPageManager.curSelectJSONObject = this.danFang;
		if (LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg != null)
		{
			LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg.SetActive(false);
		}
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg = this.HightImage;
		LianDanSystemManager.inst.DanFangPageManager.curSelectDanFangBg.SetActive(true);
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(this.danFang))
		{
			this.yaoCaiIDList = this.danFang["Type"];
			this.yaoCaiSumList = this.danFang["Num"];
			ItemDatebase component = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
			for (int i = 0; i < this.yaoCaiIDList.Count; i++)
			{
				if (this.yaoCaiIDList[i].I > 0)
				{
					LianDanSystemManager.inst.inventory.inventory[i + 25] = component.items[this.yaoCaiIDList[i].I].Clone();
					LianDanSystemManager.inst.inventory.inventory[i + 25].itemNum = this.yaoCaiSumList[i].I;
					LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[i].updateItem();
					LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
				}
			}
			return;
		}
		UIPopTip.Inst.Pop("材料不足", PopTipIconType.叹号);
	}

	// Token: 0x040014F3 RID: 5363
	public JSONObject danFang;

	// Token: 0x040014F4 RID: 5364
	private JSONObject yaoCaiIDList;

	// Token: 0x040014F5 RID: 5365
	private JSONObject yaoCaiSumList;

	// Token: 0x040014F6 RID: 5366
	[SerializeField]
	private Text ZhuYao;

	// Token: 0x040014F7 RID: 5367
	[SerializeField]
	private Text FuYao;

	// Token: 0x040014F8 RID: 5368
	[SerializeField]
	private Text YaoYing;

	// Token: 0x040014F9 RID: 5369
	[SerializeField]
	private GameObject CanLianZhiImage;

	// Token: 0x040014FA RID: 5370
	[SerializeField]
	private GameObject Line;

	// Token: 0x040014FB RID: 5371
	[SerializeField]
	private GameObject HightImage;
}
