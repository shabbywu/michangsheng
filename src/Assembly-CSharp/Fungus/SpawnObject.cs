using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E4B RID: 3659
	[CommandInfo("Scripting", "Spawn Object", "Spawns a new object based on a reference to a scene or prefab game object.", 0, Priority = 10)]
	[CommandInfo("GameObject", "Instantiate", "Instantiate a game object", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SpawnObject : Command
	{
		// Token: 0x060066E9 RID: 26345 RVA: 0x002882D0 File Offset: 0x002864D0
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

		// Token: 0x060066EA RID: 26346 RVA: 0x002883AE File Offset: 0x002865AE
		public override string GetSummary()
		{
			if (this._sourceObject.Value == null)
			{
				return "Error: No source GameObject specified";
			}
			return this._sourceObject.Value.name;
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x002883DC File Offset: 0x002865DC
		public override bool HasReference(Variable variable)
		{
			return this._sourceObject.gameObjectRef == variable || this._parentTransform.transformRef == variable || this._spawnAtSelf.booleanRef == variable || this._spawnPosition.vector3Ref == variable || this._spawnRotation.vector3Ref == variable;
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x0028844C File Offset: 0x0028664C
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

		// Token: 0x04005814 RID: 22548
		[Tooltip("Game object to copy when spawning. Can be a scene object or a prefab.")]
		[SerializeField]
		protected GameObjectData _sourceObject;

		// Token: 0x04005815 RID: 22549
		[Tooltip("Transform to use as parent during instantiate.")]
		[SerializeField]
		protected TransformData _parentTransform;

		// Token: 0x04005816 RID: 22550
		[Tooltip("If true, will use the Transfrom of this Flowchart for the position and rotation.")]
		[SerializeField]
		protected BooleanData _spawnAtSelf = new BooleanData(false);

		// Token: 0x04005817 RID: 22551
		[Tooltip("Local position of newly spawned object.")]
		[SerializeField]
		protected Vector3Data _spawnPosition;

		// Token: 0x04005818 RID: 22552
		[Tooltip("Local rotation of newly spawned object.")]
		[SerializeField]
		protected Vector3Data _spawnRotation;

		// Token: 0x04005819 RID: 22553
		[Tooltip("Optional variable to store the GameObject that was just created.")]
		[SerializeField]
		protected GameObjectData _newlySpawnedObject;

		// Token: 0x0400581A RID: 22554
		[HideInInspector]
		[FormerlySerializedAs("sourceObject")]
		public GameObject sourceObjectOLD;

		// Token: 0x0400581B RID: 22555
		[HideInInspector]
		[FormerlySerializedAs("parentTransform")]
		public Transform parentTransformOLD;

		// Token: 0x0400581C RID: 22556
		[HideInInspector]
		[FormerlySerializedAs("spawnPosition")]
		public Vector3 spawnPositionOLD;

		// Token: 0x0400581D RID: 22557
		[HideInInspector]
		[FormerlySerializedAs("spawnRotation")]
		public Vector3 spawnRotationOLD;
	}
}
