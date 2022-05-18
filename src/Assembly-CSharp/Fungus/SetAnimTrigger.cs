using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001283 RID: 4739
	[CommandInfo("Animation", "Set Anim Trigger", "Sets a trigger parameter on an Animator component to control a Unity animation", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetAnimTrigger : Command
	{
		// Token: 0x060072E9 RID: 29417 RVA: 0x0004E42F File Offset: 0x0004C62F
		public override void OnEnter()
		{
			if (this._animator.Value != null)
			{
				this._animator.Value.SetTrigger(this._parameterName.Value);
			}
			this.Continue();
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x002A993C File Offset: 0x002A7B3C
		public override string GetSummary()
		{
			if (this._animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this._animator.Value.name + " (" + this._parameterName.Value + ")";
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x0004DA1A File Offset: 0x0004BC1A
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x0004E465 File Offset: 0x0004C665
		public override bool HasReference(Variable variable)
		{
			return this._animator.animatorRef == variable || this._parameterName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x002A998C File Offset: 0x002A7B8C
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

		// Token: 0x04006507 RID: 25863
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData _animator;

		// Token: 0x04006508 RID: 25864
		[Tooltip("Name of the trigger Animator parameter that will have its value changed")]
		[SerializeField]
		protected StringData _parameterName;

		// Token: 0x04006509 RID: 25865
		[HideInInspector]
		[FormerlySerializedAs("animator")]
		public Animator animatorOLD;

		// Token: 0x0400650A RID: 25866
		[HideInInspector]
		[FormerlySerializedAs("parameterName")]
		public string parameterNameOLD = "";
	}
}
