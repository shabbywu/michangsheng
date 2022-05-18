using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001280 RID: 4736
	[CommandInfo("Animation", "Set Anim Bool", "Sets a boolean parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimBool : Command
	{
		// Token: 0x060072D7 RID: 29399 RVA: 0x002A954C File Offset: 0x002A774C
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetBool(this._parameterName.Value, this.value.Value);
			}
			this.Continue();
		}

		// Token: 0x060072D8 RID: 29400 RVA: 0x002A9598 File Offset: 0x002A7798
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x0004DA1A File Offset: 0x0004BC1A
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x002A95E8 File Offset: 0x002A77E8
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || this.value.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x002A9638 File Offset: 0x002A7838
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

		// Token: 0x040064F8 RID: 25848
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x040064F9 RID: 25849
		[Tooltip("Name of the boolean Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040064FA RID: 25850
		[Tooltip("The boolean value to set the parameter to")]
		[SerializeField]
		protected BooleanData value;

		// Token: 0x040064FB RID: 25851
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x040064FC RID: 25852
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
