using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E2E RID: 3630
	[CommandInfo("Scripting", "Set Active", "Sets a game object in the scene to be active / inactive.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetActive : Command
	{
		// Token: 0x06006643 RID: 26179 RVA: 0x00285D63 File Offset: 0x00283F63
		public override void OnEnter()
		{
			if (this._targetGameObject.Value != null)
			{
				this._targetGameObject.Value.SetActive(this.activeState.Value);
			}
			this.Continue();
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x00285D99 File Offset: 0x00283F99
		public override string GetSummary()
		{
			if (this._targetGameObject.Value == null)
			{
				return "Error: No game object selected";
			}
			return this._targetGameObject.Value.name + " = " + this.activeState.GetDescription();
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x00285DD9 File Offset: 0x00283FD9
		public override bool HasReference(Variable variable)
		{
			return this._targetGameObject.gameObjectRef == variable || this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x00285E0A File Offset: 0x0028400A
		protected virtual void OnEnable()
		{
			if (this.targetGameObjectOLD != null)
			{
				this._targetGameObject.Value = this.targetGameObjectOLD;
				this.targetGameObjectOLD = null;
			}
		}

		// Token: 0x040057B1 RID: 22449
		[Tooltip("Reference to game object to enable / disable")]
		[SerializeField]
		protected GameObjectData _targetGameObject;

		// Token: 0x040057B2 RID: 22450
		[Tooltip("Set to true to enable the game object")]
		[SerializeField]
		protected BooleanData activeState;

		// Token: 0x040057B3 RID: 22451
		[HideInInspector]
		[FormerlySerializedAs("targetGameObject")]
		public GameObject targetGameObjectOLD;
	}
}
