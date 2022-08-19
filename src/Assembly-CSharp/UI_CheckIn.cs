using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000404 RID: 1028
public class UI_CheckIn : ScrollList
{
	// Token: 0x06002127 RID: 8487 RVA: 0x000E7E4C File Offset: 0x000E604C
	private void Start()
	{
		this.close();
		Event.registerOut("getCheckInList", this, "getCheckInList");
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x000E7E65 File Offset: 0x000E6065
	public void open()
	{
		this.chcekinUI.SetActive(true);
		this.getCheckInList(1);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x000E7E7A File Offset: 0x000E607A
	public void close()
	{
		this.chcekinUI.SetActive(false);
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x000E7E88 File Offset: 0x000E6088
	public void Checkbutton()
	{
		if (this.chcekinUI.activeInHierarchy)
		{
			this.close();
			return;
		}
		this.open();
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x000E7EA4 File Offset: 0x000E60A4
	public void setCheckInList(ITEM_INFO_LIST infos)
	{
		base.setList<UI_CheckinBtn>(infos);
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x000E7EB0 File Offset: 0x000E60B0
	public void CheckIn()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			UI_HOMESCENE.instense.ItemCheckIn.GetComponent<Tooltip>();
			account.CheckIn((ushort)this.nowType);
		}
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x000E7EF0 File Offset: 0x000E60F0
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

	// Token: 0x0600212F RID: 8495 RVA: 0x000E7F8C File Offset: 0x000E618C
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
				item_INFO.itemId = jsonobject["checkinId"].I;
				item_INFO.itemCount = (uint)jsonobject["checkincount"].n;
				item_INFO.itemIndex = 0;
				item_INFO_LIST.values.Add(item_INFO);
			}
		}
		this.setCheckInList(item_INFO_LIST);
		this.nowType = type;
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x000E8058 File Offset: 0x000E6258
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

	// Token: 0x06002131 RID: 8497 RVA: 0x000E80C8 File Offset: 0x000E62C8
	public void closeItmeUI()
	{
		UI_HOMESCENE.instense.ItemCheckIn.SetActive(false);
	}

	// Token: 0x04001AD5 RID: 6869
	public GameObject chcekinUI;

	// Token: 0x04001AD6 RID: 6870
	public int nowType = 1;

	// Token: 0x04001AD7 RID: 6871
	public GameObject Item_CheckIn;
}
