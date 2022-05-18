using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000DD RID: 221
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Widget")]
public class UIWidget : UIRect
{
	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000885 RID: 2181 RVA: 0x0000B05A File Offset: 0x0000925A
	// (set) Token: 0x06000886 RID: 2182 RVA: 0x0000B062 File Offset: 0x00009262
	public Vector4 drawRegion
	{
		get
		{
			return this.mDrawRegion;
		}
		set
		{
			if (this.mDrawRegion != value)
			{
				this.mDrawRegion = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000887 RID: 2183 RVA: 0x0000B08D File Offset: 0x0000928D
	public Vector2 pivotOffset
	{
		get
		{
			return NGUIMath.GetPivotOffset(this.pivot);
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000888 RID: 2184 RVA: 0x0000B09A File Offset: 0x0000929A
	// (set) Token: 0x06000889 RID: 2185 RVA: 0x0008483C File Offset: 0x00082A3C
	public int width
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			int minWidth = this.minWidth;
			if (value < minWidth)
			{
				value = minWidth;
			}
			if (this.mWidth != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnHeight)
			{
				if (this.isAnchoredHorizontally)
				{
					if (this.leftAnchor.target != null && this.rightAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Left || this.mPivot == UIWidget.Pivot.TopLeft)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
							return;
						}
						if (this.mPivot == UIWidget.Pivot.BottomRight || this.mPivot == UIWidget.Pivot.Right || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
							return;
						}
						int num = value - this.mWidth;
						num -= (num & 1);
						if (num != 0)
						{
							NGUIMath.AdjustWidget(this, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f, 0f);
							return;
						}
					}
					else
					{
						if (this.leftAnchor.target != null)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
							return;
						}
						NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
						return;
					}
				}
				else
				{
					this.SetDimensions(value, this.mHeight);
				}
			}
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600088A RID: 2186 RVA: 0x0000B0A2 File Offset: 0x000092A2
	// (set) Token: 0x0600088B RID: 2187 RVA: 0x000849B0 File Offset: 0x00082BB0
	public int height
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			int minHeight = this.minHeight;
			if (value < minHeight)
			{
				value = minHeight;
			}
			if (this.mHeight != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnWidth)
			{
				if (this.isAnchoredVertically)
				{
					if (this.bottomAnchor.target != null && this.topAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Bottom || this.mPivot == UIWidget.Pivot.BottomRight)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
							return;
						}
						if (this.mPivot == UIWidget.Pivot.TopLeft || this.mPivot == UIWidget.Pivot.Top || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
							return;
						}
						int num = value - this.mHeight;
						num -= (num & 1);
						if (num != 0)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f);
							return;
						}
					}
					else
					{
						if (this.bottomAnchor.target != null)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
							return;
						}
						NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
						return;
					}
				}
				else
				{
					this.SetDimensions(this.mWidth, value);
				}
			}
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x0600088C RID: 2188 RVA: 0x0000B0AA File Offset: 0x000092AA
	// (set) Token: 0x0600088D RID: 2189 RVA: 0x00084B24 File Offset: 0x00082D24
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				bool includeChildren = this.mColor.a != value.a;
				this.mColor = value;
				this.Invalidate(includeChildren);
			}
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x0600088E RID: 2190 RVA: 0x0000B0B2 File Offset: 0x000092B2
	// (set) Token: 0x0600088F RID: 2191 RVA: 0x0000B0BF File Offset: 0x000092BF
	public override float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			if (this.mColor.a != value)
			{
				this.mColor.a = value;
				this.Invalidate(true);
			}
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000890 RID: 2192 RVA: 0x0000B0E2 File Offset: 0x000092E2
	public bool isVisible
	{
		get
		{
			return this.mIsVisibleByPanel && this.mIsVisibleByAlpha && this.mIsInFront && this.finalAlpha > 0.001f && NGUITools.GetActive(this);
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000891 RID: 2193 RVA: 0x0000B111 File Offset: 0x00009311
	public bool hasVertices
	{
		get
		{
			return this.geometry != null && this.geometry.hasVertices;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000B128 File Offset: 0x00009328
	// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000B130 File Offset: 0x00009330
	public UIWidget.Pivot rawPivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000B128 File Offset: 0x00009328
	// (set) Token: 0x06000895 RID: 2197 RVA: 0x00084B64 File Offset: 0x00082D64
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				Vector3 vector = this.worldCorners[0];
				this.mPivot = value;
				this.mChanged = true;
				Vector3 vector2 = this.worldCorners[0];
				Transform cachedTransform = base.cachedTransform;
				Vector3 vector3 = cachedTransform.position;
				float z = cachedTransform.localPosition.z;
				vector3.x += vector.x - vector2.x;
				vector3.y += vector.y - vector2.y;
				base.cachedTransform.position = vector3;
				vector3 = base.cachedTransform.localPosition;
				vector3.x = Mathf.Round(vector3.x);
				vector3.y = Mathf.Round(vector3.y);
				vector3.z = z;
				base.cachedTransform.localPosition = vector3;
			}
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000896 RID: 2198 RVA: 0x0000B156 File Offset: 0x00009356
	// (set) Token: 0x06000897 RID: 2199 RVA: 0x00084C40 File Offset: 0x00082E40
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
				if (this.panel != null)
				{
					this.panel.RemoveWidget(this);
				}
				this.mDepth = value;
				if (this.panel != null)
				{
					this.panel.AddWidget(this);
					if (!Application.isPlaying)
					{
						this.panel.SortWidgets();
						this.panel.RebuildAllDrawCalls();
					}
				}
			}
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000898 RID: 2200 RVA: 0x00084CB0 File Offset: 0x00082EB0
	public int raycastDepth
	{
		get
		{
			if (this.panel == null)
			{
				this.CreatePanel();
			}
			if (!(this.panel != null))
			{
				return this.mDepth;
			}
			return this.mDepth + this.panel.depth * 1000;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000899 RID: 2201 RVA: 0x00084D00 File Offset: 0x00082F00
	public override Vector3[] localCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			this.mCorners[0] = new Vector3(num, num2);
			this.mCorners[1] = new Vector3(num, num4);
			this.mCorners[2] = new Vector3(num3, num4);
			this.mCorners[3] = new Vector3(num3, num2);
			return this.mCorners;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600089A RID: 2202 RVA: 0x00084D98 File Offset: 0x00082F98
	public virtual Vector2 localSize
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return localCorners[2] - localCorners[0];
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x00084DC4 File Offset: 0x00082FC4
	public Vector3 localCenter
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x0600089C RID: 2204 RVA: 0x00084DF0 File Offset: 0x00082FF0
	public override Vector3[] worldCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			Transform cachedTransform = base.cachedTransform;
			this.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			this.mCorners[1] = cachedTransform.TransformPoint(num, num4, 0f);
			this.mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0f);
			this.mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000B15E File Offset: 0x0000935E
	public Vector3 worldCenter
	{
		get
		{
			return base.cachedTransform.TransformPoint(this.localCenter);
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x0600089E RID: 2206 RVA: 0x00084EAC File Offset: 0x000830AC
	public virtual Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			return new Vector4((this.mDrawRegion.x == 0f) ? num : Mathf.Lerp(num, num3, this.mDrawRegion.x), (this.mDrawRegion.y == 0f) ? num2 : Mathf.Lerp(num2, num4, this.mDrawRegion.y), (this.mDrawRegion.z == 1f) ? num3 : Mathf.Lerp(num, num3, this.mDrawRegion.z), (this.mDrawRegion.w == 1f) ? num4 : Mathf.Lerp(num2, num4, this.mDrawRegion.w));
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x0600089F RID: 2207 RVA: 0x0000B171 File Offset: 0x00009371
	// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0000B174 File Offset: 0x00009374
	public virtual Material material
	{
		get
		{
			return null;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no material setter");
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00084F94 File Offset: 0x00083194
	// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0000B18B File Offset: 0x0000938B
	public virtual Texture mainTexture
	{
		get
		{
			Material material = this.material;
			if (!(material != null))
			{
				return null;
			}
			return material.mainTexture;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no mainTexture setter");
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00084FBC File Offset: 0x000831BC
	// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0000B1A2 File Offset: 0x000093A2
	public virtual Shader shader
	{
		get
		{
			Material material = this.material;
			if (!(material != null))
			{
				return null;
			}
			return material.shader;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no shader setter");
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0000B1B9 File Offset: 0x000093B9
	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0000B1C0 File Offset: 0x000093C0
	public bool hasBoxCollider
	{
		get
		{
			return base.GetComponent<Collider>() as BoxCollider != null || base.GetComponent<BoxCollider2D>() != null;
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00084FE4 File Offset: 0x000831E4
	public void SetDimensions(int w, int h)
	{
		if (this.mWidth != w || this.mHeight != h)
		{
			this.mWidth = w;
			this.mHeight = h;
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnWidth)
			{
				this.mHeight = Mathf.RoundToInt((float)this.mWidth / this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				this.mWidth = Mathf.RoundToInt((float)this.mHeight * this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.Free)
			{
				this.aspectRatio = (float)this.mWidth / (float)this.mHeight;
			}
			this.mMoved = true;
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00085094 File Offset: 0x00083294
	public override Vector3[] GetSides(Transform relativeTo)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = -pivotOffset.x * (float)this.mWidth;
		float num2 = -pivotOffset.y * (float)this.mHeight;
		float num3 = num + (float)this.mWidth;
		float num4 = num2 + (float)this.mHeight;
		float num5 = (num + num3) * 0.5f;
		float num6 = (num2 + num4) * 0.5f;
		Transform cachedTransform = base.cachedTransform;
		this.mCorners[0] = cachedTransform.TransformPoint(num, num6, 0f);
		this.mCorners[1] = cachedTransform.TransformPoint(num5, num4, 0f);
		this.mCorners[2] = cachedTransform.TransformPoint(num3, num6, 0f);
		this.mCorners[3] = cachedTransform.TransformPoint(num5, num2, 0f);
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				this.mCorners[i] = relativeTo.InverseTransformPoint(this.mCorners[i]);
			}
		}
		return this.mCorners;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0000B1E3 File Offset: 0x000093E3
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			this.UpdateFinalAlpha(frameID);
		}
		return this.finalAlpha;
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x000851A4 File Offset: 0x000833A4
	protected void UpdateFinalAlpha(int frameID)
	{
		if (!this.mIsVisibleByAlpha || !this.mIsInFront)
		{
			this.finalAlpha = 0f;
			return;
		}
		UIRect parent = base.parent;
		this.finalAlpha = ((base.parent != null) ? (parent.CalculateFinalAlpha(frameID) * this.mColor.a) : this.mColor.a);
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00085208 File Offset: 0x00083408
	public override void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		this.mAlphaFrameID = -1;
		if (this.panel != null)
		{
			bool visibleByPanel = (!this.hideIfOffScreen && !this.panel.hasCumulativeClipping) || this.panel.IsVisible(this);
			this.UpdateVisibility(this.CalculateCumulativeAlpha(Time.frameCount) > 0.001f, visibleByPanel);
			this.UpdateFinalAlpha(Time.frameCount);
			if (includeChildren)
			{
				base.Invalidate(true);
			}
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00085288 File Offset: 0x00083488
	public float CalculateCumulativeAlpha(int frameID)
	{
		UIRect parent = base.parent;
		if (!(parent != null))
		{
			return this.mColor.a;
		}
		return parent.CalculateFinalAlpha(frameID) * this.mColor.a;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x000852C4 File Offset: 0x000834C4
	public override void SetRect(float x, float y, float width, float height)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = Mathf.Lerp(x, x + width, pivotOffset.x);
		float num2 = Mathf.Lerp(y, y + height, pivotOffset.y);
		int num3 = Mathf.FloorToInt(width + 0.5f);
		int num4 = Mathf.FloorToInt(height + 0.5f);
		if (pivotOffset.x == 0.5f)
		{
			num3 = num3 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f)
		{
			num4 = num4 >> 1 << 1;
		}
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(num + 0.5f);
		localPosition.y = Mathf.Floor(num2 + 0.5f);
		if (num3 < this.minWidth)
		{
			num3 = this.minWidth;
		}
		if (num4 < this.minHeight)
		{
			num4 = this.minHeight;
		}
		transform.localPosition = localPosition;
		this.width = num3;
		this.height = num4;
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

	// Token: 0x060008AE RID: 2222 RVA: 0x0000B202 File Offset: 0x00009402
	public void ResizeCollider()
	{
		if (NGUITools.GetActive(this))
		{
			NGUITools.UpdateWidgetCollider(base.gameObject);
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00085448 File Offset: 0x00083648
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int FullCompareFunc(UIWidget left, UIWidget right)
	{
		int num = UIPanel.CompareFunc(left.panel, right.panel);
		if (num != 0)
		{
			return num;
		}
		return UIWidget.PanelCompareFunc(left, right);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00085474 File Offset: 0x00083674
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int PanelCompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		Material material = left.material;
		Material material2 = right.material;
		if (material == material2)
		{
			return 0;
		}
		if (material != null)
		{
			return -1;
		}
		if (material2 != null)
		{
			return 1;
		}
		if (material.GetInstanceID() >= material2.GetInstanceID())
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0000B217 File Offset: 0x00009417
	public Bounds CalculateBounds()
	{
		return this.CalculateBounds(null);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x000854E4 File Offset: 0x000836E4
	public Bounds CalculateBounds(Transform relativeParent)
	{
		if (relativeParent == null)
		{
			Vector3[] localCorners = this.localCorners;
			Bounds result;
			result..ctor(localCorners[0], Vector3.zero);
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(localCorners[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = relativeParent.worldToLocalMatrix;
		Vector3[] worldCorners = this.worldCorners;
		Bounds result2;
		result2..ctor(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[0]), Vector3.zero);
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[j]));
		}
		return result2;
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0000B220 File Offset: 0x00009420
	public void SetDirty()
	{
		if (this.drawCall != null)
		{
			this.drawCall.isDirty = true;
			return;
		}
		if (this.isVisible && this.hasVertices)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0000B254 File Offset: 0x00009454
	protected void RemoveFromPanel()
	{
		if (this.panel != null)
		{
			this.panel.RemoveWidget(this);
			this.panel = null;
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00085584 File Offset: 0x00083784
	public virtual void MarkAsChanged()
	{
		if (NGUITools.GetActive(this))
		{
			this.mChanged = true;
			if (this.panel != null && base.enabled && NGUITools.GetActive(base.gameObject) && !this.mPlayMode)
			{
				this.SetDirty();
				this.CheckLayer();
			}
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x000855D8 File Offset: 0x000837D8
	public UIPanel CreatePanel()
	{
		if (this.mStarted && this.panel == null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.panel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != null)
			{
				this.mParentFound = false;
				this.panel.AddWidget(this);
				this.CheckLayer();
				this.Invalidate(true);
			}
		}
		return this.panel;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x00085664 File Offset: 0x00083864
	public void CheckLayer()
	{
		if (this.panel != null && this.panel.gameObject.layer != base.gameObject.layer)
		{
			Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.panel.gameObject.layer;
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x000856C4 File Offset: 0x000838C4
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		if (this.panel != null)
		{
			UIPanel uipanel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != uipanel)
			{
				this.RemoveFromPanel();
				this.CreatePanel();
			}
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0000B277 File Offset: 0x00009477
	protected virtual void Awake()
	{
		this.mGo = base.gameObject;
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00085718 File Offset: 0x00083918
	protected override void OnInit()
	{
		base.OnInit();
		this.RemoveFromPanel();
		this.mMoved = true;
		if (this.mWidth == 100 && this.mHeight == 100 && base.cachedTransform.localScale.magnitude > 8f)
		{
			this.UpgradeFrom265();
			base.cachedTransform.localScale = Vector3.one;
		}
		base.Update();
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00085784 File Offset: 0x00083984
	protected virtual void UpgradeFrom265()
	{
		Vector3 localScale = base.cachedTransform.localScale;
		this.mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		this.mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0000B290 File Offset: 0x00009490
	protected override void OnStart()
	{
		this.CreatePanel();
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x000857D8 File Offset: 0x000839D8
	protected override void OnAnchor()
	{
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector3 localPosition = cachedTransform.localPosition;
		Vector2 pivotOffset = this.pivotOffset;
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
				this.mIsInFront = true;
			}
			else
			{
				Vector3 localPos = base.GetLocalPos(this.leftAnchor, parent);
				num = localPos.x + (float)this.leftAnchor.absolute;
				num3 = localPos.y + (float)this.bottomAnchor.absolute;
				num2 = localPos.x + (float)this.rightAnchor.absolute;
				num4 = localPos.y + (float)this.topAnchor.absolute;
				this.mIsInFront = (!this.hideIfOffScreen || localPos.z >= 0f);
			}
		}
		else
		{
			this.mIsInFront = true;
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
				num = localPosition.x - pivotOffset.x * (float)this.mWidth;
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
				num2 = localPosition.x - pivotOffset.x * (float)this.mWidth + (float)this.mWidth;
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
				num3 = localPosition.y - pivotOffset.y * (float)this.mHeight;
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
				num4 = localPosition.y - pivotOffset.y * (float)this.mHeight + (float)this.mHeight;
			}
		}
		Vector3 vector;
		vector..ctor(Mathf.Lerp(num, num2, pivotOffset.x), Mathf.Lerp(num3, num4, pivotOffset.y), localPosition.z);
		int num5 = Mathf.FloorToInt(num2 - num + 0.5f);
		int num6 = Mathf.FloorToInt(num4 - num3 + 0.5f);
		if (this.keepAspectRatio != UIWidget.AspectRatioSource.Free && this.aspectRatio != 0f)
		{
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				num5 = Mathf.RoundToInt((float)num6 * this.aspectRatio);
			}
			else
			{
				num6 = Mathf.RoundToInt((float)num5 / this.aspectRatio);
			}
		}
		if (num5 < this.minWidth)
		{
			num5 = this.minWidth;
		}
		if (num6 < this.minHeight)
		{
			num6 = this.minHeight;
		}
		if (Vector3.SqrMagnitude(localPosition - vector) > 0.001f)
		{
			base.cachedTransform.localPosition = vector;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
		}
		if (this.mWidth != num5 || this.mHeight != num6)
		{
			this.mWidth = num5;
			this.mHeight = num6;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0000B299 File Offset: 0x00009499
	protected override void OnUpdate()
	{
		if (this.panel == null)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0000B2B0 File Offset: 0x000094B0
	private void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			this.MarkAsChanged();
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0000B2BB File Offset: 0x000094BB
	protected override void OnDisable()
	{
		this.RemoveFromPanel();
		base.OnDisable();
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0000B2C9 File Offset: 0x000094C9
	private void OnDestroy()
	{
		this.RemoveFromPanel();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0000B2D1 File Offset: 0x000094D1
	public bool UpdateVisibility(bool visibleByAlpha, bool visibleByPanel)
	{
		if (this.mIsVisibleByAlpha != visibleByAlpha || this.mIsVisibleByPanel != visibleByPanel)
		{
			this.mChanged = true;
			this.mIsVisibleByAlpha = visibleByAlpha;
			this.mIsVisibleByPanel = visibleByPanel;
			return true;
		}
		return false;
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00085D90 File Offset: 0x00083F90
	public bool UpdateTransform(int frame)
	{
		if (!this.mMoved && !this.panel.widgetsAreStatic && base.cachedTransform.hasChanged)
		{
			this.mTrans.hasChanged = false;
			this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
			this.mMatrixFrame = frame;
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			Transform cachedTransform = base.cachedTransform;
			Vector3 vector = cachedTransform.TransformPoint(num, num2, 0f);
			Vector3 vector2 = cachedTransform.TransformPoint(num3, num4, 0f);
			vector = this.panel.worldToLocal.MultiplyPoint3x4(vector);
			vector2 = this.panel.worldToLocal.MultiplyPoint3x4(vector2);
			if (Vector3.SqrMagnitude(this.mOldV0 - vector) > 1E-06f || Vector3.SqrMagnitude(this.mOldV1 - vector2) > 1E-06f)
			{
				this.mMoved = true;
				this.mOldV0 = vector;
				this.mOldV1 = vector2;
			}
		}
		if (this.mMoved && this.onChange != null)
		{
			this.onChange();
		}
		return this.mMoved || this.mChanged;
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00085EF4 File Offset: 0x000840F4
	public bool UpdateGeometry(int frame)
	{
		float num = this.CalculateFinalAlpha(frame);
		if (this.mIsVisibleByAlpha && this.mLastAlpha != num)
		{
			this.mChanged = true;
		}
		this.mLastAlpha = num;
		if (this.mChanged)
		{
			this.mChanged = false;
			if (this.mIsVisibleByAlpha && num > 0.001f && this.shader != null)
			{
				bool hasVertices = this.geometry.hasVertices;
				if (this.fillGeometry)
				{
					this.geometry.Clear();
					this.OnFill(this.geometry.verts, this.geometry.uvs, this.geometry.cols);
				}
				if (this.geometry.hasVertices)
				{
					if (this.mMatrixFrame != frame)
					{
						this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
						this.mMatrixFrame = frame;
					}
					this.geometry.ApplyTransform(this.mLocalToPanel);
					this.mMoved = false;
					return true;
				}
				return hasVertices;
			}
			else if (this.geometry.hasVertices)
			{
				if (this.fillGeometry)
				{
					this.geometry.Clear();
				}
				this.mMoved = false;
				return true;
			}
		}
		else if (this.mMoved && this.geometry.hasVertices)
		{
			if (this.mMatrixFrame != frame)
			{
				this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
				this.mMatrixFrame = frame;
			}
			this.geometry.ApplyTransform(this.mLocalToPanel);
			this.mMoved = false;
			return true;
		}
		this.mMoved = false;
		return false;
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0000B2FD File Offset: 0x000094FD
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		this.geometry.WriteToBuffers(v, u, c, n, t);
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00086094 File Offset: 0x00084294
	public virtual void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		base.cachedTransform.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0000B311 File Offset: 0x00009511
	public virtual int minWidth
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0000B311 File Offset: 0x00009511
	public virtual int minHeight
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0000B314 File Offset: 0x00009514
	// (set) Token: 0x060008CA RID: 2250 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
		set
		{
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
	}

	// Token: 0x040005FF RID: 1535
	[HideInInspector]
	[SerializeField]
	protected Color mColor = Color.white;

	// Token: 0x04000600 RID: 1536
	[HideInInspector]
	[SerializeField]
	protected UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x04000601 RID: 1537
	[HideInInspector]
	[SerializeField]
	protected int mWidth = 100;

	// Token: 0x04000602 RID: 1538
	[HideInInspector]
	[SerializeField]
	protected int mHeight = 100;

	// Token: 0x04000603 RID: 1539
	[HideInInspector]
	[SerializeField]
	protected int mDepth;

	// Token: 0x04000604 RID: 1540
	public UIWidget.OnDimensionsChanged onChange;

	// Token: 0x04000605 RID: 1541
	public UIWidget.OnPostFillCallback onPostFill;

	// Token: 0x04000606 RID: 1542
	public bool autoResizeBoxCollider;

	// Token: 0x04000607 RID: 1543
	public bool hideIfOffScreen;

	// Token: 0x04000608 RID: 1544
	public UIWidget.AspectRatioSource keepAspectRatio;

	// Token: 0x04000609 RID: 1545
	public float aspectRatio = 1f;

	// Token: 0x0400060A RID: 1546
	public UIWidget.HitCheck hitCheck;

	// Token: 0x0400060B RID: 1547
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x0400060C RID: 1548
	[NonSerialized]
	public UIGeometry geometry = new UIGeometry();

	// Token: 0x0400060D RID: 1549
	[NonSerialized]
	public bool fillGeometry = true;

	// Token: 0x0400060E RID: 1550
	[NonSerialized]
	protected bool mPlayMode = true;

	// Token: 0x0400060F RID: 1551
	[NonSerialized]
	protected Vector4 mDrawRegion = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x04000610 RID: 1552
	[NonSerialized]
	private Matrix4x4 mLocalToPanel;

	// Token: 0x04000611 RID: 1553
	[NonSerialized]
	private bool mIsVisibleByAlpha = true;

	// Token: 0x04000612 RID: 1554
	[NonSerialized]
	private bool mIsVisibleByPanel = true;

	// Token: 0x04000613 RID: 1555
	[NonSerialized]
	private bool mIsInFront = true;

	// Token: 0x04000614 RID: 1556
	[NonSerialized]
	private float mLastAlpha;

	// Token: 0x04000615 RID: 1557
	[NonSerialized]
	private bool mMoved;

	// Token: 0x04000616 RID: 1558
	[NonSerialized]
	public UIDrawCall drawCall;

	// Token: 0x04000617 RID: 1559
	[NonSerialized]
	protected Vector3[] mCorners = new Vector3[4];

	// Token: 0x04000618 RID: 1560
	[NonSerialized]
	private int mAlphaFrameID = -1;

	// Token: 0x04000619 RID: 1561
	private int mMatrixFrame = -1;

	// Token: 0x0400061A RID: 1562
	private Vector3 mOldV0;

	// Token: 0x0400061B RID: 1563
	private Vector3 mOldV1;

	// Token: 0x020000DE RID: 222
	public enum Pivot
	{
		// Token: 0x0400061D RID: 1565
		TopLeft,
		// Token: 0x0400061E RID: 1566
		Top,
		// Token: 0x0400061F RID: 1567
		TopRight,
		// Token: 0x04000620 RID: 1568
		Left,
		// Token: 0x04000621 RID: 1569
		Center,
		// Token: 0x04000622 RID: 1570
		Right,
		// Token: 0x04000623 RID: 1571
		BottomLeft,
		// Token: 0x04000624 RID: 1572
		Bottom,
		// Token: 0x04000625 RID: 1573
		BottomRight
	}

	// Token: 0x020000DF RID: 223
	// (Invoke) Token: 0x060008CE RID: 2254
	public delegate void OnDimensionsChanged();

	// Token: 0x020000E0 RID: 224
	// (Invoke) Token: 0x060008D2 RID: 2258
	public delegate void OnPostFillCallback(UIWidget widget, int bufferOffset, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols);

	// Token: 0x020000E1 RID: 225
	public enum AspectRatioSource
	{
		// Token: 0x04000627 RID: 1575
		Free,
		// Token: 0x04000628 RID: 1576
		BasedOnWidth,
		// Token: 0x04000629 RID: 1577
		BasedOnHeight
	}

	// Token: 0x020000E2 RID: 226
	// (Invoke) Token: 0x060008D6 RID: 2262
	public delegate bool HitCheck(Vector3 worldPos);
}
