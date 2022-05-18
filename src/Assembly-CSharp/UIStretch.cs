using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	// Token: 0x06000B3F RID: 2879 RVA: 0x00091228 File Offset: 0x0008F428
	private void Awake()
	{
		this.mAnim = base.GetComponent<Animation>();
		this.mRect = default(Rect);
		this.mTrans = base.transform;
		this.mWidget = base.GetComponent<UIWidget>();
		this.mSprite = base.GetComponent<UISprite>();
		this.mPanel = base.GetComponent<UIPanel>();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0000D496 File Offset: 0x0000B696
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
	private void ScreenSizeChanged()
	{
		if (this.mStarted && this.runOnlyOnce)
		{
			this.Update();
		}
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x000912A0 File Offset: 0x0008F4A0
	private void Start()
	{
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		this.Update();
		this.mStarted = true;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00091324 File Offset: 0x0008F524
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.isPlaying)
		{
			return;
		}
		if (this.style != UIStretch.Style.None)
		{
			UIWidget uiwidget = (this.container == null) ? null : this.container.GetComponent<UIWidget>();
			UIPanel uipanel = (this.container == null && uiwidget == null) ? null : this.container.GetComponent<UIPanel>();
			float num = 1f;
			if (uiwidget != null)
			{
				Bounds bounds = uiwidget.CalculateBounds(base.transform.parent);
				this.mRect.x = bounds.min.x;
				this.mRect.y = bounds.min.y;
				this.mRect.width = bounds.size.x;
				this.mRect.height = bounds.size.y;
			}
			else if (uipanel != null)
			{
				if (uipanel.clipping == UIDrawCall.Clipping.None)
				{
					float num2 = (this.mRoot != null) ? ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f) : 0.5f;
					this.mRect.xMin = (float)(-(float)Screen.width) * num2;
					this.mRect.yMin = (float)(-(float)Screen.height) * num2;
					this.mRect.xMax = -this.mRect.xMin;
					this.mRect.yMax = -this.mRect.yMin;
				}
				else
				{
					Vector4 finalClipRegion = uipanel.finalClipRegion;
					this.mRect.x = finalClipRegion.x - finalClipRegion.z * 0.5f;
					this.mRect.y = finalClipRegion.y - finalClipRegion.w * 0.5f;
					this.mRect.width = finalClipRegion.z;
					this.mRect.height = finalClipRegion.w;
				}
			}
			else if (this.container != null)
			{
				Transform parent = base.transform.parent;
				Bounds bounds2 = (parent != null) ? NGUIMath.CalculateRelativeWidgetBounds(parent, this.container.transform) : NGUIMath.CalculateRelativeWidgetBounds(this.container.transform);
				this.mRect.x = bounds2.min.x;
				this.mRect.y = bounds2.min.y;
				this.mRect.width = bounds2.size.x;
				this.mRect.height = bounds2.size.y;
			}
			else
			{
				if (!(this.uiCamera != null))
				{
					return;
				}
				this.mRect = this.uiCamera.pixelRect;
				if (this.mRoot != null)
				{
					num = this.mRoot.pixelSizeAdjustment;
				}
			}
			float num3 = this.mRect.width;
			float num4 = this.mRect.height;
			if (num != 1f && num4 > 1f)
			{
				float num5 = (float)this.mRoot.activeHeight / num4;
				num3 *= num5;
				num4 *= num5;
			}
			Vector3 vector = (this.mWidget != null) ? new Vector3((float)this.mWidget.width, (float)this.mWidget.height) : this.mTrans.localScale;
			if (this.style == UIStretch.Style.BasedOnHeight)
			{
				vector.x = this.relativeSize.x * num4;
				vector.y = this.relativeSize.y * num4;
			}
			else if (this.style == UIStretch.Style.FillKeepingRatio)
			{
				float num6 = num3 / num4;
				if (this.initialSize.x / this.initialSize.y < num6)
				{
					float num7 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num7;
				}
				else
				{
					float num8 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num8;
					vector.y = num4;
				}
			}
			else if (this.style == UIStretch.Style.FitInternalKeepingRatio)
			{
				float num9 = num3 / num4;
				if (this.initialSize.x / this.initialSize.y > num9)
				{
					float num10 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num10;
				}
				else
				{
					float num11 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num11;
					vector.y = num4;
				}
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x = this.relativeSize.x * num3;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y = this.relativeSize.y * num4;
				}
			}
			if (this.mSprite != null)
			{
				float num12 = (this.mSprite.atlas != null) ? this.mSprite.atlas.pixelSize : 1f;
				vector.x -= this.borderPadding.x * num12;
				vector.y -= this.borderPadding.y * num12;
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mSprite.width = Mathf.RoundToInt(vector.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mSprite.height = Mathf.RoundToInt(vector.y);
				}
				vector = Vector3.one;
			}
			else if (this.mWidget != null)
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mWidget.width = Mathf.RoundToInt(vector.x - this.borderPadding.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mWidget.height = Mathf.RoundToInt(vector.y - this.borderPadding.y);
				}
				vector = Vector3.one;
			}
			else if (this.mPanel != null)
			{
				Vector4 baseClipRegion = this.mPanel.baseClipRegion;
				if (this.style != UIStretch.Style.Vertical)
				{
					baseClipRegion.z = vector.x - this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					baseClipRegion.w = vector.y - this.borderPadding.y;
				}
				this.mPanel.baseClipRegion = baseClipRegion;
				vector = Vector3.one;
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x -= this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y -= this.borderPadding.y;
				}
			}
			if (this.mTrans.localScale != vector)
			{
				this.mTrans.localScale = vector;
			}
			if (this.runOnlyOnce && Application.isPlaying)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x040007F1 RID: 2033
	public Camera uiCamera;

	// Token: 0x040007F2 RID: 2034
	public GameObject container;

	// Token: 0x040007F3 RID: 2035
	public UIStretch.Style style;

	// Token: 0x040007F4 RID: 2036
	public bool runOnlyOnce = true;

	// Token: 0x040007F5 RID: 2037
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x040007F6 RID: 2038
	public Vector2 initialSize = Vector2.one;

	// Token: 0x040007F7 RID: 2039
	public Vector2 borderPadding = Vector2.zero;

	// Token: 0x040007F8 RID: 2040
	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	// Token: 0x040007F9 RID: 2041
	private Transform mTrans;

	// Token: 0x040007FA RID: 2042
	private UIWidget mWidget;

	// Token: 0x040007FB RID: 2043
	private UISprite mSprite;

	// Token: 0x040007FC RID: 2044
	private UIPanel mPanel;

	// Token: 0x040007FD RID: 2045
	private UIRoot mRoot;

	// Token: 0x040007FE RID: 2046
	private Animation mAnim;

	// Token: 0x040007FF RID: 2047
	private Rect mRect;

	// Token: 0x04000800 RID: 2048
	private bool mStarted;

	// Token: 0x0200011E RID: 286
	public enum Style
	{
		// Token: 0x04000802 RID: 2050
		None,
		// Token: 0x04000803 RID: 2051
		Horizontal,
		// Token: 0x04000804 RID: 2052
		Vertical,
		// Token: 0x04000805 RID: 2053
		Both,
		// Token: 0x04000806 RID: 2054
		BasedOnHeight,
		// Token: 0x04000807 RID: 2055
		FillKeepingRatio,
		// Token: 0x04000808 RID: 2056
		FitInternalKeepingRatio
	}
}
