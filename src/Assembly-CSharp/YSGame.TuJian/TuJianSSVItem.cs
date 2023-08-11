using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class TuJianSSVItem : SSVItem
{
	[HideInInspector]
	public bool needRefresh;

	private bool _IsSelected;

	private Color _SelectedTextColor = new Color(7f / 15f, 74f / 85f, 0.76862746f);

	private Color _NormalTextColor = new Color(12f / 85f, 0.34509805f, 0.35686275f);

	private Color _AlphaColor = new Color(0f, 0f, 0f, 0f);

	private Image _BtnImage;

	public override void Start()
	{
		base.Start();
		_BtnImage = ((Component)_button).GetComponent<Image>();
	}

	public override void Update()
	{
		base.Update();
		if (_IsSelected)
		{
			if (SSV.NowSelectItemIndex != base.DataIndex)
			{
				OnLoseSelect();
			}
		}
		else if (SSV.NowSelectItemIndex == base.DataIndex)
		{
			OnSelect();
		}
	}

	private void OnEnable()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)_button.onClick).AddListener(new UnityAction(OnClick));
	}

	private void OnDisable()
	{
		((UnityEventBase)_button.onClick).RemoveAllListeners();
	}

	public void OnClick()
	{
		if (SSV.NowSelectItemIndex != base.DataIndex)
		{
			SSV.NowSelectItemIndex = base.DataIndex;
			OnSelect();
			TuJianManager.Inst.OnButtonClick();
		}
	}

	public void OnSelect()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		_IsSelected = true;
		((Graphic)_text).color = _SelectedTextColor;
		((Graphic)_BtnImage).color = Color.white;
	}

	public void OnLoseSelect()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		_IsSelected = false;
		((Graphic)_text).color = _NormalTextColor;
		((Graphic)_BtnImage).color = _AlphaColor;
	}
}
