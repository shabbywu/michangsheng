using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "增加化神时初始仙性", "增加化神时初始仙性", 0)]
[AddComponentMenu("")]
public class CmdAddHuaShenStartXianXing : Command
{
	[SerializeField]
	[Tooltip("仙性")]
	protected int xianXing;

	public override void OnEnter()
	{
		PlayerEx.AddHuaShenStartXianXing(xianXing);
		Continue();
	}
}
