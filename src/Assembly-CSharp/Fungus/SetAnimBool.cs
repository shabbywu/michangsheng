using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E2F RID: 3631
	[CommandInfo("Animation", "Set Anim Bool", "Sets a boolean parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimBool : Command
	{
		// Token: 0x06006649 RID: 26185 RVA: 0x00285E34 File Offset: 0x00284034
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetBool(this._parameterName.Value, this.value.Value);
			}
			this.Continue();
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x00285E80 File Offset: 0x00284080
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x002836B8 File Offset: 0x002818B8
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x00285ED0 File Offset: 0x002840D0
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || this.value.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x00285F20 File Offset: 0x00284120
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

		// Token: 0x040057B4 RID: 22452
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x040057B5 RID: 22453
		[Tooltip("Name of the boolean Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040057B6 RID: 22454
		[Tooltip("The boolean value to set the parameter to")]
		[SerializeField]
		protected BooleanData value;

		// Token: 0x040057B7 RID: 22455
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x040057B8 RID: 22456
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
