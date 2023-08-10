using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUIPackage;

public class Shop_UI : MonoBehaviour
{
	public List<Inventory2> invList;

	public List<UIselect> selectList;

	public List<UILabel> ChildTitle;

	public ThreeSceernUI threeSceernUI;

	public UILabel ShopName;

	public ExGoods ExShopMoney;

	private void Start()
	{
		initCangJinGeShop();
	}

	public void initCangJinGeShop()
	{
		List<JSONObject> shopList = GetShopList();
		if (shopList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (JSONObject item in shopList)
		{
			ChildTitle[num].text = Tools.Code64(item["ChildTitle"].str);
			if (item["ExShopID"].I == 0)
			{
				initShopItems(num);
				InitMoneyByMethod(num);
			}
			else
			{
				InitExChengShopItems(num);
				InitExChengMethod(num);
			}
			num++;
		}
	}

	public void InitMoneyByMethod(int index)
	{
		InitShopPage(0, invList[index], index + 1);
		selectList[index].list.Clear();
		JSONObject shopJsonData = GetShopJsonData(index + 1);
		setPageText(shopJsonData, invList[index], index);
	}

	public JSONObject GetShopJsonData(int shopType)
	{
		return GetShopList().Find((JSONObject aa) => shopType == (int)aa["shopType"].n);
	}

	public List<JSONObject> GetShopList()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Scene activeScene = SceneManager.GetActiveScene();
		string scencName = ((Scene)(ref activeScene)).name;
		return jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
	}

	public List<JSONObject> GetJiaoHuanShop(int JiaohanShopID)
	{
		return jsonData.instance.jiaoHuanShopGoods.list.FindAll((JSONObject aa) => JiaohanShopID == aa["ShopID"].I);
	}

	public void setPageText(JSONObject info, Inventory2 cc, int indaa)
	{
		if (info != null)
		{
			for (int i = 0; i < info["items"].Count / (int)cc.count + 1; i++)
			{
				selectList[indaa].list.Add("第" + Tools.getStr("shuzi" + (i + 1)) + "页");
			}
			if ((Object)(object)ShopName != (Object)null)
			{
				ShopName.text = Tools.instance.Code64ToString(info["Title"].str);
			}
		}
	}

	public void InitExChengShopItems(int index)
	{
		Inventory2 inventory = invList[index];
		for (int i = 0; i < (int)inventory.count; i++)
		{
			inventory.inventory.Add(new item());
		}
		inventory.InitSlot<itemcellShopEx>("SlotShopEX", (int)inventory.count, delegate
		{
		});
	}

	public void InitExChengMethod(int index)
	{
		InitExShopPage(0, invList[index], index + 1);
		selectList[index].list.Clear();
		int exShopID = getExShopID(index + 1);
		List<JSONObject> jiaoHuanShop = GetJiaoHuanShop(exShopID);
		JSONObject shopJsonData = GetShopJsonData(index + 1);
		if (jiaoHuanShop.Count > 0)
		{
			for (int i = 0; i < jiaoHuanShop.Count / (int)invList[index].count + 1; i++)
			{
				selectList[index].list.Add("第" + Tools.getStr("shuzi" + (i + 1)) + "页");
			}
			if ((Object)(object)ShopName != (Object)null && shopJsonData != null)
			{
				ShopName.text = Tools.instance.Code64ToString(shopJsonData["Title"].str);
			}
		}
	}

	public virtual int getExShopID(int type)
	{
		return GetShopJsonData(type)["ExShopID"].I;
	}

	public void resetInventory(Inventory2 inv)
	{
		for (int i = 0; i < inv.inventory.Count; i++)
		{
			inv.inventory[i] = new item();
			inv.inventory[i].itemNum = 1;
		}
	}

	public void getShopJson()
	{
	}

	public virtual bool ShouldSetLevel(int Type = 0)
	{
		if (Type != 1)
		{
			return false;
		}
		return true;
	}

	public virtual int getShopType(int type)
	{
		return GetShopJsonData(type)["SType"].I;
	}

