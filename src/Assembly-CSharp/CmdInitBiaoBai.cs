using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "初始化表白", "初始化表白", 0)]
[AddComponentMenu("")]
public class CmdInitBiaoBai : Command
{
	public override void OnEnter()
	{
		BiaoBaiManager.InitBiaoBai();
		Continue();
	}
}
