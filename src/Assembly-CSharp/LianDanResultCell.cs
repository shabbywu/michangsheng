using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000446 RID: 1094
public class LianDanResultCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001D20 RID: 7456 RVA: 0x00100898 File Offset: 0x000FEA98
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

	// Token: 0x06001D21 RID: 7457 RVA: 0x00100AC4 File Offset: 0x000FECC4
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.inventory.inventory[int.Parse(base.name)].itemName != null)
		{
			this.inventory.Show_Tooltip(this.inventory.inventory[int.Parse(base.name)], this.getItemPrice(), 0);
			this.inventory.showTooltip = true;
			return;
		}
		this.inventory.showTooltip = false;
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x00100B3C File Offset: 0x000FED3C
	public virtual int getItemPrice()
	{
		return (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(this.inventory.inventory[int.Parse(base.name)].itemID)]["price"].n) * 0.5f);
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x00018474 File Offset: 0x00016674
	public void OnPointerExit(PointerEventData eventData)
	{
		this.inventory.showTooltip = false;
	}

	// Token: 0x04001913 RID: 6419
	[SerializeField]
	private Image itemIcon;

	// Token: 0x04001914 RID: 6420
	[SerializeField]
	private GameObject NameBG;

	// Token: 0x04001915 RID: 6421
	[SerializeField]
	private Text itemName;

	// Token: 0x04001916 RID: 6422
	[SerializeField]
	private Text itemSum;

	// Token: 0x04001917 RID: 6423
	[SerializeField]
	private Image PingZhi;

	// Token: 0x04001918 RID: 6424
	[HideInInspector]
	public LianDanInventory inventory;

	// Token: 0x04001919 RID: 6425
	[HideInInspector]
	public item Item = new item();
}
