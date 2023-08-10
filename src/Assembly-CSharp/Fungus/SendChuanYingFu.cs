using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "发送第一代传音符", "发送第一代传音符", 0)]
[AddComponentMenu("")]
public class SendChuanYingFu : Command
{
	[Tooltip("传音符的ID")]
	[SerializeField]
	protected int ID;

	[Tooltip("传音符的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ChuanYingID;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		if (ID != 0)
		{
			player.chuanYingManager.addChuanYingFu(ID);
		}
		else
		{
			player.chuanYingManager.addChuanYingFu(ChuanYingID.Value);
		}
		player.updateChuanYingFu();
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
