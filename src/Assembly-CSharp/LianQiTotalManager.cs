using System;
using System.Collections.Generic;
using Bag;
using LianQi;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200044F RID: 1103
public class LianQiTotalManager : MonoBehaviour
{
	// Token: 0x06001D73 RID: 7539 RVA: 0x00018780 File Offset: 0x00016980
	public bool checkIsInLianQiPage()
	{
		return this.lianQiPanel.activeSelf;
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x00018792 File Offset: 0x00016992
	public void OpenBlack()
	{
		this.Black.SetActive(true);
	}

	// Token: 0x06001D75 RID: 7541 RVA: 0x000187A0 File Offset: 0x000169A0
	public void CloseBlack()
	{
		this.Black.SetActive(false);
	}

	// Token: 0x06001D76 RID: 7542 RVA: 0x000187AE File Offset: 0x000169AE
	public void setCurSelectEquipMuBanID(int num)
	{
		this.curSelectEquipMuBanID = num;
	}

	// Token: 0x06001D77 RID: 7543 RVA: 0x000187B7 File Offset: 0x000169B7
	public int getCurSelectEquipMuBanID()
	{
		return this.curSelectEquipMuBanID;
	}

	// Token: 0x06001D78 RID: 7544 RVA: 0x000187BF File Offset: 0x000169BF
	public void setCurSelectEquipType(int num)
	{
		this.curSelectEquipType = num;
	}

	// Token: 0x06001D79 RID: 7545 RVA: 0x000187C8 File Offset: 0x000169C8
	public int getCurSelectEquipType()
	{
		return this.curSelectEquipType;
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x0010240C File Offset: 0x0010060C
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

	// Token: 0x06001D7B RID: 7547 RVA: 0x000187D0 File Offset: 0x000169D0
	public void openLianQiPanle()
	{
		this.init();
		this.selectTypePageManager.OpenSelectEquipPanel();
	}

	// Token: 0x06001D7C RID: 7548 RVA: 0x000187E3 File Offset: 0x000169E3
	private void init()
	{
		this.curSelectEquipMuBanID = -1;
		this.curSelectEquipType = -1;
		this.selectTypePageManager.init();
		this.putMaterialPageManager.init();
		this.lianQiResultManager.init();
	}

	// Token: 0x06001D7D RID: 7549 RVA: 0x000187E3 File Offset: 0x000169E3
	public void ChangeEquip()
	{
		this.curSelectEquipMuBanID = -1;
		this.curSelectEquipType = -1;
		this.selectTypePageManager.init();
		this.putMaterialPageManager.init();
		this.lianQiResultManager.init();
	}

	// Token: 0x06001D7E RID: 7550 RVA: 0x00018814 File Offset: 0x00016A14
	public void closeLianQiPanel()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼器, 0);
	}

	// Token: 0x06001D7F RID: 7551 RVA: 0x00018822 File Offset: 0x00016A22
	public void restartSelectEquip()
	{
		this.ChangeEquip();
		this.selectTypePageManager.OpenSelectEquipPanel();
	}

	// Token: 0x06001D80 RID: 7552 RVA: 0x001024A8 File Offset: 0x001006A8
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

	// Token: 0x06001D81 RID: 7553 RVA: 0x00018835 File Offset: 0x00016A35
	public void selectEquipCallBack()
	{
		this.putMaterialPageManager.openPutMaterialPage();
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x00018842 File Offset: 0x00016A42
	public void selectLingWenCiTiaoCallBack()
	{
		this.putMaterialPageManager.zhongLingLiManager.updateZhongLingLi();
		this.putMaterialPageManager.showXiaoGuoManager.updateEquipCiTiao();
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x00102520 File Offset: 0x00100720
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

	// Token: 0x06001D84 RID: 7556 RVA: 0x00102648 File Offset: 0x00100848
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

	// Token: 0x06001D85 RID: 7557 RVA: 0x001026C0 File Offset: 0x001008C0
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

	// Token: 0x06001D86 RID: 7558 RVA: 0x00018864 File Offset: 0x00016A64
	public void lianQiSuccess()
	{
		this.lianQiResultManager.addLianQiTime();
		Tools.instance.playFader("正在炼制法宝...", delegate
		{
			this.OpenBlack();
			UCheckBox.Show("法阵之中灵气涌动，最终灵气四溢开来，炼成的灵宝呈现在你眼前", new UnityAction(this.lianQiResultManager.lianQiSuccessResult));
		});
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x0001888C File Offset: 0x00016A8C
	public void lianQiFaile()
	{
		this.lianQiResultManager.addLianQiTime();
		Tools.instance.playFader("正在炼制法宝...", delegate
		{
			this.OpenBlack();
			UCheckBox.Show("法阵之中灵气涌动，最终灵气四射开来，眼前的器胚“轰”的一声爆炸了", new UnityAction(this.lianQiResultManager.lianQiFailResult));
		});
	}

	// Token: 0x06001D88 RID: 7560 RVA: 0x000188B4 File Offset: 0x00016AB4
	public void OpenBag()
	{
		this.Bag.Open();
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x000188C1 File Offset: 0x00016AC1
	public void CloseBag()
	{
		this.Bag.Close();
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x00102744 File Offset: 0x00100944
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

	// Token: 0x06001D8B RID: 7563 RVA: 0x001027C8 File Offset: 0x001009C8
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

	// Token: 0x0400193D RID: 6461
	public static LianQiTotalManager inst;

	// Token: 0x0400193E RID: 6462
	[SerializeField]
	private GameObject lianQiPanel;

	// Token: 0x0400193F RID: 6463
	public SelectTypePageManager selectTypePageManager;

	// Token: 0x04001940 RID: 6464
	public GameObject Black;

	// Token: 0x04001941 RID: 6465
	public PutMaterialPageManager putMaterialPageManager;

	// Token: 0x04001942 RID: 6466
	public LianQiBag Bag;

	// Token: 0x04001943 RID: 6467
	public LianQiResultManager lianQiResultManager;

	// Token: 0x04001944 RID: 6468
	public LianQiSlot ToSlot;

	// Token: 0x04001945 RID: 6469
	public bool IsFirstSelect = true;

	// Token: 0x04001946 RID: 6470
	public Object[] equipSprites;

	// Token: 0x04001947 RID: 6471
	private int curSelectEquipMuBanID = -1;

	// Token: 0x04001948 RID: 6472
	private int curSelectEquipType = -1;
}
