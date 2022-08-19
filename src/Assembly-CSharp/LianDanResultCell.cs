using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002EB RID: 747
public class LianDanResultCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060019FC RID: 6652 RVA: 0x000BA0D4 File Offset: 0x000B82D4
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

	// Token: 0x060019FD RID: 6653 RVA: 0x000BA300 File Offset: 0x000B8500
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

	// Token: 0x060019FE RID: 6654 RVA: 0x000BA378 File Offset: 0x000B8578
	public virtual int getItemPrice()
	{
		return (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(this.inventory.inventory[int.Parse(base.name)].itemID)]["price"].n) * 0.5f);
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000BA3D6 File Offset: 0x000B85D6
	public void OnPointerExit(PointerEventData eventData)
	{
		this.inventory.showTooltip = false;
	}

	// Token: 0x0400150D RID: 5389
	[SerializeField]
	private Image itemIcon;

	// Token: 0x0400150E RID: 5390
	[SerializeField]
	private GameObject NameBG;

	// Token: 0x0400150F RID: 5391
	[SerializeField]
	private Text itemName;

	// Token: 0x04001510 RID: 5392
	[SerializeField]
	private Text itemSum;

	// Token: 0x04001511 RID: 5393
	[SerializeField]
	private Image PingZhi;

	// Token: 0x04001512 RID: 5394
	[HideInInspector]
	public LianDanInventory inventory;

	// Token: 0x04001513 RID: 5395
	[HideInInspector]
	public item Item = new item();
}
