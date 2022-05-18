using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x00008B25 File Offset: 0x00006D25
	// (set) Token: 0x06000545 RID: 1349 RVA: 0x00008B2D File Offset: 0x00006D2D
	public Vector3 dragMovement
	{
		get
		{
			return this.scale;
		}
		set
		{
			this.scale = value;
		}
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00071CD0 File Offset: 0x0006FED0
	private void OnEnable()
	{
		if (this.scrollWheelFactor != 0f)
		{
			this.scrollMomentum = this.scale * this.scrollWheelFactor;
			this.scrollWheelFactor = 0f;
		}
		if (this.contentRect == null && this.target != null && Application.isPlaying)
		{
			UIWidget component = this.target.GetComponent<UIWidget>();
			if (component != null)
			{
				this.contentRect = component;
			}
		}
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00008B36 File Offset: 0x00006D36
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00071D4C File Offset: 0x0006FF4C
	private void FindPanel()
	{
		this.mPanel = ((this.target != null) ? UIPanel.Find(this.target.transform.parent) : null);
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00071D9C File Offset: 0x0006FF9C
	private void UpdateBounds()
	{
		if (this.contentRect)
		{
			Matrix4x4 worldToLocalMatrix = this.mPanel.cachedTransform.worldToLocalMatrix;
			Vector3[] worldCorners = this.contentRect.worldCorners;
			for (int i = 0; i < 4; i++)
			{
				worldCorners[i] = worldToLocalMatrix.MultiplyPoint3x4(worldCorners[i]);
			}
			this.mBounds = new Bounds(worldCorners[0], Vector3.zero);
			for (int j = 1; j < 4; j++)
			{
				this.mBounds.Encapsulate(worldCorners[j]);
			}
			return;
		}
		this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00071E48 File Offset: 0x00070048
	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			if (pressed)
			{
				if (!this.mPressed)
				{
					this.mTouchID = UICamera.currentTouchID;
					this.mPressed = true;
					this.mStarted = false;
					this.CancelMovement();
					if (this.restrictWithinPanel && this.mPanel == null)
					{
						this.FindPanel();
					}
					if (this.restrictWithinPanel)
					{
						this.UpdateBounds();
					}
					this.CancelSpring();
					Transform transform = UICamera.currentCamera.transform;
					this.mPlane = new Plane(((this.mPanel != null) ? this.mPanel.cachedTransform.rotation : transform.rotation) * Vector3.back, UICamera.lastWorldPosition);
					return;
				}
			}
			else if (this.mPressed && this.mTouchID == UICamera.currentTouchID)
			{
				this.mPressed = false;
				if (this.restrictWithinPanel && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false))
				{
					this.CancelMovement();
				}
			}
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00071F7C File Offset: 0x0007017C
	private void OnDrag(Vector2 delta)
	{
		if (this.mPressed && this.mTouchID == UICamera.currentTouchID && base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float num = 0f;
			if (this.mPlane.Raycast(ray, ref num))
			{
				Vector3 point = ray.GetPoint(num);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (!this.mStarted)
				{
					this.mStarted = true;
					vector = Vector3.zero;
				}
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.target.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.target.TransformDirection(vector);
				}
				if (this.dragEffect != UIDragObject.DragEffect.None)
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				Vector3 localPosition = this.target.localPosition;
				this.Move(vector);
				if (this.restrictWithinPanel)
				{
					this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
					if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
					{
						this.CancelMovement();
					}
				}
			}
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00072130 File Offset: 0x00070330
	private void Move(Vector3 worldDelta)
	{
		if (this.mPanel != null)
		{
			this.mTargetPos += worldDelta;
			this.target.position = this.mTargetPos;
			Vector3 localPosition = this.target.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			this.target.localPosition = localPosition;
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
				return;
			}
		}
		else
		{
			this.target.position += worldDelta;
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x000721E0 File Offset: 0x000703E0
	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		this.mMomentum -= this.mScroll;
		this.mScroll = NGUIMath.SpringLerp(this.mScroll, Vector3.zero, 20f, deltaTime);
		if (!this.mPressed)
		{
			if (this.mMomentum.magnitude < 0.0001f)
			{
				return;
			}
			if (this.mPanel == null)
			{
				this.FindPanel();
			}
			this.Move(NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime));
			if (this.restrictWithinPanel && this.mPanel != null)
			{
				this.UpdateBounds();
				if (this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
				{
					this.CancelMovement();
				}
				else
				{
					this.CancelSpring();
				}
			}
		}
		else
		{
			this.mTargetPos = ((this.target != null) ? this.target.position : Vector3.zero);
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00008B3F File Offset: 0x00006D3F
	public void CancelMovement()
	{
		this.mTargetPos = ((this.target != null) ? this.target.position : Vector3.zero);
		this.mMomentum = Vector3.zero;
		this.mScroll = Vector3.zero;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00072304 File Offset: 0x00070504
	public void CancelSpring()
	{
		SpringPosition component = this.target.GetComponent<SpringPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00008B7D File Offset: 0x00006D7D
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.mScroll -= this.scrollMomentum * (delta * 0.05f);
		}
	}

	// Token: 0x04000394 RID: 916
	public Transform target;

	// Token: 0x04000395 RID: 917
	public Vector3 scrollMomentum = Vector3.zero;

	// Token: 0x04000396 RID: 918
	public bool restrictWithinPanel;

	// Token: 0x04000397 RID: 919
	public UIRect contentRect;

	// Token: 0x04000398 RID: 920
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04000399 RID: 921
	public float momentumAmount = 35f;

	// Token: 0x0400039A RID: 922
	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);

	// Token: 0x0400039B RID: 923
	[SerializeField]
	[HideInInspector]
	private float scrollWheelFactor;

	// Token: 0x0400039C RID: 924
	private Plane mPlane;

	// Token: 0x0400039D RID: 925
	private Vector3 mTargetPos;

	// Token: 0x0400039E RID: 926
	private Vector3 mLastPos;

	// Token: 0x0400039F RID: 927
	private UIPanel mPanel;

	// Token: 0x040003A0 RID: 928
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x040003A1 RID: 929
	private Vector3 mScroll = Vector3.zero;

	// Token: 0x040003A2 RID: 930
	private Bounds mBounds;

	// Token: 0x040003A3 RID: 931
	private int mTouchID;

	// Token: 0x040003A4 RID: 932
	private bool mStarted;

	// Token: 0x040003A5 RID: 933
	private bool mPressed;

	// Token: 0x02000081 RID: 129
	public enum DragEffect
	{
		// Token: 0x040003A7 RID: 935
		None,
		// Token: 0x040003A8 RID: 936
		Momentum,
		// Token: 0x040003A9 RID: 937
		MomentumAndSpring
	}
}
