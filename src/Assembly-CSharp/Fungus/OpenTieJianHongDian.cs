using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "OpenTieJianHongDian", "显示铁剑红点", 0)]
[AddComponentMenu("")]
public class OpenTieJianHongDian : Command
{
	[Tooltip("红点ID")]
	[SerializeField]
	protected int HongDianID;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().TieJianHongDianList.SetField(HongDianID.ToString(), HongDianID.ToString());
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
