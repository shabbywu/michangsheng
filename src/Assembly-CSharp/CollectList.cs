using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class CollectList : ScrollList
{
	private void Awake()
	{
		Event.registerOut("openCollect", this, "openCollect");
		Event.registerOut("showCollect", this, "showCollect");
		database = jsonData.instance.playerDatabase;
		ItemTemple = ((Component)((Component)this).transform.GetChild(0)).gameObject;
	}

	private void Start()
	{
	}

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

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void showCollect(ITEM_INFO_LIST infos, ushort day, ushort exp)
	{
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		((Component)UI_Game.instence.tran_relive).gameObject.SetActive(true);
		((Component)UI_Game.instence.tran_relive.Find("title")).GetComponent<Text>().text = "你在极端环境下存活了" + day + "天！！";
		((Component)UI_Game.instence.tran_relive.Find("exp")).GetComponent<Text>().text = "获得经验：" + exp;
		List<ITEM_INFO> values = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO info = values[i];
			GameObject val = createBtn<UI_CollectBtnWorld>(info);
			setItemImage(val, info);
			setItemCount(val, info);
			val.transform.parent = ((Component)this).gameObject.transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void openCollect()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		clenrNowBtn();
		GameObject itemCollect = UI_HOMESCENE.instense.ItemCollect;
		Tooltip component = itemCollect.GetComponent<Tooltip>();
		int num = 0;
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			List<ITEM_INFO> values = account.itemList.values;
			for (int i = 0; i < values.Count; i++)
			{
				ITEM_INFO iTEM_INFO = values[i];
				GameObject val = createBtn<UI_CollectBtn>(iTEM_INFO);
				setItemImage(val, iTEM_INFO);
				setItemCount(val, iTEM_INFO);
				val.transform.parent = ((Component)this).gameObject.transform;
				val.transform.localScale = new Vector3(1f, 1f, 1f);
				if (component.item.itemUUID == iTEM_INFO.UUID)
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
