using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetTalk", "设置后续对话", 0)]
[AddComponentMenu("")]
public class SetSTalk : Command
{
	[Tooltip("后续对话的ID")]
	[SerializeField]
	protected int TalkID;

	public override void OnEnter()
	{
		GlobalValue.SetTalk(0, TalkID, GetCommandSourceDesc());
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
