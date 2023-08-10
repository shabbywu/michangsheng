using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
	private List<TabButton> tabList = new List<TabButton>();

	private bool first;

	public void AddTab(TabButton tab)
	{
		if (tab.IsFirst)
		{
			tabList.Insert(0, tab);
		}
		else
		{
			tabList.Add(tab);
		}
	}

	public void TryToggle(TabButton toggleTab, bool isGroup = false)
	{
		foreach (TabButton tab in tabList)
		{
			if ((Object)(object)tab == (Object)(object)toggleTab)
			{
				if (!tab.IsOn || isGroup)
				{
					tab.OnToggle();
				}
			}
			else
			{
				tab.OnLose();
			}
		}
	}

	private void OnEnable()
	{
		first = true;
	}

	private void Update()
	{
		if (first)
		{
			SetFirstTab();
			first = false;
		}
	}

	public void SetFirstTab()
	{
		TryToggle(tabList[0], isGroup: true);
	}

	public void HideTab()
	{
		for (int i = 0; i < tabList.Count; i++)
		{
			((Component)tabList[i]).gameObject.SetActive(false);
		}
	}

	public void UnHideTab()
	{
		for (int i = 0; i < tabList.Count; i++)
		{
			((Component)tabList[i]).gameObject.SetActive(true);
		}
	}
}
