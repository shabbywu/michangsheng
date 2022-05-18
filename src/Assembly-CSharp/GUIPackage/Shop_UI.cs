using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUIPackage
{
	// Token: 0x02000D6C RID: 3436
	public class Shop_UI : MonoBehaviour
	{
		// Token: 0x06005299 RID: 21145 RVA: 0x0003B317 File Offset: 0x00039517
		private void Start()
		{
			this.initCangJinGeShop();
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x002270E0 File Offset: 0x002252E0
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

		// Token: 0x0600529B RID: 21147 RVA: 0x00227194 File Offset: 0x00225394
		public void InitMoneyByMethod(int index)
		{
			this.InitShopPage(0, this.invList[index], index + 1);
			this.selectList[index].list.Clear();
			JSONObject shopJsonData = this.GetShopJsonData(index + 1);
			this.setPageText(shopJsonData, this.invList[index], index);
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x002271EC File Offset: 0x002253EC
		public JSONObject GetShopJsonData(int shopType)
		{
			return this.GetShopList().Find((JSONObject aa) => shopType == (int)aa["shopType"].n);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00227220 File Offset: 0x00225420
		public List<JSONObject> GetShopList()
		{
			string scencName = SceneManager.GetActiveScene().name;
			return jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00227268 File Offset: 0x00225468
		public List<JSONObject> GetJiaoHuanShop(int JiaohanShopID)
		{
			return jsonData.instance.jiaoHuanShopGoods.list.FindAll((JSONObject aa) => JiaohanShopID == (int)aa["ShopID"].n);
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x002272A4 File Offset: 0x002254A4
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

		// Token: 0x060052A0 RID: 21152 RVA: 0x0022734C File Offset: 0x0022554C
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

		// Token: 0x060052A1 RID: 21153 RVA: 0x002273BC File Offset: 0x002255BC
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

		// Token: 0x060052A2 RID: 21154 RVA: 0x0003B31F File Offset: 0x0003951F
		public virtual int getExShopID(int type)
		{
			return this.GetShopJsonData(type)["ExShopID"].I;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x002274B4 File Offset: 0x002256B4
		public void resetInventory(Inventory2 inv)
		{
			for (int i = 0; i < inv.inventory.Count; i++)
			{
				inv.inventory[i] = new item();
				inv.inventory[i].itemNum = 1;
			}
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x000042DD File Offset: 0x000024DD
		public void getShopJson()
		{
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x0003B337 File Offset: 0x00039537
		public virtual bool ShouldSetLevel(int Type = 0)
		{
			return Type == 1;
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0003B340 File Offset: 0x00039540
		public virtual int getShopType(int type)
		{
			return this.GetShopJsonData(type)["SType"].I;
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x002274FC File Offset: 0x002256FC
		public virtual void InitExShopPage(int page, Inventory2 inv, int type)
		{
			this.resetInventory(inv);
			int shopType = this.getShopType(type);
			int shopExID = this.getExShopID(type);
			int num = 0;
			ItemDatebase component = jsonData.instance.GetComponent<ItemDatebase>();
			List<JSONObject> list = jsonData.instance.jiaoHuanShopGoods.list.FindAll((JSONObject aa) => (int)aa["ShopID"].n == shopExID);
			this.sortItem(list);
			foreach (JSONObject jsonobject in list)
			{
				if (this.ShouldSetLevel(shopType))
				{
					JSONObject jsonobject2 = jsonData.instance.ItemJsonData[((int)jsonobject["GoodsID"].n).ToString()];
					if (Tools.instance.getPlayer().getLevelType() < (int)jsonobject2["quality"].n && (jsonobject2["type"].I == 3 || jsonobject2["type"].I == 4))
					{
						continue;
					}
				}
				if (inv.isInPage(page, num, (int)inv.count))
				{
					int index = inv.addItemToNullInventory((int)jsonobject["GoodsID"].n, 1, Tools.getUUID(), null);
					if ((int)jsonobject["Money"].n == 0 && (int)jsonobject["percent"].n > 0)
					{
						int itemPrice = (int)Math.Ceiling((double)(jsonData.instance.ItemJsonData[jsonobject["GoodsID"].I.ToString()]["price"].n / jsonobject["percent"].n));
						inv.inventory[index].itemPrice = itemPrice;
					}
					else if ((int)jsonobject["EXGoodsID"].n == 10035)
					{
						inv.inventory[index].itemPrice = (int)((float)((int)jsonData.instance.ItemJsonData[string.Concat(inv.inventory[index].itemID)]["price"].n) * (jsonobject["price"].n / 100f));
					}
					else
					{
						inv.inventory[index].itemPrice = (int)jsonobject["Money"].n;
					}
					inv.inventory[index].ExGoodsID = (int)jsonobject["EXGoodsID"].n;
					inv.inventory[index].ExItemIcon = component.items[(int)jsonobject["EXGoodsID"].n].itemIcon;
					if (this.ExShopMoney != null)
					{
						this.ExShopMoney.ExGoodsID = (int)jsonobject["EXGoodsID"].n;
					}
				}
				num++;
			}
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00227830 File Offset: 0x00225A30
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

		// Token: 0x060052A9 RID: 21161 RVA: 0x002278EC File Offset: 0x00225AEC
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

		// Token: 0x060052AA RID: 21162 RVA: 0x00227A08 File Offset: 0x00225C08
		private void initShopItems(int index)
		{
			Inventory2 inventory = this.invList[index];
			for (int i = 0; i < (int)inventory.count; i++)
			{
				inventory.inventory.Add(new item());
			}
			inventory.InitSlot("SlotShop", "Win/item");
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x00227A54 File Offset: 0x00225C54
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

		// Token: 0x060052AC RID: 21164 RVA: 0x00227C2C File Offset: 0x00225E2C
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

		// Token: 0x060052AD RID: 21165 RVA: 0x0003B358 File Offset: 0x00039558
		public void sortItem(List<JSONObject> shop)
		{
			shop.Sort(delegate(JSONObject a, JSONObject b)
			{
				int num = (int)jsonData.instance.ItemJsonData[a["GoodsID"].I.ToString()]["quality"].n;
				int value = (int)jsonData.instance.ItemJsonData[b["GoodsID"].I.ToString()]["quality"].n;
				return num.CompareTo(value);
			});
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0003B37F File Offset: 0x0003957F
		public void closeShop()
		{
			base.gameObject.SetActive(false);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0001EE73 File Offset: 0x0001D073
		public void openShop()
		{
			base.transform.localPosition = Vector3.zero;
			base.gameObject.SetActive(true);
		}

		// Token: 0x040052BE RID: 21182
		public List<Inventory2> invList;

		// Token: 0x040052BF RID: 21183
		public List<UIselect> selectList;

		// Token: 0x040052C0 RID: 21184
		public List<UILabel> ChildTitle;

		// Token: 0x040052C1 RID: 21185
		public ThreeSceernUI threeSceernUI;

		// Token: 0x040052C2 RID: 21186
		public UILabel ShopName;

		// Token: 0x040052C3 RID: 21187
		public ExGoods ExShopMoney;
	}
}