	public virtual void InitExShopPage(int page, Inventory2 inv, int type)
	{
		resetInventory(inv);
		int shopType = getShopType(type);
		int shopExID = getExShopID(type);
		int num = 0;
		ItemDatebase component = ((Component)jsonData.instance).GetComponent<ItemDatebase>();
		List<JSONObject> list = jsonData.instance.jiaoHuanShopGoods.list.FindAll((JSONObject aa) => aa["ShopID"].I == shopExID);
		sortItem(list);
		foreach (JSONObject item in list)
		{
			if (ShouldSetLevel(shopType))
			{
				JSONObject jSONObject = jsonData.instance.ItemJsonData[item["GoodsID"].I.ToString()];
				if (Tools.instance.getPlayer().getLevelType() < (int)jSONObject["quality"].n && (jSONObject["type"].I == 3 || jSONObject["type"].I == 4))
				{
					continue;
				}
			}
			if (inv.isInPage(page, num, (int)inv.count))
			{
				int index = inv.addItemToNullInventory(item["GoodsID"].I, 1, Tools.getUUID(), null);
				if ((int)item["Money"].n == 0 && (int)item["percent"].n > 0)
				{
					int itemPrice = (int)Math.Ceiling(jsonData.instance.ItemJsonData[item["GoodsID"].I.ToString()]["price"].n / item["percent"].n);
					inv.inventory[index].itemPrice = itemPrice;
				}
				else if (item["EXGoodsID"].I == 10035)
				{
					inv.inventory[index].itemPrice = (int)((float)(int)jsonData.instance.ItemJsonData[string.Concat(inv.inventory[index].itemID)]["price"].n * (item["price"].n / 100f));
				}
				else
				{
					inv.inventory[index].itemPrice = (int)item["Money"].n;
				}
				inv.inventory[index].ExGoodsID = item["EXGoodsID"].I;
				inv.inventory[index].ExItemIcon = component.items[item["EXGoodsID"].I].itemIcon;
				if ((Object)(object)ExShopMoney != (Object)null)
				{
					ExShopMoney.ExGoodsID = item["EXGoodsID"].I;
				}
			}
			num++;
		}
	}

	public virtual void updateItem()
	{
		List<JSONObject> shopList = GetShopList();
		if (shopList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (JSONObject item in shopList)
		{
			if (item["ExShopID"].I == 0)
			{
				InitShopPage(selectList[num].NowIndex, invList[num], num + 1);
			}
			else
			{
				InitExShopPage(selectList[num].NowIndex, invList[num], num + 1);
			}
			num++;
		}
	}

	public void showBtn()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Scene activeScene = SceneManager.GetActiveScene();
		string scencName = ((Scene)(ref activeScene)).name;
		List<JSONObject> list = jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
		if ((Object)(object)threeSceernUI == (Object)null)
		{
			threeSceernUI = ThreeSceernUI.inst;
		}
		if (list.Count > 0)
		{
			if (list[0]["SType"].I == 1)
			{
				threeSceernUI.setPostion(((Component)threeSceernUI).transform.Find("grid/shopBtn"));
			}
			else if (list[0]["SType"].I == 2)
			{
				threeSceernUI.setPostion(((Component)threeSceernUI).transform.Find("grid/shenbingeBtn"));
			}
			else if (list[0]["SType"].I == 3)
			{
				threeSceernUI.setPostion(((Component)threeSceernUI).transform.Find("grid/yaofangBtn"));
			}
		}
	}

	private void initShopItems(int index)
	{
		Inventory2 inventory = invList[index];
		for (int i = 0; i < (int)inventory.count; i++)
		{
			inventory.inventory.Add(new item());
		}
		inventory.InitSlot("SlotShop");
	}

	public virtual void InitShopPage(int page, Inventory2 inv, int type)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		resetInventory(inv);
		Scene activeScene = SceneManager.GetActiveScene();
		string scencName = ((Scene)(ref activeScene)).name;
		int num = 0;
		foreach (JSONObject item in jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName && type == (int)aa["shopType"].n))
		{
			foreach (int item2 in sortItem(item["items"]))
			{
				if (Tools.instance.getPlayer().getLevelType() >= (int)jsonData.instance.ItemJsonData[item2.ToString()]["quality"].n)
				{
					if (inv.isInPage(page, num, (int)inv.count))
					{
						int index = inv.addItemToNullInventory(item2, 1, Tools.getUUID(), null);
						inv.inventory[index].itemPrice = (int)((float)(int)jsonData.instance.ItemJsonData[string.Concat(inv.inventory[index].itemID)]["price"].n * (item["price"].n / 100f));
					}
					num++;
				}
			}
		}
	}

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

	public void sortItem(List<JSONObject> shop)
	{
		shop.Sort(delegate(JSONObject a, JSONObject b)
		{
			int i = jsonData.instance.ItemJsonData[a["GoodsID"].I.ToString()]["quality"].I;
			int i2 = jsonData.instance.ItemJsonData[b["GoodsID"].I.ToString()]["quality"].I;
			return i.CompareTo(i2);
		});
	}

	public void closeShop()
	{
		((Component)this).gameObject.SetActive(false);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void openShop()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).gameObject.SetActive(true);
	}
}
