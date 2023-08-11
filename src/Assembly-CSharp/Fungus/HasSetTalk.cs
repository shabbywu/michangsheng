using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "是否有SetTalk", "是否有SetTalk", 0)]
[AddComponentMenu("")]
public class HasSetTalk : Command
{
	[Tooltip("结果")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable result;

	public override void OnEnter()
	{
		if (GlobalValue.GetTalk(0, "HasSetTalk") > 0)
		{
			result.Value = true;
		}
		else
		{
			result.Value = false;
		}
		Continue();
	}
}
