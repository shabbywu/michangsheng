using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E0C RID: 3596
	[CommandInfo("Animation", "Play Anim State", "Plays a state of an animator according to the state name", 0)]
	[AddComponentMenu("")]
	public class PlayAnimState : Command
	{
		// Token: 0x06006588 RID: 25992 RVA: 0x00283610 File Offset: 0x00281810
		public override void OnEnter()
		{
			if (this.animator.Value != null)
			{
				this.animator.Value.Play(this.stateName.Value, this.layer.Value, this.time.Value);
			}
			this.Continue();
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x00283668 File Offset: 0x00281868
		public override string GetSummary()
		{
			if (this.animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this.animator.Value.name + " (" + this.stateName.Value + ")";
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x002836B8 File Offset: 0x002818B8
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x002836D8 File Offset: 0x002818D8
		public override bool HasReference(Variable variable)
		{
			return this.animator.animatorRef == variable || this.stateName.stringRef == variable || this.layer.integerRef == variable || this.time.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x04005734 RID: 22324
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData animator;

		// Token: 0x04005735 RID: 22325
		[Tooltip("Name of the state you want to play")]
		[SerializeField]
		protected StringData stateName;

		// Token: 0x04005736 RID: 22326
		[Tooltip("Layer to play animation on")]
		[SerializeField]
		protected IntegerData layer = new IntegerData(-1);

		// Token: 0x04005737 RID: 22327
		[Tooltip("Start time of animation")]
		[SerializeField]
		protected FloatData time = new FloatData(0f);
	}
}
