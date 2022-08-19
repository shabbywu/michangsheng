using System;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002DE RID: 734
public class UIMiniShop : MonoBehaviour, IESCClose
{
	// Token: 0x06001979 RID: 6521 RVA: 0x000B6340 File Offset: 0x000B4540
	public static void Show(int itemID, int price, int maxSellCount, Command cmd = null)
	{
		UIMiniShop component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UIMiniShop"), NewUICanvas.Inst.Canvas.transform).GetComponent<UIMiniShop>();
		component.itemID = itemID;
		component.price = price;
		component.cmd = cmd;
		component.maxSellCount = maxSellCount;
		component.RefreshUI();
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x000B6394 File Offset: 0x000B4594
	public void RefreshUI()
	{
		UIMiniShop.Inst = this;
		this.item = BaseItem.Create(this.itemID, 1, Tools.getUUID(), Tools.CreateItemSeid(this.itemID));
		this.ItemSlot.SetSlotData(this.item);
		this.OkBtn.mouseUpEvent.AddListener(new UnityAction(this.OnOkBtnClick));
		this.CloseBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		this.LeftBtn.mouseUpEvent.AddListener(new UnityAction(this.OnSubClick));
		this.RightBtn.mouseUpEvent.AddListener(new UnityAction(this.OnAddClick));
		this.NumSlider.minValue = 0f;
		int num = (int)PlayerEx.Player.money;
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.itemID];
		if (this.price == 0)
		{
			this.price = itemJsonData.price;
			if (this.price == 0)
			{
				this.price = 1;
				Debug.LogError("MiniShop传入了价格为0的商品，以自动保底为1灵石");
			}
		}
		int num2 = num / this.price;
		if (this.maxSellCount > 0)
		{
			num2 = Mathf.Min(num2, this.maxSellCount);
		}
		this.NumSlider.maxValue = (float)num2;
		if (num2 < 1)
		{
			this.NumSlider.interactable = false;
			this.LeftBtn.enabled = false;
			this.RightBtn.enabled = false;
			return;
		}
		this.NumSlider.value = 1f;
		this.nowSelectCount = 1;
		this.RefreshCount();
		this.NumSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderValueChanged));
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x000B6530 File Offset: 0x000B4730
	public void RefreshCount()
	{
		this.ItemSlot.Item.Count = this.nowSelectCount;
		this.ItemSlot.UpdateUI();
		this.CostText.text = (this.nowSelectCount * this.price).ToString();
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x000B6580 File Offset: 0x000B4780
	public void OnSliderValueChanged(float value)
	{
		int num = Mathf.RoundToInt(value);
		this.nowSelectCount = num;
		this.RefreshCount();
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x000B65A1 File Offset: 0x000B47A1
	public void OnAddClick()
	{
		this.NumSlider.value += 1f;
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x000B65BA File Offset: 0x000B47BA
	public void OnSubClick()
	{
		this.NumSlider.value -= 1f;
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x000B65D4 File Offset: 0x000B47D4
	public void OnOkBtnClick()
	{
		if (this.nowSelectCount == 0)
		{
			this.Close();
			return;
		}
		int num = this.nowSelectCount * this.price;
		Avatar player = PlayerEx.Player;
		if ((int)player.money >= num)
		{
			player.AddMoney(-num);
			player.addItem(this.itemID, this.item.Seid, this.item.Count);
			UIPopTip.Inst.PopAddItem(this.item.GetName(), this.item.Count);
			this.Close();
			return;
		}
		UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000B666F File Offset: 0x000B486F
	private void Close()
	{
		if (this.cmd != null)
		{
			this.cmd.Continue();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x000B6695 File Offset: 0x000B4895
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040014A9 RID: 5289
	public static UIMiniShop Inst;

	// Token: 0x040014AA RID: 5290
	public BaseSlot ItemSlot;

	// Token: 0x040014AB RID: 5291
	public Text CostText;

	// Token: 0x040014AC RID: 5292
	public FpBtn LeftBtn;

	// Token: 0x040014AD RID: 5293
	public FpBtn RightBtn;

	// Token: 0x040014AE RID: 5294
	public FpBtn OkBtn;

	// Token: 0x040014AF RID: 5295
	public FpBtn CloseBtn;

	// Token: 0x040014B0 RID: 5296
	public Slider NumSlider;

	// Token: 0x040014B1 RID: 5297
	private int itemID;

	// Token: 0x040014B2 RID: 5298
	private int price;

	// Token: 0x040014B3 RID: 5299
	private int maxSellCount;

	// Token: 0x040014B4 RID: 5300
	private BaseItem item;

	// Token: 0x040014B5 RID: 5301
	private Command cmd;

	// Token: 0x040014B6 RID: 5302
	private int nowSelectCount;
}
