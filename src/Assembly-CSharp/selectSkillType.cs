using GUIPackage;
using UnityEngine;

public class selectSkillType : selectSkillConfig
{
	private void Awake()
	{
		mList = ((Component)this).GetComponent<UIPopupList>();
	}

	private void Start()
	{
		EventDelegate.Add(mList.onChange, OnChange);
	}

	public int getInputID1(string name)
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
		Tools.instance.getPlayer();
		int inputID = getInputID1(mList.value);
		Singleton.skillUI.ShowType = inputID;
	}
}
