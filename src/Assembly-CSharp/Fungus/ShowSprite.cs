using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200129B RID: 4763
	[CommandInfo("Sprite", "Show Sprite", "Makes a sprite visible / invisible by setting the color alpha.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShowSprite : Command
	{
		// Token: 0x06007370 RID: 29552 RVA: 0x002AB064 File Offset: 0x002A9264
		protected virtual void SetSpriteAlpha(SpriteRenderer renderer, bool visible)
		{
			Color color = renderer.color;
			color.a = (visible ? 1f : 0f);
			renderer.color = color;
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x002AB098 File Offset: 0x002A9298
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

		// Token: 0x06007372 RID: 29554 RVA: 0x002AB110 File Offset: 0x002A9310
		public override string GetSummary()
		{
			if (this.spriteRenderer == null)
			{
				return "Error: No sprite renderer selected";
			}
			return this.spriteRenderer.name + " to " + (this._visible.Value ? "visible" : "invisible");
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x0004CB1E File Offset: 0x0004AD1E
		public override Color GetButtonColor()
		{
			return new Color32(221, 184, 169, byte.MaxValue);
		}

		// Token: 0x06007374 RID: 29556 RVA: 0x0004EC1B File Offset: 0x0004CE1B
		public override bool HasReference(Variable variable)
		{
			return this._visible.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x0004EC39 File Offset: 0x0004CE39
		protected virtual void OnEnable()
		{
			if (this.visibleOLD)
			{
				this._visible.Value = this.visibleOLD;
				this.visibleOLD = false;
			}
		}

		// Token: 0x04006554 RID: 25940
		[Tooltip("Sprite object to be made visible / invisible")]
		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		// Token: 0x04006555 RID: 25941
		[Tooltip("Make the sprite visible or invisible")]
		[SerializeField]
		protected BooleanData _visible = new BooleanData(false);

		// Token: 0x04006556 RID: 25942
		[Tooltip("Affect the visibility of child sprites")]
		[SerializeField]
		protected bool affectChildren = true;

		// Token: 0x04006557 RID: 25943
		[HideInInspector]
		[FormerlySerializedAs("visible")]
		public bool visibleOLD;
	}
}
