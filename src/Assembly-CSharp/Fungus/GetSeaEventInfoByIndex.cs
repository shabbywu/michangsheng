using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetSeaEventInfoByIndex", "通过下标获取周围海域的事件信息", 0)]
[AddComponentMenu("")]
public class GetSeaEventInfoByIndex : Command
{
	[Tooltip("下标")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable index;

	[Tooltip("返回的事件ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable EventID;

	[Tooltip("返回的名称")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable EventName;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Tools.instance.getPlayer();
		List<SeaAvatarObjBase> roundEventList = EndlessSeaMag.Inst.RoundEventList;
		if (roundEventList.Count > index.Value)
		{
			if ((Object)(object)EventID != (Object)null)
			{
				EventID.Value = roundEventList[index.Value]._EventId;
			}
			if ((Object)(object)EventName != (Object)null)
			{
				EventName.Value = (string)roundEventList[index.Value].Json["EventName"];
			}
		}
		else
		{
			Debug.LogError((object)"获取海域周围事件时下标越界：GetSeaEventInfoByIndex");
		}
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
