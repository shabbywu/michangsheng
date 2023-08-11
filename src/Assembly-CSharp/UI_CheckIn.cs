using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UI_CheckIn : ScrollList
{
	public GameObject chcekinUI;

	public int nowType = 1;

	public GameObject Item_CheckIn;

	private void Start()
	{
		close();
		Event.registerOut("getCheckInList", this, "getCheckInList");
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void open()
	{
		chcekinUI.SetActive(true);
		getCheckInList(1);
	}

	public void close()
	{
		chcekinUI.SetActive(false);
	}

	public void Checkbutton()
	{
		if (chcekinUI.activeInHierarchy)
		{
			close();
		}
		else
		{
			open();
		}
	}

	public void setCheckInList(ITEM_INFO_LIST infos)
	{
		setList<UI_CheckinBtn>(infos);
	}

	public void CheckIn()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			UI_HOMESCENE.instense.ItemCheckIn.GetComponent<Tooltip>();
			account.CheckIn((ushort)nowType);
		}
	}

	public override void setItemButton(GameObject button, int index)
	{
		base.setItemButton(button, index);
		Account account = (Account)KBEngineApp.app.player();
		if (account == null || account.CheckInList == null)
		{
			return;
		}
		foreach (CHECKIN_INFO value in account.CheckInList.values)
		{
			if (value.type == nowType && index + 1 <= value.count)
			{
				((Selectable)button.GetComponent<Button>()).interactable = false;
			}
		}
	}

	public void getCheckInList(int type)
	{
		ITEM_INFO_LIST iTEM_INFO_LIST = new ITEM_INFO_LIST();
		JSONObject checkInJsonData = jsonData.instance.CheckInJsonData;
		for (int i = 0; i < checkInJsonData.list.Count; i++)
		{
			JSONObject jSONObject = checkInJsonData.list[i];
			if ((int)jSONObject["checkinType"].n == type)
			{
				ITEM_INFO iTEM_INFO = new ITEM_INFO();
				iTEM_INFO.UUID = (ulong)jSONObject["id"].n;
				iTEM_INFO.itemId = jSONObject["checkinId"].I;
				iTEM_INFO.itemCount = (uint)jSONObject["checkincount"].n;
				iTEM_INFO.itemIndex = 0;
				iTEM_INFO_LIST.values.Add(iTEM_INFO);
			}
		}
		setCheckInList(iTEM_INFO_LIST);
		nowType = type;
	}

	public override void clenrNowBtn()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		foreach (Transform item in UIList.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.active)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public void closeItmeUI()
	{
		UI_HOMESCENE.instense.ItemCheckIn.SetActive(false);
	}
}
