using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab;

[Serializable]
public class TabSelectMag : UIBase
{
	private List<TabSelectCell> _list;

	public TabSelectMag(GameObject go)
	{
		_list = new List<TabSelectCell>();
		_go = go;
		Init();
	}

	public void SetDeafultSelect(int index = 0)
	{
		_list[index].Click();
	}

	private void Init()
	{
		Transform val = null;
		for (int i = 0; i < _go.transform.childCount; i++)
		{
			val = _go.transform.GetChild(i);
			if (!Tools.instance.IsInDF || !(((Object)val).name == "声望"))
			{
				if (((Object)val).name == "Panel")
				{
					break;
				}
				_list.Add(new TabSelectCell(((Component)val).gameObject, SingletonMono<TabUIMag>.Instance.PanelList[i]));
			}
		}
	}

	public void UpdateAll(TabSelectCell curSelectCell)
	{
		foreach (TabSelectCell item in _list)
		{
			if (curSelectCell == item)
			{
				item.SetIsSelect(flag: true);
			}
			else
			{
				item.SetIsSelect(flag: false);
			}
		}
	}
}
