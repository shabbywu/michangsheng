using UnityEngine;
using script.Sleep;

namespace Fungus;

[CommandInfo("YSTools", "打开休息界面", "打开休息界面", 0)]
[AddComponentMenu("")]
public class OpenSleep : Command, INoCommand
{
	public override void OnEnter()
	{
		ISleepMag.Inst.Sleep();
		Continue();
	}
}
