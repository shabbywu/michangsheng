using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("YSTools", "OutFuBen", "\ufffd뿪\ufffd\ufffd\ufffd\ufffd", 0)]
[AddComponentMenu("")]
public class OutFuBen : Command
{
	[Tooltip("\ufffd\ufffd\ufffd\ufffd\ufffdĳ\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd")]
	[SerializeField]
	protected StringData _sceneName = new StringData("");

	[HideInInspector]
	[FormerlySerializedAs("sceneName")]
	public string sceneNameOLD = "";

	public override void OnEnter()
	{
		Tools.instance.loadMapScenes(_sceneName.Value);
		Tools.instance.getPlayer().fubenContorl.outFuBen(ToLast: false);
		Continue();
	}

	public override string GetSummary()
	{
		if (_sceneName.Value.Length == 0)
		{
			return "Error: No scene name selected";
		}
		return _sceneName.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_sceneName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (sceneNameOLD != "")
		{
			_sceneName.Value = sceneNameOLD;
			sceneNameOLD = "";
		}
	}
}
