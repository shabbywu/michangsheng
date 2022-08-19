using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000296 RID: 662
public class ChuanYingItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x060017D2 RID: 6098 RVA: 0x000A5080 File Offset: 0x000A3280
	public void init()
	{
		this.updateItem();
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x000A5088 File Offset: 0x000A3288
	public void updateItem()
	{
		if (this.Item.itemID == -1)
		{
			this.itemIcon.gameObject.SetActive(false);
			this.PingZhi.gameObject.SetActive(false);
			this.NameBG.SetActive(false);
			this.HasGetImage.gameObject.SetActive(false);
			return;
		}
		Texture2D texture2D = this.Item.itemIcon;
		Texture2D itemPingZhi = this.Item.itemPingZhi;
		this.itemIcon.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
		this.PingZhi.sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)itemPingZhi.width, (float)itemPingZhi.height), new Vector2(0.5f, 0.5f));
		this.itemIcon.gameObject.SetActive(true);
		this.PingZhi.gameObject.SetActive(true);
		string text = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["name"].str;
		text = text.Replace("[cce281]", "");
		text = text.Replace("[-]", "");
		this.NameBG.SetActive(true);
		this.itemName.text = Tools.Code64(Tools.setColorByID(text, this.Item.itemID));
		if (!this.hasItem)
		{
			this.HasGetImage.gameObject.SetActive(true);
			return;
		}
		this.HasGetImage.gameObject.SetActive(false);
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x000A5244 File Offset: 0x000A3444
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.Item.itemName != null && this.Item.itemID != -1)
		{
			this.inventory.Show_Tooltip(this.Item, this.getItemPrice(), 0);
			this.inventory.showTooltip = true;
			return;
		}
		this.inventory.showTooltip = false;
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x000A529D File Offset: 0x000A349D
	public virtual int getItemPrice()
	{
		return (int)((float)((int)jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["price"].n) * 0.5f);
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x000A52D6 File Offset: 0x000A34D6
	public void OnPointerExit(PointerEventData eventData)
	{
		this.inventory.showTooltip = false;
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x000A52E4 File Offset: 0x000A34E4
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == null)
		{
			this.CanClickLeft();
		}
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000A52F5 File Offset: 0x000A34F5
	private bool CanClickLeft()
	{
		return this.hasItem;
	}

	// Token: 0x04001287 RID: 4743
	[SerializeField]
	private Image itemIcon;

	// Token: 0x04001288 RID: 4744
	[SerializeField]
	private GameObject NameBG;

	// Token: 0x04001289 RID: 4745
	[SerializeField]
	private Text itemName;

	// Token: 0x0400128A RID: 4746
	[SerializeField]
	private Image PingZhi;

	// Token: 0x0400128B RID: 4747
	[SerializeField]
	private Image HasGetImage;

	// Token: 0x0400128C RID: 4748
	private Inventory2 inventory;

	// Token: 0x0400128D RID: 4749
	[HideInInspector]
	public item Item = new item();

	// Token: 0x0400128E RID: 4750
	public bool hasItem;

	// Token: 0x0400128F RID: 4751
	private Button button;
}
