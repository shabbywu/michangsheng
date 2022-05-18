using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200129C RID: 4764
	[CommandInfo("Scripting", "Spawn Object", "Spawns a new object based on a reference to a scene or prefab game object.", 0, Priority = 10)]
	[CommandInfo("GameObject", "Instantiate", "Instantiate a game object", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SpawnObject : Command
	{
		// Token: 0x06007377 RID: 29559 RVA: 0x002AB160 File Offset: 0x002A9360
		public override void OnEnter()
		{
			if (this._sourceObject.Value == null)
			{
				this.Continue();
				return;
			}
			GameObject gameObject;
			if (this._parentTransform.Value != null)
			{
				gameObject = Object.Instantiate<GameObject>(this._sourceObject.Value, this._parentTransform.Value);
			}
			else
			{
				gameObject = Object.Instantiate<GameObject>(this._sourceObject.Value);
			}
			if (!this._spawnAtSelf.Value)
			{
				gameObject.transform.localPosition = this._spawnPosition.Value;
				gameObject.transform.localRotation = Quaternion.Euler(this._spawnRotation.Value);
			}
			else
			{
				gameObject.transform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
			}
			this._newlySpawnedObject.Value = gameObject;
			this.Continue();
		}

		// Token: 0x06007378 RID: 29560 RVA: 0x0004EC76 File Offset: 0x0004CE76
		public override string GetSummary()
		{
			if (this._sourceObject.Value == null)
			{
				return "Error: No source GameObject specified";
			}
			return this._sourceObject.Value.name;
		}

		// Token: 0x06007379 RID: 29561 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600737A RID: 29562 RVA: 0x002AB240 File Offset: 0x002A9440
		public override bool HasReference(Variable variable)
		{
			return this._sourceObject.gameObjectRef == variable || this._parentTransform.transformRef == variable || this._spawnAtSelf.booleanRef == variable || this._spawnPosition.vector3Ref == variable || this._spawnRotation.vector3Ref == variable;
		}

		// Token: 0x0600737B RID: 29563 RVA: 0x002AB2B0 File Offset: 0x002A94B0
		protected virtual void OnEnable()
		{
			if (this.sourceObjectOLD != null)
			{
				this._sourceObject.Value = this.sourceObjectOLD;
				this.sourceObjectOLD = null;
			}
			if (this.parentTransformOLD != null)
			{
				this._parentTransform.Value = this.parentTransformOLD;
				this.parentTransformOLD = null;
			}
			if (this.spawnPositionOLD != default(Vector3))
			{
				this._spawnPosition.Value = this.spawnPositionOLD;
				this.spawnPositionOLD = default(Vector3);
			}
			if (this.spawnRotationOLD != default(Vector3))
			{
				this._spawnRotation.Value = this.spawnRotationOLD;
				this.spawnRotationOLD = default(Vector3);
			}
		}

		// Token: 0x04006558 RID: 25944
		[Tooltip("Game object to copy when spawning. Can be a scene object or a prefab.")]
		[SerializeField]
		protected GameObjectData _sourceObject;

		// Token: 0x04006559 RID: 25945
		[Tooltip("Transform to use as parent during instantiate.")]
		[SerializeField]
		protected TransformData _parentTransform;

		// Token: 0x0400655A RID: 25946
		[Tooltip("If true, will use the Transfrom of this Flowchart for the position and rotation.")]
		[SerializeField]
		protected BooleanData _spawnAtSelf = new BooleanData(false);

		// Token: 0x0400655B RID: 25947
		[Tooltip("Local position of newly spawned object.")]
		[SerializeField]
		protected Vector3Data _spawnPosition;

		// Token: 0x0400655C RID: 25948
		[Tooltip("Local rotation of newly spawned object.")]
		[SerializeField]
		protected Vector3Data _spawnRotation;

		// Token: 0x0400655D RID: 25949
		[Tooltip("Optional variable to store the GameObject that was just created.")]
		[SerializeField]
		protected GameObjectData _newlySpawnedObject;

		// Token: 0x0400655E RID: 25950
		[HideInInspector]
		[FormerlySerializedAs("sourceObject")]
		public GameObject sourceObjectOLD;

		// Token: 0x0400655F RID: 25951
		[HideInInspector]
		[FormerlySerializedAs("parentTransform")]
		public Transform parentTransformOLD;

		// Token: 0x04006560 RID: 25952
		[HideInInspector]
		[FormerlySerializedAs("spawnPosition")]
		public Vector3 spawnPositionOLD;

		// Token: 0x04006561 RID: 25953
		[HideInInspector]
		[FormerlySerializedAs("spawnRotation")]
		public Vector3 spawnRotationOLD;
	}
}
