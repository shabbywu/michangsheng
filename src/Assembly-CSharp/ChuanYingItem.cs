using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003C6 RID: 966
public class ChuanYingItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06001AB2 RID: 6834 RVA: 0x00016AB8 File Offset: 0x00014CB8
	public void init()
	{
		this.updateItem();
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x000EC0BC File Offset: 0x000EA2BC
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

	// Token: 0x06001AB4 RID: 6836 RVA: 0x000EC278 File Offset: 0x000EA478
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

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00016AC0 File Offset: 0x00014CC0
	public virtual int getItemPrice()
	{
		return (int)((float)((int)jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["price"].n) * 0.5f);
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x00016AF9 File Offset: 0x00014CF9
	public void OnPointerExit(PointerEventData eventData)
	{
		this.inventory.showTooltip = false;
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x00016B07 File Offset: 0x00014D07
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == null)
		{
			this.CanClickLeft();
		}
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x00016B18 File Offset: 0x00014D18
	private bool CanClickLeft()
	{
		return this.hasItem;
	}

	// Token: 0x04001610 RID: 5648
	[SerializeField]
	private Image itemIcon;

	// Token: 0x04001611 RID: 5649
	[SerializeField]
	private GameObject NameBG;

	// Token: 0x04001612 RID: 5650
	[SerializeField]
	private Text itemName;

	// Token: 0x04001613 RID: 5651
	[SerializeField]
	private Image PingZhi;

	// Token: 0x04001614 RID: 5652
	[SerializeField]
	private Image HasGetImage;

	// Token: 0x04001615 RID: 5653
	private Inventory2 inventory;

	// Token: 0x04001616 RID: 5654
	[HideInInspector]
	public item Item = new item();

	// Token: 0x04001617 RID: 5655
	public bool hasItem;

	// Token: 0x04001618 RID: 5656
	private Button button;
}
