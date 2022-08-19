using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Panel")]
public class UIPanel : UIRect
{
	// Token: 0x17000191 RID: 401
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0003B480 File Offset: 0x00039680
	public static int nextUnusedDepth
	{
		get
		{
			int num = int.MinValue;
			for (int i = 0; i < UIPanel.list.size; i++)
			{
				num = Mathf.Max(num, UIPanel.list[i].depth);
			}
			if (num != -2147483648)
			{
				return num + 1;
			}
			return 0;
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0003B4CC File Offset: 0x000396CC
	public override bool canBeAnchored
	{
		get
		{
			return this.mClipping > UIDrawCall.Clipping.None;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0003B4D7 File Offset: 0x000396D7
	// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0003B4E0 File Offset: 0x000396E0
	public override float alpha
	{
		get
		{
			return this.mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mAlpha != num)
			{
				this.mAlphaFrameID = -1;
				this.mResized = true;
				this.mAlpha = num;
				this.SetDirty();
			}
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0003B518 File Offset: 0x00039718
	// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0003B520 File Offset: 0x00039720
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				UIPanel.list.Sort(new BetterList<UIPanel>.CompareFunc(UIPanel.CompareFunc));
			}
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0003B548 File Offset: 0x00039748
	// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0003B550 File Offset: 0x00039750
	public int sortingOrder
	{
		get
		{
			return this.mSortingOrder;
		}
		set
		{
			if (this.mSortingOrder != value)
			{
				this.mSortingOrder = value;
				this.UpdateDrawCalls();
			}
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0003B568 File Offset: 0x00039768
	public static int CompareFunc(UIPanel a, UIPanel b)
	{
		if (!(a != b) || !(a != null) || !(b != null))
		{
			return 0;
		}
		if (a.mDepth < b.mDepth)
		{
			return -1;
		}
		if (a.mDepth > b.mDepth)
		{
			return 1;
		}
		if (a.GetInstanceID() >= b.GetInstanceID())
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x060009EB RID: 2539 RVA: 0x0003B5C3 File Offset: 0x000397C3
	public float width
	{
		get
		{
			return this.GetViewSize().x;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x060009EC RID: 2540 RVA: 0x0003B5D0 File Offset: 0x000397D0
	public float height
	{
		get
		{
			return this.GetViewSize().y;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x060009ED RID: 2541 RVA: 0x0003B5DD File Offset: 0x000397DD
	public bool halfPixelOffset
	{
		get
		{
			return this.mHalfPixelOffset;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x060009EE RID: 2542 RVA: 0x0003B5E5 File Offset: 0x000397E5
	public bool usedForUI
	{
		get
		{
			return this.mCam != null && this.mCam.orthographic;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x060009EF RID: 2543 RVA: 0x0003B604 File Offset: 0x00039804
	public Vector3 drawCallOffset
	{
		get
		{
			if (this.mHalfPixelOffset && this.mCam != null && this.mCam.orthographic)
			{
				Vector2 windowSize = this.GetWindowSize();
				float num = 1f / windowSize.y / this.mCam.orthographicSize;
				return new Vector3(-num, num);
			}
			return Vector3.zero;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0003B662 File Offset: 0x00039862
	// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0003B66A File Offset: 0x0003986A
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mResized = true;
				this.mClipping = value;
				this.mMatrixFrame = -1;
			}
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0003B68A File Offset: 0x0003988A
	public UIPanel parentPanel
	{
		get
		{
			return this.mParentPanel;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0003B694 File Offset: 0x00039894
	public int clipCount
	{
		get
		{
			int num = 0;
			UIPanel uipanel = this;
			while (uipanel != null)
			{
				if (uipanel.mClipping == UIDrawCall.Clipping.SoftClip)
				{
					num++;
				}
				uipanel = uipanel.mParentPanel;
			}
			return num;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0003B6C5 File Offset: 0x000398C5
	public bool hasClipping
	{
		get
		{
			return this.mClipping == UIDrawCall.Clipping.SoftClip;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0003B6D0 File Offset: 0x000398D0
	public bool hasCumulativeClipping
	{
		get
		{
			return this.clipCount != 0;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0003B6DB File Offset: 0x000398DB
	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool clipsChildren
	{
		get
		{
			return this.hasCumulativeClipping;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0003B6E3 File Offset: 0x000398E3
	// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0003B6EC File Offset: 0x000398EC
	public Vector2 clipOffset
	{
		get
		{
			return this.mClipOffset;
		}
		set
		{
			if (Mathf.Abs(this.mClipOffset.x - value.x) > 0.001f || Mathf.Abs(this.mClipOffset.y - value.y) > 0.001f)
			{
				this.mClipOffset = value;
				this.InvalidateClipping();
				if (this.onClipMove != null)
				{
					this.onClipMove(this);
				}
			}
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0003B758 File Offset: 0x00039958
	private void InvalidateClipping()
	{
		this.mResized = true;
		this.mMatrixFrame = -1;
		this.mCullTime = ((this.mCullTime == 0f) ? 0.001f : (RealTime.time + 0.15f));
		for (int i = 0; i < UIPanel.list.size; i++)
		{
			UIPanel uipanel = UIPanel.list[i];
			if (uipanel != this && uipanel.parentPanel == this)
			{
				uipanel.InvalidateClipping();
			}
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x060009FA RID: 2554 RVA: 0x0003B7D6 File Offset: 0x000399D6
	// (set) Token: 0x060009FB RID: 2555 RVA: 0x0003B7DE File Offset: 0x000399DE
	[Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
	public Vector4 clipRange
	{
		get
		{
			return this.baseClipRegion;
		}
		set
		{
			this.baseClipRegion = value;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x060009FC RID: 2556 RVA: 0x0003B7E7 File Offset: 0x000399E7
	// (set) Token: 0x060009FD RID: 2557 RVA: 0x0003B7F0 File Offset: 0x000399F0
	public Vector4 baseClipRegion
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (Mathf.Abs(this.mClipRange.x - value.x) > 0.001f || Mathf.Abs(this.mClipRange.y - value.y) > 0.001f || Mathf.Abs(this.mClipRange.z - value.z) > 0.001f || Mathf.Abs(this.mClipRange.w - value.w) > 0.001f)
			{
				this.mResized = true;
				this.mCullTime = ((this.mCullTime == 0f) ? 0.001f : (RealTime.time + 0.15f));
				this.mClipRange = value;
				this.mMatrixFrame = -1;
				UIScrollView component = base.GetComponent<UIScrollView>();
				if (component != null)
				{
					component.UpdatePosition();
				}
				if (this.onClipMove != null)
				{
					this.onClipMove(this);
				}
			}
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x060009FE RID: 2558 RVA: 0x0003B8DC File Offset: 0x00039ADC
	public Vector4 finalClipRegion
	{
		get
		{
			Vector2 viewSize = this.GetViewSize();
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				return new Vector4(this.mClipRange.x + this.mClipOffset.x, this.mClipRange.y + this.mClipOffset.y, viewSize.x, viewSize.y);
			}
			return new Vector4(0f, 0f, viewSize.x, viewSize.y);
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x0003B953 File Offset: 0x00039B53
	// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0003B95B File Offset: 0x00039B5B
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
			}
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0003B974 File Offset: 0x00039B74
	public override Vector3[] localCorners
	{
		get
		{
			if (this.mClipping == UIDrawCall.Clipping.None)
			{
				Vector2 viewSize = this.GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float num3 = num + viewSize.x;
				float num4 = num2 + viewSize.y;
				Transform transform = (this.mCam != null) ? this.mCam.transform : null;
				if (transform != null)
				{
					UIPanel.mCorners[0] = transform.TransformPoint(num, num2, 0f);
					UIPanel.mCorners[1] = transform.TransformPoint(num, num4, 0f);
					UIPanel.mCorners[2] = transform.TransformPoint(num3, num4, 0f);
					UIPanel.mCorners[3] = transform.TransformPoint(num3, num2, 0f);
					transform = base.cachedTransform;
					for (int i = 0; i < 4; i++)
					{
						UIPanel.mCorners[i] = transform.InverseTransformPoint(UIPanel.mCorners[i]);
					}
				}
				else
				{
					UIPanel.mCorners[0] = new Vector3(num, num2);
					UIPanel.mCorners[1] = new Vector3(num, num4);
					UIPanel.mCorners[2] = new Vector3(num3, num4);
					UIPanel.mCorners[3] = new Vector3(num3, num2);
				}
			}
			else
			{
				float num5 = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
				float num6 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
				float num7 = num5 + this.mClipRange.z;
				float num8 = num6 + this.mClipRange.w;
				UIPanel.mCorners[0] = new Vector3(num5, num6);
				UIPanel.mCorners[1] = new Vector3(num5, num8);
				UIPanel.mCorners[2] = new Vector3(num7, num8);
				UIPanel.mCorners[3] = new Vector3(num7, num6);
			}
			return UIPanel.mCorners;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0003BBA8 File Offset: 0x00039DA8
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.mClipping == UIDrawCall.Clipping.None)
			{
				if (this.mCam != null)
				{
					Vector3[] worldCorners = this.mCam.GetWorldCorners();
					UIRoot root = base.root;
					if (root != null)
					{
						float pixelSizeAdjustment = root.pixelSizeAdjustment;
						for (int i = 0; i < 4; i++)
						{
							worldCorners[i] *= pixelSizeAdjustment;
						}
					}
					return worldCorners;
				}
				Vector2 viewSize = this.GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float num3 = num + viewSize.x;
				float num4 = num2 + viewSize.y;
				UIPanel.mCorners[0] = new Vector3(num, num2, 0f);
				UIPanel.mCorners[1] = new Vector3(num, num4, 0f);
				UIPanel.mCorners[2] = new Vector3(num3, num4, 0f);
				UIPanel.mCorners[3] = new Vector3(num3, num2, 0f);
			}
			else
			{
				float num5 = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
				float num6 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
				float num7 = num5 + this.mClipRange.z;
				float num8 = num6 + this.mClipRange.w;
				Transform cachedTransform = base.cachedTransform;
				UIPanel.mCorners[0] = cachedTransform.TransformPoint(num5, num6, 0f);
				UIPanel.mCorners[1] = cachedTransform.TransformPoint(num5, num8, 0f);
				UIPanel.mCorners[2] = cachedTransform.TransformPoint(num7, num8, 0f);
				UIPanel.mCorners[3] = cachedTransform.TransformPoint(num7, num6, 0f);
			}
			return UIPanel.mCorners;
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0003BDAC File Offset: 0x00039FAC
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.mClipping != UIDrawCall.Clipping.None || this.anchorOffset)
		{
			Vector2 viewSize = this.GetViewSize();
			Vector2 vector = (this.mClipping != UIDrawCall.Clipping.None) ? (this.mClipRange + this.mClipOffset) : Vector2.zero;
			float num = vector.x - 0.5f * viewSize.x;
			float num2 = vector.y - 0.5f * viewSize.y;
			float num3 = num + viewSize.x;
			float num4 = num2 + viewSize.y;
			float num5 = (num + num3) * 0.5f;
			float num6 = (num2 + num4) * 0.5f;
			Matrix4x4 localToWorldMatrix = base.cachedTransform.localToWorldMatrix;
			UIPanel.mCorners[0] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num, num6));
			UIPanel.mCorners[1] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num5, num4));
			UIPanel.mCorners[2] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num3, num6));
			UIPanel.mCorners[3] = localToWorldMatrix.MultiplyPoint3x4(new Vector3(num5, num2));
			if (relativeTo != null)
			{
				for (int i = 0; i < 4; i++)
				{
					UIPanel.mCorners[i] = relativeTo.InverseTransformPoint(UIPanel.mCorners[i]);
				}
			}
			return UIPanel.mCorners;
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0003BF03 File Offset: 0x0003A103
	public override void Invalidate(bool includeChildren)
	{
		this.mAlphaFrameID = -1;
		base.Invalidate(includeChildren);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0003BF14 File Offset: 0x0003A114
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			UIRect parent = base.parent;
			this.finalAlpha = ((base.parent != null) ? (parent.CalculateFinalAlpha(frameID) * this.mAlpha) : this.mAlpha);
		}
		return this.finalAlpha;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0003BF68 File Offset: 0x0003A168
	public override void SetRect(float x, float y, float width, float height)
	{
		int num = Mathf.FloorToInt(width + 0.5f);
		int num2 = Mathf.FloorToInt(height + 0.5f);
		num = num >> 1 << 1;
		num2 = num2 >> 1 << 1;
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(x + 0.5f);
		localPosition.y = Mathf.Floor(y + 0.5f);
		if (num < 2)
		{
			num = 2;
		}
		if (num2 < 2)
		{
			num2 = 2;
		}
		this.baseClipRegion = new Vector4(localPosition.x, localPosition.y, (float)num, (float)num2);
		if (base.isAnchored)
		{
			transform = transform.parent;
			if (this.leftAnchor.target)
			{
				this.leftAnchor.SetHorizontal(transform, x);
			}
			if (this.rightAnchor.target)
			{
				this.rightAnchor.SetHorizontal(transform, x + width);
			}
			if (this.bottomAnchor.target)
			{
				this.bottomAnchor.SetVertical(transform, y);
			}
			if (this.topAnchor.target)
			{
				this.topAnchor.SetVertical(transform, y + height);
			}
		}
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0003C08C File Offset: 0x0003A28C
	public bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.worldToLocal.MultiplyPoint3x4(a);
		b = this.worldToLocal.MultiplyPoint3x4(b);
		c = this.worldToLocal.MultiplyPoint3x4(c);
		d = this.worldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0003C1B0 File Offset: 0x0003A3B0
	public bool IsVisible(Vector3 worldPos)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip)
		{
			return true;
		}
		this.UpdateTransformMatrix();
		Vector3 vector = this.worldToLocal.MultiplyPoint3x4(worldPos);
		return vector.x >= this.mMin.x && vector.y >= this.mMin.y && vector.x <= this.mMax.x && vector.y <= this.mMax.y;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0003C248 File Offset: 0x0003A448
	public bool IsVisible(UIWidget w)
	{
		UIPanel uipanel = this;
		Vector3[] array = null;
		while (uipanel != null)
		{
			if ((uipanel.mClipping == UIDrawCall.Clipping.None || uipanel.mClipping == UIDrawCall.Clipping.ConstrainButDontClip) && !w.hideIfOffScreen)
			{
				uipanel = uipanel.mParentPanel;
			}
			else
			{
				if (array == null)
				{
					array = w.worldCorners;
				}
				if (!uipanel.IsVisible(array[0], array[1], array[2], array[3]))
				{
					return false;
				}
				uipanel = uipanel.mParentPanel;
			}
		}
		return true;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0003C2C0 File Offset: 0x0003A4C0
	public bool Affects(UIWidget w)
	{
		if (w == null)
		{
			return false;
		}
		UIPanel panel = w.panel;
		if (panel == null)
		{
			return false;
		}
		UIPanel uipanel = this;
		while (uipanel != null)
		{
			if (uipanel == panel)
			{
				return true;
			}
			if (!uipanel.hasCumulativeClipping)
			{
				return false;
			}
			uipanel = uipanel.mParentPanel;
		}
		return false;
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0003C314 File Offset: 0x0003A514
	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		this.mRebuild = true;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0003C320 File Offset: 0x0003A520
	public void SetDirty()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			this.drawCalls.buffer[i].isDirty = true;
		}
		this.Invalidate(true);
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0003C360 File Offset: 0x0003A560
	private void Awake()
	{
		this.mGo = base.gameObject;
		this.mTrans = base.transform;
		this.mHalfPixelOffset = (Application.platform == 2 || Application.platform == 10 || Application.platform == 7);
		if (this.mHalfPixelOffset)
		{
			this.mHalfPixelOffset = (SystemInfo.graphicsShaderLevel < 40);
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0003C3C0 File Offset: 0x0003A5C0
	private void FindParent()
	{
		Transform parent = base.cachedTransform.parent;
		this.mParentPanel = ((parent != null) ? NGUITools.FindInParents<UIPanel>(parent.gameObject) : null);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0003C3F6 File Offset: 0x0003A5F6
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		this.FindParent();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0003C404 File Offset: 0x0003A604
	protected override void OnStart()
	{
		this.mLayer = this.mGo.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((uicamera != null) ? uicamera.cachedCamera : NGUITools.FindCameraForLayer(this.mLayer));
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0003C450 File Offset: 0x0003A650
	protected override void OnEnable()
	{
		this.mRebuild = true;
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		base.OnEnable();
		this.mMatrixFrame = -1;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0003C474 File Offset: 0x0003A674
	protected override void OnInit()
	{
		base.OnInit();
		if (base.GetComponent<Rigidbody>() == null)
		{
			UICamera uicamera = (this.mCam != null) ? this.mCam.GetComponent<UICamera>() : null;
			if (uicamera != null && (uicamera.eventType == UICamera.EventType.UI_3D || uicamera.eventType == UICamera.EventType.World_3D))
			{
				Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.useGravity = false;
			}
		}
		this.FindParent();
		this.mRebuild = true;
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		UIPanel.list.Add(this);
		UIPanel.list.Sort(new BetterList<UIPanel>.CompareFunc(UIPanel.CompareFunc));
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0003C520 File Offset: 0x0003A720
	protected override void OnDisable()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			if (uidrawCall != null)
			{
				UIDrawCall.Destroy(uidrawCall);
			}
		}
		this.drawCalls.Clear();
		UIPanel.list.Remove(this);
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		if (UIPanel.list.size == 0)
		{
			UIDrawCall.ReleaseAll();
			UIPanel.mUpdateFrame = -1;
		}
		base.OnDisable();
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
	private void UpdateTransformMatrix()
	{
		int frameCount = Time.frameCount;
		if (this.mMatrixFrame != frameCount)
		{
			this.mMatrixFrame = frameCount;
			this.worldToLocal = base.cachedTransform.worldToLocalMatrix;
			Vector2 vector = this.GetViewSize() * 0.5f;
			float num = this.mClipOffset.x + this.mClipRange.x;
			float num2 = this.mClipOffset.y + this.mClipRange.y;
			this.mMin.x = num - vector.x;
			this.mMin.y = num2 - vector.y;
			this.mMax.x = num + vector.x;
			this.mMax.y = num2 + vector.y;
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0003C668 File Offset: 0x0003A868
	protected override void OnAnchor()
	{
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return;
		}
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector2 viewSize = this.GetViewSize();
		Vector2 vector = cachedTransform.localPosition;
		float num;
		float num2;
		float num3;
		float num4;
		if (this.leftAnchor.target == this.bottomAnchor.target && this.leftAnchor.target == this.rightAnchor.target && this.leftAnchor.target == this.topAnchor.target)
		{
			Vector3[] sides = this.leftAnchor.GetSides(parent);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
			}
			else
			{
				Vector2 vector2 = base.GetLocalPos(this.leftAnchor, parent);
				num = vector2.x + (float)this.leftAnchor.absolute;
				num3 = vector2.y + (float)this.bottomAnchor.absolute;
				num2 = vector2.x + (float)this.rightAnchor.absolute;
				num4 = vector2.y + (float)this.topAnchor.absolute;
			}
		}
		else
		{
			if (this.leftAnchor.target)
			{
				Vector3[] sides2 = this.leftAnchor.GetSides(parent);
				if (sides2 != null)
				{
					num = NGUIMath.Lerp(sides2[0].x, sides2[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				}
				else
				{
					num = base.GetLocalPos(this.leftAnchor, parent).x + (float)this.leftAnchor.absolute;
				}
			}
			else
			{
				num = this.mClipRange.x - 0.5f * viewSize.x;
			}
			if (this.rightAnchor.target)
			{
				Vector3[] sides3 = this.rightAnchor.GetSides(parent);
				if (sides3 != null)
				{
					num2 = NGUIMath.Lerp(sides3[0].x, sides3[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				}
				else
				{
					num2 = base.GetLocalPos(this.rightAnchor, parent).x + (float)this.rightAnchor.absolute;
				}
			}
			else
			{
				num2 = this.mClipRange.x + 0.5f * viewSize.x;
			}
			if (this.bottomAnchor.target)
			{
				Vector3[] sides4 = this.bottomAnchor.GetSides(parent);
				if (sides4 != null)
				{
					num3 = NGUIMath.Lerp(sides4[3].y, sides4[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				}
				else
				{
					num3 = base.GetLocalPos(this.bottomAnchor, parent).y + (float)this.bottomAnchor.absolute;
				}
			}
			else
			{
				num3 = this.mClipRange.y - 0.5f * viewSize.y;
			}
			if (this.topAnchor.target)
			{
				Vector3[] sides5 = this.topAnchor.GetSides(parent);
				if (sides5 != null)
				{
					num4 = NGUIMath.Lerp(sides5[3].y, sides5[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
				}
				else
				{
					num4 = base.GetLocalPos(this.topAnchor, parent).y + (float)this.topAnchor.absolute;
				}
			}
			else
			{
				num4 = this.mClipRange.y + 0.5f * viewSize.y;
			}
		}
		num -= vector.x + this.mClipOffset.x;
		num2 -= vector.x + this.mClipOffset.x;
		num3 -= vector.y + this.mClipOffset.y;
		num4 -= vector.y + this.mClipOffset.y;
		float num5 = Mathf.Lerp(num, num2, 0.5f);
		float num6 = Mathf.Lerp(num3, num4, 0.5f);
		float num7 = num2 - num;
		float num8 = num4 - num3;
		float num9 = Mathf.Max(2f, this.mClipSoftness.x);
		float num10 = Mathf.Max(2f, this.mClipSoftness.y);
		if (num7 < num9)
		{
			num7 = num9;
		}
		if (num8 < num10)
		{
			num8 = num10;
		}
		this.baseClipRegion = new Vector4(num5, num6, num7, num8);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0003CBAC File Offset: 0x0003ADAC
	private void LateUpdate()
	{
		if (UIPanel.mUpdateFrame != Time.frameCount)
		{
			UIPanel.mUpdateFrame = Time.frameCount;
			for (int i = 0; i < UIPanel.list.size; i++)
			{
				UIPanel.list[i].UpdateSelf();
			}
			int num = 3000;
			for (int j = 0; j < UIPanel.list.size; j++)
			{
				UIPanel uipanel = UIPanel.list.buffer[j];
				if (uipanel.renderQueue == UIPanel.RenderQueue.Automatic)
				{
					uipanel.startingRenderQueue = num;
					uipanel.UpdateDrawCalls();
					num += uipanel.drawCalls.size;
				}
				else if (uipanel.renderQueue == UIPanel.RenderQueue.StartAt)
				{
					uipanel.UpdateDrawCalls();
					if (uipanel.drawCalls.size != 0)
					{
						num = Mathf.Max(num, uipanel.startingRenderQueue + uipanel.drawCalls.size);
					}
				}
				else
				{
					uipanel.UpdateDrawCalls();
					if (uipanel.drawCalls.size != 0)
					{
						num = Mathf.Max(num, uipanel.startingRenderQueue + 1);
					}
				}
			}
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
	private void UpdateSelf()
	{
		this.mUpdateTime = RealTime.time;
		this.UpdateTransformMatrix();
		this.UpdateLayers();
		this.UpdateWidgets();
		if (this.mRebuild)
		{
			this.mRebuild = false;
			this.FillAllDrawCalls();
		}
		else
		{
			int i = 0;
			while (i < this.drawCalls.size)
			{
				UIDrawCall uidrawCall = this.drawCalls.buffer[i];
				if (uidrawCall.isDirty && !this.FillDrawCall(uidrawCall))
				{
					UIDrawCall.Destroy(uidrawCall);
					this.drawCalls.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}
		if (this.mUpdateScroll)
		{
			this.mUpdateScroll = false;
			UIScrollView component = base.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars();
			}
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0003CD53 File Offset: 0x0003AF53
	public void SortWidgets()
	{
		this.mSortWidgets = false;
		this.widgets.Sort(new BetterList<UIWidget>.CompareFunc(UIWidget.PanelCompareFunc));
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0003CD74 File Offset: 0x0003AF74
	private void FillAllDrawCalls()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall.Destroy(this.drawCalls.buffer[i]);
		}
		this.drawCalls.Clear();
		Material material = null;
		Texture texture = null;
		Shader shader = null;
		UIDrawCall uidrawCall = null;
		if (this.mSortWidgets)
		{
			this.SortWidgets();
		}
		for (int j = 0; j < this.widgets.size; j++)
		{
			UIWidget uiwidget = this.widgets.buffer[j];
			if (uiwidget.isVisible && uiwidget.hasVertices)
			{
				Material material2 = uiwidget.material;
				Texture mainTexture = uiwidget.mainTexture;
				Shader shader2 = uiwidget.shader;
				if (material != material2 || texture != mainTexture || shader != shader2)
				{
					if (uidrawCall != null && uidrawCall.verts.size != 0)
					{
						this.drawCalls.Add(uidrawCall);
						uidrawCall.UpdateGeometry();
						uidrawCall = null;
					}
					material = material2;
					texture = mainTexture;
					shader = shader2;
				}
				if (material != null || shader != null || texture != null)
				{
					if (uidrawCall == null)
					{
						uidrawCall = UIDrawCall.Create(this, material, texture, shader);
						uidrawCall.depthStart = uiwidget.depth;
						uidrawCall.depthEnd = uidrawCall.depthStart;
						uidrawCall.panel = this;
					}
					else
					{
						int depth = uiwidget.depth;
						if (depth < uidrawCall.depthStart)
						{
							uidrawCall.depthStart = depth;
						}
						if (depth > uidrawCall.depthEnd)
						{
							uidrawCall.depthEnd = depth;
						}
					}
					uiwidget.drawCall = uidrawCall;
					if (this.generateNormals)
					{
						uiwidget.WriteToBuffers(uidrawCall.verts, uidrawCall.uvs, uidrawCall.cols, uidrawCall.norms, uidrawCall.tans);
					}
					else
					{
						uiwidget.WriteToBuffers(uidrawCall.verts, uidrawCall.uvs, uidrawCall.cols, null, null);
					}
				}
			}
			else
			{
				uiwidget.drawCall = null;
			}
		}
		if (uidrawCall != null && uidrawCall.verts.size != 0)
		{
			this.drawCalls.Add(uidrawCall);
			uidrawCall.UpdateGeometry();
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x0003CF88 File Offset: 0x0003B188
	private bool FillDrawCall(UIDrawCall dc)
	{
		if (dc != null)
		{
			dc.isDirty = false;
			int i = 0;
			while (i < this.widgets.size)
			{
				UIWidget uiwidget = this.widgets[i];
				if (uiwidget == null)
				{
					this.widgets.RemoveAt(i);
				}
				else
				{
					if (uiwidget.drawCall == dc)
					{
						if (uiwidget.isVisible && uiwidget.hasVertices)
						{
							if (this.generateNormals)
							{
								uiwidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, dc.norms, dc.tans);
							}
							else
							{
								uiwidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, null, null);
							}
						}
						else
						{
							uiwidget.drawCall = null;
						}
					}
					i++;
				}
			}
			if (dc.verts.size != 0)
			{
				dc.UpdateGeometry();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0003D070 File Offset: 0x0003B270
	private void UpdateDrawCalls()
	{
		Transform cachedTransform = base.cachedTransform;
		bool usedForUI = this.usedForUI;
		if (this.clipping != UIDrawCall.Clipping.None)
		{
			this.drawCallClipRange = this.finalClipRegion;
			this.drawCallClipRange.z = this.drawCallClipRange.z * 0.5f;
			this.drawCallClipRange.w = this.drawCallClipRange.w * 0.5f;
		}
		else
		{
			this.drawCallClipRange = Vector4.zero;
		}
		if (this.drawCallClipRange.z == 0f)
		{
			this.drawCallClipRange.z = (float)Screen.width * 0.5f;
		}
		if (this.drawCallClipRange.w == 0f)
		{
			this.drawCallClipRange.w = (float)Screen.height * 0.5f;
		}
		if (this.halfPixelOffset)
		{
			this.drawCallClipRange.x = this.drawCallClipRange.x - 0.5f;
			this.drawCallClipRange.y = this.drawCallClipRange.y + 0.5f;
		}
		Vector3 vector;
		if (usedForUI)
		{
			Transform parent = base.cachedTransform.parent;
			vector = base.cachedTransform.localPosition;
			if (parent != null)
			{
				float num = Mathf.Round(vector.x);
				float num2 = Mathf.Round(vector.y);
				this.drawCallClipRange.x = this.drawCallClipRange.x + (vector.x - num);
				this.drawCallClipRange.y = this.drawCallClipRange.y + (vector.y - num2);
				vector.x = num;
				vector.y = num2;
				vector = parent.TransformPoint(vector);
			}
			vector += this.drawCallOffset;
		}
		else
		{
			vector = cachedTransform.position;
		}
		Quaternion rotation = cachedTransform.rotation;
		Vector3 lossyScale = cachedTransform.lossyScale;
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			Transform cachedTransform2 = uidrawCall.cachedTransform;
			cachedTransform2.position = vector;
			cachedTransform2.rotation = rotation;
			cachedTransform2.localScale = lossyScale;
			uidrawCall.renderQueue = ((this.renderQueue == UIPanel.RenderQueue.Explicit) ? this.startingRenderQueue : (this.startingRenderQueue + i));
			uidrawCall.alwaysOnScreen = (this.alwaysOnScreen && (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip));
			uidrawCall.sortingOrder = this.mSortingOrder;
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0003D29C File Offset: 0x0003B49C
	private void UpdateLayers()
	{
		if (this.mLayer != base.cachedGameObject.layer)
		{
			this.mLayer = this.mGo.layer;
			UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((uicamera != null) ? uicamera.cachedCamera : NGUITools.FindCameraForLayer(this.mLayer));
			NGUITools.SetChildLayer(base.cachedTransform, this.mLayer);
			for (int i = 0; i < this.drawCalls.size; i++)
			{
				this.drawCalls.buffer[i].gameObject.layer = this.mLayer;
			}
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0003D344 File Offset: 0x0003B544
	private void UpdateWidgets()
	{
		bool flag = !this.cullWhileDragging && this.mCullTime > this.mUpdateTime;
		bool flag2 = false;
		if (this.mForced != flag)
		{
			this.mForced = flag;
			this.mResized = true;
		}
		bool hasCumulativeClipping = this.hasCumulativeClipping;
		int i = 0;
		int size = this.widgets.size;
		while (i < size)
		{
			UIWidget uiwidget = this.widgets.buffer[i];
			if (uiwidget.panel == this && uiwidget.enabled)
			{
				int frameCount = Time.frameCount;
				if (uiwidget.UpdateTransform(frameCount) || this.mResized)
				{
					bool visibleByAlpha = flag || uiwidget.CalculateCumulativeAlpha(frameCount) > 0.001f;
					uiwidget.UpdateVisibility(visibleByAlpha, flag || (!hasCumulativeClipping && !uiwidget.hideIfOffScreen) || this.IsVisible(uiwidget));
				}
				if (uiwidget.UpdateGeometry(frameCount))
				{
					flag2 = true;
					if (!this.mRebuild)
					{
						if (uiwidget.drawCall != null)
						{
							uiwidget.drawCall.isDirty = true;
						}
						else
						{
							this.FindDrawCall(uiwidget);
						}
					}
				}
			}
			i++;
		}
		if (flag2 && this.onGeometryUpdated != null)
		{
			this.onGeometryUpdated();
		}
		this.mResized = false;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0003D48C File Offset: 0x0003B68C
	public UIDrawCall FindDrawCall(UIWidget w)
	{
		Material material = w.material;
		Texture mainTexture = w.mainTexture;
		int depth = w.depth;
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			int num = (i == 0) ? int.MinValue : (this.drawCalls.buffer[i - 1].depthEnd + 1);
			int num2 = (i + 1 == this.drawCalls.size) ? int.MaxValue : (this.drawCalls.buffer[i + 1].depthStart - 1);
			if (num <= depth && num2 >= depth)
			{
				if (uidrawCall.baseMaterial == material && uidrawCall.mainTexture == mainTexture)
				{
					if (w.isVisible)
					{
						w.drawCall = uidrawCall;
						if (w.hasVertices)
						{
							uidrawCall.isDirty = true;
						}
						return uidrawCall;
					}
				}
				else
				{
					this.mRebuild = true;
				}
				return null;
			}
		}
		this.mRebuild = true;
		return null;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0003D584 File Offset: 0x0003B784
	public void AddWidget(UIWidget w)
	{
		this.mUpdateScroll = true;
		if (this.widgets.size == 0)
		{
			this.widgets.Add(w);
		}
		else if (this.mSortWidgets)
		{
			this.widgets.Add(w);
			this.SortWidgets();
		}
		else if (UIWidget.PanelCompareFunc(w, this.widgets[0]) == -1)
		{
			this.widgets.Insert(0, w);
		}
		else
		{
			int i = this.widgets.size;
			while (i > 0)
			{
				if (UIWidget.PanelCompareFunc(w, this.widgets[--i]) != -1)
				{
					this.widgets.Insert(i + 1, w);
					break;
				}
			}
		}
		this.FindDrawCall(w);
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0003D638 File Offset: 0x0003B838
	public void RemoveWidget(UIWidget w)
	{
		if (this.widgets.Remove(w) && w.drawCall != null)
		{
			int depth = w.depth;
			if (depth == w.drawCall.depthStart || depth == w.drawCall.depthEnd)
			{
				this.mRebuild = true;
			}
			w.drawCall.isDirty = true;
			w.drawCall = null;
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0003D69E File Offset: 0x0003B89E
	public void Refresh()
	{
		this.mRebuild = true;
		if (UIPanel.list.size > 0)
		{
			UIPanel.list[0].LateUpdate();
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0003D6C4 File Offset: 0x0003B8C4
	public virtual Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		Vector4 finalClipRegion = this.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		Vector2 minRect = new Vector2(min.x, min.y);
		Vector2 maxRect;
		maxRect..ctor(max.x, max.y);
		Vector2 minArea;
		minArea..ctor(finalClipRegion.x - num, finalClipRegion.y - num2);
		Vector2 maxArea;
		maxArea..ctor(finalClipRegion.x + num, finalClipRegion.y + num2);
		if (this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.clipSoftness.x;
			minArea.y += this.clipSoftness.y;
			maxArea.x -= this.clipSoftness.x;
			maxArea.y -= this.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		Vector3 vector = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (vector.sqrMagnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += vector;
				targetBounds.center += vector;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + vector, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0003D858 File Offset: 0x0003BA58
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0003D87C File Offset: 0x0003BA7C
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, false, -1);
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0003D886 File Offset: 0x0003BA86
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		return UIPanel.Find(trans, createIfMissing, -1);
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0003D890 File Offset: 0x0003BA90
	public static UIPanel Find(Transform trans, bool createIfMissing, int layer)
	{
		UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<UIPanel>();
			if (uipanel != null)
			{
				return uipanel;
			}
			if (trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (!createIfMissing)
		{
			return null;
		}
		return NGUITools.CreateUI(trans, false, layer);
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0003D8E8 File Offset: 0x0003BAE8
	private Vector2 GetWindowSize()
	{
		UIRoot root = base.root;
		Vector2 vector = NGUITools.screenSize;
		if (root != null)
		{
			vector *= root.GetPixelSizeAdjustment(Mathf.RoundToInt(vector.y));
		}
		return vector;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0003D924 File Offset: 0x0003BB24
	public Vector2 GetViewSize()
	{
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			return new Vector2(this.mClipRange.z, this.mClipRange.w);
		}
		Vector2 vector = NGUITools.screenSize;
		UIRoot root = base.root;
		if (root != null)
		{
			vector *= root.pixelSizeAdjustment;
		}
		return vector;
	}

	// Token: 0x0400060F RID: 1551
	public static BetterList<UIPanel> list = new BetterList<UIPanel>();

	// Token: 0x04000610 RID: 1552
	public UIPanel.OnGeometryUpdated onGeometryUpdated;

	// Token: 0x04000611 RID: 1553
	public bool showInPanelTool = true;

	// Token: 0x04000612 RID: 1554
	public bool generateNormals;

	// Token: 0x04000613 RID: 1555
	public bool widgetsAreStatic;

	// Token: 0x04000614 RID: 1556
	public bool cullWhileDragging;

	// Token: 0x04000615 RID: 1557
	public bool alwaysOnScreen;

	// Token: 0x04000616 RID: 1558
	public bool anchorOffset;

	// Token: 0x04000617 RID: 1559
	public UIPanel.RenderQueue renderQueue;

	// Token: 0x04000618 RID: 1560
	public int startingRenderQueue = 3000;

	// Token: 0x04000619 RID: 1561
	[NonSerialized]
	public BetterList<UIWidget> widgets = new BetterList<UIWidget>();

	// Token: 0x0400061A RID: 1562
	[NonSerialized]
	public BetterList<UIDrawCall> drawCalls = new BetterList<UIDrawCall>();

	// Token: 0x0400061B RID: 1563
	[NonSerialized]
	public Matrix4x4 worldToLocal = Matrix4x4.identity;

	// Token: 0x0400061C RID: 1564
	[NonSerialized]
	public Vector4 drawCallClipRange = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x0400061D RID: 1565
	public UIPanel.OnClippingMoved onClipMove;

	// Token: 0x0400061E RID: 1566
	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	// Token: 0x0400061F RID: 1567
	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04000620 RID: 1568
	[HideInInspector]
	[SerializeField]
	private Vector4 mClipRange = new Vector4(0f, 0f, 300f, 200f);

	// Token: 0x04000621 RID: 1569
	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(4f, 4f);

	// Token: 0x04000622 RID: 1570
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x04000623 RID: 1571
	[HideInInspector]
	[SerializeField]
	private int mSortingOrder;

	// Token: 0x04000624 RID: 1572
	private bool mRebuild;

	// Token: 0x04000625 RID: 1573
	private bool mResized;

	// Token: 0x04000626 RID: 1574
	private Camera mCam;

	// Token: 0x04000627 RID: 1575
	[SerializeField]
	private Vector2 mClipOffset = Vector2.zero;

	// Token: 0x04000628 RID: 1576
	private float mCullTime;

	// Token: 0x04000629 RID: 1577
	private float mUpdateTime;

	// Token: 0x0400062A RID: 1578
	private int mMatrixFrame = -1;

	// Token: 0x0400062B RID: 1579
	private int mAlphaFrameID;

	// Token: 0x0400062C RID: 1580
	private int mLayer = -1;

	// Token: 0x0400062D RID: 1581
	private static float[] mTemp = new float[4];

	// Token: 0x0400062E RID: 1582
	private Vector2 mMin = Vector2.zero;

	// Token: 0x0400062F RID: 1583
	private Vector2 mMax = Vector2.zero;

	// Token: 0x04000630 RID: 1584
	private bool mHalfPixelOffset;

	// Token: 0x04000631 RID: 1585
	private bool mSortWidgets;

	// Token: 0x04000632 RID: 1586
	private bool mUpdateScroll;

	// Token: 0x04000633 RID: 1587
	private UIPanel mParentPanel;

	// Token: 0x04000634 RID: 1588
	private static Vector3[] mCorners = new Vector3[4];

	// Token: 0x04000635 RID: 1589
	private static int mUpdateFrame = -1;

	// Token: 0x04000636 RID: 1590
	private bool mForced;

	// Token: 0x0200122C RID: 4652
	public enum RenderQueue
	{
		// Token: 0x040064DF RID: 25823
		Automatic,
		// Token: 0x040064E0 RID: 25824
		StartAt,
		// Token: 0x040064E1 RID: 25825
		Explicit
	}

	// Token: 0x0200122D RID: 4653
	// (Invoke) Token: 0x0600788B RID: 30859
	public delegate void OnGeometryUpdated();

	// Token: 0x0200122E RID: 4654
	// (Invoke) Token: 0x0600788F RID: 30863
	public delegate void OnClippingMoved(UIPanel panel);
}
