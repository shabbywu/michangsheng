using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "离开随机副本", "离开随机副本，必须在随机副本调用", 0)]
[AddComponentMenu("")]
public class CmdOutRandomFuben : Command
{
	public override void OnEnter()
	{
		MapRandomCompent.ShowOutRandomFubenTalk();
		Continue();
	}
}
