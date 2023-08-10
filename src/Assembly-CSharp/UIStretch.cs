using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	public enum Style
	{
		None,
		Horizontal,
		Vertical,
		Both,
		BasedOnHeight,
		FillKeepingRatio,
		FitInternalKeepingRatio
	}

	public Camera uiCamera;

	public GameObject container;

	public Style style;

	public bool runOnlyOnce = true;

	public Vector2 relativeSize = Vector2.one;

	public Vector2 initialSize = Vector2.one;

	public Vector2 borderPadding = Vector2.zero;

	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	private Transform mTrans;

	private UIWidget mWidget;

	private UISprite mSprite;

	private UIPanel mPanel;

	private UIRoot mRoot;

	private Animation mAnim;

	private Rect mRect;

	private bool mStarted;

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		mAnim = ((Component)this).GetComponent<Animation>();
		mRect = default(Rect);
		mTrans = ((Component)this).transform;
		mWidget = ((Component)this).GetComponent<UIWidget>();
		mSprite = ((Component)this).GetComponent<UISprite>();
		mPanel = ((Component)this).GetComponent<UIPanel>();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(ScreenSizeChanged));
	}

	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(ScreenSizeChanged));
	}

	private void ScreenSizeChanged()
	{
		if (mStarted && runOnlyOnce)
		{
			Update();
		}
	}

	private void Start()
	{
		if ((Object)(object)container == (Object)null && (Object)(object)widgetContainer != (Object)null)
		{
			container = ((Component)widgetContainer).gameObject;
			widgetContainer = null;
		}
		if ((Object)(object)uiCamera == (Object)null)
		{
			uiCamera = NGUITools.FindCameraForLayer(((Component)this).gameObject.layer);
		}
		mRoot = NGUITools.FindInParents<UIRoot>(((Component)this).gameObject);
		Update();
		mStarted = true;
	}

	private void Update()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		if (((Object)(object)mAnim != (Object)null && mAnim.isPlaying) || style == Style.None)
		{
			return;
		}
		UIWidget uIWidget = (((Object)(object)container == (Object)null) ? null : container.GetComponent<UIWidget>());
		UIPanel uIPanel = (((Object)(object)container == (Object)null && (Object)(object)uIWidget == (Object)null) ? null : container.GetComponent<UIPanel>());
		float num = 1f;
		if ((Object)(object)uIWidget != (Object)null)
		{
			Bounds val = uIWidget.CalculateBounds(((Component)this).transform.parent);
			((Rect)(ref mRect)).x = ((Bounds)(ref val)).min.x;
			((Rect)(ref mRect)).y = ((Bounds)(ref val)).min.y;
			((Rect)(ref mRect)).width = ((Bounds)(ref val)).size.x;
			((Rect)(ref mRect)).height = ((Bounds)(ref val)).size.y;
		}
		else if ((Object)(object)uIPanel != (Object)null)
		{
			if (uIPanel.clipping == UIDrawCall.Clipping.None)
			{
				float num2 = (((Object)(object)mRoot != (Object)null) ? ((float)mRoot.activeHeight / (float)Screen.height * 0.5f) : 0.5f);
				((Rect)(ref mRect)).xMin = (float)(-Screen.width) * num2;
				((Rect)(ref mRect)).yMin = (float)(-Screen.height) * num2;
				((Rect)(ref mRect)).xMax = 0f - ((Rect)(ref mRect)).xMin;
				((Rect)(ref mRect)).yMax = 0f - ((Rect)(ref mRect)).yMin;
			}
			else
			{
				Vector4 finalClipRegion = uIPanel.finalClipRegion;
				((Rect)(ref mRect)).x = finalClipRegion.x - finalClipRegion.z * 0.5f;
				((Rect)(ref mRect)).y = finalClipRegion.y - finalClipRegion.w * 0.5f;
				((Rect)(ref mRect)).width = finalClipRegion.z;
				((Rect)(ref mRect)).height = finalClipRegion.w;
			}
		}
		else if ((Object)(object)container != (Object)null)
		{
			Transform parent = ((Component)this).transform.parent;
			Bounds val2 = (((Object)(object)parent != (Object)null) ? NGUIMath.CalculateRelativeWidgetBounds(parent, container.transform) : NGUIMath.CalculateRelativeWidgetBounds(container.transform));
			((Rect)(ref mRect)).x = ((Bounds)(ref val2)).min.x;
			((Rect)(ref mRect)).y = ((Bounds)(ref val2)).min.y;
			((Rect)(ref mRect)).width = ((Bounds)(ref val2)).size.x;
			((Rect)(ref mRect)).height = ((Bounds)(ref val2)).size.y;
		}
		else
		{
			if (!((Object)(object)uiCamera != (Object)null))
			{
				return;
			}
			mRect = uiCamera.pixelRect;
			if ((Object)(object)mRoot != (Object)null)
			{
				num = mRoot.pixelSizeAdjustment;
			}
		}
		float num3 = ((Rect)(ref mRect)).width;
		float num4 = ((Rect)(ref mRect)).height;
		if (num != 1f && num4 > 1f)
		{
			float num5 = (float)mRoot.activeHeight / num4;
			num3 *= num5;
			num4 *= num5;
		}
		Vector3 val3 = (Vector3)(((Object)(object)mWidget != (Object)null) ? new Vector3((float)mWidget.width, (float)mWidget.height) : mTrans.localScale);
		if (style == Style.BasedOnHeight)
		{
			val3.x = relativeSize.x * num4;
			val3.y = relativeSize.y * num4;
		}
		else if (style == Style.FillKeepingRatio)
		{
			float num6 = num3 / num4;
			if (initialSize.x / initialSize.y < num6)
			{
				float num7 = num3 / initialSize.x;
				val3.x = num3;
				val3.y = initialSize.y * num7;
			}
			else
			{
				float num8 = num4 / initialSize.y;
				val3.x = initialSize.x * num8;
				val3.y = num4;
			}
		}
		else if (style == Style.FitInternalKeepingRatio)
		{
			float num9 = num3 / num4;
			if (initialSize.x / initialSize.y > num9)
			{
				float num10 = num3 / initialSize.x;
				val3.x = num3;
				val3.y = initialSize.y * num10;
			}
			else
			{
				float num11 = num4 / initialSize.y;
				val3.x = initialSize.x * num11;
				val3.y = num4;
			}
		}
		else
		{
			if (style != Style.Vertical)
			{
				val3.x = relativeSize.x * num3;
			}
			if (style != Style.Horizontal)
			{
				val3.y = relativeSize.y * num4;
			}
		}
		if ((Object)(object)mSprite != (Object)null)
		{
			float num12 = (((Object)(object)mSprite.atlas != (Object)null) ? mSprite.atlas.pixelSize : 1f);
			val3.x -= borderPadding.x * num12;
			val3.y -= borderPadding.y * num12;
			if (style != Style.Vertical)
			{
				mSprite.width = Mathf.RoundToInt(val3.x);
			}
			if (style != Style.Horizontal)
			{
				mSprite.height = Mathf.RoundToInt(val3.y);
			}
			val3 = Vector3.one;
		}
		else if ((Object)(object)mWidget != (Object)null)
		{
			if (style != Style.Vertical)
			{
				mWidget.width = Mathf.RoundToInt(val3.x - borderPadding.x);
			}
			if (style != Style.Horizontal)
			{
				mWidget.height = Mathf.RoundToInt(val3.y - borderPadding.y);
			}
			val3 = Vector3.one;
		}
		else if ((Object)(object)mPanel != (Object)null)
		{
			Vector4 baseClipRegion = mPanel.baseClipRegion;
			if (style != Style.Vertical)
			{
				baseClipRegion.z = val3.x - borderPadding.x;
			}
			if (style != Style.Horizontal)
			{
				baseClipRegion.w = val3.y - borderPadding.y;
			}
			mPanel.baseClipRegion = baseClipRegion;
			val3 = Vector3.one;
		}
		else
		{
			if (style != Style.Vertical)
			{
				val3.x -= borderPadding.x;
			}
			if (style != Style.Horizontal)
			{
				val3.y -= borderPadding.y;
			}
		}
		if (mTrans.localScale != val3)
		{
			mTrans.localScale = val3;
		}
		if (runOnlyOnce && Application.isPlaying)
		{
			((Behaviour)this).enabled = false;
		}
	}
}
