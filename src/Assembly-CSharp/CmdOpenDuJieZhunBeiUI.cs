using Fungus;
using UnityEngine;

[CommandInfo("渡劫", "打开渡劫准备UI", "打开渡劫准备UI", 0)]
[AddComponentMenu("")]
public class CmdOpenDuJieZhunBeiUI : Command
{
	public override void OnEnter()
	{
		UIDuJieZhunBei.OpenPanel(isDuJie: false);
		Continue();
	}
}
