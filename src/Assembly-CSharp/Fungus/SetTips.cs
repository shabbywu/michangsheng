using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "SetTips", "提示", 0)]
[AddComponentMenu("")]
public class SetTips : Command
{
	[Tooltip("提示内容")]
	[SerializeField]
	protected string Content;

	public override void OnEnter()
	{
		UIPopTip.Inst.Pop(Content);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
