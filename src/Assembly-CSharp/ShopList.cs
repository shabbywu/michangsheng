using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005AC RID: 1452
public class ShopList : ScrollList
{
	// Token: 0x06002499 RID: 9369 RVA: 0x0001D684 File Offset: 0x0001B884
	private void Start()
	{
		this.database = (ItemDatabase)Resources.Load("Custom Data/Items/PlayerItem Database");
		Event.registerOut("onReqShopList", this, "onReqShopList");
		Event.registerOut("ShopOnDeselect", this, "ShopOnDeselect");
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x0001D6BD File Offset: 0x0001B8BD
	public void ShopOnDeselect(RecipeSlot slot)
	{
		this.ItemInspector.SetActive(true);
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x00128FD8 File Offset: 0x001271D8
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

	// Token: 0x0600249D RID: 9373 RVA: 0x00129020 File Offset: 0x00127220
	public void onReqShopList(ITEM_INFO_LIST infos, string shopPrice)
	{
		this.clenrNowBtn();
		JSONObject jsonobject = new JSONObject(shopPrice, -2, false, false);
		List<ITEM_INFO> values = infos.values;
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		CollectList component = UI_HOMESCENE.instense.CollectUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List").GetComponent<CollectList>();
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO item_INFO = values[i];
			bool flag = true;
			if ((int)jsonData.instance.PlayerGoodsSJsonData[string.Concat(item_INFO.itemId)]["onlyOne"].n == 1 && component.getItemByID(item_INFO.itemId))
			{
				flag = false;
			}
			if (flag)
			{
				list.Add(values[i]);
			}
		}
		list.Sort(new Comparison<ITEM_INFO>(this.SortList));
		for (int j = 0; j < list.Count; j++)
		{
			ITEM_INFO info = list[j];
			GameObject gameObject = base.createBtn<UI_ShopBtn>(info);
			this.setItemImage(gameObject, info);
			this.setItemCount(gameObject, info);
			UI_ShopBtn component2 = gameObject.GetComponent<UI_ShopBtn>();
			Transform transform = gameObject.transform.Find("price");
			transform.Find("Text").GetComponent<Text>().text = string.Concat(jsonobject[string.Concat(component2.ietmUUID)]["price"].n);
			if (jsonobject[string.Concat(component2.ietmUUID)]["priceType"].n != -1f)
			{
				ItemData itemData;
				this.database.FindItemById((int)jsonobject[string.Concat(component2.ietmUUID)]["priceType"].n, out itemData);
				transform.Find("Image").GetComponent<Image>().overrideSprite = itemData.Icon;
			}
			gameObject.transform.parent = base.gameObject.transform;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	// Token: 0x04001F71 RID: 8049
	public GameObject ItemInspector;
}
