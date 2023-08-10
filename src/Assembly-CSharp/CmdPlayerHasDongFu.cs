using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "玩家是否拥有某洞府", "玩家是否拥有某洞府，赋值到TmpValue，0没有，1有，赋值洞府名字到TmpString", 0)]
[AddComponentMenu("")]
public class CmdPlayerHasDongFu : Command
{
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;

	public override void OnEnter()
	{
		int value = 0;
		if (DongFuManager.PlayerHasDongFu(dongFuID))
		{
			value = 1;
		}
		Flowchart flowchart = GetFlowchart();
		flowchart.SetIntegerVariable("TmpValue", value);
		flowchart.SetStringVariable("TmpString", DongFuManager.GetDongFuName(dongFuID));
		Continue();
	}
}
