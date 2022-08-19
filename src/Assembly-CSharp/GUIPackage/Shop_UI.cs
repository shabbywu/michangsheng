using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUIPackage
{
	// Token: 0x02000A54 RID: 2644
	public class Shop_UI : MonoBehaviour
	{
		// Token: 0x060049C1 RID: 18881 RVA: 0x001F46E6 File Offset: 0x001F28E6
		private void Start()
		{
			this.initCangJinGeShop();
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x001F46F0 File Offset: 0x001F28F0
		public void initCangJinGeShop()
		{
			List<JSONObject> shopList = this.GetShopList();
			if (shopList.Count == 0)
			{
				return;
			}
			int num = 0;
			foreach (JSONObject jsonobject in shopList)
			{
				this.ChildTitle[num].text = Tools.Code64(jsonobject["ChildTitle"].str);
				if (jsonobject["ExShopID"].I == 0)
				{
					this.initShopItems(num);
					this.InitMoneyByMethod(num);
				}
				else
				{
					this.InitExChengShopItems(num);
					this.InitExChengMethod(num);
				}
				num++;
			}
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x001F47A4 File Offset: 0x001F29A4
		public void InitMoneyByMethod(int index)
		{
			this.InitShopPage(0, this.invList[index], index + 1);
			this.selectList[index].list.Clear();
			JSONObject shopJsonData = this.GetShopJsonData(index + 1);
			this.setPageText(shopJsonData, this.invList[index], index);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x001F47FC File Offset: 0x001F29FC
		public JSONObject GetShopJsonData(int shopType)
		{
			return this.GetShopList().Find((JSONObject aa) => shopType == (int)aa["shopType"].n);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x001F4830 File Offset: 0x001F2A30
		public List<JSONObject> GetShopList()
		{
			string scencName = SceneManager.GetActiveScene().name;
			return jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x001F4878 File Offset: 0x001F2A78
		public List<JSONObject> GetJiaoHuanShop(int JiaohanShopID)
		{
			return jsonData.instance.jiaoHuanShopGoods.list.FindAll((JSONObject aa) => JiaohanShopID == aa["ShopID"].I);
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x001F48B4 File Offset: 0x001F2AB4
		public void setPageText(JSONObject info, Inventory2 cc, int indaa)
		{
			if (info != null)
			{
				for (int i = 0; i < info["items"].Count / (int)cc.count + 1; i++)
				{
					this.selectList[indaa].list.Add("第" + Tools.getStr("shuzi" + (i + 1)) + "页");
				}
				if (this.ShopName != null)
				{
					this.ShopName.text = Tools.instance.Code64ToString(info["Title"].str);
				}
			}
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x001F495C File Offset: 0x001F2B5C
		public void InitExChengShopItems(int index)
		{
			Inventory2 inventory = this.invList[index];
			for (int i = 0; i < (int)inventory.count; i++)
			{
				inventory.inventory.Add(new item());
			}
			inventory.InitSlot<itemcellShopEx>("SlotShopEX", (int)inventory.count, delegate(itemcellShopEx aa)
			{
			}, "Win/item");
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x001F49CC File Offset: 0x001F2BCC
		public void InitExChengMethod(int index)
		{
			this.InitExShopPage(0, this.invList[index], index + 1);
			this.selectList[index].list.Clear();
			int exShopID = this.getExShopID(index + 1);
			List<JSONObject> jiaoHuanShop = this.GetJiaoHuanShop(exShopID);
			JSONObject shopJsonData = this.GetShopJsonData(index + 1);
			if (jiaoHuanShop.Count > 0)
			{
				for (int i = 0; i < jiaoHuanShop.Count / (int)this.invList[index].count + 1; i++)
				{
					this.selectList[index].list.Add("第" + Tools.getStr("shuzi" + (i + 1)) + "页");
				}
				if (this.ShopName != null && shopJsonData != null)
				{
					this.ShopName.text = Tools.instance.Code64ToString(shopJsonData["Title"].str);
				}
			}
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x001F4AC4 File Offset: 0x001F2CC4
		public virtual int getExShopID(int type)
		{
			return this.GetShopJsonData(type)["ExShopID"].I;
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x001F4ADC File Offset: 0x001F2CDC
		public void resetInventory(Inventory2 inv)
		{
			for (int i = 0; i < inv.inventory.Count; i++)
			{
				inv.inventory[i] = new item();
				inv.inventory[i].itemNum = 1;
			}
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x00004095 File Offset: 0x00002295
		public void getShopJson()
		{
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x001F4B22 File Offset: 0x001F2D22
		public virtual bool ShouldSetLevel(int Type = 0)
		{
			return Type == 1;
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x001F4B2B File Offset: 0x001F2D2B
		public virtual int getShopType(int type)
		{
			return this.GetShopJsonData(type)["SType"].I;
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x001F4B44 File Offset: 0x001F2D44
		public virtual void InitExShopPage(int page, Inventory2 inv, int type)
		{
			this.resetInventory(inv);
			int shopType = this.getShopType(type);
			int shopExID = this.getExShopID(type);
			int num = 0;
			ItemDatebase component = jsonData.instance.GetComponent<ItemDatebase>();
			List<JSONObject> list = jsonData.instance.jiaoHuanShopGoods.list.FindAll((JSONObject aa) => aa["ShopID"].I == shopExID);
			this.sortItem(list);
			foreach (JSONObject jsonobject in list)
			{
				if (this.ShouldSetLevel(shopType))
				{
					JSONObject jsonobject2 = jsonData.instance.ItemJsonData[jsonobject["GoodsID"].I.ToString()];
					if (Tools.instance.getPlayer().getLevelType() < (int)jsonobject2["quality"].n && (jsonobject2["type"].I == 3 || jsonobject2["type"].I == 4))
					{
						continue;
					}
				}
				if (inv.isInPage(page, num, (int)inv.count))
				{
					int index = inv.addItemToNullInventory(jsonobject["GoodsID"].I, 1, Tools.getUUID(), null);
					if ((int)jsonobject["Money"].n == 0 && (int)jsonobject["percent"].n > 0)
					{
						int itemPrice = (int)Math.Ceiling((double)(jsonData.instance.ItemJsonData[jsonobject["GoodsID"].I.ToString()]["price"].n / jsonobject["percent"].n));
						inv.inventory[index].itemPrice = itemPrice;
					}
					else if (jsonobject["EXGoodsID"].I == 10035)
					{
						inv.inventory[index].itemPrice = (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(inv.inventory[index].itemID)]["price"].n) * (jsonobject["price"].n / 100f));
					}
					else
					{
						inv.inventory[index].itemPrice = (int)jsonobject["Money"].n;
					}
					inv.inventory[index].ExGoodsID = jsonobject["EXGoodsID"].I;
					inv.inventory[index].ExItemIcon = component.items[jsonobject["EXGoodsID"].I].itemIcon;
					if (this.ExShopMoney != null)
					{
						this.ExShopMoney.ExGoodsID = jsonobject["EXGoodsID"].I;
					}
				}
				num++;
			}
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x001F4E74 File Offset: 0x001F3074
		public virtual void updateItem()
		{
			List<JSONObject> shopList = this.GetShopList();
			if (shopList.Count == 0)
			{
				return;
			}
			int num = 0;
			using (List<JSONObject>.Enumerator enumerator = shopList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current["ExShopID"].I == 0)
					{
						this.InitShopPage(this.selectList[num].NowIndex, this.invList[num], num + 1);
					}
					else
					{
						this.InitExShopPage(this.selectList[num].NowIndex, this.invList[num], num + 1);
					}
					num++;
				}
			}
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x001F4F30 File Offset: 0x001F3130
		public void showBtn()
		{
			string scencName = SceneManager.GetActiveScene().name;
			List<JSONObject> list = jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
			if (this.threeSceernUI == null)
			{
				this.threeSceernUI = ThreeSceernUI.inst;
			}
			if (list.Count > 0)
			{
				if (list[0]["SType"].I == 1)
				{
					this.threeSceernUI.setPostion(this.threeSceernUI.transform.Find("grid/shopBtn"));
					return;
				}
				if (list[0]["SType"].I == 2)
				{
					this.threeSceernUI.setPostion(this.threeSceernUI.transform.Find("grid/shenbingeBtn"));
					return;
				}
				if (list[0]["SType"].I == 3)
				{
					this.threeSceernUI.setPostion(this.threeSceernUI.transform.Find("grid/yaofangBtn"));
				}
			}
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x001F504C File Offset: 0x001F324C
		private void initShopItems(int index)
		{
			Inventory2 inventory = this.invList[index];
			for (int i = 0; i < (int)inventory.count; i++)
			{
				inventory.inventory.Add(new item());
			}
			inventory.InitSlot("SlotShop", "Win/item");
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x001F5098 File Offset: 0x001F3298
		public virtual void InitShopPage(int page, Inventory2 inv, int type)
		{
			this.resetInventory(inv);
			string scencName = SceneManager.GetActiveScene().name;
			int num = 0;
			List<JSONObject> list = jsonData.instance.NomelShopJsonData.list;
			Predicate<JSONObject> <>9__0;
			Predicate<JSONObject> match;
			if ((match = <>9__0) == null)
			{
				match = (<>9__0 = ((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName && type == (int)aa["shopType"].n));
			}
			foreach (JSONObject jsonobject in list.FindAll(match))
			{
				foreach (int id in this.sortItem(jsonobject["items"]))
				{
					if (Tools.instance.getPlayer().getLevelType() >= (int)jsonData.instance.ItemJsonData[id.ToString()]["quality"].n)
					{
						if (inv.isInPage(page, num, (int)inv.count))
						{
							int index = inv.addItemToNullInventory(id, 1, Tools.getUUID(), null);
							inv.inventory[index].itemPrice = (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(inv.inventory[index].itemID)]["price"].n) * (jsonobject["price"].n / 100f));
						}
						num++;
					}
				}
			}
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x001F5270 File Offset: 0x001F3470
		public List<int> sortItem(JSONObject shop)
		{
			List<int> itemsList = new List<int>();
			shop["items"].list.ForEach(delegate(JSONObject cc)
			{
				itemsList.Add((int)cc.n);
			});
			itemsList.Sort(delegate(int a, int b)
			{
				int num = (int)jsonData.instance.ItemJsonData[a.ToString()]["quality"].n;
				int value = (int)jsonData.instance.ItemJsonData[b.ToString()]["quality"].n;
				return num.CompareTo(value);
			});
			return itemsList;
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x001F52DF File Offset: 0x001F34DF
		public void sortItem(List<JSONObject> shop)
		{
			shop.Sort(delegate(JSONObject a, JSONObject b)
			{
				int i = jsonData.instance.ItemJsonData[a["GoodsID"].I.ToString()]["quality"].I;
				int i2 = jsonData.instance.ItemJsonData[b["GoodsID"].I.ToString()]["quality"].I;
				return i.CompareTo(i2);
			});
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x001F5306 File Offset: 0x001F3506
		public void closeShop()
		{
			base.gameObject.SetActive(false);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x000F03FC File Offset: 0x000EE5FC
		public void openShop()
		{
			base.transform.localPosition = Vector3.zero;
			base.gameObject.SetActive(true);
		}

		// Token: 0x04004946 RID: 18758
		public List<Inventory2> invList;

		// Token: 0x04004947 RID: 18759
		public List<UIselect> selectList;

		// Token: 0x04004948 RID: 18760
		public List<UILabel> ChildTitle;

		// Token: 0x04004949 RID: 18761
		public ThreeSceernUI threeSceernUI;

		// Token: 0x0400494A RID: 18762
		public UILabel ShopName;

		// Token: 0x0400494B RID: 18763
		public ExGoods ExShopMoney;
	}
}
