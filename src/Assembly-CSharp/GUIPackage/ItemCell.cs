using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GUIPackage;

public class ItemCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public GameObject Icon;

	public GameObject Num;

	public GameObject PingZhi;

	public GameObject PingZhiUI;

	public ItemDatebase itemDatebase;

	public bool JustShow;

	public bool isPlayer = true;

	public bool isUGUI;

	public Inventory2 inventory;

	public GameObject YiWu;

	public GameObject NaiYao;

	public string Btn1Text = "";

	public bool AutoSetBtnText;

	public bool CanShowTooltips = true;

	public bool CanDrawItem;

	protected item Item = new item();

	public bool ISPrepare;

	public GameObject KeyObject;

	public UILabel KeyName;

	private float refreshCD;

	public item GetItem => Item;

	private void Start()
	{
		if ((Object)(object)inventory == (Object)null)
		{
			inventory = Singleton.inventory;
		}
		Item = inventory.inventory[int.Parse(((Object)this).name)];
	}

	public void ShowName()
	{
		if (Item.itemID != -1 && (Object)(object)KeyName != (Object)null && (Object)(object)KeyObject != (Object)null)
		{
			JSONObject jSONObject = jsonData.instance.ItemJsonData[Item.itemID.ToString()];
			KeyName.text = "[" + jsonData.instance.NameColor[Inventory2.GetItemQuality(Item, jSONObject["quality"].I) - 1] + "]" + Inventory2.GetItemName(Item, Tools.instance.Code64ToString(jSONObject["name"].str)) + "[-]";
			KeyObject.SetActive(true);
		}
		else if ((Object)(object)KeyObject != (Object)null)
		{
			KeyObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (refreshCD < 0f)
		{
			UpdateRefresh();
			refreshCD = 0.2f;
		}
		else
		{
			refreshCD -= Time.deltaTime;
		}
	}

	public void UpdateRefresh()
	{
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (!isUGUI)
		{
			Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemIcon;
			if (Item.Seid != null && Item.Seid.HasField("quality"))
			{
				int i = Item.Seid["quality"].I;
				if ((Object)(object)itemDatebase == (Object)null)
				{
					itemDatebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
				}
				PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)itemDatebase.PingZhi[i];
				if ((Object)(object)PingZhiUI != (Object)null)
				{
					PingZhiUI.GetComponent<UI2DSprite>().sprite2D = itemDatebase.PingZhiUp[i];
				}
			}
			else
			{
				PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemPingZhi;
				if ((Object)(object)PingZhiUI != (Object)null)
				{
					PingZhiUI.GetComponent<UI2DSprite>().sprite2D = inventory.inventory[int.Parse(((Object)this).name)].itemPingZhiUP;
				}
			}
			if (inventory.inventory[int.Parse(((Object)this).name)].itemNum > 1)
			{
				Num.GetComponent<UILabel>().text = inventory.inventory[int.Parse(((Object)this).name)].itemNum.ToString();
			}
			else
			{
				Num.GetComponent<UILabel>().text = "";
			}
			showYiWu();
		}
		else
		{
			Texture2D itemIcon = inventory.inventory[int.Parse(((Object)this).name)].itemIcon;
			Texture2D itemPingZhi = inventory.inventory[int.Parse(((Object)this).name)].itemPingZhi;
			Icon.GetComponent<Image>().sprite = Sprite.Create(itemIcon, new Rect(0f, 0f, (float)((Texture)itemIcon).width, (float)((Texture)itemIcon).height), new Vector2(0.5f, 0.5f));
			PingZhi.GetComponent<Image>().sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)((Texture)itemPingZhi).width, (float)((Texture)itemPingZhi).height), new Vector2(0.5f, 0.5f));
			if (inventory.inventory[int.Parse(((Object)this).name)].itemNum > 1)
			{
				Num.GetComponent<Text>().text = inventory.inventory[int.Parse(((Object)this).name)].itemNum.ToString();
			}
			else
			{
				Num.GetComponent<Text>().text = "";
			}
		}
		ShowName();
	}

	public void showYiWu()
	{
		if ((Object)(object)YiWu != (Object)null)
		{
			if (inventory.inventory[int.Parse(((Object)this).name)].itemName != null && inventory.inventory[int.Parse(((Object)this).name)].itemID != -1)
			{
				item item2 = inventory.inventory[int.Parse(((Object)this).name)];
				JSONObject jSONObject = jsonData.instance.ItemJsonData[item2.itemID.ToString()];
				if ((int)jSONObject["type"].n == 3)
				{
					int getskillID2 = 0;
					try
					{
						if (item2.itemID > jsonData.QingJiaoItemIDSegment)
						{
							JSONObject jSONObject2 = jsonData.instance.ItemsSeidJsonData[1][(item2.itemID - jsonData.QingJiaoItemIDSegment).ToString()];
							getskillID2 = jSONObject2["value1"].I;
						}
						else
						{
							JSONObject jSONObject2 = jsonData.instance.ItemsSeidJsonData[1][item2.itemID.ToString()];
							getskillID2 = jSONObject2["value1"].I;
						}
					}
					catch
					{
						Debug.LogError((object)$"获取神通特性出错，请检查消耗品特性表1，物品ID{item2.itemID}");
					}
					if (Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == getskillID2) != null)
					{
						YiWu.SetActive(true);
					}
					else
					{
						YiWu.SetActive(false);
					}
				}
				else if ((int)jSONObject["type"].n == 4)
				{
					int getskillID = 0;
					try
					{
						if (item2.itemID > jsonData.QingJiaoItemIDSegment)
						{
							JSONObject jSONObject3 = jsonData.instance.ItemsSeidJsonData[2][(item2.itemID - jsonData.QingJiaoItemIDSegment).ToString()];
							getskillID = jSONObject3["value1"].I;
						}
						else
						{
							JSONObject jSONObject3 = jsonData.instance.ItemsSeidJsonData[2][item2.itemID.ToString()];
							getskillID = jSONObject3["value1"].I;
						}
					}
					catch
					{
						Debug.LogError((object)$"获取功法特性出错，请检查消耗品特性表2，物品ID{item2.itemID}");
					}
					if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
					{
						YiWu.SetActive(true);
					}
					else
					{
						YiWu.SetActive(false);
					}
				}
				else if ((int)jSONObject["type"].n == 10)
				{
					int id = (int)jsonData.instance.ItemsSeidJsonData[13][string.Concat(item2.itemID)]["value1"].n;
					if (Tools.instance.getPlayer().ISStudyDanFan(id))
					{
						YiWu.SetActive(true);
					}
					else
					{
						YiWu.SetActive(false);
					}
				}
				else
				{
					YiWu.SetActive(false);
				}
			}
			else
			{
				YiWu.SetActive(false);
			}
		}
		if (!((Object)(object)NaiYao != (Object)null))
		{
			return;
		}
		if (inventory.inventory[int.Parse(((Object)this).name)].itemName != null && inventory.inventory[int.Parse(((Object)this).name)].itemID != -1)
		{
			item item3 = inventory.inventory[int.Parse(((Object)this).name)];
			if ((int)jsonData.instance.ItemJsonData[item3.itemID.ToString()]["type"].n == 5)
			{
				int jsonobject = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, item3.itemID.ToString());
				int itemCanUseNum = item.GetItemCanUseNum(Item.itemID);
				if (jsonobject >= itemCanUseNum)
				{
					NaiYao.SetActive(true);
				}
				else
				{
					NaiYao.SetActive(false);
				}
			}
			else
			{
				NaiYao.SetActive(false);
			}
		}
		else
		{
			NaiYao.SetActive(false);
		}
	}

	private void OnDrop(GameObject obj)
	{
		if (Input.GetMouseButtonUp(0) && !JustShow)
		{
			chengeItem();
		}
	}

	private void OnPress()
	{
		PCOnPress();
	}

	public virtual void MobilePress()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (Item.itemID != -1 && CanShowTooltips)
		{
			PCOnHover(isOver: true);
			Singleton.ToolTipsBackGround.openTooltips();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.CloseAction = (UnityAction)delegate
			{
				PCOnHover(isOver: false);
			};
			toolTipsBackGround.UseAction = (UnityAction)delegate
			{
				ClickUseItem();
			};
		}
	}

	public virtual void PCOnPress()
	{
		if (!JustShow)
		{
			Item = inventory.inventory[int.Parse(((Object)this).name)];
			if (Input.GetMouseButtonDown(1) && Item.itemName != null && !inventory.draggingItem)
			{
				ClickUseItem();
			}
			if (!ISPrepare && Input.GetMouseButtonDown(0))
			{
				chengeItem();
			}
		}
	}

	public void ClickUseItem()
	{
		int num = (int)jsonData.instance.ItemJsonData[inventory.inventory[int.Parse(((Object)this).name)].itemID.ToString()]["type"].n;
		if ((num != 6 && num != 8) || (int)jsonData.instance.ItemJsonData[inventory.inventory[int.Parse(((Object)this).name)].itemID.ToString()]["vagueType"].n != 0)
		{
			switch (num)
			{
			case 7:
			case 9:
				break;
			case 3:
			case 4:
			case 13:
				UIPopTip.Inst.Pop("需要在洞府或客栈中闭关领悟");
				break;
			default:
				inventory.UseItem(int.Parse(((Object)this).name));
				break;
			}
		}
	}

	private void chengeItem()
	{
		if (!inventory.CanClick())
		{
			return;
		}
		if (!Singleton.key.draggingKey)
		{
			if (Item.itemName != null)
			{
				if (!inventory.draggingItem)
				{
					inventory.dragedID = int.Parse(((Object)this).name);
					inventory.draggingItem = true;
					inventory.dragedItem = inventory.inventory[int.Parse(((Object)this).name)];
					inventory.inventory[int.Parse(((Object)this).name)] = new item();
				}
				else if (Singleton.equip.is_draged && inventory.dragedItem.itemType != Item.itemType)
				{
					for (int i = 0; i < inventory.inventory.Count; i++)
					{
						if (inventory.inventory[i].itemID == -1)
						{
							inventory.inventory[i] = inventory.dragedItem;
							inventory.Clear_dragedItem();
							Singleton.equip.is_draged = false;
						}
					}
				}
				else
				{
					inventory.ChangeItem(ref Item, ref Singleton.inventory.dragedItem);
					inventory.inventory[int.Parse(((Object)this).name)] = Item;
				}
			}
			else if (inventory.draggingItem)
			{
				inventory.ChangeItem(ref Item, ref inventory.dragedItem);
				inventory.inventory[int.Parse(((Object)this).name)] = Item;
				inventory.Temp.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.dragedItem.itemIcon;
				inventory.draggingItem = false;
				Singleton.equip.is_draged = false;
			}
		}
		else
		{
			inventory.Clear_dragedItem();
		}
	}

	public virtual int getItemPrice()
	{
		int num = (int)jsonData.instance.ItemJsonData[string.Concat(inventory.inventory[int.Parse(((Object)this).name)].itemID)]["price"].n;
		if (inventory.inventory[int.Parse(((Object)this).name)].Seid != null && inventory.inventory[int.Parse(((Object)this).name)].Seid.HasField("Money"))
		{
			num = inventory.inventory[int.Parse(((Object)this).name)].Seid["Money"].I;
		}
		return (int)((float)num * 0.5f);
	}

	private void OnHover(bool isOver)
	{
		PCOnHover(isOver);
	}

	public virtual int MoneyPercent(item a)
	{
		return 0;
	}

	public virtual void PCOnHover(bool isOver)
	{
		if (isOver && inventory.inventory[int.Parse(((Object)this).name)].itemName != null)
		{
			inventory.Show_Tooltip(inventory.inventory[int.Parse(((Object)this).name)], getItemPrice(), MoneyPercent(inventory.inventory[int.Parse(((Object)this).name)]));
			inventory.showTooltip = true;
		}
		else
		{
			inventory.showTooltip = false;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isUGUI)
		{
			OnHover(isOver: false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isUGUI)
		{
			OnHover(isOver: true);
		}
	}
}
