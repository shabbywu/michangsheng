using System;
using Bag;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003A6 RID: 934
public class UIXiuChuanPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001E6A RID: 7786 RVA: 0x000D60B3 File Offset: 0x000D42B3
	private void Awake()
	{
		UIXiuChuanPanel.Inst = this;
		this.CancelBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x000D60E2 File Offset: 0x000D42E2
	public void Close()
	{
		UIXiuChuanPanel.Inst = null;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x000D60F8 File Offset: 0x000D42F8
	public void SetLingZhou(ITEM_INFO item)
	{
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item.itemId];
		int i = item.Seid["NaiJiu"].I;
		JToken jtoken = jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()];
		int qualityNaiJiu = (int)jtoken["Naijiu"];
		int num = (int)jtoken["xiuli"];
		int num2 = qualityNaiJiu - i;
		int needLingShi = num2 * num;
		this.Slot.SetSlotData(BaseItem.Create(item.itemId, 1, item.uuid, item.Seid));
		this.Tip.text = string.Format("修复<color=#{0}>{1}</color>{2}耐久需要{3}灵石", new object[]
		{
			ColorEx.ItemQualityColor[itemJsonData.quality - 1].ColorToString(),
			itemJsonData.name,
			num2,
			needLingShi
		});
		this.OkBtn.mouseUpEvent.AddListener(delegate()
		{
			Avatar player = PlayerEx.Player;
			if ((int)player.money >= needLingShi)
			{
				this.Close();
				player.AddMoney(-needLingShi);
				item.Seid.SetField("NaiJiu", qualityNaiJiu);
				UIPopTip.Inst.Pop("修复完毕", PopTipIconType.叹号);
				return;
			}
			UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
		});
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x000D6248 File Offset: 0x000D4448
	public static void OpenDefaultXiuChuan()
	{
		AvatarBase player = PlayerEx.Player;
		ITEM_INFO item_INFO = null;
		_ItemJsonData itemJsonData = null;
		foreach (ITEM_INFO item_INFO2 in player.equipItemList.values)
		{
			_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[item_INFO2.itemId];
			if (itemJsonData2.type == 14)
			{
				item_INFO = item_INFO2;
				itemJsonData = itemJsonData2;
				break;
			}
		}
		if (item_INFO == null)
		{
			UIPopTip.Inst.Pop("当前没有装备灵舟", PopTipIconType.叹号);
			return;
		}
		if (item_INFO.Seid == null || !item_INFO.Seid.HasField("NaiJiu"))
		{
			UIPopTip.Inst.Pop("灵舟数据异常", PopTipIconType.叹号);
			return;
		}
		int i = item_INFO.Seid["NaiJiu"].I;
		int num = (int)jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()]["Naijiu"];
		if (i >= num)
		{
			UIPopTip.Inst.Pop("此灵舟不需要修理", PopTipIconType.叹号);
			return;
		}
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UIXiuChuan"), NewUICanvas.Inst.Canvas.transform).GetComponent<UIXiuChuanPanel>().SetLingZhou(item_INFO);
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x000D6384 File Offset: 0x000D4584
	bool IESCClose.TryEscClose()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		this.Close();
		return true;
	}

	// Token: 0x040018ED RID: 6381
	public static UIXiuChuanPanel Inst;

	// Token: 0x040018EE RID: 6382
	public Text Tip;

	// Token: 0x040018EF RID: 6383
	public BaseSlot Slot;

	// Token: 0x040018F0 RID: 6384
	public FpBtn OkBtn;

	// Token: 0x040018F1 RID: 6385
	public FpBtn CancelBtn;
}
