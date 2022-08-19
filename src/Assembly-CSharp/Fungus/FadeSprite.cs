using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DCE RID: 3534
	[CommandInfo("Sprite", "Fade Sprite", "Fades a sprite to a target color over a period of time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class FadeSprite : Command
	{
		// Token: 0x06006477 RID: 25719 RVA: 0x0027EC6C File Offset: 0x0027CE6C
		public override void OnEnter()
		{
			if (this.spriteRenderer == null)
			{
				this.Continue();
				return;
			}
			SpriteFader.FadeSprite(this.spriteRenderer, this._targetColor.Value, this._duration.Value, Vector2.zero, delegate
			{
				if (this.waitUntilFinished)
				{
					this.Continue();
				}
			});
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x0027ECD0 File Offset: 0x0027CED0
		public override string GetSummary()
		{
			if (this.spriteRenderer == null)
			{
				return "Error: No sprite renderer selected";
			}
			return this.spriteRenderer.name + " to " + this._targetColor.Value.ToString();
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x0027ED1F File Offset: 0x0027CF1F
		public override Color GetButtonColor()
		{
			return new Color32(221, 184, 169, byte.MaxValue);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x0027ED3F File Offset: 0x0027CF3F
		public override bool HasReference(Variable variable)
		{
			return this._duration.floatRef == variable || this._targetColor.colorRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x0027ED70 File Offset: 0x0027CF70
		protected virtual void OnEnable()
		{
			if (this.durationOLD != 0f)
			{
				this._duration.Value = this.durationOLD;
				this.durationOLD = 0f;
			}
			if (this.targetColorOLD != default(Color))
			{
				this._targetColor.Value = this.targetColorOLD;
				this.targetColorOLD = default(Color);
			}
		}

		// Token: 0x04005653 RID: 22099
		[Tooltip("Sprite object to be faded")]
		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		// Token: 0x04005654 RID: 22100
		[Tooltip("Length of time to perform the fade")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x04005655 RID: 22101
		[Tooltip("Target color to fade to. To only fade transparency level, set the color to white and set the alpha to required transparency.")]
		[SerializeField]
		protected ColorData _targetColor = new ColorData(Color.white);

		// Token: 0x04005656 RID: 22102
		[Tooltip("Wait until the fade has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04005657 RID: 22103
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;

		// Token: 0x04005658 RID: 22104
		[HideInInspector]
		[FormerlySerializedAs("targetColor")]
		public Color targetColorOLD;
	}
}
