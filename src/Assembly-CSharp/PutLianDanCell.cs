using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000447 RID: 1095
public class PutLianDanCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06001D26 RID: 7462 RVA: 0x00018495 File Offset: 0x00016695
	// (set) Token: 0x06001D25 RID: 7461 RVA: 0x00100B9C File Offset: 0x000FED9C
	public bool isLock
	{
		get
		{
			return this._isLock;
		}
		set
		{
			this._isLock = value;
			if (this._isLock)
			{
				base.GetComponent<Image>().sprite = LianDanSystemManager.inst.LockSprite;
				if (this.Item.itemID != -1)
				{
					LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
					UIPopTip.Inst.Pop("丹炉品级不足", PopTipIconType.叹号);
					return;
				}
			}
			else
			{
				base.GetComponent<Image>().sprite = LianDanSystemManager.inst.NoLockSprite;
			}
		}
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0001849D File Offset: 0x0001669D
	private void Awake()
	{
		this.inventory = LianDanSystemManager.inst.inventory;
		this.updateItem();
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x00100C10 File Offset: 0x000FEE10
	public void updateItem()
	{
		if (this.inventory == null)
		{
			this.inventory = LianDanSystemManager.inst.inventory;
		}
		this.Item = this.inventory.inventory[int.Parse(base.name)];
		if (this.Item.itemID != -1)
		{
			Texture2D texture2D = this.inventory.inventory[int.Parse(base.name)].itemIcon;
			Texture2D itemPingZhi = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
			this.itemIcon.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			this.PingZhi.sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)itemPingZhi.width, (float)itemPingZhi.height), new Vector2(0.5f, 0.5f));
			this.itemIcon.gameObject.SetActive(true);
			this.PingZhi.gameObject.SetActive(true);
			string text = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["name"].str;
			text = text.Replace("[cce281]", "");
			text = text.Replace("[-]", "");
			this.NameBG.SetActive(true);
			this.itemName.text = Tools.Code64(Tools.setColorByID(text, this.Item.itemID));
			this.itemSum.gameObject.SetActive(true);
			this.itemSum.text = Tools.Code64(this.Item.itemNum.ToString());
			return;
		}
		this.itemIcon.gameObject.SetActive(false);
		this.PingZhi.gameObject.SetActive(false);
		this.NameBG.SetActive(false);
		this.itemSum.gameObject.SetActive(false);
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x00100E3C File Offset: 0x000FF03C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf)
		{
			return;
		}
		if (this.inventory.inventory[int.Parse(base.name)].itemName != null && !LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf)
		{
			this.inventory.Show_Tooltip(this.inventory.inventory[int.Parse(base.name)], this.getItemPrice(), 0);
			this.inventory.showTooltip = true;
			return;
		}
		this.inventory.showTooltip = false;
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x00100EE0 File Offset: 0x000FF0E0
	public virtual int getItemPrice()
	{
		return (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(this.inventory.inventory[int.Parse(base.name)].itemID)]["price"].n) * 0.5f);
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x000184B5 File Offset: 0x000166B5
	public void OnPointerExit(PointerEventData eventData)
	{
		if (LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf)
		{
			return;
		}
		this.inventory.showTooltip = false;
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x000184DA File Offset: 0x000166DA
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == 1 && this.CanClickRight())
		{
			this.removeItem();
			return;
		}
		if (eventData.button == null && this.CanClickLeft())
		{
			this.selectItem();
		}
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x00100F40 File Offset: 0x000FF140
	public void selectItem()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = false;
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.setCurSelectIndex(int.Parse(base.name));
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.openCaiLiaoPackge((int)this.itemType);
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x00100F8C File Offset: 0x000FF18C
	public void removeItem()
	{
		int num = this.inventory.inventory[int.Parse(base.name)].itemNum - 1;
		if (num <= 0)
		{
			this.inventory.inventory[int.Parse(base.name)] = new item();
			this.inventory.inventory[int.Parse(base.name)].itemNum = 0;
		}
		else
		{
			this.inventory.inventory[int.Parse(base.name)].itemNum = num;
		}
		this.updateItem();
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x0010102C File Offset: 0x000FF22C
	private bool CanClickRight()
	{
		return this.itemType != ItemTypes.丹炉 && !LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf && LianDanSystemManager.inst.lianDanPageManager.CanClick && !this.isLock && this.Item.itemID != -1;
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x0001850A File Offset: 0x0001670A
	private bool CanClickLeft()
	{
		return !LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf && !this.isLock && LianDanSystemManager.inst.lianDanPageManager.CanClick;
	}

	// Token: 0x0400191A RID: 6426
	[SerializeField]
	private Image itemIcon;

	// Token: 0x0400191B RID: 6427
	[SerializeField]
	private GameObject NameBG;

	// Token: 0x0400191C RID: 6428
	[SerializeField]
	private Text itemName;

	// Token: 0x0400191D RID: 6429
	[SerializeField]
	private Text itemSum;

	// Token: 0x0400191E RID: 6430
	[SerializeField]
	private Image PingZhi;

	// Token: 0x0400191F RID: 6431
	[HideInInspector]
	public LianDanInventory inventory;

	// Token: 0x04001920 RID: 6432
	[HideInInspector]
	public item Item = new item();

	// Token: 0x04001921 RID: 6433
	public ItemTypes itemType;

	// Token: 0x04001922 RID: 6434
	private Button button;

	// Token: 0x04001923 RID: 6435
	private bool _isLock;
}
