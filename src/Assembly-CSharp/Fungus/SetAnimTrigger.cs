using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E32 RID: 3634
	[CommandInfo("Animation", "Set Anim Trigger", "Sets a trigger parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimTrigger : Command
	{
		// Token: 0x0600665B RID: 26203 RVA: 0x00286254 File Offset: 0x00284454
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetTrigger(this._parameterName.Value);
			}
			this.Continue();
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x0028628C File Offset: 0x0028448C
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x002836B8 File Offset: 0x002818B8
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x002862DC File Offset: 0x002844DC
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x00286310 File Offset: 0x00284510
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

		// Token: 0x040057C3 RID: 22467
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x040057C4 RID: 22468
		[Tooltip("Name of the trigger Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x040057C5 RID: 22469
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x040057C6 RID: 22470
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
