using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenEventOption", "打开事件选择界面", 0)]
[AddComponentMenu("")]
public class OpenEventOption : Command
{
	[Tooltip("需要打开的事件ID")]
	[SerializeField]
	protected int ShiJianID = 1;

	public override void OnEnter()
	{
		new AddOption().addOption(ShiJianID);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
