using Fungus;
using UnityEngine;

[CommandInfo("YSTools", "OpenLianQi", "打开炼器界面", 0)]
[AddComponentMenu("")]
public class OpenLianQi : Command
{
	public override void OnEnter()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼器);
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
