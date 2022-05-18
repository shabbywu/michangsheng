using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000D76 RID: 3446
	[AddComponentMenu("UI/Effects/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		// Token: 0x060052CC RID: 21196 RVA: 0x00227E50 File Offset: 0x00226050
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

		// Token: 0x060052CD RID: 21197 RVA: 0x00227E90 File Offset: 0x00226090
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

		// Token: 0x040052D4 RID: 21204
		[SerializeField]
		private Color32 topColor = Color.white;

		// Token: 0x040052D5 RID: 21205
		[SerializeField]
		private Color32 bottomColor = Color.black;
	}
}
