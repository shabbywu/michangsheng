using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
public class UIDraggableCamera : MonoBehaviour
{
	public Transform rootForBounds;

	public Vector2 scale = Vector2.one;

	public float scrollWheelFactor;

	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	public bool smoothDragStart = true;

	public float momentumAmount = 35f;

	private Camera mCam;

	private Transform mTrans;

	private bool mPressed;

	private Vector2 mMomentum = Vector2.zero;

	private Bounds mBounds;

	private float mScroll;

	private UIRoot mRoot;

	private bool mDragStarted;

	public Vector2 currentMomentum
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mMomentum;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			mMomentum = value;
		}
	}

	private void Start()
	{
		mCam = ((Component)this).GetComponent<Camera>();
		mTrans = ((Component)this).transform;
		mRoot = NGUITools.FindInParents<UIRoot>(((Component)this).gameObject);
		if ((Object)(object)rootForBounds == (Object)null)
		{
			Debug.LogError((object)(NGUITools.GetHierarchy(((Component)this).gameObject) + " needs the 'Root For Bounds' parameter to be set"), (Object)(object)this);
			((Behaviour)this).enabled = false;
		}
	}

	private Vector3 CalculateConstrainOffset()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)rootForBounds == (Object)null || rootForBounds.childCount == 0)
		{
			return Vector3.zero;
		}
		Rect rect = mCam.rect;
		float num = ((Rect)(ref rect)).xMin * (float)Screen.width;
		rect = mCam.rect;
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(num, ((Rect)(ref rect)).yMin * (float)Screen.height, 0f);
		rect = mCam.rect;
		float num2 = ((Rect)(ref rect)).xMax * (float)Screen.width;
		rect = mCam.rect;
		Vector3 val2 = default(Vector3);
		((Vector3)(ref val2))._002Ector(num2, ((Rect)(ref rect)).yMax * (float)Screen.height, 0f);
		val = mCam.ScreenToWorldPoint(val);
		val2 = mCam.ScreenToWorldPoint(val2);
		Vector2 minRect = new Vector2(((Bounds)(ref mBounds)).min.x, ((Bounds)(ref mBounds)).min.y);
		Vector2 maxRect = default(Vector2);
		((Vector2)(ref maxRect))._002Ector(((Bounds)(ref mBounds)).max.x, ((Bounds)(ref mBounds)).max.y);
		return Vector2.op_Implicit(NGUIMath.ConstrainRect(minRect, maxRect, Vector2.op_Implicit(val), Vector2.op_Implicit(val2)));
	}

	public bool ConstrainToBounds(bool immediate)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mTrans != (Object)null && (Object)(object)rootForBounds != (Object)null)
		{
			Vector3 val = CalculateConstrainOffset();
			if (((Vector3)(ref val)).sqrMagnitude > 0f)
			{
				if (immediate)
				{
					Transform obj = mTrans;
					obj.position -= val;
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(((Component)this).gameObject, mTrans.position - val, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	public void Press(bool isPressed)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (isPressed)
		{
			mDragStarted = false;
		}
		if (!((Object)(object)rootForBounds != (Object)null))
		{
			return;
		}
		mPressed = isPressed;
		if (isPressed)
		{
			mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
			mMomentum = Vector2.zero;
			mScroll = 0f;
			SpringPosition component = ((Component)this).GetComponent<SpringPosition>();
			if ((Object)(object)component != (Object)null)
			{
				((Behaviour)component).enabled = false;
			}
		}
		else if (dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
		{
			ConstrainToBounds(immediate: false);
		}
	}

	public void Drag(Vector2 delta)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if ((Object)(object)mRoot != (Object)null)
		{
			delta *= mRoot.pixelSizeAdjustment;
		}
		Vector2 val = Vector2.Scale(delta, -scale);
		Transform obj = mTrans;
		obj.localPosition += Vector2.op_Implicit(val);
		mMomentum = Vector2.Lerp(mMomentum, mMomentum + val * (0.01f * momentumAmount), 0.67f);
		if (dragEffect != UIDragObject.DragEffect.MomentumAndSpring && ConstrainToBounds(immediate: true))
		{
			mMomentum = Vector2.zero;
			mScroll = 0f;
		}
	}

	public void Scroll(float delta)
	{
		if (((Behaviour)this).enabled && NGUITools.GetActive(((Component)this).gameObject))
		{
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}

	private void Update()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = RealTime.deltaTime;
		if (mPressed)
		{
			SpringPosition component = ((Component)this).GetComponent<SpringPosition>();
			if ((Object)(object)component != (Object)null)
			{
				((Behaviour)component).enabled = false;
			}
			mScroll = 0f;
		}
		else
		{
			mMomentum += scale * (mScroll * 20f);
			mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
			if (((Vector2)(ref mMomentum)).magnitude > 0.01f)
			{
				Transform obj = mTrans;
				obj.localPosition += Vector2.op_Implicit(NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime));
				mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
				if (!ConstrainToBounds(dragEffect == UIDragObject.DragEffect.None))
				{
					SpringPosition component2 = ((Component)this).GetComponent<SpringPosition>();
					if ((Object)(object)component2 != (Object)null)
					{
						((Behaviour)component2).enabled = false;
					}
				}
				return;
			}
			mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
	}
}
