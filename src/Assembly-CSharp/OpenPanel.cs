using Fungus;
using UnityEngine;

[CommandInfo("YSTools", "OpenPanel", "打开UI界面", 0)]
[AddComponentMenu("")]
public class OpenPanel : Command
{
	[Tooltip("打开界面名称")]
	[SerializeField]
	protected PanelMamager.PanelType PanelName;

	public override void OnEnter()
	{
		PanelMamager.inst.OpenPanel(PanelName);
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
