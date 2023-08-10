using System.Collections.Generic;
using UnityEngine;

namespace Tab;

public class SysSelectMag : UIBase
{
	private List<SysSelectCell> _list;

	public SysSelectMag(GameObject go)
	{
		_list = new List<SysSelectCell>();
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
			if (Tools.instance.IsInDF && (((Object)val).name == "保存" || ((Object)val).name == "读取"))
			{
				((Component)val).gameObject.SetActive(false);
				continue;
			}
			if (!(((Object)val).name == "返回标题"))
			{
				_list.Add(new SysSelectCell(((Component)val).gameObject, SingletonMono<TabUIMag>.Instance.SystemPanel.PanelList[i]));
				continue;
			}
			break;
		}
	}

	public void UpdateAll(SysSelectCell curSelectCell)
	{
		foreach (SysSelectCell item in _list)
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
