using System;
using JiaoYi;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus;

[CommandInfo("YSTools", "OpenJiaoYi", "打开交易界面", 0)]
[AddComponentMenu("")]
public class OpenJiaoYi : Command, INoCommand
{
	[Tooltip("进行交易的武将ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AvatarID;

	public override void OnEnter()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		ResManager.inst.LoadPrefab("JiaoYiUI").Inst(((Component)NewUICanvas.Inst).transform);
		JiaoYiUIMag.Inst.Init(AvatarID.Value, new UnityAction(Continue));
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
