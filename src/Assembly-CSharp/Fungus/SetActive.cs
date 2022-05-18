using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200127F RID: 4735
	[CommandInfo("Scripting", "Set Active", "Sets a game object in the scene to be active / inactive.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetActive : Command
	{
		// Token: 0x060072D1 RID: 29393 RVA: 0x0004E327 File Offset: 0x0004C527
		public override void OnEnter()
		{
			if (this._targetGameObject.Value != null)
			{
				this._targetGameObject.Value.SetActive(this.activeState.Value);
			}
			this.Continue();
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x0004E35D File Offset: 0x0004C55D
		public override string GetSummary()
		{
			if (this._targetGameObject.Value == null)
			{
				return "Error: No game object selected";
			}
			return this._targetGameObject.Value.name + " = " + this.activeState.GetDescription();
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060072D4 RID: 29396 RVA: 0x0004E39D File Offset: 0x0004C59D
		public override bool HasReference(Variable variable)
		{
			return this._targetGameObject.gameObjectRef == variable || this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x0004E3CE File Offset: 0x0004C5CE
		protected virtual void OnEnable()
		{
			if (this.targetGameObjectOLD != null)
			{
				this._targetGameObject.Value = this.targetGameObjectOLD;
				this.targetGameObjectOLD = null;
			}
		}

		// Token: 0x040064F5 RID: 25845
		[Tooltip("Reference to game object to enable / disable")]
		[SerializeField]
		protected GameObjectData _targetGameObject;

		// Token: 0x040064F6 RID: 25846
		[Tooltip("Set to true to enable the game object")]
		[SerializeField]
		protected BooleanData activeState;

		// Token: 0x040064F7 RID: 25847
		[HideInInspector]
		[FormerlySerializedAs("targetGameObject")]
		public GameObject targetGameObjectOLD;
	}
}
