using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E44 RID: 3652
	[CommandInfo("UI", "Set UI Image", "Changes the Image property of a list of UI Images.", 0)]
	[AddComponentMenu("")]
	public class SetUIImage : Command
	{
		// Token: 0x060066C3 RID: 26307 RVA: 0x00287044 File Offset: 0x00285244
		public override void OnEnter()
		{
			for (int i = 0; i < this.images.Count; i++)
			{
				this.images[i].sprite = this.sprite;
			}
			this.Continue();
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x00287084 File Offset: 0x00285284
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

		// Token: 0x060066C5 RID: 26309 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x00287104 File Offset: 0x00285304
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "images";
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x00287116 File Offset: 0x00285316
		public override void OnCommandAdded(Block parentBlock)
		{
			this.images.Add(null);
		}

		// Token: 0x040057EF RID: 22511
		[Tooltip("List of UI Images to set the source image property on")]
		[SerializeField]
		protected List<Image> images = new List<Image>();

		// Token: 0x040057F0 RID: 22512
		[Tooltip("The sprite set on the source image property")]
		[SerializeField]
		protected Sprite sprite;
	}
}
