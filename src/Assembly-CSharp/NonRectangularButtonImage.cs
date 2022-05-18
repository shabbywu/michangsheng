using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D2 RID: 1234
[RequireComponent(typeof(PolygonCollider2D))]
public class NonRectangularButtonImage : Image
{
	// Token: 0x0600204F RID: 8271 RVA: 0x0001A86A File Offset: 0x00018A6A
	protected NonRectangularButtonImage()
	{
		base.useLegacyMeshGeneration = true;
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06002050 RID: 8272 RVA: 0x0001A879 File Offset: 0x00018A79
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

	// Token: 0x06002051 RID: 8273 RVA: 0x0001A8A2 File Offset: 0x00018AA2
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x0001A8AA File Offset: 0x00018AAA
	public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{
		return !(eventCamera == null) && this.Polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
	}

	// Token: 0x04001BC8 RID: 7112
	private PolygonCollider2D areaPolygon;
}
