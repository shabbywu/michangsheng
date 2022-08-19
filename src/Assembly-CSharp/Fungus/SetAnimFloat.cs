using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E30 RID: 3632
	[CommandInfo("Animation", "Set Anim Float", "Sets a float parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimFloat : Command
	{
		// Token: 0x0600664F RID: 26191 RVA: 0x00285F94 File Offset: 0x00284194
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetFloat(this._parameterName.Value, this.value.Value);
			}
			this.Continue();
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x00285FE0 File Offset: 0x002841E0
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x002836B8 File Offset: 0x002818B8
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x00286030 File Offset: 0x00284230
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || this.value.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x00286080 File Offset: 0x00284280
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

		// Token: 0x040057B9 RID: 22457
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x040057BA RID: 22458
		[Tooltip("Name of the float Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040057BB RID: 22459
		[Tooltip("The float value to set the parameter to")]
		[SerializeField]
		protected FloatData value;

		// Token: 0x040057BC RID: 22460
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x040057BD RID: 22461
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
