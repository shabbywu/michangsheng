using UnityEngine;

namespace Fungus;

[CommandInfo("Scripting", "Debug Log", "Writes a log message to the debug console.", 0)]
[AddComponentMenu("")]
public class DebugLog : Command
{
	[Tooltip("Display type of debug log info")]
	[SerializeField]
	protected DebugLogType logType;

	[Tooltip("Text to write to the debug log. Supports variable substitution, e.g. {$Myvar}")]
	[SerializeField]
	protected StringDataMulti logMessage;

	public override void OnEnter()
	{
		string text = GetFlowchart().SubstituteVariables(logMessage.Value);
		switch (logType)
		{
		case DebugLogType.Info:
			Debug.Log((object)text);
			break;
		case DebugLogType.Warning:
			Debug.LogWarning((object)text);
			break;
		case DebugLogType.Error:
			Debug.LogError((object)text);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return logMessage.GetDescription();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)logMessage.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
