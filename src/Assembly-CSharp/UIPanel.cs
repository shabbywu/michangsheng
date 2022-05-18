using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Panel")]
public class UIPanel : UIRect
{
	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0008E0A8 File Offset: 0x0008C2A8
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

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0000CFA7 File Offset: 0x0000B1A7
	public override bool canBeAnchored
	{
		get
		{
			return this.mClipping > UIDrawCall.Clipping.None;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0000CFB2 File Offset: 0x0000B1B2
	// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0008E0F4 File Offset: 0x0008C2F4
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

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0000CFBA File Offset: 0x0000B1BA
	// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
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

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0000CFEA File Offset: 0x0000B1EA
	// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0000CFF2 File Offset: 0x0000B1F2
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

	// Token: 0x06000ABE RID: 2750 RVA: 0x0008E12C File Offset: 0x0008C32C
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

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0000D00A File Offset: 0x0000B20A
	public float width
	{
		get
		{
			return this.GetViewSize().x;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0000D017 File Offset: 0x0000B217
	public float height
	{
		get
		{
			return this.GetViewSize().y;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0000D024 File Offset: 0x0000B224
	public bool halfPixelOffset
	{
		get
		{
			return this.mHalfPixelOffset;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0000D02C File Offset: 0x0000B22C
	public bool usedForUI
	{
		get
		{
			return this.mCam != null && this.mCam.orthographic;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0008E188 File Offset: 0x0008C388
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

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0000D049 File Offset: 0x0000B249
	// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0000D051 File Offset: 0x0000B251
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

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0000D071 File Offset: 0x0000B271
	public UIPanel parentPanel
	{
		get
		{
			return this.mParentPanel;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0008E1E8 File Offset: 0x0008C3E8
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

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0000D079 File Offset: 0x0000B279
	public bool hasClipping
	{
		get
		{
			return this.mClipping == UIDrawCall.Clipping.SoftClip;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0000D084 File Offset: 0x0000B284
	public bool hasCumulativeClipping
	{
		get
		{
			return this.clipCount != 0;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0000D08F File Offset: 0x0000B28F
	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool clipsChildren
	{
		get
		{
			return this.hasCumulativeClipping;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0000D097 File Offset: 0x0000B297
	// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0008E21C File Offset: 0x0008C41C
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

	// Token: 0x06000ACD RID: 2765 RVA: 0x0008E288 File Offset: 0x0008C488
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

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0000D09F File Offset: 0x0000B29F
	// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
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

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
	// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0008E308 File Offset: 0x0008C508
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

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0008E3F4 File Offset: 0x0008C5F4
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

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
	// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
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

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0008E46C File Offset: 0x0008C66C
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

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0008E6A0 File Offset: 0x0008C8A0
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

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0008E8A4 File Offset: 0x0008CAA4
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

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
	public override void Invalidate(bool includeChildren)
	{
		this.mAlphaFrameID = -1;
		base.Invalidate(includeChildren);
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0008E9FC File Offset: 0x0008CBFC
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

	// Token: 0x06000ADA RID: 2778 RVA: 0x0008EA50 File Offset: 0x0008CC50
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

	// Token: 0x06000ADB RID: 2779 RVA: 0x0008EB74 File Offset: 0x0008CD74
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

	// Token: 0x06000ADC RID: 2780 RVA: 0x0008EC98 File Offset: 0x0008CE98
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

	// Token: 0x06000ADD RID: 2781 RVA: 0x0008ED30 File Offset: 0x0008CF30
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

	// Token: 0x06000ADE RID: 2782 RVA: 0x0008EDA8 File Offset: 0x0008CFA8
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

	// Token: 0x06000ADF RID: 2783 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		this.mRebuild = true;
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0008EDFC File Offset: 0x0008CFFC
	public void SetDirty()
	{
		for (int i = 0; i < this.drawCalls.size; i++)
		{
			this.drawCalls.buffer[i].isDirty = true;
		}
		this.Invalidate(true);
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0008EE3C File Offset: 0x0008D03C
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

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0008EE9C File Offset: 0x0008D09C
	private void FindParent()
	{
		Transform parent = base.cachedTransform.parent;
		this.mParentPanel = ((parent != null) ? NGUITools.FindInParents<UIPanel>(parent.gameObject) : null);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		this.FindParent();
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0008EED4 File Offset: 0x0008D0D4
	protected override void OnStart()
	{
		this.mLayer = this.mGo.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((uicamera != null) ? uicamera.cachedCamera : NGUITools.FindCameraForLayer(this.mLayer));
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0000D0FE File Offset: 0x0000B2FE
	protected override void OnEnable()
	{
		this.mRebuild = true;
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		base.OnEnable();
		this.mMatrixFrame = -1;
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0008EF20 File Offset: 0x0008D120
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

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0008EFCC File Offset: 0x0008D1CC
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

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0008F050 File Offset: 0x0008D250
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

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0008F114 File Offset: 0x0008D314
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

	// Token: 0x06000AEA RID: 2794 RVA: 0x0008F658 File Offset: 0x0008D858
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

	// Token: 0x06000AEB RID: 2795 RVA: 0x0008F750 File Offset: 0x0008D950
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

	// Token: 0x06000AEC RID: 2796 RVA: 0x0000D122 File Offset: 0x0000B322
	public void SortWidgets()
	{
		this.mSortWidgets = false;
		this.widgets.Sort(new BetterList<UIWidget>.CompareFunc(UIWidget.PanelCompareFunc));
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0008F800 File Offset: 0x0008DA00
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

	// Token: 0x06000AEE RID: 2798 RVA: 0x0008FA14 File Offset: 0x0008DC14
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

	// Token: 0x06000AEF RID: 2799 RVA: 0x0008FAFC File Offset: 0x0008DCFC
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

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0008FD28 File Offset: 0x0008DF28
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

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0008FDD0 File Offset: 0x0008DFD0
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

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0008FF18 File Offset: 0x0008E118
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

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00090010 File Offset: 0x0008E210
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

	// Token: 0x06000AF4 RID: 2804 RVA: 0x000900C4 File Offset: 0x0008E2C4
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

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0000D142 File Offset: 0x0000B342
	public void Refresh()
	{
		this.mRebuild = true;
		if (UIPanel.list.size > 0)
		{
			UIPanel.list[0].LateUpdate();
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0009012C File Offset: 0x0008E32C
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

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0009021C File Offset: 0x0008E41C
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

	// Token: 0x06000AF8 RID: 2808 RVA: 0x000902C0 File Offset: 0x0008E4C0
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0000D168 File Offset: 0x0000B368
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, false, -1);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0000D172 File Offset: 0x0000B372
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		return UIPanel.Find(trans, createIfMissing, -1);
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x000902E4 File Offset: 0x0008E4E4
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

	// Token: 0x06000AFC RID: 2812 RVA: 0x0009033C File Offset: 0x0008E53C
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

	// Token: 0x06000AFD RID: 2813 RVA: 0x00090378 File Offset: 0x0008E578
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

	// Token: 0x0400079E RID: 1950
	public static BetterList<UIPanel> list = new BetterList<UIPanel>();

	// Token: 0x0400079F RID: 1951
	public UIPanel.OnGeometryUpdated onGeometryUpdated;

	// Token: 0x040007A0 RID: 1952
	public bool showInPanelTool = true;

	// Token: 0x040007A1 RID: 1953
	public bool generateNormals;

	// Token: 0x040007A2 RID: 1954
	public bool widgetsAreStatic;

	// Token: 0x040007A3 RID: 1955
	public bool cullWhileDragging;

	// Token: 0x040007A4 RID: 1956
	public bool alwaysOnScreen;

	// Token: 0x040007A5 RID: 1957
	public bool anchorOffset;

	// Token: 0x040007A6 RID: 1958
	public UIPanel.RenderQueue renderQueue;

	// Token: 0x040007A7 RID: 1959
	public int startingRenderQueue = 3000;

	// Token: 0x040007A8 RID: 1960
	[NonSerialized]
	public BetterList<UIWidget> widgets = new BetterList<UIWidget>();

	// Token: 0x040007A9 RID: 1961
	[NonSerialized]
	public BetterList<UIDrawCall> drawCalls = new BetterList<UIDrawCall>();

	// Token: 0x040007AA RID: 1962
	[NonSerialized]
	public Matrix4x4 worldToLocal = Matrix4x4.identity;

	// Token: 0x040007AB RID: 1963
	[NonSerialized]
	public Vector4 drawCallClipRange = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x040007AC RID: 1964
	public UIPanel.OnClippingMoved onClipMove;

	// Token: 0x040007AD RID: 1965
	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	// Token: 0x040007AE RID: 1966
	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x040007AF RID: 1967
	[HideInInspector]
	[SerializeField]
	private Vector4 mClipRange = new Vector4(0f, 0f, 300f, 200f);

	// Token: 0x040007B0 RID: 1968
	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(4f, 4f);

	// Token: 0x040007B1 RID: 1969
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x040007B2 RID: 1970
	[HideInInspector]
	[SerializeField]
	private int mSortingOrder;

	// Token: 0x040007B3 RID: 1971
	private bool mRebuild;

	// Token: 0x040007B4 RID: 1972
	private bool mResized;

	// Token: 0x040007B5 RID: 1973
	private Camera mCam;

	// Token: 0x040007B6 RID: 1974
	[SerializeField]
	private Vector2 mClipOffset = Vector2.zero;

	// Token: 0x040007B7 RID: 1975
	private float mCullTime;

	// Token: 0x040007B8 RID: 1976
	private float mUpdateTime;

	// Token: 0x040007B9 RID: 1977
	private int mMatrixFrame = -1;

	// Token: 0x040007BA RID: 1978
	private int mAlphaFrameID;

	// Token: 0x040007BB RID: 1979
	private int mLayer = -1;

	// Token: 0x040007BC RID: 1980
	private static float[] mTemp = new float[4];

	// Token: 0x040007BD RID: 1981
	private Vector2 mMin = Vector2.zero;

	// Token: 0x040007BE RID: 1982
	private Vector2 mMax = Vector2.zero;

	// Token: 0x040007BF RID: 1983
	private bool mHalfPixelOffset;

	// Token: 0x040007C0 RID: 1984
	private bool mSortWidgets;

	// Token: 0x040007C1 RID: 1985
	private bool mUpdateScroll;

	// Token: 0x040007C2 RID: 1986
	private UIPanel mParentPanel;

	// Token: 0x040007C3 RID: 1987
	private static Vector3[] mCorners = new Vector3[4];

	// Token: 0x040007C4 RID: 1988
	private static int mUpdateFrame = -1;

	// Token: 0x040007C5 RID: 1989
	private bool mForced;

	// Token: 0x02000115 RID: 277
	public enum RenderQueue
	{
		// Token: 0x040007C7 RID: 1991
		Automatic,
		// Token: 0x040007C8 RID: 1992
		StartAt,
		// Token: 0x040007C9 RID: 1993
		Explicit
	}

	// Token: 0x02000116 RID: 278
	// (Invoke) Token: 0x06000B01 RID: 2817
	public delegate void OnGeometryUpdated();

	// Token: 0x02000117 RID: 279
	// (Invoke) Token: 0x06000B05 RID: 2821
	public delegate void OnClippingMoved(UIPanel panel);
}
