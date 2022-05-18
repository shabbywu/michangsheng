using System;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000441 RID: 1089
public class DanFangChildCell : MonoBehaviour
{
	// Token: 0x06001CFA RID: 7418 RVA: 0x000FF7C4 File Offset: 0x000FD9C4
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

	// Token: 0x06001CFB RID: 7419 RVA: 0x000182FD File Offset: 0x000164FD
	public void updateState()
	{
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(this.danFang))
		{
			this.CanLianZhiImage.SetActive(true);
			return;
		}
		this.CanLianZhiImage.SetActive(false);
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x0001832F File Offset: 0x0001652F
	public void hideLine()
	{
		this.Line.SetActive(false);
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x0001833D File Offset: 0x0001653D
	public void showLine()
	{
		this.Line.SetActive(true);
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x000FFA70 File Offset: 0x000FDC70
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

	// Token: 0x040018F7 RID: 6391
	public JSONObject danFang;

	// Token: 0x040018F8 RID: 6392
	private JSONObject yaoCaiIDList;

	// Token: 0x040018F9 RID: 6393
	private JSONObject yaoCaiSumList;

	// Token: 0x040018FA RID: 6394
	[SerializeField]
	private Text ZhuYao;

	// Token: 0x040018FB RID: 6395
	[SerializeField]
	private Text FuYao;

	// Token: 0x040018FC RID: 6396
	[SerializeField]
	private Text YaoYing;

	// Token: 0x040018FD RID: 6397
	[SerializeField]
	private GameObject CanLianZhiImage;

	// Token: 0x040018FE RID: 6398
	[SerializeField]
	private GameObject Line;

	// Token: 0x040018FF RID: 6399
	[SerializeField]
	private GameObject HightImage;
}
