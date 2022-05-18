using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001259 RID: 4697
	[CommandInfo("Animation", "Play Anim State", "Plays a state of an animator according to the state name", 0)]
	[AddComponentMenu("")]
	public class PlayAnimState : Command
	{
		// Token: 0x06007216 RID: 29206 RVA: 0x002A770C File Offset: 0x002A590C
		public override void OnEnter()
		{
			if (this.animator.Value != null)
			{
				this.animator.Value.Play(this.stateName.Value, this.layer.Value, this.time.Value);
			}
			this.Continue();
		}

		// Token: 0x06007217 RID: 29207 RVA: 0x002A7764 File Offset: 0x002A5964
		public override string GetSummary()
		{
			if (this.animator.Value == null)
			{
				return "Error: No animator selected";
			}
			return this.animator.Value.name + " (" + this.stateName.Value + ")";
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x0004DA1A File Offset: 0x0004BC1A
		public override Color GetButtonColor()
		{
			return new Color32(170, 204, 169, byte.MaxValue);
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x002A77B4 File Offset: 0x002A59B4
		public override bool HasReference(Variable variable)
		{
			return this.animator.animatorRef == variable || this.stateName.stringRef == variable || this.layer.integerRef == variable || this.time.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006469 RID: 25705
		[Tooltip("Reference to an Animator component in a game object")]
		[SerializeField]
		protected AnimatorData animator;

		// Token: 0x0400646A RID: 25706
		[Tooltip("Name of the state you want to play")]
		[SerializeField]
		protected StringData stateName;

		// Token: 0x0400646B RID: 25707
		[Tooltip("Layer to play animation on")]
		[SerializeField]
		protected IntegerData layer = new IntegerData(-1);

		// Token: 0x0400646C RID: 25708
		[Tooltip("Start time of animation")]
		[SerializeField]
		protected FloatData time = new FloatData(0f);
	}
}
