using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000572 RID: 1394
public class CollectList : ScrollList
{
	// Token: 0x0600236A RID: 9066 RVA: 0x00123DE8 File Offset: 0x00121FE8
	private void Awake()
	{
		Event.registerOut("openCollect", this, "openCollect");
		Event.registerOut("showCollect", this, "showCollect");
		this.database = jsonData.instance.playerDatabase;
		this.ItemTemple = base.transform.GetChild(0).gameObject;
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x00123E40 File Offset: 0x00122040
	public bool getItemByID(int itemID)
	{
		List<ITEM_INFO> values = ((Account)KBEngineApp.app.player()).itemList.values;
		bool result = false;
		for (int i = 0; i < values.Count; i++)
		{
			if (values[i].itemId == itemID)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x00123E8C File Offset: 0x0012208C
	public void showCollect(ITEM_INFO_LIST infos, ushort day, ushort exp)
	{
		UI_Game.instence.tran_relive.gameObject.SetActive(true);
		UI_Game.instence.tran_relive.Find("title").GetComponent<Text>().text = "你在极端环境下存活了" + day + "天！！";
		UI_Game.instence.tran_relive.Find("exp").GetComponent<Text>().text = "获得经验：" + exp;
		List<ITEM_INFO> values = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO info = values[i];
			GameObject gameObject = base.createBtn<UI_CollectBtnWorld>(info);
			this.setItemImage(gameObject, info);
			this.setItemCount(gameObject, info);
			gameObject.transform.parent = base.gameObject.transform;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x00123F7C File Offset: 0x0012217C
	public void openCollect()
	{
		this.clenrNowBtn();
		GameObject itemCollect = UI_HOMESCENE.instense.ItemCollect;
		Tooltip component = itemCollect.GetComponent<Tooltip>();
		int num = 0;
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			List<ITEM_INFO> values = account.itemList.values;
			for (int i = 0; i < values.Count; i++)
			{
				ITEM_INFO item_INFO = values[i];
				GameObject gameObject = base.createBtn<UI_CollectBtn>(item_INFO);
				this.setItemImage(gameObject, item_INFO);
				this.setItemCount(gameObject, item_INFO);
				gameObject.transform.parent = base.gameObject.transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				if (component.item.itemUUID == item_INFO.UUID)
				{
					num = 1;
				}
			}
		}
		if (num == 0)
		{
			itemCollect.SetActive(false);
		}
	}
}
