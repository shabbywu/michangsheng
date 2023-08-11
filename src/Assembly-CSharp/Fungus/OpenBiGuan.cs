using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenBiGuan", "打开闭关界面", 0)]
[AddComponentMenu("")]
public class OpenBiGuan : Command
{
	[Tooltip("闭关界面的类型")]
	[SerializeField]
	protected int BiGuanType = 1;

	public override void OnEnter()
	{
		NewUI();
		Continue();
	}

	public void NewUI()
	{
		UIBiGuanPanel.Inst.OpenBiGuan(BiGuanType);
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
