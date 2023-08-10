using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUIPackage;

[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
	[SerializeField]
	private Color32 topColor = Color32.op_Implicit(Color.white);

	[SerializeField]
	private Color32 bottomColor = Color32.op_Implicit(Color.black);

	public override void ModifyMesh(VertexHelper vh)
	{
		if (((UIBehaviour)this).IsActive())
		{
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			int count = list.Count;
			ApplyGradient(list, 0, count);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}
	}

	private void ApplyGradient(List<UIVertex> vertexList, int start, int end)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
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
			UIVertex val = vertexList[j];
			val.color = Color32.Lerp(bottomColor, topColor, (val.position.y - num) / num3);
			vertexList[j] = val;
		}
	}
}
