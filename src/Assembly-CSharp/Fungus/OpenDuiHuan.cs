using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenDuiHuan", "打开兑换界面", 0)]
[AddComponentMenu("")]
public class OpenDuiHuan : Command
{
	[Tooltip("兑换界面ID")]
	[SerializeField]
	protected int DuiHuanType = 1;

	public override void OnEnter()
	{
		UIDuiHuanShop.Inst.Show(DuiHuanType);
		UIDuiHuanShop.Inst.RefreshUI();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
