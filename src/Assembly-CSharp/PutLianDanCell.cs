using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002EC RID: 748
public class PutLianDanCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000BA46C File Offset: 0x000B866C
	// (set) Token: 0x06001A01 RID: 6657 RVA: 0x000BA3F8 File Offset: 0x000B85F8
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

	// Token: 0x06001A03 RID: 6659 RVA: 0x000BA474 File Offset: 0x000B8674
	private void Awake()
	{
		this.inventory = LianDanSystemManager.inst.inventory;
		this.updateItem();
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x000BA48C File Offset: 0x000B868C
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

	// Token: 0x06001A05 RID: 6661 RVA: 0x000BA6B8 File Offset: 0x000B88B8
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

	// Token: 0x06001A06 RID: 6662 RVA: 0x000BA75C File Offset: 0x000B895C
	public virtual int getItemPrice()
	{
		return (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(this.inventory.inventory[int.Parse(base.name)].itemID)]["price"].n) * 0.5f);
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x000BA7BA File Offset: 0x000B89BA
	public void OnPointerExit(PointerEventData eventData)
	{
		if (LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf)
		{
			return;
		}
		this.inventory.showTooltip = false;
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x000BA7DF File Offset: 0x000B89DF
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

	// Token: 0x06001A09 RID: 6665 RVA: 0x000BA810 File Offset: 0x000B8A10
	public void selectItem()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = false;
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.setCurSelectIndex(int.Parse(base.name));
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.openCaiLiaoPackge((int)this.itemType);
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x000BA85C File Offset: 0x000B8A5C
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

	// Token: 0x06001A0B RID: 6667 RVA: 0x000BA8FC File Offset: 0x000B8AFC
	private bool CanClickRight()
	{
		return this.itemType != ItemTypes.丹炉 && !LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf && LianDanSystemManager.inst.lianDanPageManager.CanClick && !this.isLock && this.Item.itemID != -1;
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000BA95B File Offset: 0x000B8B5B
	private bool CanClickLeft()
	{
		return !LianDanSystemManager.inst.selectLianDanCaiLiaoPage.gameObject.activeSelf && !this.isLock && LianDanSystemManager.inst.lianDanPageManager.CanClick;
	}

	// Token: 0x04001514 RID: 5396
	[SerializeField]
	private Image itemIcon;

	// Token: 0x04001515 RID: 5397
	[SerializeField]
	private GameObject NameBG;

	// Token: 0x04001516 RID: 5398
	[SerializeField]
	private Text itemName;

	// Token: 0x04001517 RID: 5399
	[SerializeField]
	private Text itemSum;

	// Token: 0x04001518 RID: 5400
	[SerializeField]
	private Image PingZhi;

	// Token: 0x04001519 RID: 5401
	[HideInInspector]
	public LianDanInventory inventory;

	// Token: 0x0400151A RID: 5402
	[HideInInspector]
	public item Item = new item();

	// Token: 0x0400151B RID: 5403
	public ItemTypes itemType;

	// Token: 0x0400151C RID: 5404
	private Button button;

	// Token: 0x0400151D RID: 5405
	private bool _isLock;
}
