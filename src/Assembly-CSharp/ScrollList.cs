using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005A9 RID: 1449
public class ScrollList : MonoBehaviour
{
	// Token: 0x0600247E RID: 9342 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x00128990 File Offset: 0x00126B90
	public GameObject createBtn<Templet>(ITEM_INFO info) where Templet : ScrollBtn
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemple.gameObject);
		Templet component = gameObject.GetComponent<Templet>();
		gameObject.SetActive(true);
		component.ItemID = info.itemId;
		component.ietmUUID = info.UUID;
		return gameObject;
	}

	// Token: 0x06002480 RID: 9344 RVA: 0x001289E0 File Offset: 0x00126BE0
	public virtual void setItemImage(GameObject _Button, ITEM_INFO info)
	{
		ItemData itemData;
		this.database.FindItemById(info.itemId, out itemData);
		Image component = _Button.transform.Find("Image").GetComponent<Image>();
		component.overrideSprite = itemData.Icon;
		component.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	// Token: 0x06002481 RID: 9345 RVA: 0x00128A48 File Offset: 0x00126C48
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

	// Token: 0x06002482 RID: 9346 RVA: 0x00128AA0 File Offset: 0x00126CA0
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

	// Token: 0x06002483 RID: 9347 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void setItemButton(GameObject button, int itemIndex)
	{
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x00128B0C File Offset: 0x00126D0C
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

	// Token: 0x04001F65 RID: 8037
	public GameObject ItemTemple;

	// Token: 0x04001F66 RID: 8038
	public ItemDatabase database;

	// Token: 0x04001F67 RID: 8039
	public GameObject UIList;
}
