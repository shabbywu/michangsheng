using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000A56 RID: 2646
	[AddComponentMenu("UI/Effects/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		// Token: 0x060049E1 RID: 18913 RVA: 0x001F53CC File Offset: 0x001F35CC
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			int count = list.Count;
			this.ApplyGradient(list, 0, count);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x001F540C File Offset: 0x001F360C
		private void ApplyGradient(List<UIVertex> vertexList, int start, int end)
		{
			float num = vertexList[0].position.y;
			float num2 = vertexList[0].position.y;
			for (int i = start; i < end; i++)
			{
				float y = vertexList[i].position.y;
				if (y > num2)
				{
					num2 = y;
				}
				else if (y < num)
				{
					num = y;
				}
			}
			float num3 = num2 - num;
			for (int j = start; j < end; j++)
			{
				UIVertex uivertex = vertexList[j];
				uivertex.color = Color32.Lerp(this.bottomColor, this.topColor, (uivertex.position.y - num) / num3);
				vertexList[j] = uivertex;
			}
		}

		// Token: 0x0400494F RID: 18767
		[SerializeField]
		private Color32 topColor = Color.white;

		// Token: 0x04004950 RID: 18768
		[SerializeField]
		private Color32 bottomColor = Color.black;
	}
}
