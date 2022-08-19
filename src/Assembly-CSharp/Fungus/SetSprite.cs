using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E40 RID: 3648
	[CommandInfo("Sprite", "Set Sprite", "Changes the sprite property of a list of Sprite Renderers.", 0)]
	[AddComponentMenu("")]
	public class SetSprite : Command
	{
		// Token: 0x060066A7 RID: 26279 RVA: 0x00286C7C File Offset: 0x00284E7C
		public override void OnEnter()
		{
			for (int i = 0; i < this.spriteRenderers.Count; i++)
			{
				this.spriteRenderers[i].sprite = this.sprite;
			}
			this.Continue();
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x00286CBC File Offset: 0x00284EBC
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

		// Token: 0x060066A9 RID: 26281 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x00286D3C File Offset: 0x00284F3C
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "spriteRenderers";
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x00286D4E File Offset: 0x00284F4E
		public override void OnCommandAdded(Block parentBlock)
		{
			this.spriteRenderers.Add(null);
		}

		// Token: 0x040057E5 RID: 22501
		[Tooltip("List of sprites to set the sprite property on")]
		[SerializeField]
		protected List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

		// Token: 0x040057E6 RID: 22502
		[Tooltip("The sprite set on the target sprite renderers")]
		[SerializeField]
		protected Sprite sprite;
	}
}
