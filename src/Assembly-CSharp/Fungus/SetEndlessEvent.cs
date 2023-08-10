using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "设置无尽之海事件可点击", "设置无尽之海事件可点击", 0)]
[AddComponentMenu("")]
public class SetEndlessEvent : Command
{
	public override void OnEnter()
	{
		EndlessSeaMag.Inst.NeedRefresh = true;
		Continue();
	}
}
