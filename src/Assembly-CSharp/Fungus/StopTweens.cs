using UnityEngine;

namespace Fungus;

[CommandInfo("iTween", "Stop Tweens", "Stop all active iTweens in the current scene.", 0)]
[AddComponentMenu("")]
public class StopTweens : Command
{
	public override void OnEnter()
	{
		iTween.Stop();
		Continue();
	}
}
