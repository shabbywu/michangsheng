using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Narrative", "Set Language", "Set the active language for the scene. A Localization object with a localization file must be present in the scene.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class SetLanguage : Command
{
	[Tooltip("Code of the language to set. e.g. ES, DE, JA")]
	[SerializeField]
	protected StringData _languageCode;

	public static string mostRecentLanguage = "";

	[HideInInspector]
	[FormerlySerializedAs("languageCode")]
	public string languageCodeOLD = "";

	public override void OnEnter()
	{
		Localization localization = Object.FindObjectOfType<Localization>();
		if ((Object)(object)localization != (Object)null)
		{
			localization.SetActiveLanguage(_languageCode.Value, forceUpdateSceneText: true);
			mostRecentLanguage = _languageCode.Value;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return _languageCode.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_languageCode.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (languageCodeOLD != "")
		{
			_languageCode.Value = languageCodeOLD;
			languageCodeOLD = "";
		}
	}
}
