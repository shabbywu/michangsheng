using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000A5C RID: 2652
	public class ItemCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x001F9334 File Offset: 0x001F7534
		public item GetItem
		{
			get
			{
				return this.Item;
			}
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x001F933C File Offset: 0x001F753C
		private void Start()
		{
			if (this.inventory == null)
			{
				this.inventory = Singleton.inventory;
			}
			this.Item = this.inventory.inventory[int.Parse(base.name)];
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x001F9378 File Offset: 0x001F7578
		public void ShowName()
		{
			if (this.Item.itemID != -1 && this.KeyName != null && this.KeyObject != null)
			{
				JSONObject jsonobject = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()];
				this.KeyName.text = string.Concat(new string[]
				{
					"[",
					jsonData.instance.NameColor[Inventory2.GetItemQuality(this.Item, jsonobject["quality"].I) - 1],
					"]",
					Inventory2.GetItemName(this.Item, Tools.instance.Code64ToString(jsonobject["name"].str)),
					"[-]"
				});
				this.KeyObject.SetActive(true);
				return;
			}
			if (this.KeyObject != null)
			{
				this.KeyObject.SetActive(false);
			}
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x001F9484 File Offset: 0x001F7684
		private void Update()
		{
			if (this.refreshCD < 0f)
			{
				this.UpdateRefresh();
				this.refreshCD = 0.2f;
				return;
			}
			this.refreshCD -= Time.deltaTime;
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x001F94B8 File Offset: 0x001F76B8
		public void UpdateRefresh()
		{
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (!this.isUGUI)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemIcon;
				if (this.Item.Seid != null && this.Item.Seid.HasField("quality"))
				{
					int i = this.Item.Seid["quality"].I;
					if (this.itemDatebase == null)
					{
						this.itemDatebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
					}
					this.PingZhi.GetComponent<UITexture>().mainTexture = this.itemDatebase.PingZhi[i];
					if (this.PingZhiUI != null)
					{
						this.PingZhiUI.GetComponent<UI2DSprite>().sprite2D = this.itemDatebase.PingZhiUp[i];
					}
				}
				else
				{
					this.PingZhi.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
					if (this.PingZhiUI != null)
					{
						this.PingZhiUI.GetComponent<UI2DSprite>().sprite2D = this.inventory.inventory[int.Parse(base.name)].itemPingZhiUP;
					}
				}
				if (this.inventory.inventory[int.Parse(base.name)].itemNum > 1)
				{
					this.Num.GetComponent<UILabel>().text = this.inventory.inventory[int.Parse(base.name)].itemNum.ToString();
				}
				else
				{
					this.Num.GetComponent<UILabel>().text = "";
				}
				this.showYiWu();
			}
			else
			{
				Texture2D itemIcon = this.inventory.inventory[int.Parse(base.name)].itemIcon;
				Texture2D itemPingZhi = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
				this.Icon.GetComponent<Image>().sprite = Sprite.Create(itemIcon, new Rect(0f, 0f, (float)itemIcon.width, (float)itemIcon.height), new Vector2(0.5f, 0.5f));
				this.PingZhi.GetComponent<Image>().sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)itemPingZhi.width, (float)itemPingZhi.height), new Vector2(0.5f, 0.5f));
				if (this.inventory.inventory[int.Parse(base.name)].itemNum > 1)
				{
					this.Num.GetComponent<Text>().text = this.inventory.inventory[int.Parse(base.name)].itemNum.ToString();
				}
				else
				{
					this.Num.GetComponent<Text>().text = "";
				}
			}
			this.ShowName();
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x001F9804 File Offset: 0x001F7A04
		public void showYiWu()
		{
			if (this.YiWu != null)
			{
				if (this.inventory.inventory[int.Parse(base.name)].itemName != null && this.inventory.inventory[int.Parse(base.name)].itemID != -1)
				{
					item item = this.inventory.inventory[int.Parse(base.name)];
					JSONObject jsonobject = jsonData.instance.ItemJsonData[item.itemID.ToString()];
					if ((int)jsonobject["type"].n == 3)
					{
						int getskillID = 0;
						try
						{
							if (item.itemID > jsonData.QingJiaoItemIDSegment)
							{
								JSONObject jsonobject2 = jsonData.instance.ItemsSeidJsonData[1][(item.itemID - jsonData.QingJiaoItemIDSegment).ToString()];
								getskillID = jsonobject2["value1"].I;
							}
							else
							{
								JSONObject jsonobject2 = jsonData.instance.ItemsSeidJsonData[1][item.itemID.ToString()];
								getskillID = jsonobject2["value1"].I;
							}
						}
						catch
						{
							Debug.LogError(string.Format("获取神通特性出错，请检查消耗品特性表1，物品ID{0}", item.itemID));
						}
						if (Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
						{
							this.YiWu.SetActive(true);
						}
						else
						{
							this.YiWu.SetActive(false);
						}
					}
					else if ((int)jsonobject["type"].n == 4)
					{
						int getskillID = 0;
						try
						{
							if (item.itemID > jsonData.QingJiaoItemIDSegment)
							{
								JSONObject jsonobject3 = jsonData.instance.ItemsSeidJsonData[2][(item.itemID - jsonData.QingJiaoItemIDSegment).ToString()];
								getskillID = jsonobject3["value1"].I;
							}
							else
							{
								JSONObject jsonobject3 = jsonData.instance.ItemsSeidJsonData[2][item.itemID.ToString()];
								getskillID = jsonobject3["value1"].I;
							}
						}
						catch
						{
							Debug.LogError(string.Format("获取功法特性出错，请检查消耗品特性表2，物品ID{0}", item.itemID));
						}
						if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
						{
							this.YiWu.SetActive(true);
						}
						else
						{
							this.YiWu.SetActive(false);
						}
					}
					else if ((int)jsonobject["type"].n == 10)
					{
						int id = (int)jsonData.instance.ItemsSeidJsonData[13][string.Concat(item.itemID)]["value1"].n;
						if (Tools.instance.getPlayer().ISStudyDanFan(id))
						{
							this.YiWu.SetActive(true);
						}
						else
						{
							this.YiWu.SetActive(false);
						}
					}
					else
					{
						this.YiWu.SetActive(false);
					}
				}
				else
				{
					this.YiWu.SetActive(false);
				}
			}
			if (this.NaiYao != null)
			{
				if (this.inventory.inventory[int.Parse(base.name)].itemName != null && this.inventory.inventory[int.Parse(base.name)].itemID != -1)
				{
					item item2 = this.inventory.inventory[int.Parse(base.name)];
					if ((int)jsonData.instance.ItemJsonData[item2.itemID.ToString()]["type"].n != 5)
					{
						this.NaiYao.SetActive(false);
						return;
					}
					int jsonobject4 = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, item2.itemID.ToString());
					int itemCanUseNum = item.GetItemCanUseNum(this.Item.itemID);
					if (jsonobject4 >= itemCanUseNum)
					{
						this.NaiYao.SetActive(true);
						return;
					}
					this.NaiYao.SetActive(false);
					return;
				}
				else
				{
					this.NaiYao.SetActive(false);
				}
			}
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x001F9C78 File Offset: 0x001F7E78
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && !this.JustShow)
			{
				this.chengeItem();
			}
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x001F9C90 File Offset: 0x001F7E90
		private void OnPress()
		{
			this.PCOnPress();
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x001F9C98 File Offset: 0x001F7E98
		public virtual void MobilePress()
		{
			if (this.Item.itemID == -1)
			{
				return;
			}
			if (!this.CanShowTooltips)
			{
				return;
			}
			this.PCOnHover(true);
			Singleton.ToolTipsBackGround.openTooltips();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.CloseAction = delegate()
			{
				this.PCOnHover(false);
			};
			toolTipsBackGround.UseAction = delegate()
			{
				this.ClickUseItem();
			};
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x001F9CF8 File Offset: 0x001F7EF8
		public virtual void PCOnPress()
		{
			if (this.JustShow)
			{
				return;
			}
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (Input.GetMouseButtonDown(1) && this.Item.itemName != null && !this.inventory.draggingItem)
			{
				this.ClickUseItem();
			}
			if (this.ISPrepare)
			{
				return;
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.chengeItem();
			}
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x001F9D70 File Offset: 0x001F7F70
		public void ClickUseItem()
		{
			int num = (int)jsonData.instance.ItemJsonData[this.inventory.inventory[int.Parse(base.name)].itemID.ToString()]["type"].n;
			if ((num == 6 || num == 8) && (int)jsonData.instance.ItemJsonData[this.inventory.inventory[int.Parse(base.name)].itemID.ToString()]["vagueType"].n == 0)
			{
				return;
			}
			if (num == 7 || num == 9)
			{
				return;
			}
			if (num - 3 <= 1 || num == 13)
			{
				UIPopTip.Inst.Pop("需要在洞府或客栈中闭关领悟", PopTipIconType.叹号);
				return;
			}
			this.inventory.UseItem(int.Parse(base.name));
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x001F9E50 File Offset: 0x001F8050
		private void chengeItem()
		{
			if (!this.inventory.CanClick())
			{
				return;
			}
			if (!Singleton.key.draggingKey)
			{
				if (this.Item.itemName != null)
				{
					if (!this.inventory.draggingItem)
					{
						this.inventory.dragedID = int.Parse(base.name);
						this.inventory.draggingItem = true;
						this.inventory.dragedItem = this.inventory.inventory[int.Parse(base.name)];
						this.inventory.inventory[int.Parse(base.name)] = new item();
						return;
					}
					if (Singleton.equip.is_draged && this.inventory.dragedItem.itemType != this.Item.itemType)
					{
						for (int i = 0; i < this.inventory.inventory.Count; i++)
						{
							if (this.inventory.inventory[i].itemID == -1)
							{
								this.inventory.inventory[i] = this.inventory.dragedItem;
								this.inventory.Clear_dragedItem();
								Singleton.equip.is_draged = false;
							}
						}
						return;
					}
					this.inventory.ChangeItem(ref this.Item, ref Singleton.inventory.dragedItem);
					this.inventory.inventory[int.Parse(base.name)] = this.Item;
					return;
				}
				else if (this.inventory.draggingItem)
				{
					this.inventory.ChangeItem(ref this.Item, ref this.inventory.dragedItem);
					this.inventory.inventory[int.Parse(base.name)] = this.Item;
					this.inventory.Temp.GetComponent<UITexture>().mainTexture = this.inventory.dragedItem.itemIcon;
					this.inventory.draggingItem = false;
					Singleton.equip.is_draged = false;
					return;
				}
			}
			else
			{
				this.inventory.Clear_dragedItem();
			}
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x001FA068 File Offset: 0x001F8268
		public virtual int getItemPrice()
		{
			int num = (int)jsonData.instance.ItemJsonData[string.Concat(this.inventory.inventory[int.Parse(base.name)].itemID)]["price"].n;
			if (this.inventory.inventory[int.Parse(base.name)].Seid != null && this.inventory.inventory[int.Parse(base.name)].Seid.HasField("Money"))
			{
				num = this.inventory.inventory[int.Parse(base.name)].Seid["Money"].I;
			}
			return (int)((float)num * 0.5f);
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x001FA146 File Offset: 0x001F8346
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual int MoneyPercent(item a)
		{
			return 0;
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x001FA150 File Offset: 0x001F8350
		public virtual void PCOnHover(bool isOver)
		{
			if (isOver && this.inventory.inventory[int.Parse(base.name)].itemName != null)
			{
				this.inventory.Show_Tooltip(this.inventory.inventory[int.Parse(base.name)], this.getItemPrice(), this.MoneyPercent(this.inventory.inventory[int.Parse(base.name)]));
				this.inventory.showTooltip = true;
				return;
			}
			this.inventory.showTooltip = false;
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x001FA1E8 File Offset: 0x001F83E8
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.isUGUI)
			{
				this.OnHover(false);
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x001FA1F9 File Offset: 0x001F83F9
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.isUGUI)
			{
				this.OnHover(true);
			}
		}

		// Token: 0x04004999 RID: 18841
		public GameObject Icon;

		// Token: 0x0400499A RID: 18842
		public GameObject Num;

		// Token: 0x0400499B RID: 18843
		public GameObject PingZhi;

		// Token: 0x0400499C RID: 18844
		public GameObject PingZhiUI;

		// Token: 0x0400499D RID: 18845
		public ItemDatebase itemDatebase;

		// Token: 0x0400499E RID: 18846
		public bool JustShow;

		// Token: 0x0400499F RID: 18847
		public bool isPlayer = true;

		// Token: 0x040049A0 RID: 18848
		public bool isUGUI;

		// Token: 0x040049A1 RID: 18849
		public Inventory2 inventory;

		// Token: 0x040049A2 RID: 18850
		public GameObject YiWu;

		// Token: 0x040049A3 RID: 18851
		public GameObject NaiYao;

		// Token: 0x040049A4 RID: 18852
		public string Btn1Text = "";

		// Token: 0x040049A5 RID: 18853
		public bool AutoSetBtnText;

		// Token: 0x040049A6 RID: 18854
		public bool CanShowTooltips = true;

		// Token: 0x040049A7 RID: 18855
		public bool CanDrawItem;

		// Token: 0x040049A8 RID: 18856
		protected item Item = new item();

		// Token: 0x040049A9 RID: 18857
		public bool ISPrepare;

		// Token: 0x040049AA RID: 18858
		public GameObject KeyObject;

		// Token: 0x040049AB RID: 18859
		public UILabel KeyName;

		// Token: 0x040049AC RID: 18860
		private float refreshCD;
	}
}
