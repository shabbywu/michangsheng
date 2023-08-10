using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Scripting", "Destroy", "Destroys a specified game object in the scene.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class Destroy : Command
{
	[Tooltip("Reference to game object to destroy")]
	[SerializeField]
	protected GameObjectData _targetGameObject;

	[Tooltip("Optional delay given to destroy")]
	[SerializeField]
	protected FloatData destroyInXSeconds = new FloatData(0f);

	[HideInInspector]
	[FormerlySerializedAs("targetGameObject")]
	public GameObject targetGameObjectOLD;

	public override void OnEnter()
	{
		if ((Object)(object)_targetGameObject.Value != (Object)null)
		{
			if (destroyInXSeconds.Value != 0f)
			{
				Object.Destroy((Object)(object)(GameObject)_targetGameObject, destroyInXSeconds.Value);
			}
			else
			{
				Object.Destroy((Object)(object)_targetGameObject.Value);
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)_targetGameObject.Value == (Object)null)
		{
			return "Error: No game object selected";
		}
		return ((Object)_targetGameObject.Value).name + ((destroyInXSeconds.Value == 0f) ? "" : (" in " + destroyInXSeconds.Value));
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)_targetGameObject.gameObjectRef == (Object)(object)variable || (Object)(object)destroyInXSeconds.floatRef == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)targetGameObjectOLD != (Object)null)
		{
			_targetGameObject.Value = targetGameObjectOLD;
			targetGameObjectOLD = null;
		}
	}
}
