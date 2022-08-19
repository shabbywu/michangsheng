using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
	// Token: 0x06000500 RID: 1280 RVA: 0x0001B9D8 File Offset: 0x00019BD8
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

	// Token: 0x06000501 RID: 1281 RVA: 0x0001BA88 File Offset: 0x00019C88
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

	// Token: 0x06000502 RID: 1282 RVA: 0x0001BB90 File Offset: 0x00019D90
	private void OnDragEnd()
	{
		this.mDragging = false;
	}

	// Token: 0x04000322 RID: 802
	public UIWidget target;

	// Token: 0x04000323 RID: 803
	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	// Token: 0x04000324 RID: 804
	public int minWidth = 100;

	// Token: 0x04000325 RID: 805
	public int minHeight = 100;

	// Token: 0x04000326 RID: 806
	public int maxWidth = 100000;

	// Token: 0x04000327 RID: 807
	public int maxHeight = 100000;

	// Token: 0x04000328 RID: 808
	private Plane mPlane;

	// Token: 0x04000329 RID: 809
	private Vector3 mRayPos;

	// Token: 0x0400032A RID: 810
	private Vector3 mLocalPos;

	// Token: 0x0400032B RID: 811
	private int mWidth;

	// Token: 0x0400032C RID: 812
	private int mHeight;

	// Token: 0x0400032D RID: 813
	private bool mDragging;
}
