using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : ScrollList
{
	public GameObject ItemInspector;

	private void Start()
	{
		database = (ItemDatabase)(object)Resources.Load("Custom Data/Items/PlayerItem Database");
		Event.registerOut("onReqShopList", this, "onReqShopList");
		Event.registerOut("ShopOnDeselect", this, "ShopOnDeselect");
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void ShopOnDeselect(RecipeSlot slot)
	{
		ItemInspector.SetActive(true);
	}

	private int SortList(ITEM_INFO c, ITEM_INFO d)
	{
		if (Convert.ToInt32(c.itemCount) > Convert.ToInt32(d.itemCount))
		{
			return 1;
		}
		if (Convert.ToInt32(c.itemCount) < Convert.ToInt32(d.itemCount))
		{
			return -1;
		}
		return 0;
	}

	public void onReqShopList(ITEM_INFO_LIST infos, string shopPrice)
	{
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		clenrNowBtn();
		JSONObject jSONObject = new JSONObject(shopPrice);
		List<ITEM_INFO> values = infos.values;
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		CollectList component = ((Component)UI_HOMESCENE.instense.CollectUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List")).GetComponent<CollectList>();
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO iTEM_INFO = values[i];
			bool flag = true;
			if ((int)jsonData.instance.PlayerGoodsSJsonData[string.Concat(iTEM_INFO.itemId)]["onlyOne"].n == 1 && component.getItemByID(iTEM_INFO.itemId))
			{
				flag = false;
			}
			if (flag)
			{
				list.Add(values[i]);
			}
		}
		list.Sort(SortList);
		for (int j = 0; j < list.Count; j++)
		{
			ITEM_INFO info = list[j];
			GameObject val = createBtn<UI_ShopBtn>(info);
			setItemImage(val, info);
			setItemCount(val, info);
			UI_ShopBtn component2 = val.GetComponent<UI_ShopBtn>();
			Transform val2 = val.transform.Find("price");
			((Component)val2.Find("Text")).GetComponent<Text>().text = string.Concat(jSONObject[string.Concat(component2.ietmUUID)]["price"].n);
			if (jSONObject[string.Concat(component2.ietmUUID)]["priceType"].n != -1f)
			{
				database.FindItemById((int)jSONObject[string.Concat(component2.ietmUUID)]["priceType"].n, out var itemData);
				((Component)val2.Find("Image")).GetComponent<Image>().overrideSprite = itemData.Icon;
			}
			val.transform.parent = ((Component)this).gameObject.transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
}
