using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000D77 RID: 3447
	[AddComponentMenu("UI/Effects/TextSpacingEasyTest")]
	public class TextSpacingEasyTest : BaseMeshEffect
	{
		// Token: 0x060052CF RID: 21199 RVA: 0x00227F44 File Offset: 0x00226144
		private void Start()
		{
			this.text = base.GetComponent<Text>();
			this.rect = this.text.GetComponent<RectTransform>();
			this.textWidth = this.rect.rect.height;
			this.fontSize = this.text.fontSize;
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00227F98 File Offset: 0x00226198
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

		// Token: 0x040052D6 RID: 21206
		[SerializeField]
		private Text text;

		// Token: 0x040052D7 RID: 21207
		[SerializeField]
		private float textWidth;

		// Token: 0x040052D8 RID: 21208
		[SerializeField]
		private bool AutoSpace;

		// Token: 0x040052D9 RID: 21209
		private int fontSize = 14;

		// Token: 0x040052DA RID: 21210
		private RectTransform rect;

		// Token: 0x040052DB RID: 21211
		public float spacing;
	}
}
