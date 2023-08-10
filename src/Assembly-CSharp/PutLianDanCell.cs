using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PutLianDanCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
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

	public ItemTypes itemType;

	private Button button;

	private bool _isLock;

	public bool isLock
	{
		get
		{
			return _isLock;
		}
		set
		{
			_isLock = value;
			if (_isLock)
			{
				((Component)this).GetComponent<Image>().sprite = LianDanSystemManager.inst.LockSprite;
				if (Item.itemID != -1)
				{
					LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
					UIPopTip.Inst.Pop("丹炉品级不足");
				}
			}
			else
			{
				((Component)this).GetComponent<Image>().sprite = LianDanSystemManager.inst.NoLockSprite;
			}
		}
	}

	private void Awake()
	{
		inventory = LianDanSystemManager.inst.inventory;
		updateItem();
	}

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
		if (!((Component)LianDanSystemManager.inst.selectLianDanCaiLiaoPage).gameObject.activeSelf)
		{
			if (inventory.inventory[int.Parse(((Object)this).name)].itemName != null && !((Component)LianDanSystemManager.inst.selectLianDanCaiLiaoPage).gameObject.activeSelf)
			{
				inventory.Show_Tooltip(inventory.inventory[int.Parse(((Object)this).name)], getItemPrice());
				inventory.showTooltip = true;
			}
			else
			{
				inventory.showTooltip = false;
			}
		}
	}

	public virtual int getItemPrice()
	{
		return (int)((float)(int)jsonData.instance.ItemJsonData[string.Concat(inventory.inventory[int.Parse(((Object)this).name)].itemID)]["price"].n * 0.5f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!((Component)LianDanSystemManager.inst.selectLianDanCaiLiaoPage).gameObject.activeSelf)
		{
			inventory.showTooltip = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 1 && CanClickRight())
		{
			removeItem();
		}
		else if ((int)eventData.button == 0 && CanClickLeft())
		{
			selectItem();
		}
	}

	public void selectItem()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = false;
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.setCurSelectIndex(int.Parse(((Object)this).name));
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.openCaiLiaoPackge((int)itemType);
	}

	public void removeItem()
	{
		int num = inventory.inventory[int.Parse(((Object)this).name)].itemNum - 1;
		if (num <= 0)
		{
			inventory.inventory[int.Parse(((Object)this).name)] = new item();
			inventory.inventory[int.Parse(((Object)this).name)].itemNum = 0;
		}
		else
		{
			inventory.inventory[int.Parse(((Object)this).name)].itemNum = num;
		}
		updateItem();
	}

	private bool CanClickRight()
	{
		if (itemType == ItemTypes.丹炉)
		{
			return false;
		}
		if (((Component)LianDanSystemManager.inst.selectLianDanCaiLiaoPage).gameObject.activeSelf)
		{
			return false;
		}
		if (!LianDanSystemManager.inst.lianDanPageManager.CanClick)
		{
			return false;
		}
		if (isLock)
		{
			return false;
		}
		if (Item.itemID == -1)
		{
			return false;
		}
		return true;
	}

	private bool CanClickLeft()
	{
		if (((Component)LianDanSystemManager.inst.selectLianDanCaiLiaoPage).gameObject.activeSelf)
		{
			return false;
		}
		if (isLock)
		{
			return false;
		}
		if (!LianDanSystemManager.inst.lianDanPageManager.CanClick)
		{
			return false;
		}
		return true;
	}
}
