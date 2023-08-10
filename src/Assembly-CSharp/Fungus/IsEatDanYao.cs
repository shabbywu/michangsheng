using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "是否吃过丹药", "是否吃过丹药", 0)]
[AddComponentMenu("")]
public class IsEatDanYao : Command
{
	[Tooltip("丹药Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable DanYaoValue;

	[Tooltip("丹药Id")]
	[SerializeField]
	protected int DanYao;

	[Tooltip("结果")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable result;

	public override void OnEnter()
	{
		int num = 0;
		num = ((DanYao != 0) ? DanYao : DanYaoValue.Value);
		if (num == 0)
		{
			Debug.LogError((object)"物品Id不能为空");
			result.Value = false;
			Continue();
		}
		else
		{
			int jsonobject = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(num));
			result.Value = jsonobject > 0;
			Continue();
		}
	}
}
