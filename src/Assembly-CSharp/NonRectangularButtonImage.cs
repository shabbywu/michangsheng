using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class NonRectangularButtonImage : Image
{
	private PolygonCollider2D areaPolygon;

	private PolygonCollider2D Polygon
	{
		get
		{
			if ((Object)(object)areaPolygon != (Object)null)
			{
				return areaPolygon;
			}
			areaPolygon = ((Component)this).GetComponent<PolygonCollider2D>();
			return areaPolygon;
		}
	}

	protected NonRectangularButtonImage()
	{
		((Graphic)this).useLegacyMeshGeneration = true;
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)eventCamera == (Object)null)
		{
			return false;
		}
		return ((Collider2D)Polygon).OverlapPoint(Vector2.op_Implicit(eventCamera.ScreenToWorldPoint(Vector2.op_Implicit(screenPoint))));
	}
}
