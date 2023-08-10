using Fungus;
using UnityEngine;

[CommandInfo("YSTutorial", "设置焦点", "设置焦点", 0)]
[AddComponentMenu("")]
public class CmdSetPlayTutorialHand : Command
{
	[SerializeField]
	[Tooltip("是否显示")]
	protected bool Show;

	public override void OnEnter()
	{
		if ((Object)(object)PlayTutorialCircle.Inst != (Object)null)
		{
			PlayTutorialCircle.Inst.SetShow(Show);
		}
		Continue();
	}
}
