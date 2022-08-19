using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E1E RID: 3614
	[CommandInfo("Animation", "Reset Anim Trigger", "Resets a trigger parameter on an Animator component.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ResetAnimTrigger : Command
	{
		// Token: 0x060065E8 RID: 26088 RVA: 0x00284598 File Offset: 0x00282798
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.ResetTrigger(this._parameterName.Value);
			}
			this.Continue();
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x002845D0 File Offset: 0x002827D0
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x002836B8 File Offset: 0x002818B8
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x00284620 File Offset: 0x00282820
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x00284654 File Offset: 0x00282854
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

		// Token: 0x04005769 RID: 22377
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x0400576A RID: 22378
		[Tooltip("Name of the trigger Animator parameter that will be reset")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x0400576B RID: 22379
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x0400576C RID: 22380
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
