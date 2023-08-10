using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckPingJin", "检测是否是瓶颈期数量", 0)]
[AddComponentMenu("")]
public class CheckPingJin : Command
{
	[Tooltip("获取到的瓶颈值存放位置")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempValue;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		TempValue.Value = player.level % 3 == 0 && player.exp >= (ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n;
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
