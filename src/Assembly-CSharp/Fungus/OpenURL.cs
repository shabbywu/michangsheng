using UnityEngine;

namespace Fungus;

[CommandInfo("Scripting", "Open URL", "Opens the specified URL in the browser.", 0)]
public class OpenURL : Command
{
	[Tooltip("URL to open in the browser")]
	[SerializeField]
	protected StringData url;

	public override void OnEnter()
	{
		Application.OpenURL(url.Value);
		Continue();
	}

	public override string GetSummary()
	{
		return url.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)url.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
