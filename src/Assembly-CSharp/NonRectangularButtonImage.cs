using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000355 RID: 853
[RequireComponent(typeof(PolygonCollider2D))]
public class NonRectangularButtonImage : Image
{
	// Token: 0x06001CE5 RID: 7397 RVA: 0x000CE2AF File Offset: 0x000CC4AF
	protected NonRectangularButtonImage()
	{
		base.useLegacyMeshGeneration = true;
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x000CE2BE File Offset: 0x000CC4BE
	private PolygonCollider2D Polygon
	{
		get
		{
			if (this.areaPolygon != null)
			{
				return this.areaPolygon;
			}
			this.areaPolygon = base.GetComponent<PolygonCollider2D>();
			return this.areaPolygon;
		}
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x000CE2E7 File Offset: 0x000CC4E7
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x000CE2EF File Offset: 0x000CC4EF
	public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{
		return !(eventCamera == null) && this.Polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
	}

	// Token: 0x04001770 RID: 6000
	private PolygonCollider2D areaPolygon;
}
