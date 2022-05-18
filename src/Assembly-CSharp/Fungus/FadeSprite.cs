using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001208 RID: 4616
	[CommandInfo("Sprite", "Fade Sprite", "Fades a sprite to a target color over a period of time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class FadeSprite : Command
	{
		// Token: 0x060070F6 RID: 28918 RVA: 0x002A3B3C File Offset: 0x002A1D3C
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

		// Token: 0x060070F7 RID: 28919 RVA: 0x002A3BA0 File Offset: 0x002A1DA0
		public override string GetSummary()
		{
			if (this.spriteRenderer == null)
			{
				return "Error: No sprite renderer selected";
			}
			return this.spriteRenderer.name + " to " + this._targetColor.Value.ToString();
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x0004CB1E File Offset: 0x0004AD1E
		public override Color GetButtonColor()
		{
			return new Color32(221, 184, 169, byte.MaxValue);
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x0004CB3E File Offset: 0x0004AD3E
		public override bool HasReference(Variable variable)
		{
			return this._duration.floatRef == variable || this._targetColor.colorRef == variable || base.HasReference(variable);
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x002A3BF0 File Offset: 0x002A1DF0
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

		// Token: 0x04006352 RID: 25426
		[Tooltip("Sprite object to be faded")]
		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		// Token: 0x04006353 RID: 25427
		[Tooltip("Length of time to perform the fade")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x04006354 RID: 25428
		[Tooltip("Target color to fade to. To only fade transparency level, set the color to white and set the alpha to required transparency.")]
		[SerializeField]
		protected ColorData _targetColor = new ColorData(Color.white);

		// Token: 0x04006355 RID: 25429
		[Tooltip("Wait until the fade has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04006356 RID: 25430
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;

		// Token: 0x04006357 RID: 25431
		[HideInInspector]
		[FormerlySerializedAs("targetColor")]
		public Color targetColorOLD;
	}
}
