using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "打开捏脸界面", "打开捏脸界面", 0)]
[AddComponentMenu("")]
public class OpenSetFace : Command, INoCommand
{
	public override void OnEnter()
	{
		SetFaceUI.Open();
		Continue();
	}
}
