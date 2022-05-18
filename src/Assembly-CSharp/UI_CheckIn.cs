using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005B4 RID: 1460
public class UI_CheckIn : ScrollList
{
	// Token: 0x060024D9 RID: 9433 RVA: 0x0001D991 File Offset: 0x0001BB91
	private void Start()
	{
		this.close();
		Event.registerOut("getCheckInList", this, "getCheckInList");
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x0001D9AA File Offset: 0x0001BBAA
	public void open()
	{
		this.chcekinUI.SetActive(true);
		this.getCheckInList(1);
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x0001D9BF File Offset: 0x0001BBBF
	public void close()
	{
		this.chcekinUI.SetActive(false);
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x0001D9CD File Offset: 0x0001BBCD
	public void Checkbutton()
	{
		if (this.chcekinUI.activeInHierarchy)
		{
			this.close();
			return;
		}
		this.open();
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x0001D9E9 File Offset: 0x0001BBE9
	public void setCheckInList(ITEM_INFO_LIST infos)
	{
		base.setList<UI_CheckinBtn>(infos);
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x00129B48 File Offset: 0x00127D48
	public void CheckIn()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			UI_HOMESCENE.instense.ItemCheckIn.GetComponent<Tooltip>();
			account.CheckIn((ushort)this.nowType);
		}
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x00129B88 File Offset: 0x00127D88
	public override void setItemButton(GameObject button, int index)
	{
		base.setItemButton(button, index);
		Account account = (Account)KBEngineApp.app.player();
		if (account != null && account.CheckInList != null)
		{
			foreach (CHECKIN_INFO checkin_INFO in account.CheckInList.values)
			{
				if ((int)checkin_INFO.type == this.nowType && index + 1 <= (int)checkin_INFO.count)
				{
					button.GetComponent<Button>().interactable = false;
				}
			}
		}
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x00129C24 File Offset: 0x00127E24
	public void getCheckInList(int type)
	{
		ITEM_INFO_LIST item_INFO_LIST = new ITEM_INFO_LIST();
		JSONObject checkInJsonData = jsonData.instance.CheckInJsonData;
		for (int i = 0; i < checkInJsonData.list.Count; i++)
		{
			JSONObject jsonobject = checkInJsonData.list[i];
			if ((int)jsonobject["checkinType"].n == type)
			{
				ITEM_INFO item_INFO = new ITEM_INFO();
				item_INFO.UUID = (ulong)jsonobject["id"].n;
				item_INFO.itemId = (int)jsonobject["checkinId"].n;
				item_INFO.itemCount = (uint)jsonobject["checkincount"].n;
				item_INFO.itemIndex = 0;
				item_INFO_LIST.values.Add(item_INFO);
			}
		}
		this.setCheckInList(item_INFO_LIST);
		this.nowType = type;
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x00129CF4 File Offset: 0x00127EF4
	public override void clenrNowBtn()
	{
		foreach (object obj in this.UIList.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.active)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x0001D9F2 File Offset: 0x0001BBF2
	public void closeItmeUI()
	{
		UI_HOMESCENE.instense.ItemCheckIn.SetActive(false);
	}

	// Token: 0x04001F91 RID: 8081
	public GameObject chcekinUI;

	// Token: 0x04001F92 RID: 8082
	public int nowType = 1;

	// Token: 0x04001F93 RID: 8083
	public GameObject Item_CheckIn;
}
