using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetHaoGanDuSay", "进行请教功法对话，只能用在最后，无法在链接回该talk", 0)]
[AddComponentMenu("")]
public class GetHaoGanDuSay : Command
{
	public static Dictionary<string, JObject> JsonData = new Dictionary<string, JObject>();

	[Tooltip("NPC的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable StaticValueID;

	public override void OnEnter()
	{
		((ThreeSceneMag)(object)Object.FindObjectOfType(typeof(ThreeSceneMag))).qingJiaoGongFanom(StaticValueID.Value);
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
