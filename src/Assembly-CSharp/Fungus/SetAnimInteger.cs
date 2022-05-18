using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001282 RID: 4738
	[CommandInfo("Animation", "Set Anim Integer", "Sets an integer parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimInteger : Command
	{
		// Token: 0x060072E3 RID: 29411 RVA: 0x002A97EC File Offset: 0x002A79EC
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetInteger(this._parameterName.Value, this.value.Value);
			}
			this.Continue();
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x002A9838 File Offset: 0x002A7A38
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x0004DA1A File Offset: 0x0004BC1A
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x002A9888 File Offset: 0x002A7A88
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || this.value.integerRef == variable || base.HasReference(variable);
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x002A98D8 File Offset: 0x002A7AD8
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

		// Token: 0x04006502 RID: 25858
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x04006503 RID: 25859
		[Tooltip("Name of the integer Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x04006504 RID: 25860
		[Tooltip("The integer value to set the parameter to")]
		[SerializeField]
		protected IntegerData value;

		// Token: 0x04006505 RID: 25861
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x04006506 RID: 25862
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
