using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001202 RID: 4610
	[CommandInfo("Scripting", "Destroy", "Destroys a specified game object in the scene.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Destroy : Command
	{
		// Token: 0x060070D1 RID: 28881 RVA: 0x002A3438 File Offset: 0x002A1638
		public override void OnEnter()
		{
			if (this._targetGameObject.Value != null)
			{
				if (this.destroyInXSeconds.Value != 0f)
				{
					Object.Destroy(this._targetGameObject, this.destroyInXSeconds.Value);
				}
				else
				{
					Object.Destroy(this._targetGameObject.Value);
				}
			}
			this.Continue();
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x002A34A0 File Offset: 0x002A16A0
		public override string GetSummary()
		{
			if (this._targetGameObject.Value == null)
			{
				return "Error: No game object selected";
			}
			return this._targetGameObject.Value.name + ((this.destroyInXSeconds.Value == 0f) ? "" : (" in " + this.destroyInXSeconds.Value.ToString()));
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x0004C9A1 File Offset: 0x0004ABA1
		public override bool HasReference(Variable variable)
		{
			return this._targetGameObject.gameObjectRef == variable || this.destroyInXSeconds.floatRef == variable;
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x0004C9CC File Offset: 0x0004ABCC
		protected virtual void OnEnable()
		{
			if (this.targetGameObjectOLD != null)
			{
				this._targetGameObject.Value = this.targetGameObjectOLD;
				this.targetGameObjectOLD = null;
			}
		}

		// Token: 0x04006340 RID: 25408
		[Tooltip("Reference to game object to destroy")]
		[SerializeField]
		protected GameObjectData _targetGameObject;

		// Token: 0x04006341 RID: 25409
		[Tooltip("Optional delay given to destroy")]
		[SerializeField]
		protected FloatData destroyInXSeconds = new FloatData(0f);

		// Token: 0x04006342 RID: 25410
		[HideInInspector]
		[FormerlySerializedAs("targetGameObject")]
		public GameObject targetGameObjectOLD;
	}
}
