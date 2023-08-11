using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/TextSpacing")]
public class TextSpacing : BaseMeshEffect
{
	[SerializeField]
	private float spacing_x;

	[SerializeField]
	private float spacing_y;

	private List<UIVertex> mVertexList;

	public override void ModifyMesh(VertexHelper vh)
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		if ((spacing_x == 0f && spacing_y == 0f) || !((UIBehaviour)this).IsActive() || vh.currentVertCount == 0)
		{
			return;
		}
		if (mVertexList == null)
		{
			mVertexList = new List<UIVertex>();
		}
		vh.GetUIVertexStream(mVertexList);
		int num = 1;
		int num2 = 2;
		float num3 = mVertexList.GetRange(0, 6).Min((UIVertex v) => v.position.x);
		int count = mVertexList.Count;
		int num4 = 6;
		while (num4 < count)
		{
			if (num4 % 6 == 0)
			{
				float num5 = mVertexList.GetRange(num4, 6).Min((UIVertex v) => v.position.x);
				if (num5 <= num3)
				{
					num3 = num5;
					num++;
					num2 = 1;
				}
			}
			for (int i = 0; i < 6; i++)
			{
				UIVertex value = mVertexList[num4];
				ref Vector3 position = ref value.position;
				position += Vector3.right * (float)(num2 - 1) * spacing_x;
				ref Vector3 position2 = ref value.position;
				position2 += Vector3.down * (float)(num - 1) * spacing_y;
				mVertexList[num4] = value;
				num4++;
			}
			num2++;
		}
		vh.Clear();
		vh.AddUIVertexTriangleStream(mVertexList);
	}
}
