using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E31 RID: 3633
	[CommandInfo("Animation", "Set Anim Integer", "Sets an integer parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimInteger : Command
	{
		// Token: 0x06006655 RID: 26197 RVA: 0x002860F4 File Offset: 0x002842F4
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetInteger(this._parameterName.Value, this.value.Value);
			}
			this.Continue();
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x00286140 File Offset: 0x00284340
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x002836B8 File Offset: 0x002818B8
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x00286190 File Offset: 0x00284390
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || this.value.integerRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x002861E0 File Offset: 0x002843E0
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

		// Token: 0x040057BE RID: 22462
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x040057BF RID: 22463
		[Tooltip("Name of the integer Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040057C0 RID: 22464
		[Tooltip("The integer value to set the parameter to")]
		[SerializeField]
		protected IntegerData value;

		// Token: 0x040057C1 RID: 22465
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x040057C2 RID: 22466
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
