using System;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000430 RID: 1072
public class UIMiniShop : MonoBehaviour, IESCClose
{
	// Token: 0x06001C8F RID: 7311 RVA: 0x000FC24C File Offset: 0x000FA44C
	public static void Show(int itemID, int price, int maxSellCount, Command cmd = null)
	{
		UIMiniShop component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UIMiniShop"), NewUICanvas.Inst.Canvas.transform).GetComponent<UIMiniShop>();
		component.itemID = itemID;
		component.price = price;
		component.cmd = cmd;
		component.maxSellCount = maxSellCount;
		component.RefreshUI();
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x000FC2A0 File Offset: 0x000FA4A0
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

	// Token: 0x06001C91 RID: 7313 RVA: 0x000FC43C File Offset: 0x000FA63C
	public void RefreshCount()
	{
		this.ItemSlot.Item.Count = this.nowSelectCount;
		this.ItemSlot.UpdateUI();
		this.CostText.text = (this.nowSelectCount * this.price).ToString();
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x000FC48C File Offset: 0x000FA68C
	public void OnSliderValueChanged(float value)
	{
		int num = Mathf.RoundToInt(value);
		this.nowSelectCount = num;
		this.RefreshCount();
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x00017DBB File Offset: 0x00015FBB
	public void OnAddClick()
	{
		this.NumSlider.value += 1f;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x00017DD4 File Offset: 0x00015FD4
	public void OnSubClick()
	{
		this.NumSlider.value -= 1f;
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x000FC4B0 File Offset: 0x000FA6B0
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

	// Token: 0x06001C96 RID: 7318 RVA: 0x00017DED File Offset: 0x00015FED
	private void Close()
	{
		if (this.cmd != null)
		{
			this.cmd.Continue();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x00017E13 File Offset: 0x00016013
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001889 RID: 6281
	public static UIMiniShop Inst;

	// Token: 0x0400188A RID: 6282
	public BaseSlot ItemSlot;

	// Token: 0x0400188B RID: 6283
	public Text CostText;

	// Token: 0x0400188C RID: 6284
	public FpBtn LeftBtn;

	// Token: 0x0400188D RID: 6285
	public FpBtn RightBtn;

	// Token: 0x0400188E RID: 6286
	public FpBtn OkBtn;

	// Token: 0x0400188F RID: 6287
	public FpBtn CloseBtn;

	// Token: 0x04001890 RID: 6288
	public Slider NumSlider;

	// Token: 0x04001891 RID: 6289
	private int itemID;

	// Token: 0x04001892 RID: 6290
	private int price;

	// Token: 0x04001893 RID: 6291
	private int maxSellCount;

	// Token: 0x04001894 RID: 6292
	private BaseItem item;

	// Token: 0x04001895 RID: 6293
	private Command cmd;

	// Token: 0x04001896 RID: 6294
	private int nowSelectCount;
}
