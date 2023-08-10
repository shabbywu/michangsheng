using UnityEngine;

namespace Fungus;

[CommandInfo("", "Comment", "Use comments to record design notes and reminders about your game.", 0)]
[AddComponentMenu("")]
public class Comment : Command
{
	[Tooltip("Name of Commenter")]
	[SerializeField]
	protected string commenterName = "";

	[Tooltip("Text to display for this comment")]
	[TextArea(2, 4)]
	[SerializeField]
	protected string commentText = "";

	public override void OnEnter()
	{
		Continue();
	}

	public override string GetSummary()
	{
		if (commenterName != "")
		{
			return commenterName + ": " + commentText;
		}
		return commentText;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)220, (byte)220, (byte)220, byte.MaxValue));
	}
}
