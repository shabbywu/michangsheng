using Fungus;
using UnityEngine;

[CommandInfo("YSTools", "存储点", "存储点", 0)]
[AddComponentMenu("")]
public class SavePoint : Command
{
	public override void OnEnter()
	{
		Continue();
	}
}
