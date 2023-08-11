using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "天劫加速", "天劫加速", 0)]
[AddComponentMenu("")]
public class CmdTianJieJiaSu : Command
{
	[SerializeField]
	[Tooltip("年数")]
	protected int year;

	public override void OnEnter()
	{
		TianJieManager.TianJieJiaSu(year);
		Continue();
	}
}
