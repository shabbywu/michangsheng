using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E41 RID: 3649
	[CommandInfo("Sprite", "Set Sprite Order", "Controls the render order of sprites by setting the Order In Layer property of a list of sprites.", 0)]
	[AddComponentMenu("")]
	public class SetSpriteOrder : Command
	{
		// Token: 0x060066AD RID: 26285 RVA: 0x00286D70 File Offset: 0x00284F70
		public override void OnEnter()
		{
			for (int i = 0; i < this.targetSprites.Count; i++)
			{
				this.targetSprites[i].sortingOrder = this.orderInLayer;
			}
			this.Continue();
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x00286DB8 File Offset: 0x00284FB8
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

		// Token: 0x060066AF RID: 26287 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x00286E42 File Offset: 0x00285042
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetSprites";
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x00286E54 File Offset: 0x00285054
		public override void OnCommandAdded(Block parentBlock)
		{
			this.targetSprites.Add(null);
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x00286E62 File Offset: 0x00285062
		public override bool HasReference(Variable variable)
		{
			return this.orderInLayer.integerRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057E7 RID: 22503
		[Tooltip("List of sprites to set the order in layer property on")]
		[SerializeField]
		protected List<SpriteRenderer> targetSprites = new List<SpriteRenderer>();

		// Token: 0x040057E8 RID: 22504
		[Tooltip("The order in layer value to set on the target sprites")]
		[SerializeField]
		protected IntegerData orderInLayer;
	}
}
