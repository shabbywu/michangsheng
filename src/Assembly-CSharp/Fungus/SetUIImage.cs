using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001295 RID: 4757
	[CommandInfo("UI", "Set UI Image", "Changes the Image property of a list of UI Images.", 0)]
	[AddComponentMenu("")]
	public class SetUIImage : Command
	{
		// Token: 0x06007351 RID: 29521 RVA: 0x002AA02C File Offset: 0x002A822C
		public override void OnEnter()
		{
			for (int i = 0; i < this.images.Count; i++)
			{
				this.images[i].sprite = this.sprite;
			}
			this.Continue();
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x002AA06C File Offset: 0x002A826C
		public override string GetSummary()
		{
			string text = "";
			for (int i = 0; i < this.images.Count; i++)
			{
				Image image = this.images[i];
				if (!(image == null))
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += image.name;
				}
			}
			if (text.Length == 0)
			{
				return "Error: No sprite selected";
			}
			return text + " = " + this.sprite;
		}

		// Token: 0x06007353 RID: 29523 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007354 RID: 29524 RVA: 0x0004EB25 File Offset: 0x0004CD25
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "images";
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x0004EB37 File Offset: 0x0004CD37
		public override void OnCommandAdded(Block parentBlock)
		{
			this.images.Add(null);
		}

		// Token: 0x04006533 RID: 25907
		[Tooltip("List of UI Images to set the source image property on")]
		[SerializeField]
		protected List<Image> images = new List<Image>();

		// Token: 0x04006534 RID: 25908
		[Tooltip("The sprite set on the source image property")]
		[SerializeField]
		protected Sprite sprite;
	}
}
