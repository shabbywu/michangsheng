using System;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

namespace Tab;

[Serializable]
public class SysSelectCell : UIBase
{
	private bool _isActive;

	private GameObject _unSelect;

	private GameObject _select;

	private ISysPanelBase _panel;

	public SysSelectCell(GameObject go, ISysPanelBase panel)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		_go = go;
		_isActive = false;
		Get<FpBtn>("UnSelect").mouseUpEvent.AddListener(new UnityAction(Click));
		Get<FpBtn>("Select").mouseUpEvent.AddListener(new UnityAction(Click));
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
			SingletonMono<TabUIMag>.Instance.SystemPanel.SelectMag.UpdateAll(this);
			MusicMag.instance.PlayEffectMusic(13);
		}
	}
}
