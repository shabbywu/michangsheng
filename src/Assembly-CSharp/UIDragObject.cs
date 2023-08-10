using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
	public enum DragEffect
	{
		None,
		Momentum,
		MomentumAndSpring
	}

	public Transform target;

	public Vector3 scrollMomentum = Vector3.zero;

	public bool restrictWithinPanel;

	public UIRect contentRect;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public float momentumAmount = 35f;

	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);

	[SerializeField]
	[HideInInspector]
	private float scrollWheelFactor;

	private Plane mPlane;

	private Vector3 mTargetPos;

	private Vector3 mLastPos;

	private UIPanel mPanel;

	private Vector3 mMomentum = Vector3.zero;

	private Vector3 mScroll = Vector3.zero;

	private Bounds mBounds;

	private int mTouchID;

	private bool mStarted;

	private bool mPressed;

	public Vector3 dragMovement
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return scale;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			scale = value;
		}
	}

	private void OnEnable()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (scrollWheelFactor != 0f)
		{
			scrollMomentum = scale * scrollWheelFactor;
			scrollWheelFactor = 0f;
		}
		if ((Object)(object)contentRect == (Object)null && (Object)(object)target != (Object)null && Application.isPlaying)
		{
			UIWidget component = ((Component)target).GetComponent<UIWidget>();
			if ((Object)(object)component != (Object)null)
			{
				contentRect = component;
			}
		}
	}

	private void OnDisable()
	{
		mStarted = false;
	}

	private void FindPanel()
	{
		mPanel = (((Object)(object)target != (Object)null) ? UIPanel.Find(((Component)target).transform.parent) : null);
		if ((Object)(object)mPanel == (Object)null)
		{
			restrictWithinPanel = false;
		}
	}

	private void UpdateBounds()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)contentRect))
		{
			Matrix4x4 worldToLocalMatrix = mPanel.cachedTransform.worldToLocalMatrix;
			Vector3[] worldCorners = contentRect.worldCorners;
			for (int i = 0; i < 4; i++)
			{
				worldCorners[i] = ((Matrix4x4)(ref worldToLocalMatrix)).MultiplyPoint3x4(worldCorners[i]);
			}
			mBounds = new Bounds(worldCorners[0], Vector3.zero);
			for (int j = 1; j < 4; j++)
			{
				((Bounds)(ref mBounds)).Encapsulate(worldCorners[j]);
			}
		}
		else
		{
			mBounds = NGUIMath.CalculateRelativeWidgetBounds(mPanel.cachedTransform, target);
		}
	}

	private void OnPress(bool pressed)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (!((Behaviour)this).enabled || !NGUITools.GetActive(((Component)this).gameObject) || !((Object)(object)target != (Object)null))
		{
			return;
		}
		if (pressed)
		{
			if (!mPressed)
			{
				mTouchID = UICamera.currentTouchID;
				mPressed = true;
				mStarted = false;
				CancelMovement();
				if (restrictWithinPanel && (Object)(object)mPanel == (Object)null)
				{
					FindPanel();
				}
				if (restrictWithinPanel)
				{
					UpdateBounds();
				}
				CancelSpring();
				Transform transform = ((Component)UICamera.currentCamera).transform;
				mPlane = new Plane((((Object)(object)mPanel != (Object)null) ? mPanel.cachedTransform.rotation : transform.rotation) * Vector3.back, UICamera.lastWorldPosition);
			}
		}
		else if (mPressed && mTouchID == UICamera.currentTouchID)
		{
			mPressed = false;
			if (restrictWithinPanel && dragEffect == DragEffect.MomentumAndSpring && mPanel.ConstrainTargetToBounds(target, ref mBounds, immediate: false))
			{
				CancelMovement();
			}
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		if (!mPressed || mTouchID != UICamera.currentTouchID || !((Behaviour)this).enabled || !NGUITools.GetActive(((Component)this).gameObject) || !((Object)(object)target != (Object)null))
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		Ray val = UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos));
		float num = 0f;
		if (!((Plane)(ref mPlane)).Raycast(val, ref num))
		{
			return;
		}
		Vector3 point = ((Ray)(ref val)).GetPoint(num);
		Vector3 val2 = point - mLastPos;
		mLastPos = point;
		if (!mStarted)
		{
			mStarted = true;
			val2 = Vector3.zero;
		}
		if (val2.x != 0f || val2.y != 0f)
		{
			val2 = target.InverseTransformDirection(val2);
			((Vector3)(ref val2)).Scale(scale);
			val2 = target.TransformDirection(val2);
		}
		if (dragEffect != 0)
		{
			mMomentum = Vector3.Lerp(mMomentum, mMomentum + val2 * (0.01f * momentumAmount), 0.67f);
		}
		Vector3 localPosition = target.localPosition;
		Move(val2);
		if (restrictWithinPanel)
		{
			((Bounds)(ref mBounds)).center = ((Bounds)(ref mBounds)).center + (target.localPosition - localPosition);
			if (dragEffect != DragEffect.MomentumAndSpring && mPanel.ConstrainTargetToBounds(target, ref mBounds, immediate: true))
			{
				CancelMovement();
			}
		}
	}

	private void Move(Vector3 worldDelta)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mPanel != (Object)null)
		{
			mTargetPos += worldDelta;
			target.position = mTargetPos;
			Vector3 localPosition = target.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			target.localPosition = localPosition;
			UIScrollView component = ((Component)mPanel).GetComponent<UIScrollView>();
			if ((Object)(object)component != (Object)null)
			{
				component.UpdateScrollbars(recalculateBounds: true);
			}
		}
		else
		{
			Transform obj = target;
			obj.position += worldDelta;
		}
	}

	private void LateUpdate()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)target == (Object)null)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		mMomentum -= mScroll;
		mScroll = NGUIMath.SpringLerp(mScroll, Vector3.zero, 20f, deltaTime);
		if (!mPressed)
		{
			if (((Vector3)(ref mMomentum)).magnitude < 0.0001f)
			{
				return;
			}
			if ((Object)(object)mPanel == (Object)null)
			{
				FindPanel();
			}
			Move(NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime));
			if (restrictWithinPanel && (Object)(object)mPanel != (Object)null)
			{
				UpdateBounds();
				if (mPanel.ConstrainTargetToBounds(target, ref mBounds, dragEffect == DragEffect.None))
				{
					CancelMovement();
				}
				else
				{
					CancelSpring();
				}
			}
		}
		else
		{
			mTargetPos = (((Object)(object)target != (Object)null) ? target.position : Vector3.zero);
		}
		NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
	}

	public void CancelMovement()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		mTargetPos = (((Object)(object)target != (Object)null) ? target.position : Vector3.zero);
		mMomentum = Vector3.zero;
		mScroll = Vector3.zero;
	}

	public void CancelSpring()
	{
		SpringPosition component = ((Component)target).GetComponent<SpringPosition>();
		if ((Object)(object)component != (Object)null)
		{
			((Behaviour)component).enabled = false;
		}
	}

	private void OnScroll(float delta)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled && NGUITools.GetActive(((Component)this).gameObject))
		{
			mScroll -= scrollMomentum * (delta * 0.05f);
		}
	}
}
