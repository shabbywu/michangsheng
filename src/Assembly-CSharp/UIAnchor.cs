using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
	// Token: 0x0600098B RID: 2443 RVA: 0x0000BDC7 File Offset: 0x00009FC7
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mAnim = base.GetComponent<Animation>();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0000BE01 File Offset: 0x0000A001
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0000BE23 File Offset: 0x0000A023
	private void ScreenSizeChanged()
	{
		if (this.mStarted && this.runOnlyOnce)
		{
			this.Update();
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00087A0C File Offset: 0x00085C0C
	private void Start()
	{
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.Update();
		this.mStarted = true;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00087A90 File Offset: 0x00085C90
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.enabled && this.mAnim.isPlaying)
		{
			return;
		}
		bool flag = false;
		UIWidget uiwidget = (this.container == null) ? null : this.container.GetComponent<UIWidget>();
		UIPanel uipanel = (this.container == null && uiwidget == null) ? null : this.container.GetComponent<UIPanel>();
		if (uiwidget != null)
		{
			Bounds bounds = uiwidget.CalculateBounds(this.container.transform.parent);
			this.mRect.x = bounds.min.x;
			this.mRect.y = bounds.min.y;
			this.mRect.width = bounds.size.x;
			this.mRect.height = bounds.size.y;
		}
		else if (uipanel != null)
		{
			if (uipanel.clipping == UIDrawCall.Clipping.None)
			{
				float num = (this.mRoot != null) ? ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f) : 0.5f;
				this.mRect.xMin = (float)(-(float)Screen.width) * num;
				this.mRect.yMin = (float)(-(float)Screen.height) * num;
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
			Transform parent = this.container.transform.parent;
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
			flag = true;
			this.mRect = this.uiCamera.pixelRect;
		}
		float num2 = (this.mRect.xMin + this.mRect.xMax) * 0.5f;
		float num3 = (this.mRect.yMin + this.mRect.yMax) * 0.5f;
		Vector3 vector;
		vector..ctor(num2, num3, 0f);
		if (this.side != UIAnchor.Side.Center)
		{
			if (this.side == UIAnchor.Side.Right || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.BottomRight)
			{
				vector.x = this.mRect.xMax;
			}
			else if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Bottom)
			{
				vector.x = num2;
			}
			else
			{
				vector.x = this.mRect.xMin;
			}
			if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.TopLeft)
			{
				vector.y = this.mRect.yMax;
			}
			else if (this.side == UIAnchor.Side.Left || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Right)
			{
				vector.y = num3;
			}
			else
			{
				vector.y = this.mRect.yMin;
			}
		}
		float width = this.mRect.width;
		float height = this.mRect.height;
		vector.x += this.pixelOffset.x + this.relativeOffset.x * width;
		vector.y += this.pixelOffset.y + this.relativeOffset.y * height;
		if (flag)
		{
			if (this.uiCamera.orthographic)
			{
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
			}
			vector.z = this.uiCamera.WorldToScreenPoint(this.mTrans.position).z;
			vector = this.uiCamera.ScreenToWorldPoint(vector);
		}
		else
		{
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			if (uipanel != null)
			{
				vector = uipanel.cachedTransform.TransformPoint(vector);
			}
			else if (this.container != null)
			{
				Transform parent2 = this.container.transform.parent;
				if (parent2 != null)
				{
					vector = parent2.TransformPoint(vector);
				}
			}
			vector.z = this.mTrans.position.z;
		}
		if (this.mTrans.position != vector)
		{
			this.mTrans.position = vector;
		}
		if (this.runOnlyOnce && Application.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x04000695 RID: 1685
	public Camera uiCamera;

	// Token: 0x04000696 RID: 1686
	public GameObject container;

	// Token: 0x04000697 RID: 1687
	public UIAnchor.Side side = UIAnchor.Side.Center;

	// Token: 0x04000698 RID: 1688
	public bool runOnlyOnce = true;

	// Token: 0x04000699 RID: 1689
	public Vector2 relativeOffset = Vector2.zero;

	// Token: 0x0400069A RID: 1690
	public Vector2 pixelOffset = Vector2.zero;

	// Token: 0x0400069B RID: 1691
	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	// Token: 0x0400069C RID: 1692
	private Transform mTrans;

	// Token: 0x0400069D RID: 1693
	private Animation mAnim;

	// Token: 0x0400069E RID: 1694
	private Rect mRect;

	// Token: 0x0400069F RID: 1695
	private UIRoot mRoot;

	// Token: 0x040006A0 RID: 1696
	private bool mStarted;

	// Token: 0x020000F8 RID: 248
	public enum Side
	{
		// Token: 0x040006A2 RID: 1698
		BottomLeft,
		// Token: 0x040006A3 RID: 1699
		Left,
		// Token: 0x040006A4 RID: 1700
		TopLeft,
		// Token: 0x040006A5 RID: 1701
		Top,
		// Token: 0x040006A6 RID: 1702
		TopRight,
		// Token: 0x040006A7 RID: 1703
		Right,
		// Token: 0x040006A8 RID: 1704
		BottomRight,
		// Token: 0x040006A9 RID: 1705
		Bottom,
		// Token: 0x040006AA RID: 1706
		Center
	}
}
