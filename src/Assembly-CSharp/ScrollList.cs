using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F9 RID: 1017
public class ScrollList : MonoBehaviour
{
	// Token: 0x060020CC RID: 8396 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x000E68D0 File Offset: 0x000E4AD0
	public GameObject createBtn<Templet>(ITEM_INFO info) where Templet : ScrollBtn
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemple.gameObject);
		Templet component = gameObject.GetComponent<Templet>();
		gameObject.SetActive(true);
		component.ItemID = info.itemId;
		component.ietmUUID = info.UUID;
		return gameObject;
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x000E6920 File Offset: 0x000E4B20
	public virtual void setItemImage(GameObject _Button, ITEM_INFO info)
	{
		ItemData itemData;
		this.database.FindItemById(info.itemId, out itemData);
		Image component = _Button.transform.Find("Image").GetComponent<Image>();
		component.overrideSprite = itemData.Icon;
		component.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x000E6988 File Offset: 0x000E4B88
	public virtual void setItemCount(GameObject _Button, ITEM_INFO info)
	{
		int num = Convert.ToInt32(info.itemCount);
		Text component = _Button.transform.Find("Text").GetComponent<Text>();
		if (num > 1)
		{
			component.text = "x" + num;
			return;
		}
		component.text = "";
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x000E69E0 File Offset: 0x000E4BE0
	public void setList<T>(ITEM_INFO_LIST infos) where T : ScrollBtn
	{
		this.clenrNowBtn();
		List<ITEM_INFO> values = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO info = values[i];
			GameObject gameObject = this.createBtn<T>(info);
			this.setItemImage(gameObject, info);
			this.setItemCount(gameObject, info);
			this.setItemButton(gameObject, i);
			gameObject.transform.parent = this.UIList.transform;
		}
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void setItemButton(GameObject button, int itemIndex)
	{
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x000E6A4C File Offset: 0x000E4C4C
	public virtual void clenrNowBtn()
	{
		foreach (object obj in base.gameObject.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.active)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x04001AA9 RID: 6825
	public GameObject ItemTemple;

	// Token: 0x04001AAA RID: 6826
	public ItemDatabase database;

	// Token: 0x04001AAB RID: 6827
	public GameObject UIList;
}
