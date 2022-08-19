using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class TabGroup : MonoBehaviour
{
	// Token: 0x06001CFE RID: 7422 RVA: 0x000CE5E3 File Offset: 0x000CC7E3
	public void AddTab(TabButton tab)
	{
		if (tab.IsFirst)
		{
			this.tabList.Insert(0, tab);
			return;
		}
		this.tabList.Add(tab);
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x000CE608 File Offset: 0x000CC808
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

	// Token: 0x06001D00 RID: 7424 RVA: 0x000CE678 File Offset: 0x000CC878
	private void OnEnable()
	{
		this.first = true;
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x000CE681 File Offset: 0x000CC881
	private void Update()
	{
		if (this.first)
		{
			this.SetFirstTab();
			this.first = false;
		}
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x000CE698 File Offset: 0x000CC898
	public void SetFirstTab()
	{
		this.TryToggle(this.tabList[0], true);
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x000CE6B0 File Offset: 0x000CC8B0
	public void HideTab()
	{
		for (int i = 0; i < this.tabList.Count; i++)
		{
			this.tabList[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x000CE6EC File Offset: 0x000CC8EC
	public void UnHideTab()
	{
		for (int i = 0; i < this.tabList.Count; i++)
		{
			this.tabList[i].gameObject.SetActive(true);
		}
	}

	// Token: 0x04001788 RID: 6024
	private List<TabButton> tabList = new List<TabButton>();

	// Token: 0x04001789 RID: 6025
	private bool first;
}
