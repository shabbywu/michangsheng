using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class TabGroup : MonoBehaviour
{
	// Token: 0x06002068 RID: 8296 RVA: 0x0001AA5F File Offset: 0x00018C5F
	public void AddTab(TabButton tab)
	{
		if (tab.IsFirst)
		{
			this.tabList.Insert(0, tab);
			return;
		}
		this.tabList.Add(tab);
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x00113254 File Offset: 0x00111454
	public void TryToggle(TabButton toggleTab, bool isGroup = false)
	{
		foreach (TabButton tabButton in this.tabList)
		{
			if (tabButton == toggleTab)
			{
				if (!tabButton.IsOn || isGroup)
				{
					tabButton.OnToggle();
				}
			}
			else
			{
				tabButton.OnLose();
			}
		}
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x0001AA83 File Offset: 0x00018C83
	private void OnEnable()
	{
		this.first = true;
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x0001AA8C File Offset: 0x00018C8C
	private void Update()
	{
		if (this.first)
		{
			this.SetFirstTab();
			this.first = false;
		}
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x0001AAA3 File Offset: 0x00018CA3
	public void SetFirstTab()
	{
		this.TryToggle(this.tabList[0], true);
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x001132C4 File Offset: 0x001114C4
	public void HideTab()
	{
		for (int i = 0; i < this.tabList.Count; i++)
		{
			this.tabList[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x00113300 File Offset: 0x00111500
	public void UnHideTab()
	{
		for (int i = 0; i < this.tabList.Count; i++)
		{
			this.tabList[i].gameObject.SetActive(true);
		}
	}

	// Token: 0x04001BE0 RID: 7136
	private List<TabButton> tabList = new List<TabButton>();

	// Token: 0x04001BE1 RID: 7137
	private bool first;
}
