using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
	public enum Side
	{
		BottomLeft,
		Left,
		TopLeft,
		Top,
		TopRight,
		Right,
		BottomRight,
		Bottom,
		Center
	}

	public Camera uiCamera;

	public GameObject container;

	public Side side = Side.Center;

	public bool runOnlyOnce = true;

	public Vector2 relativeOffset = Vector2.zero;

	public Vector2 pixelOffset = Vector2.zero;

	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	private Transform mTrans;

	private Animation mAnim;

	private Rect mRect;

	private UIRoot mRoot;

	private bool mStarted;

	private void Awake()
	{
		mTrans = ((Component)this).transform;
		mAnim = ((Component)this).GetComponent<Animation>();
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
		mRoot = NGUITools.FindInParents<UIRoot>(((Component)this).gameObject);
		if ((Object)(object)uiCamera == (Object)null)
		{
			uiCamera = NGUITools.FindCameraForLayer(((Component)this).gameObject.layer);
		}
		Update();
		mStarted = true;
	}

	private void Update()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mAnim != (Object)null && ((Behaviour)mAnim).enabled && mAnim.isPlaying)
		{
			return;
		}
		bool flag = false;
		UIWidget uIWidget = (((Object)(object)container == (Object)null) ? null : container.GetComponent<UIWidget>());
		UIPanel uIPanel = (((Object)(object)container == (Object)null && (Object)(object)uIWidget == (Object)null) ? null : container.GetComponent<UIPanel>());
		if ((Object)(object)uIWidget != (Object)null)
		{
			Bounds val = uIWidget.CalculateBounds(container.transform.parent);
			((Rect)(ref mRect)).x = ((Bounds)(ref val)).min.x;
			((Rect)(ref mRect)).y = ((Bounds)(ref val)).min.y;
			((Rect)(ref mRect)).width = ((Bounds)(ref val)).size.x;
			((Rect)(ref mRect)).height = ((Bounds)(ref val)).size.y;
		}
		else if ((Object)(object)uIPanel != (Object)null)
		{
			if (uIPanel.clipping == UIDrawCall.Clipping.None)
			{
				float num = (((Object)(object)mRoot != (Object)null) ? ((float)mRoot.activeHeight / (float)Screen.height * 0.5f) : 0.5f);
				((Rect)(ref mRect)).xMin = (float)(-Screen.width) * num;
				((Rect)(ref mRect)).yMin = (float)(-Screen.height) * num;
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
			Transform parent = container.transform.parent;
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
			flag = true;
			mRect = uiCamera.pixelRect;
		}
		float num2 = (((Rect)(ref mRect)).xMin + ((Rect)(ref mRect)).xMax) * 0.5f;
		float num3 = (((Rect)(ref mRect)).yMin + ((Rect)(ref mRect)).yMax) * 0.5f;
		Vector3 val3 = default(Vector3);
		((Vector3)(ref val3))._002Ector(num2, num3, 0f);
		if (side != Side.Center)
		{
			if (side == Side.Right || side == Side.TopRight || side == Side.BottomRight)
			{
				val3.x = ((Rect)(ref mRect)).xMax;
			}
			else if (side == Side.Top || side == Side.Center || side == Side.Bottom)
			{
				val3.x = num2;
			}
			else
			{
				val3.x = ((Rect)(ref mRect)).xMin;
			}
			if (side == Side.Top || side == Side.TopRight || side == Side.TopLeft)
			{
				val3.y = ((Rect)(ref mRect)).yMax;
			}
			else if (side == Side.Left || side == Side.Center || side == Side.Right)
			{
				val3.y = num3;
			}
			else
			{
				val3.y = ((Rect)(ref mRect)).yMin;
			}
		}
		float width = ((Rect)(ref mRect)).width;
		float height = ((Rect)(ref mRect)).height;
		val3.x += pixelOffset.x + relativeOffset.x * width;
		val3.y += pixelOffset.y + relativeOffset.y * height;
		if (flag)
		{
			if (uiCamera.orthographic)
			{
				val3.x = Mathf.Round(val3.x);
				val3.y = Mathf.Round(val3.y);
			}
			val3.z = uiCamera.WorldToScreenPoint(mTrans.position).z;
			val3 = uiCamera.ScreenToWorldPoint(val3);
		}
		else
		{
			val3.x = Mathf.Round(val3.x);
			val3.y = Mathf.Round(val3.y);
			if ((Object)(object)uIPanel != (Object)null)
			{
				val3 = uIPanel.cachedTransform.TransformPoint(val3);
			}
			else if ((Object)(object)container != (Object)null)
			{
				Transform parent2 = container.transform.parent;
				if ((Object)(object)parent2 != (Object)null)
				{
					val3 = parent2.TransformPoint(val3);
				}
			}
			val3.z = mTrans.position.z;
		}
		if (mTrans.position != val3)
		{
			mTrans.position = val3;
		}
		if (runOnlyOnce && Application.isPlaying)
		{
			((Behaviour)this).enabled = false;
		}
	}
}
