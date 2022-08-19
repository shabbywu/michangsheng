using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000164 RID: 356
[AddComponentMenu("UI/Effects/TextSpacing")]
public class TextSpacing : BaseMeshEffect
{
	// Token: 0x06000F6A RID: 3946 RVA: 0x0005CB20 File Offset: 0x0005AD20
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

	// Token: 0x04000B85 RID: 2949
	[SerializeField]
	private float spacing_x;

	// Token: 0x04000B86 RID: 2950
	[SerializeField]
	private float spacing_y;

	// Token: 0x04000B87 RID: 2951
	private List<UIVertex> mVertexList;
}
