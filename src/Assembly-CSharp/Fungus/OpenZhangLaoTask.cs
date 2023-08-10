using UnityEngine;
using script.MenPaiTask.ZhangLao.UI;

namespace Fungus;

[CommandInfo("YSTools", "打开长老任务界面", "打开长老任务界面", 0)]
[AddComponentMenu("")]
public class OpenZhangLaoTask : Command, INoCommand
{
	public override void OnEnter()
	{
		ElderTaskUIMag.Open();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
