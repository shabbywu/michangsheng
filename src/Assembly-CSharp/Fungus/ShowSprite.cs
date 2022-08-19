using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E4A RID: 3658
	[CommandInfo("Sprite", "Show Sprite", "Makes a sprite visible / invisible by setting the color alpha.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShowSprite : Command
	{
		// Token: 0x060066E2 RID: 26338 RVA: 0x00288178 File Offset: 0x00286378
		protected virtual void SetSpriteAlpha(SpriteRenderer renderer, bool visible)
		{
			Color color = renderer.color;
			color.a = (visible ? 1f : 0f);
			renderer.color = color;
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x002881AC File Offset: 0x002863AC
		public override void OnEnter()
		{
			if (this.spriteRenderer != null)
			{
				if (this.affectChildren)
				{
					foreach (SpriteRenderer renderer in this.spriteRenderer.gameObject.GetComponentsInChildren<SpriteRenderer>())
					{
						this.SetSpriteAlpha(renderer, this._visible.Value);
					}
				}
				else
				{
					this.SetSpriteAlpha(this.spriteRenderer, this._visible.Value);
				}
			}
			this.Continue();
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x00288224 File Offset: 0x00286424
		public override string GetSummary()
		{
			if (this.spriteRenderer == null)
			{
				return "Error: No sprite renderer selected";
			}
			return this.spriteRenderer.name + " to " + (this._visible.Value ? "visible" : "invisible");
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0027ED1F File Offset: 0x0027CF1F
		public override Color GetButtonColor()
		{
			return new Color32(221, 184, 169, byte.MaxValue);
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x00288273 File Offset: 0x00286473
		public override bool HasReference(Variable variable)
		{
			return this._visible.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x00288291 File Offset: 0x00286491
		protected virtual void OnEnable()
		{
			if (this.visibleOLD)
			{
				this._visible.Value = this.visibleOLD;
				this.visibleOLD = false;
			}
		}

		// Token: 0x04005810 RID: 22544
		[Tooltip("Sprite object to be made visible / invisible")]
		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		// Token: 0x04005811 RID: 22545
		[Tooltip("Make the sprite visible or invisible")]
		[SerializeField]
		protected BooleanData _visible = new BooleanData(false);

		// Token: 0x04005812 RID: 22546
		[Tooltip("Affect the visibility of child sprites")]
		[SerializeField]
		protected bool affectChildren = true;

		// Token: 0x04005813 RID: 22547
		[HideInInspector]
		[FormerlySerializedAs("visible")]
		public bool visibleOLD;
	}
}
