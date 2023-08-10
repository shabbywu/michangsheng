using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab;

[Serializable]
public class TabSelectCell : UIBase
{
	private bool _isActive;

	private GameObject _unSelect;

	private GameObject _select;

	private ITabPanelBase _panel;

	public TabSelectCell(GameObject go, ITabPanelBase panel)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		_go = go;
		((UnityEvent)_go.AddComponent<Button>().onClick).AddListener(new UnityAction(Click));
		_isActive = false;
		_unSelect = Get("UnSelect");
		_select = Get("Select");
		_select.gameObject.SetActive(false);
		_panel = panel;
	}

	public void SetIsSelect(bool flag)
	{
		_isActive = flag;
		UpdateUI();
	}

	private void UpdateUI()
	{
		if (_isActive)
		{
			_select.SetActive(true);
			_unSelect.SetActive(false);
			_panel.Show();
			if (_panel.HasHp)
			{
				SingletonMono<TabUIMag>.Instance.ShowBaseData();
			}
			else
			{
				SingletonMono<TabUIMag>.Instance.HideBaseData();
			}
		}
		else
		{
			_unSelect.SetActive(true);
			_select.SetActive(false);
			_panel.Hide();
		}
	}

	public void Click()
	{
		if (!_isActive)
		{
			SingletonMono<TabUIMag>.Instance.TabBag.Close();
			SingletonMono<TabUIMag>.Instance.TabFangAnPanel.Close();
			SingletonMono<TabUIMag>.Instance.TabSelect.UpdateAll(this);
			MusicMag.instance.PlayEffectMusic(13);
		}
	}
}
