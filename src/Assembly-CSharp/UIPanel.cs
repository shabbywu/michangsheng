using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Panel")]
public class UIPanel : UIRect
{
	public enum RenderQueue
	{
		Automatic,
		StartAt,
		Explicit
	}

	public delegate void OnGeometryUpdated();

	public delegate void OnClippingMoved(UIPanel panel);

	public static BetterList<UIPanel> list = new BetterList<UIPanel>();

	public OnGeometryUpdated onGeometryUpdated;

	public bool showInPanelTool = true;

	public bool generateNormals;

	public bool widgetsAreStatic;

	public bool cullWhileDragging;

	public bool alwaysOnScreen;

	public bool anchorOffset;

	public RenderQueue renderQueue;

	public int startingRenderQueue = 3000;

	[NonSerialized]
	public BetterList<UIWidget> widgets = new BetterList<UIWidget>();

	[NonSerialized]
	public BetterList<UIDrawCall> drawCalls = new BetterList<UIDrawCall>();

	[NonSerialized]
	public Matrix4x4 worldToLocal = Matrix4x4.identity;

	[NonSerialized]
	public Vector4 drawCallClipRange = new Vector4(0f, 0f, 1f, 1f);

	public OnClippingMoved onClipMove;

	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	[HideInInspector]
	[SerializeField]
	private Vector4 mClipRange = new Vector4(0f, 0f, 300f, 200f);

	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(4f, 4f);

	[HideInInspector]
	[SerializeField]
	private int mDepth;

	[HideInInspector]
	[SerializeField]
	private int mSortingOrder;

	private bool mRebuild;

	private bool mResized;

	private Camera mCam;

	[SerializeField]
	private Vector2 mClipOffset = Vector2.zero;

	private float mCullTime;

	private float mUpdateTime;

	private int mMatrixFrame = -1;

	private int mAlphaFrameID;

	private int mLayer = -1;

	private static float[] mTemp = new float[4];

	private Vector2 mMin = Vector2.zero;

	private Vector2 mMax = Vector2.zero;

	private bool mHalfPixelOffset;

	private bool mSortWidgets;

	private bool mUpdateScroll;

	private UIPanel mParentPanel;

	private static Vector3[] mCorners = (Vector3[])(object)new Vector3[4];

	private static int mUpdateFrame = -1;

	private bool mForced;

	public static int nextUnusedDepth
	{
		get
		{
			int num = int.MinValue;
			for (int i = 0; i < list.size; i++)
			{
				num = Mathf.Max(num, list[i].depth);
			}
			if (num != int.MinValue)
			{
				return num + 1;
			}
			return 0;
		}
	}

	public override bool canBeAnchored => mClipping != UIDrawCall.Clipping.None;

	public override float alpha
	{
		get
		{
			return mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (mAlpha != num)
			{
				mAlphaFrameID = -1;
				mResized = true;
				mAlpha = num;
				SetDirty();
			}
		}
	}

	public int depth
	{
		get
		{
			return mDepth;
		}
		set
		{
			if (mDepth != value)
			{
				mDepth = value;
				list.Sort(CompareFunc);
			}
		}
	}

	public int sortingOrder
	{
		get
		{
			return mSortingOrder;
		}
		set
		{
			if (mSortingOrder != value)
			{
				mSortingOrder = value;
				UpdateDrawCalls();
			}
		}
	}

	public float width => GetViewSize().x;

	public float height => GetViewSize().y;

	public bool halfPixelOffset => mHalfPixelOffset;

	public bool usedForUI
	{
		get
		{
			if ((Object)(object)mCam != (Object)null)
			{
				return mCam.orthographic;
			}
			return false;
		}
	}

