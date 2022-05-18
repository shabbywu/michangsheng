using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023F RID: 575
[AddComponentMenu("UI/Effects/TextSpacing")]
public class TextSpacing : BaseMeshEffect
{
	// Token: 0x060011C4 RID: 4548 RVA: 0x000AC754 File Offset: 0x000AA954
	public override void ModifyMesh(VertexHelper vh)
	{
		if (this.spacing_x == 0f && this.spacing_y == 0f)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		if (vh.currentVertCount == 0)
		{
			return;
		}
		if (this.mVertexList == null)
		{
			this.mVertexList = new List<UIVertex>();
		}
		vh.GetUIVertexStream(this.mVertexList);
		int num = 1;
		int num2 = 2;
		float num3 = this.mVertexList.GetRange(0, 6).Min((UIVertex v) => v.position.x);
		int count = this.mVertexList.Count;
		int i = 6;
		while (i < count)
		{
			if (i % 6 == 0)
			{
				float num4 = this.mVertexList.GetRange(i, 6).Min((UIVertex v) => v.position.x);
				if (num4 <= num3)
				{
					num3 = num4;
					num++;
					num2 = 1;
				}
			}
			for (int j = 0; j < 6; j++)
			{
				UIVertex value = this.mVertexList[i];
				value.position += Vector3.right * (float)(num2 - 1) * this.spacing_x;
				value.position += Vector3.down * (float)(num - 1) * this.spacing_y;
				this.mVertexList[i] = value;
				i++;
			}
			num2++;
		}
		vh.Clear();
		vh.AddUIVertexTriangleStream(this.mVertexList);
	}

	// Token: 0x04000E52 RID: 3666
	[SerializeField]
	private float spacing_x;

	// Token: 0x04000E53 RID: 3667
	[SerializeField]
	private float spacing_y;

	// Token: 0x04000E54 RID: 3668
	private List<UIVertex> mVertexList;
}
