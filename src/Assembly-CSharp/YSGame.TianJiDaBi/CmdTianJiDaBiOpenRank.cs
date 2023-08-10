using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "打开天机榜", "打开天机榜(关闭UI时才会Continue)", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiOpenRank : Command
{
	public override void OnEnter()
	{
		UITianJiDaBiRankPanel.Show(this);
	}
}
