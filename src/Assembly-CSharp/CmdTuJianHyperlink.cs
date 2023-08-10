using Fungus;
using UnityEngine;
using YSGame.TuJian;

[CommandInfo("YSTools", "触发图鉴超链接", "触发图鉴超链接", 0)]
[AddComponentMenu("")]
public class CmdTuJianHyperlink : Command
{
	[Tooltip("超链接")]
	[SerializeField]
	protected string Hyperlink;

	public override void OnEnter()
	{
		TuJianManager.Inst.OnHyperlink(Hyperlink);
		Continue();
	}
}
