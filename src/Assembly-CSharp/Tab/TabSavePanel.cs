using System.Collections.Generic;
using UnityEngine;

namespace Tab;

public class TabSavePanel : ISysPanelBase
{
	private bool _isInit;

	public List<TabDataBase> SaveList;

	public TabSavePanel(GameObject go)
	{
		_go = go;
		_isInit = false;
		SaveList = new List<TabDataBase>();
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		UpdateUI();
		_go.SetActive(true);
	}

	private void Init()
	{
		Transform transform = Get("SaveList/ViewPort/Content").transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			SaveList.Add(new TabDataBase(((Component)transform.GetChild(i)).gameObject));
		}
	}

	private void UpdateUI()
	{
		foreach (TabDataBase save in SaveList)
		{
			save.UpdateDate();
		}
	}
}
