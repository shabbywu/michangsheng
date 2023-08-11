using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddLingGuang", "增加灵光", 0)]
[AddComponentMenu("")]
public class AddLingGuang : Command
{
	[Tooltip("增加的灵光ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable LingGuangID;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().wuDaoMag.AddLingGuangByJsonID(LingGuangID.Value);
		UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