	public Vector3 drawCallOffset
	{
		get
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			if (mHalfPixelOffset && (Object)(object)mCam != (Object)null && mCam.orthographic)
			{
				Vector2 windowSize = GetWindowSize();
				float num = 1f / windowSize.y / mCam.orthographicSize;
				return new Vector3(0f - num, num);
			}
			return Vector3.zero;
		}
	}

	public UIDrawCall.Clipping clipping
	{
		get
		{
			return mClipping;
		}
		set
		{
			if (mClipping != value)
			{
				mResized = true;
				mClipping = value;
				mMatrixFrame = -1;
			}
		}
	}

	public UIPanel parentPanel => mParentPanel;

	public int clipCount
	{
		get
		{
			int num = 0;
			UIPanel uIPanel = this;
			while ((Object)(object)uIPanel != (Object)null)
			{
				if (uIPanel.mClipping == UIDrawCall.Clipping.SoftClip)
				{
					num++;
				}
				uIPanel = uIPanel.mParentPanel;
			}
			return num;
		}
	}

	public bool hasClipping => mClipping == UIDrawCall.Clipping.SoftClip;

	public bool hasCumulativeClipping => clipCount != 0;

	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool clipsChildren => hasCumulativeClipping;

	public Vector2 clipOffset
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mClipOffset;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (Mathf.Abs(mClipOffset.x - value.x) > 0.001f || Mathf.Abs(mClipOffset.y - value.y) > 0.001f)
			{
				mClipOffset = value;
				InvalidateClipping();
				if (onClipMove != null)
				{
					onClipMove(this);
				}
			}
		}
	}

	[Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
	public Vector4 clipRange
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return baseClipRegion;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			baseClipRegion = value;
		}
	}

	public Vector4 baseClipRegion
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mClipRange;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			if (Mathf.Abs(mClipRange.x - value.x) > 0.001f || Mathf.Abs(mClipRange.y - value.y) > 0.001f || Mathf.Abs(mClipRange.z - value.z) > 0.001f || Mathf.Abs(mClipRange.w - value.w) > 0.001f)
			{
				mResized = true;
				mCullTime = ((mCullTime == 0f) ? 0.001f : (RealTime.time + 0.15f));
				mClipRange = value;
				mMatrixFrame = -1;
				UIScrollView component = ((Component)this).GetComponent<UIScrollView>();
				if ((Object)(object)component != (Object)null)
				{
					component.UpdatePosition();
				}
				if (onClipMove != null)
				{
					onClipMove(this);
				}
			}
		}
	}

	public Vector4 finalClipRegion
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			Vector2 viewSize = GetViewSize();
			if (mClipping != 0)
			{
				return new Vector4(mClipRange.x + mClipOffset.x, mClipRange.y + mClipOffset.y, viewSize.x, viewSize.y);
			}
			return new Vector4(0f, 0f, viewSize.x, viewSize.y);
		}
	}

	public Vector2 clipSoftness
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mClipSoftness;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (mClipSoftness != value)
			{
				mClipSoftness = value;
			}
		}
	}

	public override Vector3[] localCorners
	{
		get
		{
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0203: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			if (mClipping == UIDrawCall.Clipping.None)
			{
				Vector2 viewSize = GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float num3 = num + viewSize.x;
				float num4 = num2 + viewSize.y;
				Transform val = (((Object)(object)mCam != (Object)null) ? ((Component)mCam).transform : null);
				if ((Object)(object)val != (Object)null)
				{
					mCorners[0] = val.TransformPoint(num, num2, 0f);
					mCorners[1] = val.TransformPoint(num, num4, 0f);
					mCorners[2] = val.TransformPoint(num3, num4, 0f);
					mCorners[3] = val.TransformPoint(num3, num2, 0f);
					val = base.cachedTransform;
					for (int i = 0; i < 4; i++)
					{
						mCorners[i] = val.InverseTransformPoint(mCorners[i]);
					}
				}
				else
				{
					mCorners[0] = new Vector3(num, num2);
					mCorners[1] = new Vector3(num, num4);
					mCorners[2] = new Vector3(num3, num4);
					mCorners[3] = new Vector3(num3, num2);
				}
			}
			else
			{
				float num5 = mClipOffset.x + mClipRange.x - 0.5f * mClipRange.z;
				float num6 = mClipOffset.y + mClipRange.y - 0.5f * mClipRange.w;
				float num7 = num5 + mClipRange.z;
				float num8 = num6 + mClipRange.w;
				mCorners[0] = new Vector3(num5, num6);
				mCorners[1] = new Vector3(num5, num8);
				mCorners[2] = new Vector3(num7, num8);
				mCorners[3] = new Vector3(num7, num6);
			}
			return mCorners;
		}
	}

	public override Vector3[] worldCorners
	{
		get
		{
			//IL_0195: Unknown result type (might be due to invalid IL or missing references)
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			if (mClipping == UIDrawCall.Clipping.None)
			{
				if ((Object)(object)mCam != (Object)null)
				{
					Vector3[] array = mCam.GetWorldCorners();
					UIRoot uIRoot = base.root;
					if ((Object)(object)uIRoot != (Object)null)
					{
						float pixelSizeAdjustment = uIRoot.pixelSizeAdjustment;
						for (int i = 0; i < 4; i++)
						{
							ref Vector3 reference = ref array[i];
							reference *= pixelSizeAdjustment;
						}
					}
					return array;
				}
				Vector2 viewSize = GetViewSize();
				float num = -0.5f * viewSize.x;
				float num2 = -0.5f * viewSize.y;
				float num3 = num + viewSize.x;
				float num4 = num2 + viewSize.y;
				mCorners[0] = new Vector3(num, num2, 0f);
				mCorners[1] = new Vector3(num, num4, 0f);
				mCorners[2] = new Vector3(num3, num4, 0f);
				mCorners[3] = new Vector3(num3, num2, 0f);
			}
			else
			{
				float num5 = mClipOffset.x + mClipRange.x - 0.5f * mClipRange.z;
				float num6 = mClipOffset.y + mClipRange.y - 0.5f * mClipRange.w;
				float num7 = num5 + mClipRange.z;
				float num8 = num6 + mClipRange.w;
				Transform val = base.cachedTransform;
				mCorners[0] = val.TransformPoint(num5, num6, 0f);
				mCorners[1] = val.TransformPoint(num5, num8, 0f);
				mCorners[2] = val.TransformPoint(num7, num8, 0f);
				mCorners[3] = val.TransformPoint(num7, num6, 0f);
			}
			return mCorners;
		}
	}

	public static int CompareFunc(UIPanel a, UIPanel b)
	{
		if ((Object)(object)a != (Object)(object)b && (Object)(object)a != (Object)null && (Object)(object)b != (Object)null)
		{
			if (a.mDepth < b.mDepth)
			{
				return -1;
			}
			if (a.mDepth > b.mDepth)
			{
				return 1;
			}
			if (((Object)a).GetInstanceID() >= ((Object)b).GetInstanceID())
			{
				return 1;
			}
			return -1;
		}
		return 0;
	}

	private void InvalidateClipping()
	{
		mResized = true;
		mMatrixFrame = -1;
		mCullTime = ((mCullTime == 0f) ? 0.001f : (RealTime.time + 0.15f));
		for (int i = 0; i < list.size; i++)
		{
			UIPanel uIPanel = list[i];
			if ((Object)(object)uIPanel != (Object)(object)this && (Object)(object)uIPanel.parentPanel == (Object)(object)this)
			{
				uIPanel.InvalidateClipping();
			}
		}
	}

	public override Vector3[] GetSides(Transform relativeTo)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		if (mClipping != 0 || anchorOffset)
		{
			Vector2 viewSize = GetViewSize();
			Vector2 val = ((mClipping != 0) ? (Vector4.op_Implicit(mClipRange) + mClipOffset) : Vector2.zero);
			float num = val.x - 0.5f * viewSize.x;
			float num2 = val.y - 0.5f * viewSize.y;
			float num3 = num + viewSize.x;
			float num4 = num2 + viewSize.y;
			float num5 = (num + num3) * 0.5f;
			float num6 = (num2 + num4) * 0.5f;
			Matrix4x4 localToWorldMatrix = base.cachedTransform.localToWorldMatrix;
			mCorners[0] = ((Matrix4x4)(ref localToWorldMatrix)).MultiplyPoint3x4(new Vector3(num, num6));
			mCorners[1] = ((Matrix4x4)(ref localToWorldMatrix)).MultiplyPoint3x4(new Vector3(num5, num4));
			mCorners[2] = ((Matrix4x4)(ref localToWorldMatrix)).MultiplyPoint3x4(new Vector3(num3, num6));
			mCorners[3] = ((Matrix4x4)(ref localToWorldMatrix)).MultiplyPoint3x4(new Vector3(num5, num2));
			if ((Object)(object)relativeTo != (Object)null)
			{
				for (int i = 0; i < 4; i++)
				{
					mCorners[i] = relativeTo.InverseTransformPoint(mCorners[i]);
				}
			}
			return mCorners;
		}
		return base.GetSides(relativeTo);
	}

	public override void Invalidate(bool includeChildren)
	{
		mAlphaFrameID = -1;
		base.Invalidate(includeChildren);
	}

	public override float CalculateFinalAlpha(int frameID)
	{
		if (mAlphaFrameID != frameID)
		{
			mAlphaFrameID = frameID;
			UIRect uIRect = base.parent;
			finalAlpha = (((Object)(object)base.parent != (Object)null) ? (uIRect.CalculateFinalAlpha(frameID) * mAlpha) : mAlpha);
		}
		return finalAlpha;
	}

	public override void SetRect(float x, float y, float width, float height)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		int num = Mathf.FloorToInt(width + 0.5f);
		int num2 = Mathf.FloorToInt(height + 0.5f);
		num = num >> 1 << 1;
		num2 = num2 >> 1 << 1;
		Transform val = base.cachedTransform;
		Vector3 localPosition = val.localPosition;
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
		baseClipRegion = new Vector4(localPosition.x, localPosition.y, (float)num, (float)num2);
		if (base.isAnchored)
		{
			val = val.parent;
			if (Object.op_Implicit((Object)(object)leftAnchor.target))
			{
				leftAnchor.SetHorizontal(val, x);
			}
			if (Object.op_Implicit((Object)(object)rightAnchor.target))
			{
				rightAnchor.SetHorizontal(val, x + width);
			}
			if (Object.op_Implicit((Object)(object)bottomAnchor.target))
			{
				bottomAnchor.SetVertical(val, y);
			}
			if (Object.op_Implicit((Object)(object)topAnchor.target))
			{
				topAnchor.SetVertical(val, y + height);
			}
		}
	}

	public bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		UpdateTransformMatrix();
		a = ((Matrix4x4)(ref worldToLocal)).MultiplyPoint3x4(a);
		b = ((Matrix4x4)(ref worldToLocal)).MultiplyPoint3x4(b);
		c = ((Matrix4x4)(ref worldToLocal)).MultiplyPoint3x4(c);
		d = ((Matrix4x4)(ref worldToLocal)).MultiplyPoint3x4(d);
		mTemp[0] = a.x;
		mTemp[1] = b.x;
		mTemp[2] = c.x;
		mTemp[3] = d.x;
		float num = Mathf.Min(mTemp);
		float num2 = Mathf.Max(mTemp);
		mTemp[0] = a.y;
		mTemp[1] = b.y;
		mTemp[2] = c.y;
		mTemp[3] = d.y;
		float num3 = Mathf.Min(mTemp);
		float num4 = Mathf.Max(mTemp);
		if (num2 < mMin.x)
		{
			return false;
		}
		if (num4 < mMin.y)
		{
			return false;
		}
		if (num > mMax.x)
		{
			return false;
		}
		if (num3 > mMax.y)
		{
			return false;
		}
		return true;
	}

	public bool IsVisible(Vector3 worldPos)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (mAlpha < 0.001f)
		{
			return false;
		}
		if (mClipping == UIDrawCall.Clipping.None || mClipping == UIDrawCall.Clipping.ConstrainButDontClip)
		{
			return true;
		}
		UpdateTransformMatrix();
		Vector3 val = ((Matrix4x4)(ref worldToLocal)).MultiplyPoint3x4(worldPos);
		if (val.x < mMin.x)
		{
			return false;
		}
		if (val.y < mMin.y)
		{
			return false;
		}
		if (val.x > mMax.x)
		{
			return false;
		}
		if (val.y > mMax.y)
		{
			return false;
		}
		return true;
	}

	public bool IsVisible(UIWidget w)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = this;
		Vector3[] array = null;
		while ((Object)(object)uIPanel != (Object)null)
		{
			if ((uIPanel.mClipping == UIDrawCall.Clipping.None || uIPanel.mClipping == UIDrawCall.Clipping.ConstrainButDontClip) && !w.hideIfOffScreen)
			{
				uIPanel = uIPanel.mParentPanel;
				continue;
			}
			if (array == null)
			{
				array = w.worldCorners;
			}
			if (!uIPanel.IsVisible(array[0], array[1], array[2], array[3]))
			{
				return false;
			}
			uIPanel = uIPanel.mParentPanel;
		}
		return true;
	}

	public bool Affects(UIWidget w)
	{
		if ((Object)(object)w == (Object)null)
		{
			return false;
		}
		UIPanel panel = w.panel;
		if ((Object)(object)panel == (Object)null)
		{
			return false;
		}
		UIPanel uIPanel = this;
		while ((Object)(object)uIPanel != (Object)null)
		{
			if ((Object)(object)uIPanel == (Object)(object)panel)
			{
				return true;
			}
			if (!uIPanel.hasCumulativeClipping)
			{
				return false;
			}
			uIPanel = uIPanel.mParentPanel;
		}
		return false;
	}

	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		mRebuild = true;
	}

	public void SetDirty()
	{
		for (int i = 0; i < drawCalls.size; i++)
		{
			drawCalls.buffer[i].isDirty = true;
		}
		Invalidate(includeChildren: true);
	}

	private void Awake()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Invalid comparison between Unknown and I4
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Invalid comparison between Unknown and I4
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		mGo = ((Component)this).gameObject;
		mTrans = ((Component)this).transform;
		mHalfPixelOffset = (int)Application.platform == 2 || (int)Application.platform == 10 || (int)Application.platform == 7;
		if (mHalfPixelOffset)
		{
			mHalfPixelOffset = SystemInfo.graphicsShaderLevel < 40;
		}
	}

	private void FindParent()
	{
		Transform val = base.cachedTransform.parent;
		mParentPanel = (((Object)(object)val != (Object)null) ? NGUITools.FindInParents<UIPanel>(((Component)val).gameObject) : null);
	}

	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		FindParent();
	}

	protected override void OnStart()
	{
		mLayer = mGo.layer;
		UICamera uICamera = UICamera.FindCameraForLayer(mLayer);
		mCam = (((Object)(object)uICamera != (Object)null) ? uICamera.cachedCamera : NGUITools.FindCameraForLayer(mLayer));
	}

	protected override void OnEnable()
	{
		mRebuild = true;
		mAlphaFrameID = -1;
		mMatrixFrame = -1;
		base.OnEnable();
		mMatrixFrame = -1;
	}

	protected override void OnInit()
	{
		base.OnInit();
		if ((Object)(object)((Component)this).GetComponent<Rigidbody>() == (Object)null)
		{
			UICamera uICamera = (((Object)(object)mCam != (Object)null) ? ((Component)mCam).GetComponent<UICamera>() : null);
			if ((Object)(object)uICamera != (Object)null && (uICamera.eventType == UICamera.EventType.UI_3D || uICamera.eventType == UICamera.EventType.World_3D))
			{
				Rigidbody obj = ((Component)this).gameObject.AddComponent<Rigidbody>();
				obj.isKinematic = true;
				obj.useGravity = false;
			}
		}
		FindParent();
		mRebuild = true;
		mAlphaFrameID = -1;
		mMatrixFrame = -1;
		list.Add(this);
		list.Sort(CompareFunc);
	}

	protected override void OnDisable()
	{
		for (int i = 0; i < drawCalls.size; i++)
		{
			UIDrawCall uIDrawCall = drawCalls.buffer[i];
			if ((Object)(object)uIDrawCall != (Object)null)
			{
				UIDrawCall.Destroy(uIDrawCall);
			}
		}
		drawCalls.Clear();
		list.Remove(this);
		mAlphaFrameID = -1;
		mMatrixFrame = -1;
		if (list.size == 0)
		{
			UIDrawCall.ReleaseAll();
			mUpdateFrame = -1;
		}
		base.OnDisable();
	}

	private void UpdateTransformMatrix()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		int frameCount = Time.frameCount;
		if (mMatrixFrame != frameCount)
		{
			mMatrixFrame = frameCount;
			worldToLocal = base.cachedTransform.worldToLocalMatrix;
			Vector2 val = GetViewSize() * 0.5f;
			float num = mClipOffset.x + mClipRange.x;
			float num2 = mClipOffset.y + mClipRange.y;
			mMin.x = num - val.x;
			mMin.y = num2 - val.y;
			mMax.x = num + val.x;
			mMax.y = num2 + val.y;
		}
	}

	protected override void OnAnchor()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		if (mClipping == UIDrawCall.Clipping.None)
		{
			return;
		}
		Transform obj = base.cachedTransform;
		Transform val = obj.parent;
		Vector2 viewSize = GetViewSize();
		Vector2 val2 = Vector2.op_Implicit(obj.localPosition);
		float num;
		float num2;
		float num3;
		float num4;
		if ((Object)(object)leftAnchor.target == (Object)(object)bottomAnchor.target && (Object)(object)leftAnchor.target == (Object)(object)rightAnchor.target && (Object)(object)leftAnchor.target == (Object)(object)topAnchor.target)
		{
			Vector3[] sides = leftAnchor.GetSides(val);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, leftAnchor.relative) + (float)leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, rightAnchor.relative) + (float)rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, topAnchor.relative) + (float)topAnchor.absolute;
			}
			else
			{
				Vector2 val3 = Vector2.op_Implicit(GetLocalPos(leftAnchor, val));
				num = val3.x + (float)leftAnchor.absolute;
				num3 = val3.y + (float)bottomAnchor.absolute;
				num2 = val3.x + (float)rightAnchor.absolute;
				num4 = val3.y + (float)topAnchor.absolute;
			}
		}
		else
		{
			if (Object.op_Implicit((Object)(object)leftAnchor.target))
			{
				Vector3[] sides2 = leftAnchor.GetSides(val);
				num = ((sides2 == null) ? (GetLocalPos(leftAnchor, val).x + (float)leftAnchor.absolute) : (NGUIMath.Lerp(sides2[0].x, sides2[2].x, leftAnchor.relative) + (float)leftAnchor.absolute));
			}
			else
			{
				num = mClipRange.x - 0.5f * viewSize.x;
			}
			if (Object.op_Implicit((Object)(object)rightAnchor.target))
			{
				Vector3[] sides3 = rightAnchor.GetSides(val);
				num2 = ((sides3 == null) ? (GetLocalPos(rightAnchor, val).x + (float)rightAnchor.absolute) : (NGUIMath.Lerp(sides3[0].x, sides3[2].x, rightAnchor.relative) + (float)rightAnchor.absolute));
			}
			else
			{
				num2 = mClipRange.x + 0.5f * viewSize.x;
			}
			if (Object.op_Implicit((Object)(object)bottomAnchor.target))
			{
				Vector3[] sides4 = bottomAnchor.GetSides(val);
				num3 = ((sides4 == null) ? (GetLocalPos(bottomAnchor, val).y + (float)bottomAnchor.absolute) : (NGUIMath.Lerp(sides4[3].y, sides4[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute));
			}
			else
			{
				num3 = mClipRange.y - 0.5f * viewSize.y;
			}
			if (Object.op_Implicit((Object)(object)topAnchor.target))
			{
				Vector3[] sides5 = topAnchor.GetSides(val);
				num4 = ((sides5 == null) ? (GetLocalPos(topAnchor, val).y + (float)topAnchor.absolute) : (NGUIMath.Lerp(sides5[3].y, sides5[1].y, topAnchor.relative) + (float)topAnchor.absolute));
			}
			else
			{
				num4 = mClipRange.y + 0.5f * viewSize.y;
			}
		}
		num -= val2.x + mClipOffset.x;
		num2 -= val2.x + mClipOffset.x;
		num3 -= val2.y + mClipOffset.y;
		num4 -= val2.y + mClipOffset.y;
		float num5 = Mathf.Lerp(num, num2, 0.5f);
		float num6 = Mathf.Lerp(num3, num4, 0.5f);
		float num7 = num2 - num;
		float num8 = num4 - num3;
		float num9 = Mathf.Max(2f, mClipSoftness.x);
		float num10 = Mathf.Max(2f, mClipSoftness.y);
		if (num7 < num9)
		{
			num7 = num9;
		}
		if (num8 < num10)
		{
			num8 = num10;
		}
		baseClipRegion = new Vector4(num5, num6, num7, num8);
	}

	private void LateUpdate()
	{
		if (mUpdateFrame == Time.frameCount)
		{
			return;
		}
		mUpdateFrame = Time.frameCount;
		for (int i = 0; i < list.size; i++)
		{
			list[i].UpdateSelf();
		}
		int num = 3000;
		for (int j = 0; j < list.size; j++)
		{
			UIPanel uIPanel = list.buffer[j];
			if (uIPanel.renderQueue == RenderQueue.Automatic)
			{
				uIPanel.startingRenderQueue = num;
				uIPanel.UpdateDrawCalls();
				num += uIPanel.drawCalls.size;
			}
			else if (uIPanel.renderQueue == RenderQueue.StartAt)
			{
				uIPanel.UpdateDrawCalls();
				if (uIPanel.drawCalls.size != 0)
				{
					num = Mathf.Max(num, uIPanel.startingRenderQueue + uIPanel.drawCalls.size);
				}
			}
			else
			{
				uIPanel.UpdateDrawCalls();
				if (uIPanel.drawCalls.size != 0)
				{
					num = Mathf.Max(num, uIPanel.startingRenderQueue + 1);
				}
			}
		}
	}

	private void UpdateSelf()
	{
		mUpdateTime = RealTime.time;
		UpdateTransformMatrix();
		UpdateLayers();
		UpdateWidgets();
		if (mRebuild)
		{
			mRebuild = false;
			FillAllDrawCalls();
		}
		else
		{
			int num = 0;
			while (num < drawCalls.size)
			{
				UIDrawCall uIDrawCall = drawCalls.buffer[num];
				if (uIDrawCall.isDirty && !FillDrawCall(uIDrawCall))
				{
					UIDrawCall.Destroy(uIDrawCall);
					drawCalls.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}
		if (mUpdateScroll)
		{
			mUpdateScroll = false;
			UIScrollView component = ((Component)this).GetComponent<UIScrollView>();
			if ((Object)(object)component != (Object)null)
			{
				component.UpdateScrollbars();
			}
		}
	}

	public void SortWidgets()
	{
		mSortWidgets = false;
		widgets.Sort(UIWidget.PanelCompareFunc);
	}

	private void FillAllDrawCalls()
	{
		for (int i = 0; i < drawCalls.size; i++)
		{
			UIDrawCall.Destroy(drawCalls.buffer[i]);
		}
		drawCalls.Clear();
		Material val = null;
		Texture val2 = null;
		Shader val3 = null;
		UIDrawCall uIDrawCall = null;
		if (mSortWidgets)
		{
			SortWidgets();
		}
		for (int j = 0; j < widgets.size; j++)
		{
			UIWidget uIWidget = widgets.buffer[j];
			if (uIWidget.isVisible && uIWidget.hasVertices)
			{
				Material material = uIWidget.material;
				Texture mainTexture = uIWidget.mainTexture;
				Shader shader = uIWidget.shader;
				if ((Object)(object)val != (Object)(object)material || (Object)(object)val2 != (Object)(object)mainTexture || (Object)(object)val3 != (Object)(object)shader)
				{
					if ((Object)(object)uIDrawCall != (Object)null && uIDrawCall.verts.size != 0)
					{
						drawCalls.Add(uIDrawCall);
						uIDrawCall.UpdateGeometry();
						uIDrawCall = null;
					}
					val = material;
					val2 = mainTexture;
					val3 = shader;
				}
				if (!((Object)(object)val != (Object)null) && !((Object)(object)val3 != (Object)null) && !((Object)(object)val2 != (Object)null))
				{
					continue;
				}
				if ((Object)(object)uIDrawCall == (Object)null)
				{
					uIDrawCall = UIDrawCall.Create(this, val, val2, val3);
					uIDrawCall.depthStart = uIWidget.depth;
					uIDrawCall.depthEnd = uIDrawCall.depthStart;
					uIDrawCall.panel = this;
				}
				else
				{
					int num = uIWidget.depth;
					if (num < uIDrawCall.depthStart)
					{
						uIDrawCall.depthStart = num;
					}
					if (num > uIDrawCall.depthEnd)
					{
						uIDrawCall.depthEnd = num;
					}
				}
				uIWidget.drawCall = uIDrawCall;
				if (generateNormals)
				{
					uIWidget.WriteToBuffers(uIDrawCall.verts, uIDrawCall.uvs, uIDrawCall.cols, uIDrawCall.norms, uIDrawCall.tans);
				}
				else
				{
					uIWidget.WriteToBuffers(uIDrawCall.verts, uIDrawCall.uvs, uIDrawCall.cols, null, null);
				}
			}
			else
			{
				uIWidget.drawCall = null;
			}
		}
		if ((Object)(object)uIDrawCall != (Object)null && uIDrawCall.verts.size != 0)
		{
			drawCalls.Add(uIDrawCall);
			uIDrawCall.UpdateGeometry();
		}
	}

	private bool FillDrawCall(UIDrawCall dc)
	{
		if ((Object)(object)dc != (Object)null)
		{
			dc.isDirty = false;
			int num = 0;
			while (num < widgets.size)
			{
				UIWidget uIWidget = widgets[num];
				if ((Object)(object)uIWidget == (Object)null)
				{
					widgets.RemoveAt(num);
					continue;
				}
				if ((Object)(object)uIWidget.drawCall == (Object)(object)dc)
				{
					if (uIWidget.isVisible && uIWidget.hasVertices)
					{
						if (generateNormals)
						{
							uIWidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, dc.norms, dc.tans);
						}
						else
						{
							uIWidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, null, null);
						}
					}
					else
					{
						uIWidget.drawCall = null;
					}
				}
				num++;
			}
			if (dc.verts.size != 0)
			{
				dc.UpdateGeometry();
				return true;
			}
		}
		return false;
	}

	private void UpdateDrawCalls()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		Transform val = base.cachedTransform;
		bool num = usedForUI;
		if (clipping != 0)
		{
			drawCallClipRange = finalClipRegion;
			drawCallClipRange.z *= 0.5f;
			drawCallClipRange.w *= 0.5f;
		}
		else
		{
			drawCallClipRange = Vector4.zero;
		}
		if (drawCallClipRange.z == 0f)
		{
			drawCallClipRange.z = (float)Screen.width * 0.5f;
		}
		if (drawCallClipRange.w == 0f)
		{
			drawCallClipRange.w = (float)Screen.height * 0.5f;
		}
		if (halfPixelOffset)
		{
			drawCallClipRange.x -= 0.5f;
			drawCallClipRange.y += 0.5f;
		}
		Vector3 val3;
		if (num)
		{
			Transform val2 = base.cachedTransform.parent;
			val3 = base.cachedTransform.localPosition;
			if ((Object)(object)val2 != (Object)null)
			{
				float num2 = Mathf.Round(val3.x);
				float num3 = Mathf.Round(val3.y);
				drawCallClipRange.x += val3.x - num2;
				drawCallClipRange.y += val3.y - num3;
				val3.x = num2;
				val3.y = num3;
				val3 = val2.TransformPoint(val3);
			}
			val3 += drawCallOffset;
		}
		else
		{
			val3 = val.position;
		}
		Quaternion rotation = val.rotation;
		Vector3 lossyScale = val.lossyScale;
		for (int i = 0; i < drawCalls.size; i++)
		{
			UIDrawCall obj = drawCalls.buffer[i];
			Transform obj2 = obj.cachedTransform;
			obj2.position = val3;
			obj2.rotation = rotation;
			obj2.localScale = lossyScale;
			obj.renderQueue = ((renderQueue == RenderQueue.Explicit) ? startingRenderQueue : (startingRenderQueue + i));
			obj.alwaysOnScreen = alwaysOnScreen && (mClipping == UIDrawCall.Clipping.None || mClipping == UIDrawCall.Clipping.ConstrainButDontClip);
			obj.sortingOrder = mSortingOrder;
		}
	}

	private void UpdateLayers()
	{
		if (mLayer != base.cachedGameObject.layer)
		{
			mLayer = mGo.layer;
			UICamera uICamera = UICamera.FindCameraForLayer(mLayer);
			mCam = (((Object)(object)uICamera != (Object)null) ? uICamera.cachedCamera : NGUITools.FindCameraForLayer(mLayer));
			NGUITools.SetChildLayer(base.cachedTransform, mLayer);
			for (int i = 0; i < drawCalls.size; i++)
			{
				((Component)drawCalls.buffer[i]).gameObject.layer = mLayer;
			}
		}
	}

	private void UpdateWidgets()
	{
		bool flag = !cullWhileDragging && mCullTime > mUpdateTime;
		bool flag2 = false;
		if (mForced != flag)
		{
			mForced = flag;
			mResized = true;
		}
		bool flag3 = hasCumulativeClipping;
		int i = 0;
		for (int size = widgets.size; i < size; i++)
		{
			UIWidget uIWidget = widgets.buffer[i];
			if (!((Object)(object)uIWidget.panel == (Object)(object)this) || !((Behaviour)uIWidget).enabled)
			{
				continue;
			}
			int frameCount = Time.frameCount;
			if (uIWidget.UpdateTransform(frameCount) || mResized)
			{
				bool visibleByAlpha = flag || uIWidget.CalculateCumulativeAlpha(frameCount) > 0.001f;
				uIWidget.UpdateVisibility(visibleByAlpha, flag || (!flag3 && !uIWidget.hideIfOffScreen) || IsVisible(uIWidget));
			}
			if (!uIWidget.UpdateGeometry(frameCount))
			{
				continue;
			}
			flag2 = true;
			if (!mRebuild)
			{
				if ((Object)(object)uIWidget.drawCall != (Object)null)
				{
					uIWidget.drawCall.isDirty = true;
				}
				else
				{
					FindDrawCall(uIWidget);
				}
			}
		}
		if (flag2 && onGeometryUpdated != null)
		{
			onGeometryUpdated();
		}
		mResized = false;
	}

	public UIDrawCall FindDrawCall(UIWidget w)
	{
		Material material = w.material;
		Texture mainTexture = w.mainTexture;
		int num = w.depth;
		for (int i = 0; i < drawCalls.size; i++)
		{
			UIDrawCall uIDrawCall = drawCalls.buffer[i];
			int num2 = ((i == 0) ? int.MinValue : (drawCalls.buffer[i - 1].depthEnd + 1));
			int num3 = ((i + 1 == drawCalls.size) ? int.MaxValue : (drawCalls.buffer[i + 1].depthStart - 1));
			if (num2 > num || num3 < num)
			{
				continue;
			}
			if ((Object)(object)uIDrawCall.baseMaterial == (Object)(object)material && (Object)(object)uIDrawCall.mainTexture == (Object)(object)mainTexture)
			{
				if (w.isVisible)
				{
					w.drawCall = uIDrawCall;
					if (w.hasVertices)
					{
						uIDrawCall.isDirty = true;
					}
					return uIDrawCall;
				}
			}
			else
			{
				mRebuild = true;
			}
			return null;
		}
		mRebuild = true;
		return null;
	}

	public void AddWidget(UIWidget w)
	{
		mUpdateScroll = true;
		if (widgets.size == 0)
		{
			widgets.Add(w);
		}
		else if (mSortWidgets)
		{
			widgets.Add(w);
			SortWidgets();
		}
		else if (UIWidget.PanelCompareFunc(w, widgets[0]) == -1)
		{
			widgets.Insert(0, w);
		}
		else
		{
			int num = widgets.size;
			while (num > 0)
			{
				if (UIWidget.PanelCompareFunc(w, widgets[--num]) != -1)
				{
					widgets.Insert(num + 1, w);
					break;
				}
			}
		}
		FindDrawCall(w);
	}

	public void RemoveWidget(UIWidget w)
	{
		if (widgets.Remove(w) && (Object)(object)w.drawCall != (Object)null)
		{
			int num = w.depth;
			if (num == w.drawCall.depthStart || num == w.drawCall.depthEnd)
			{
				mRebuild = true;
			}
			w.drawCall.isDirty = true;
			w.drawCall = null;
		}
	}

	public void Refresh()
	{
		mRebuild = true;
		if (list.size > 0)
		{
			list[0].LateUpdate();
		}
	}

	public virtual Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = finalClipRegion;
		float num = val.z * 0.5f;
		float num2 = val.w * 0.5f;
		Vector2 minRect = new Vector2(min.x, min.y);
		Vector2 maxRect = default(Vector2);
		((Vector2)(ref maxRect))._002Ector(max.x, max.y);
		Vector2 minArea = default(Vector2);
		((Vector2)(ref minArea))._002Ector(val.x - num, val.y - num2);
		Vector2 maxArea = default(Vector2);
		((Vector2)(ref maxArea))._002Ector(val.x + num, val.y + num2);
		if (clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += clipSoftness.x;
			minArea.y += clipSoftness.y;
			maxArea.x -= clipSoftness.x;
			maxArea.y -= clipSoftness.y;
		}
		return Vector2.op_Implicit(NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea));
	}

	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = CalculateConstrainOffset(Vector2.op_Implicit(((Bounds)(ref targetBounds)).min), Vector2.op_Implicit(((Bounds)(ref targetBounds)).max));
		if (((Vector3)(ref val)).sqrMagnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += val;
				((Bounds)(ref targetBounds)).center = ((Bounds)(ref targetBounds)).center + val;
				SpringPosition component = ((Component)target).GetComponent<SpringPosition>();
				if ((Object)(object)component != (Object)null)
				{
					((Behaviour)component).enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(((Component)target).gameObject, target.localPosition + val, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Bounds targetBounds = NGUIMath.CalculateRelativeWidgetBounds(base.cachedTransform, target);
		return ConstrainTargetToBounds(target, ref targetBounds, immediate);
	}

	public static UIPanel Find(Transform trans)
	{
		return Find(trans, createIfMissing: false, -1);
	}

	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		return Find(trans, createIfMissing, -1);
	}

	public static UIPanel Find(Transform trans, bool createIfMissing, int layer)
	{
		UIPanel uIPanel = null;
		while ((Object)(object)uIPanel == (Object)null && (Object)(object)trans != (Object)null)
		{
			uIPanel = ((Component)trans).GetComponent<UIPanel>();
			if ((Object)(object)uIPanel != (Object)null)
			{
				return uIPanel;
			}
			if ((Object)(object)trans.parent == (Object)null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (!createIfMissing)
		{
			return null;
		}
		return NGUITools.CreateUI(trans, advanced3D: false, layer);
	}

	private Vector2 GetWindowSize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		UIRoot uIRoot = base.root;
		Vector2 val = NGUITools.screenSize;
		if ((Object)(object)uIRoot != (Object)null)
		{
			val *= uIRoot.GetPixelSizeAdjustment(Mathf.RoundToInt(val.y));
		}
		return val;
	}

	public Vector2 GetViewSize()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (mClipping != 0)
		{
			return new Vector2(mClipRange.z, mClipRange.w);
		}
		Vector2 val = NGUITools.screenSize;
		UIRoot uIRoot = base.root;
		if ((Object)(object)uIRoot != (Object)null)
		{
			val *= uIRoot.pixelSizeAdjustment;
		}
		return val;
	}
}
