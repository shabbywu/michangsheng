using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001292 RID: 4754
	[CommandInfo("Sprite", "Set Sprite Order", "Controls the render order of sprites by setting the Order In Layer property of a list of sprites.", 0)]
	[AddComponentMenu("")]
	public class SetSpriteOrder : Command
	{
		// Token: 0x0600733B RID: 29499 RVA: 0x002A9EF4 File Offset: 0x002A80F4
		public override void OnEnter()
		{
			for (int i = 0; i < this.targetSprites.Count; i++)
			{
				this.targetSprites[i].sortingOrder = this.orderInLayer;
			}
			this.Continue();
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x002A9F3C File Offset: 0x002A813C
		public override string GetSummary()
		{
			string text = "";
			for (int i = 0; i < this.targetSprites.Count; i++)
			{
				SpriteRenderer spriteRenderer = this.targetSprites[i];
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
				return "Error: No cursor sprite selected";
			}
			return text + " = " + this.orderInLayer.Value;
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600733E RID: 29502 RVA: 0x0004E988 File Offset: 0x0004CB88
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetSprites";
		}

		// Token: 0x0600733F RID: 29503 RVA: 0x0004E99A File Offset: 0x0004CB9A
		public override void OnCommandAdded(Block parentBlock)
		{
			this.targetSprites.Add(null);
		}

		// Token: 0x06007340 RID: 29504 RVA: 0x0004E9A8 File Offset: 0x0004CBA8
		public override bool HasReference(Variable variable)
		{
			return this.orderInLayer.integerRef == variable || base.HasReference(variable);
		}

		// Token: 0x0400652B RID: 25899
		[Tooltip("List of sprites to set the order in layer property on")]
		[SerializeField]
		protected List<SpriteRenderer> targetSprites = new List<SpriteRenderer>();

		// Token: 0x0400652C RID: 25900
		[Tooltip("The order in layer value to set on the target sprites")]
		[SerializeField]
		protected IntegerData orderInLayer;
	}
}
