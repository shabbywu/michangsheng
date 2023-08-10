using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Scripting", "Spawn Object", "Spawns a new object based on a reference to a scene or prefab game object.", 0, Priority = 10)]
[CommandInfo("GameObject", "Instantiate", "Instantiate a game object", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class SpawnObject : Command
{
	[Tooltip("Game object to copy when spawning. Can be a scene object or a prefab.")]
	[SerializeField]
	protected GameObjectData _sourceObject;

	[Tooltip("Transform to use as parent during instantiate.")]
	[SerializeField]
	protected TransformData _parentTransform;

	[Tooltip("If true, will use the Transfrom of this Flowchart for the position and rotation.")]
	[SerializeField]
	protected BooleanData _spawnAtSelf = new BooleanData(v: false);

	[Tooltip("Local position of newly spawned object.")]
	[SerializeField]
	protected Vector3Data _spawnPosition;

	[Tooltip("Local rotation of newly spawned object.")]
	[SerializeField]
	protected Vector3Data _spawnRotation;

	[Tooltip("Optional variable to store the GameObject that was just created.")]
	[SerializeField]
	protected GameObjectData _newlySpawnedObject;

	[HideInInspector]
	[FormerlySerializedAs("sourceObject")]
	public GameObject sourceObjectOLD;

	[HideInInspector]
	[FormerlySerializedAs("parentTransform")]
	public Transform parentTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("spawnPosition")]
	public Vector3 spawnPositionOLD;

	[HideInInspector]
	[FormerlySerializedAs("spawnRotation")]
	public Vector3 spawnRotationOLD;

	public override void OnEnter()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_sourceObject.Value == (Object)null)
		{
			Continue();
			return;
		}
		GameObject val = null;
		val = ((!((Object)(object)_parentTransform.Value != (Object)null)) ? Object.Instantiate<GameObject>(_sourceObject.Value) : Object.Instantiate<GameObject>(_sourceObject.Value, _parentTransform.Value));
		if (!_spawnAtSelf.Value)
		{
			val.transform.localPosition = _spawnPosition.Value;
			val.transform.localRotation = Quaternion.Euler(_spawnRotation.Value);
		}
		else
		{
			val.transform.SetPositionAndRotation(((Component)this).transform.position, ((Component)this).transform.rotation);
		}
		_newlySpawnedObject.Value = val;
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)_sourceObject.Value == (Object)null)
		{
			return "Error: No source GameObject specified";
		}
		return ((Object)_sourceObject.Value).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)_sourceObject.gameObjectRef == (Object)(object)variable || (Object)(object)_parentTransform.transformRef == (Object)(object)variable || (Object)(object)_spawnAtSelf.booleanRef == (Object)(object)variable || (Object)(object)_spawnPosition.vector3Ref == (Object)(object)variable || (Object)(object)_spawnRotation.vector3Ref == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}

	protected virtual void OnEnable()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)sourceObjectOLD != (Object)null)
		{
			_sourceObject.Value = sourceObjectOLD;
			sourceObjectOLD = null;
		}
		if ((Object)(object)parentTransformOLD != (Object)null)
		{
			_parentTransform.Value = parentTransformOLD;
			parentTransformOLD = null;
		}
		if (spawnPositionOLD != default(Vector3))
		{
			_spawnPosition.Value = spawnPositionOLD;
			spawnPositionOLD = default(Vector3);
		}
		if (spawnRotationOLD != default(Vector3))
		{
			_spawnRotation.Value = spawnRotationOLD;
			spawnRotationOLD = default(Vector3);
		}
	}
}
