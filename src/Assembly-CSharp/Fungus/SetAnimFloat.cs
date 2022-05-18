using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001281 RID: 4737
	[CommandInfo("Animation", "Set Anim Float", "Sets a float parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimFloat : Command
	{
		// Token: 0x060072DD RID: 29405 RVA: 0x002A969C File Offset: 0x002A789C
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetFloat(this._parameterName.Value, this.value.Value);
			}
			this.Continue();
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x002A96E8 File Offset: 0x002A78E8
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x0004DA1A File Offset: 0x0004BC1A
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x002A9738 File Offset: 0x002A7938
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || this.value.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x002A9788 File Offset: 0x002A7988
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

		// Token: 0x040064FD RID: 25853
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x040064FE RID: 25854
		[Tooltip("Name of the float Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040064FF RID: 25855
		[Tooltip("The float value to set the parameter to")]
		[SerializeField]
		protected FloatData value;

		// Token: 0x04006500 RID: 25856
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x04006501 RID: 25857
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
