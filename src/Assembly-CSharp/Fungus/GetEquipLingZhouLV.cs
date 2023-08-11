using System;
using JSONClass;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetEquipLingZhouLV", "获取装备的灵舟等级", 0)]
[AddComponentMenu("")]
public class GetEquipLingZhouLV : Command
{
	[Tooltip("获取到的灵舟等级")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable LV;

	public override void OnEnter()
	{
		_ItemJsonData equipLingZhouData = Tools.instance.getPlayer().GetEquipLingZhouData();
		if (equipLingZhouData != null)
		{
			LV.Value = equipLingZhouData.quality;
		}
		else
		{
			LV.Value = 0;
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
