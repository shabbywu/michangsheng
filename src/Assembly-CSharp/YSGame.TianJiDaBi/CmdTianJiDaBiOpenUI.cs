using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "打开对战UI", "打开对战UI", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiOpenUI : Command
{
	public override void OnEnter()
	{
		UITianJiDaBiSaiChang.ShowNormal();
		Continue();
	}
}
