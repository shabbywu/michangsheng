using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddNpcField", "增加npc属性", 0)]
[AddComponentMenu("")]
public class AddNpcField : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("增加的属性")]
	[SerializeField]
	public string fieldName;

	[Tooltip("增加的数量")]
	[SerializeField]
	public int addNum;

	public override void OnEnter()
	{
		int i = jsonData.instance.AvatarJsonData[npcId.Value.ToString()][fieldName].I;
		jsonData.instance.AvatarJsonData[npcId.Value.ToString()].SetField(fieldName, i + addNum);
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
