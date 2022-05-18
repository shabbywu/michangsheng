using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001291 RID: 4753
	[CommandInfo("Sprite", "Set Sprite", "Changes the sprite property of a list of Sprite Renderers.", 0)]
	[AddComponentMenu("")]
	public class SetSprite : Command
	{
		// Token: 0x06007335 RID: 29493 RVA: 0x002A9E34 File Offset: 0x002A8034
		public override void OnEnter()
		{
			for (int i = 0; i < this.spriteRenderers.Count; i++)
			{
				this.spriteRenderers[i].sprite = this.sprite;
			}
			this.Continue();
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x002A9E74 File Offset: 0x002A8074
		public override string GetSummary()
		{
			string text = "";
			for (int i = 0; i < this.spriteRenderers.Count; i++)
			{
				SpriteRenderer spriteRenderer = this.spriteRenderers[i];
				if (!(spriteRenderer == null))
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += spriteRenderer.name;
				}
			}
			if (text.Length == 0)
			{
				return "Error: No sprite selected";
			}
			return text + " = " + this.sprite;
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x0004E955 File Offset: 0x0004CB55
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "spriteRenderers";
		}

		// Token: 0x06007339 RID: 29497 RVA: 0x0004E967 File Offset: 0x0004CB67
		public override void OnCommandAdded(Block parentBlock)
		{
			this.spriteRenderers.Add(null);
		}

		// Token: 0x04006529 RID: 25897
		[Tooltip("List of sprites to set the sprite property on")]
		[SerializeField]
		protected List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

		// Token: 0x0400652A RID: 25898
		[Tooltip("The sprite set on the target sprite renderers")]
		[SerializeField]
		protected Sprite sprite;
	}
}
