using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
	// Token: 0x06000552 RID: 1362 RVA: 0x00072390 File Offset: 0x00070590
	private void OnDragStart()
	{
		if (this.target != null)
		{
			Vector3[] worldCorners = this.target.worldCorners;
			this.mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
			Ray currentRay = UICamera.currentRay;
			float num;
			if (this.mPlane.Raycast(currentRay, ref num))
			{
				this.mRayPos = currentRay.GetPoint(num);
				this.mLocalPos = this.target.cachedTransform.localPosition;
				this.mWidth = this.target.width;
				this.mHeight = this.target.height;
				this.mDragging = true;
			}
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00072440 File Offset: 0x00070640
	private void OnDrag(Vector2 delta)
	{
		if (this.mDragging && this.target != null)
		{
			Ray currentRay = UICamera.currentRay;
			float num;
			if (this.mPlane.Raycast(currentRay, ref num))
			{
				Transform cachedTransform = this.target.cachedTransform;
				cachedTransform.localPosition = this.mLocalPos;
				this.target.width = this.mWidth;
				this.target.height = this.mHeight;
				Vector3 vector = currentRay.GetPoint(num) - this.mRayPos;
				cachedTransform.position += vector;
				Vector3 vector2 = Quaternion.Inverse(cachedTransform.localRotation) * (cachedTransform.localPosition - this.mLocalPos);
				cachedTransform.localPosition = this.mLocalPos;
				NGUIMath.ResizeWidget(this.target, this.pivot, vector2.x, vector2.y, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight);
			}
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00008BB7 File Offset: 0x00006DB7
	private void OnDragEnd()
	{
		this.mDragging = false;
	}

	// Token: 0x040003AA RID: 938
	public UIWidget target;

	// Token: 0x040003AB RID: 939
	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	// Token: 0x040003AC RID: 940
	public int minWidth = 100;

	// Token: 0x040003AD RID: 941
	public int minHeight = 100;

	// Token: 0x040003AE RID: 942
	public int maxWidth = 100000;

	// Token: 0x040003AF RID: 943
	public int maxHeight = 100000;

	// Token: 0x040003B0 RID: 944
	private Plane mPlane;

	// Token: 0x040003B1 RID: 945
	private Vector3 mRayPos;

	// Token: 0x040003B2 RID: 946
	private Vector3 mLocalPos;

	// Token: 0x040003B3 RID: 947
	private int mWidth;

	// Token: 0x040003B4 RID: 948
	private int mHeight;

	// Token: 0x040003B5 RID: 949
	private bool mDragging;
}
