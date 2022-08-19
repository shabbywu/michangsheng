using System;
using System.Collections.Generic;
using Bag;
using LianQi;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002F2 RID: 754
public class LianQiTotalManager : MonoBehaviour
{
	// Token: 0x06001A4D RID: 6733 RVA: 0x000BBF51 File Offset: 0x000BA151
	public bool checkIsInLianQiPage()
	{
		return this.lianQiPanel.activeSelf;
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x000BBF63 File Offset: 0x000BA163
	public void OpenBlack()
	{
		this.Black.SetActive(true);
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000BBF71 File Offset: 0x000BA171
	public void CloseBlack()
	{
		this.Black.SetActive(false);
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000BBF7F File Offset: 0x000BA17F
	public void setCurSelectEquipMuBanID(int num)
	{
		this.curSelectEquipMuBanID = num;
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x000BBF88 File Offset: 0x000BA188
	public int getCurSelectEquipMuBanID()
	{
		return this.curSelectEquipMuBanID;
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x000BBF90 File Offset: 0x000BA190
	public void setCurSelectEquipType(int num)
	{
		this.curSelectEquipType = num;
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x000BBF99 File Offset: 0x000BA199
	public int getCurSelectEquipType()
	{
		return this.curSelectEquipType;
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x000BBFA4 File Offset: 0x000BA1A4
	private void Awake()
	{
		LianQiTotalManager.inst = this;
		Transform transform = base.transform;
		transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		transform.localScale = Vector3.one;
		transform.SetAsLastSibling();
		transform.localPosition = Vector3.zero;
		this.equipSprites = ResManager.inst.LoadSprites("NewUI/LianQi/BigEquip/BigEquip");
		this.openLianQiPanle();
		if (UIHeadPanel.Inst != null)
		{
			UIHeadPanel.Inst.transform.SetAsLastSibling();
		}
		if (UIMiniTaskPanel.Inst != null)
		{
			UIMiniTaskPanel.Inst.transform.SetAsLastSibling();
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x000BC040 File Offset: 0x000BA240
	public void openLianQiPanle()
	{
		this.init();
		this.selectTypePageManager.OpenSelectEquipPanel();
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x000BC053 File Offset: 0x000BA253
	private void init()
	{
		this.curSelectEquipMuBanID = -1;
		this.curSelectEquipType = -1;
		this.selectTypePageManager.init();
		this.putMaterialPageManager.init();
		this.lianQiResultManager.init();
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x000BC053 File Offset: 0x000BA253
	public void ChangeEquip()
	{
		this.curSelectEquipMuBanID = -1;
		this.curSelectEquipType = -1;
		this.selectTypePageManager.init();
		this.putMaterialPageManager.init();
		this.lianQiResultManager.init();
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x000BC084 File Offset: 0x000BA284
	public void closeLianQiPanel()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼器, 0);
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x000BC092 File Offset: 0x000BA292
	public void restartSelectEquip()
	{
		this.ChangeEquip();
		this.selectTypePageManager.OpenSelectEquipPanel();
	}

	// Token: 0x06001A5A RID: 6746 RVA: 0x000BC0A8 File Offset: 0x000BA2A8
	public void putCaiLiaoCallBack()
	{
		this.putMaterialPageManager.wuWeiManager.updateWuWei();
		this.putMaterialPageManager.chuShiLingLiManager.updateChushiLingLi();
		this.putMaterialPageManager.zhongLingLiManager.updateZhongLingLi();
		this.putMaterialPageManager.lianQiPageManager.showEquipCell.updateEquipPingJie();
		this.putMaterialPageManager.showXiaoGuoManager.updateEquipCiTiao();
		this.putMaterialPageManager.updateWuXingXiangKeTips();
		this.putMaterialPageManager.updateCaiLiaoSum();
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x000BC120 File Offset: 0x000BA320
	public void selectEquipCallBack()
	{
		this.putMaterialPageManager.openPutMaterialPage();
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x000BC12D File Offset: 0x000BA32D
	public void selectLingWenCiTiaoCallBack()
	{
		this.putMaterialPageManager.zhongLingLiManager.updateZhongLingLi();
		this.putMaterialPageManager.showXiaoGuoManager.updateEquipCiTiao();
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x000BC150 File Offset: 0x000BA350
	public string buildNomalLingWenDesc(JSONObject obj)
	{
		string text = (obj["value3"].I == 1) ? "x" : "+";
		string result = "";
		switch (obj["type"].I)
		{
		case 1:
			result = string.Format("{0}<color=#fff227>x{1}</color>,灵力<color=#fff227>{2}{3}</color>", new object[]
			{
				obj["desc"].str,
				obj["value1"].I,
				text,
				obj["value4"].I
			});
			break;
		case 2:
			result = string.Format("对自己造成<color=#fff227>x{0}</color>点真实伤害,灵力<color=#fff227>{1}{2}</color>", obj["value1"].I, text, obj["value4"].I);
			break;
		case 4:
			result = string.Format("{0}才能使用,灵力<color=#fff227>{1}{2}</color>", obj["desc"].str, text, obj["value4"].n);
			break;
		}
		return result;
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x000BC278 File Offset: 0x000BA478
	public int getLingWenIDByBUFFIDAndSum(int id, int sum)
	{
		List<JSONObject> list = jsonData.instance.LianQiLingWenBiao.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["value1"].I == id && list[i]["value2"].I == sum)
			{
				return list[i]["id"].I;
			}
		}
		return -1;
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x000BC2F0 File Offset: 0x000BA4F0
	public JToken getcurEquipQualityDate()
	{
		JObject lianQiWuQiQuality = jsonData.instance.LianQiWuQiQuality;
		int realZongLingLi = LianQiTotalManager.inst.putMaterialPageManager.zhongLingLiManager.getRealZongLingLi();
		JToken result = null;
		foreach (KeyValuePair<string, JToken> keyValuePair in lianQiWuQiQuality)
		{
			if (realZongLingLi >= (int)keyValuePair.Value["power"])
			{
				result = keyValuePair.Value;
			}
		}
		return result;
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x000BC374 File Offset: 0x000BA574
	public void lianQiSuccess()
	{
		this.lianQiResultManager.addLianQiTime();
		Tools.instance.playFader("正在炼制法宝...", delegate
		{
			this.OpenBlack();
			UCheckBox.Show("法阵之中灵气涌动，最终灵气四溢开来，炼成的灵宝呈现在你眼前", new UnityAction(this.lianQiResultManager.lianQiSuccessResult));
		});
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x000BC39C File Offset: 0x000BA59C
	public void lianQiFaile()
	{
		this.lianQiResultManager.addLianQiTime();
		Tools.instance.playFader("正在炼制法宝...", delegate
		{
			this.OpenBlack();
			UCheckBox.Show("法阵之中灵气涌动，最终灵气四射开来，眼前的器胚“轰”的一声爆炸了", new UnityAction(this.lianQiResultManager.lianQiFailResult));
		});
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000BC3C4 File Offset: 0x000BA5C4
	public void OpenBag()
	{
		this.Bag.Open();
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x000BC3D1 File Offset: 0x000BA5D1
	public void CloseBag()
	{
		this.Bag.Close();
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x000BC3E0 File Offset: 0x000BA5E0
	public void PutItem(LianQiSlot dragSlot)
	{
		if (this.ToSlot == null)
		{
			return;
		}
		BaseItem baseItem = dragSlot.Item.Clone();
		baseItem.Count = 1;
		if (!this.ToSlot.IsNull())
		{
			this.Bag.AddTempItem(this.ToSlot.Item, 1);
		}
		this.Bag.RemoveTempItem(dragSlot.Item.Uid, 1);
		this.ToSlot.SetSlotData(baseItem);
		this.CloseBag();
		this.putCaiLiaoCallBack();
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x000BC464 File Offset: 0x000BA664
	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		LianQiTotalManager.inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼器, 1);
		if (UIMiniTaskPanel.Inst != null)
		{
			UIMiniTaskPanel.Inst.transform.SetAsFirstSibling();
		}
		if (UIHeadPanel.Inst != null)
		{
			UIHeadPanel.Inst.transform.SetAsFirstSibling();
		}
	}

	// Token: 0x04001530 RID: 5424
	public static LianQiTotalManager inst;

	// Token: 0x04001531 RID: 5425
	[SerializeField]
	private GameObject lianQiPanel;

	// Token: 0x04001532 RID: 5426
	public SelectTypePageManager selectTypePageManager;

	// Token: 0x04001533 RID: 5427
	public GameObject Black;

	// Token: 0x04001534 RID: 5428
	public PutMaterialPageManager putMaterialPageManager;

	// Token: 0x04001535 RID: 5429
	public LianQiBag Bag;

	// Token: 0x04001536 RID: 5430
	public LianQiResultManager lianQiResultManager;

	// Token: 0x04001537 RID: 5431
	public LianQiSlot ToSlot;

	// Token: 0x04001538 RID: 5432
	public bool IsFirstSelect = true;

	// Token: 0x04001539 RID: 5433
	public Object[] equipSprites;

	// Token: 0x0400153A RID: 5434
	private int curSelectEquipMuBanID = -1;

	// Token: 0x0400153B RID: 5435
	private int curSelectEquipType = -1;
}
