using System;
using UnityEngine;

namespace Tab;

[Serializable]
public class TabShengWangPanel : ITabPanelBase
{
	private bool _isInit;

	public TabShengWangPanel(GameObject gameObject)
	{
		_go = gameObject;
		_isInit = false;
	}

	private void Init()
	{
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		_go.SetActive(true);
	}
}
