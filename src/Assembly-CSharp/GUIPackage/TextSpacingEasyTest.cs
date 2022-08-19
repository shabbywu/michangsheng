using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000A57 RID: 2647
	[AddComponentMenu("UI/Effects/TextSpacingEasyTest")]
	public class TextSpacingEasyTest : BaseMeshEffect
	{
		// Token: 0x060049E4 RID: 18916 RVA: 0x001F54E8 File Offset: 0x001F36E8
		private void Start()
		{
			this.text = base.GetComponent<Text>();
			this.rect = this.text.GetComponent<RectTransform>();
			this.textWidth = this.rect.rect.height;
			this.fontSize = this.text.fontSize;
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x001F553C File Offset: 0x001F373C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (this.text.text.Length >= 6)
			{
				return;
			}
			if (this.textWidth != this.rect.rect.height)
			{
				this.textWidth = this.rect.rect.height;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			int count = list.Count;
			int num = count / 6 - 1;
			if (this.AutoSpace)
			{
				this.spacing = (this.textWidth - (float)((num + 1) * this.fontSize)) / (float)num;
			}
			for (int i = 6; i < count; i++)
			{
				UIVertex uivertex = list[i];
				uivertex.position -= new Vector3(0f, this.spacing * (float)(i / 6), 0f);
				list[i] = uivertex;
				if (i % 6 <= 2)
				{
					vh.SetUIVertex(uivertex, i / 6 * 4 + i % 6);
				}
				if (i % 6 == 4)
				{
					vh.SetUIVertex(uivertex, i / 6 * 4 + i % 6 - 1);
				}
			}
		}

		// Token: 0x04004951 RID: 18769
		[SerializeField]
		private Text text;

		// Token: 0x04004952 RID: 18770
		[SerializeField]
		private float textWidth;

		// Token: 0x04004953 RID: 18771
		[SerializeField]
		private bool AutoSpace;

		// Token: 0x04004954 RID: 18772
		private int fontSize = 14;

		// Token: 0x04004955 RID: 18773
		private RectTransform rect;

		// Token: 0x04004956 RID: 18774
		public float spacing;
	}
}
