using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LianDanResultCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private Image itemIcon;

	[SerializeField]
	private GameObject NameBG;

	[SerializeField]
	private Text itemName;

	[SerializeField]
	private Text itemSum;

	[SerializeField]
	private Image PingZhi;

	[HideInInspector]
	public LianDanInventory inventory;

	[HideInInspector]
	public item Item = new item();

	public void updateItem()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)inventory == (Object)null)
		{
			inventory = LianDanSystemManager.inst.inventory;
		}
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (Item.itemID != -1)
		{
			Texture2D val = inventory.inventory[int.Parse(((Object)this).name)].itemIcon;
			Texture2D itemPingZhi = inventory.inventory[int.Parse(((Object)this).name)].itemPingZhi;
			itemIcon.sprite = Sprite.Create(val, new Rect(0f, 0f, (float)((Texture)val).width, (float)((Texture)val).height), new Vector2(0.5f, 0.5f));
			PingZhi.sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)((Texture)itemPingZhi).width, (float)((Texture)itemPingZhi).height), new Vector2(0.5f, 0.5f));
			((Component)itemIcon).gameObject.SetActive(true);
			((Component)PingZhi).gameObject.SetActive(true);
			string str = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["name"].str;
			str = str.Replace("[cce281]", "");
			str = str.Replace("[-]", "");
			NameBG.SetActive(true);
			itemName.text = Tools.Code64(Tools.setColorByID(str, Item.itemID));
			((Component)itemSum).gameObject.SetActive(true);
			itemSum.text = Tools.Code64(Item.itemNum.ToString());
		}
		else
		{
			((Component)itemIcon).gameObject.SetActive(false);
			((Component)PingZhi).gameObject.SetActive(false);
			NameBG.SetActive(false);
			((Component)itemSum).gameObject.SetActive(false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (inventory.inventory[int.Parse(((Object)this).name)].itemName != null)
		{
			inventory.Show_Tooltip(inventory.inventory[int.Parse(((Object)this).name)], getItemPrice());
			inventory.showTooltip = true;
		}
		else
		{
			inventory.showTooltip = false;
		}
	}

	public virtual int getItemPrice()
	{
		return (int)((float)(int)jsonData.instance.ItemJsonData[string.Concat(inventory.inventory[int.Parse(((Object)this).name)].itemID)]["price"].n * 0.5f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		inventory.showTooltip = false;
	}
}
