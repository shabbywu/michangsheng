using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DC8 RID: 3528
	[CommandInfo("Scripting", "Destroy", "Destroys a specified game object in the scene.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Destroy : Command
	{
		// Token: 0x06006452 RID: 25682 RVA: 0x0027E3F0 File Offset: 0x0027C5F0
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

		// Token: 0x06006453 RID: 25683 RVA: 0x0027E458 File Offset: 0x0027C658
		public override string GetSummary()
		{
			if (this._targetGameObject.Value == null)
			{
				return "Error: No game object selected";
			}
			return this._targetGameObject.Value.name + ((this.destroyInXSeconds.Value == 0f) ? "" : (" in " + this.destroyInXSeconds.Value.ToString()));
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x0027E4C9 File Offset: 0x0027C6C9
		public override bool HasReference(Variable variable)
		{
			return this._targetGameObject.gameObjectRef == variable || this.destroyInXSeconds.floatRef == variable;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x0027E4F4 File Offset: 0x0027C6F4
		protected virtual void OnEnable()
		{
			if (this.targetGameObjectOLD != null)
			{
				this._targetGameObject.Value = this.targetGameObjectOLD;
				this.targetGameObjectOLD = null;
			}
		}

		// Token: 0x04005641 RID: 22081
		[Tooltip("Reference to game object to destroy")]
		[SerializeField]
		protected GameObjectData _targetGameObject;

		// Token: 0x04005642 RID: 22082
		[Tooltip("Optional delay given to destroy")]
		[SerializeField]
		protected FloatData destroyInXSeconds = new FloatData(0f);

		// Token: 0x04005643 RID: 22083
		[HideInInspector]
		[FormerlySerializedAs("targetGameObject")]
		public GameObject targetGameObjectOLD;
	}
}
