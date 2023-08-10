using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "进入洞府", "进入洞府", 0)]
[AddComponentMenu("")]
public class CmdLoadDongFu : Command
{
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;

	public override void OnEnter()
	{
		DongFuManager.LoadDongFuScene(dongFuID);
		Continue();
	}
}
