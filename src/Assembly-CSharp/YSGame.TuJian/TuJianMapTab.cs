using UnityEngine;

namespace YSGame.TuJian;

public class TuJianMapTab : TuJianTab
{
	[HideInInspector]
	public static TuJianMapTab Inst;

	public override void Awake()
	{
		Inst = this;
		TabType = TuJianTabType.Map;
		base.Awake();
	}

	private void Update()
	{
	}

	public override void Show()
	{
		base.Show();
	}

	public override void Hide()
	{
		base.Hide();
	}
}
