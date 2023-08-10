using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "给与玩家洞府", "给与玩家洞府", 0)]
[AddComponentMenu("")]
public class CmdCreateDongFu : Command
{
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int DongFuID;

	[Tooltip("灵眼等级 限定123")]
	[SerializeField]
	protected int level;

	public override void OnEnter()
	{
		DongFuManager.CreateDongFu(DongFuID, level);
		Continue();
	}
}
