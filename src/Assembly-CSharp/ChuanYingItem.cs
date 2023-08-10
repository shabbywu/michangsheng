using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChuanYingItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private Image itemIcon;

	[SerializeField]
	private GameObject NameBG;

	[SerializeField]
	private Text itemName;

	[SerializeField]
	private Image PingZhi;

	[SerializeField]
	private Image HasGetImage;

	private Inventory2 inventory;

	[HideInInspector]
	public item Item = new item();

	public bool hasItem;

	private Button button;

	public void init()
	{
		updateItem();
	}

	public void updateItem()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (Item.itemID != -1)
		{
			Texture2D val = Item.itemIcon;
			Texture2D itemPingZhi = Item.itemPingZhi;
			itemIcon.sprite = Sprite.Create(val, new Rect(0f, 0f, (float)((Texture)val).width, (float)((Texture)val).height), new Vector2(0.5f, 0.5f));
			PingZhi.sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)((Texture)itemPingZhi).width, (float)((Texture)itemPingZhi).height), new Vector2(0.5f, 0.5f));
			((Component)itemIcon).gameObject.SetActive(true);
			((Component)PingZhi).gameObject.SetActive(true);
			string str = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["name"].str;
			str = str.Replace("[cce281]", "");
			str = str.Replace("[-]", "");
			NameBG.SetActive(true);
			itemName.text = Tools.Code64(Tools.setColorByID(str, Item.itemID));
			if (!hasItem)
			{
				((Component)HasGetImage).gameObject.SetActive(true);
			}
			else
			{
				((Component)HasGetImage).gameObject.SetActive(false);
			}
		}
		else
		{
			((Component)itemIcon).gameObject.SetActive(false);
			((Component)PingZhi).gameObject.SetActive(false);
			NameBG.SetActive(false);
			((Component)HasGetImage).gameObject.SetActive(false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Item.itemName != null && Item.itemID != -1)
		{
			inventory.Show_Tooltip(Item, getItemPrice());
			inventory.showTooltip = true;
		}
		else
		{
			inventory.showTooltip = false;
		}
	}

	public virtual int getItemPrice()
	{
		return (int)((float)(int)jsonData.instance.ItemJsonData[Item.itemID.ToString()]["price"].n * 0.5f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		inventory.showTooltip = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			CanClickLeft();
		}
	}

	private bool CanClickLeft()
	{
		if (!hasItem)
		{
			return false;
		}
		return true;
	}
}
