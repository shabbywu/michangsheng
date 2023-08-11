using Tab;
using UnityEngine;
using UnityEngine.Events;

public class MainConfig : TabSetPanel, IESCClose
{
	public MainConfig(GameObject go)
		: base(go)
	{
	}

	protected override void Init()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		base.Init();
		Get<FpBtn>("关闭").mouseUpEvent.AddListener(new UnityAction(Hide));
	}

	public override void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		base.Show();
	}

	public bool TryEscClose()
	{
		Hide();
		ESCCloseManager.Inst.UnRegisterClose(this);
		return true;
	}
}
