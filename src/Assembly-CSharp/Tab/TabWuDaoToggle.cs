using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab;

[Serializable]
public class TabWuDaoToggle : UIBase
{
	private bool _isActive;

	private GameObject _active;

	private GameObject _noActive;

	public int Id;

	public TabWuDaoToggle(GameObject go, int wudaoId, Sprite sprite)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		_go = go;
		_isActive = false;
		Id = wudaoId;
		_active = Get("Active");
		_noActive = Get("NoActive");
		WuDaoAllTypeJson wuDaoAllTypeJson = WuDaoAllTypeJson.DataDict[wudaoId];
		UIListener uIListener = go.AddComponent<UIListener>();
		uIListener.mouseEnterEvent.AddListener(new UnityAction(Enter));
		uIListener.mouseOutEvent.AddListener(new UnityAction(Out));
		uIListener.mouseUpEvent.AddListener(new UnityAction(Click));
		Get<Image>("Active/Icon").sprite = sprite;
		Get<Image>("NoActive/Icon").sprite = sprite;
		Get<Text>("Active/Name").text = wuDaoAllTypeJson.name;
		Get<Text>("NoActive/Name").text = wuDaoAllTypeJson.name;
		_go.SetActive(true);
	}

	private void Click()
	{
		if (!_isActive)
		{
			SingletonMono<TabUIMag>.Instance.WuDaoPanel.SelectTypeCallBack(this);
			MusicMag.instance.PlayEffectMusic(1);
		}
	}

	public void SetIsActive(bool flag)
	{
		_isActive = flag;
	}

	public void UpdateUI()
	{
		if (_isActive)
		{
			_noActive.SetActive(false);
			_active.SetActive(true);
		}
		else
		{
			_noActive.SetActive(true);
			_active.SetActive(false);
		}
	}

	private void Enter()
	{
		if (!_isActive)
		{
			_noActive.SetActive(false);
			_active.SetActive(true);
		}
	}

	private void Out()
	{
		UpdateUI();
	}
}
