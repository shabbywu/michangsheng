using GUIPackage;
using KBEngine;
using UnityEngine;

public class selectSkillConfig : MonoBehaviour
{
	public enum selectType
	{
		SelectSkill,
		SelectStaticSkill,
		SelectItem
	}

	protected UIPopupList mList;

	public selectType type;

	private void Awake()
	{
		mList = ((Component)this).GetComponent<UIPopupList>();
	}

	private void Start()
	{
		Avatar player = Tools.instance.getPlayer();
		switch (type)
		{
		case selectType.SelectSkill:
			mList.value = mList.items[player.nowConfigEquipSkill];
			OnChange();
			break;
		case selectType.SelectStaticSkill:
			mList.value = mList.items[player.nowConfigEquipStaticSkill];
			break;
		case selectType.SelectItem:
			mList.value = mList.items[player.nowConfigEquipItem];
			break;
		}
		EventDelegate.Add(mList.onChange, OnChange);
	}

	public int getInputID(string name)
	{
		int num = 0;
		foreach (string item in mList.items)
		{
			if (name == item)
			{
				break;
			}
			num++;
		}
		return num;
	}

	private void OnChange()
	{
		Avatar player = Tools.instance.getPlayer();
		int inputID = getInputID(mList.value);
		switch (type)
		{
		case selectType.SelectSkill:
			player.setSkillConfigIndex(inputID);
			Singleton.key.LoadMapKey();
			break;
		case selectType.SelectStaticSkill:
			player.setStatikConfigIndex(inputID);
			Singleton.key2.LoadMapPassKey();
			break;
		case selectType.SelectItem:
			player.setItemConfigIndex(inputID);
			break;
		}
	}
}
