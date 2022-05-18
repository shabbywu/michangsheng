using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200126B RID: 4715
	[CommandInfo("Animation", "Reset Anim Trigger", "Resets a trigger parameter on an Animator component.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ResetAnimTrigger : Command
	{
		// Token: 0x06007276 RID: 29302 RVA: 0x0004DEF9 File Offset: 0x0004C0F9
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.ResetTrigger(this._parameterName.Value);
			}
			this.Continue();
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x002A81B8 File Offset: 0x002A63B8
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x0004DA1A File Offset: 0x0004BC1A
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x06007279 RID: 29305 RVA: 0x0004DF2F File Offset: 0x0004C12F
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x002A8208 File Offset: 0x002A6408
		protected virtual void OnEnable()
		{
			if (this.animatorOLD != null)
			{
				this._animator.Value = this.animatorOLD;
				this.animatorOLD = null;
			}
			if (this.parameterNameOLD != "")
			{
				this._parameterName.Value = this.parameterNameOLD;
				this.parameterNameOLD = "";
			}
		}

		// Token: 0x0400649E RID: 25758
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x0400649F RID: 25759
		[Tooltip("Name of the trigger Animator parameter that will be reset")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040064A0 RID: 25760
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x040064A1 RID: 25761
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
