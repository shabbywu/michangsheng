using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001B286 File Offset: 0x00019486
	// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001B28E File Offset: 0x0001948E
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

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001B298 File Offset: 0x00019498
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

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001B313 File Offset: 0x00019513
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001B31C File Offset: 0x0001951C
	private void FindPanel()
	{
		this.mPanel = ((this.target != null) ? UIPanel.Find(this.target.transform.parent) : null);
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001B36C File Offset: 0x0001956C
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

	// Token: 0x060004F8 RID: 1272 RVA: 0x0001B418 File Offset: 0x00019618
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

	// Token: 0x060004F9 RID: 1273 RVA: 0x0001B54C File Offset: 0x0001974C
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

	// Token: 0x060004FA RID: 1274 RVA: 0x0001B700 File Offset: 0x00019900
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

	// Token: 0x060004FB RID: 1275 RVA: 0x0001B7B0 File Offset: 0x000199B0
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

	// Token: 0x060004FC RID: 1276 RVA: 0x0001B8D4 File Offset: 0x00019AD4
	public void CancelMovement()
	{
		this.mTargetPos = ((this.target != null) ? this.target.position : Vector3.zero);
		this.mMomentum = Vector3.zero;
		this.mScroll = Vector3.zero;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0001B914 File Offset: 0x00019B14
	public void CancelSpring()
	{
		SpringPosition component = this.target.GetComponent<SpringPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0001B93D File Offset: 0x00019B3D
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.mScroll -= this.scrollMomentum * (delta * 0.05f);
		}
	}

	// Token: 0x04000310 RID: 784
	public Transform target;

	// Token: 0x04000311 RID: 785
	public Vector3 scrollMomentum = Vector3.zero;

	// Token: 0x04000312 RID: 786
	public bool restrictWithinPanel;

	// Token: 0x04000313 RID: 787
	public UIRect contentRect;

	// Token: 0x04000314 RID: 788
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04000315 RID: 789
	public float momentumAmount = 35f;

	// Token: 0x04000316 RID: 790
	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);

	// Token: 0x04000317 RID: 791
	[SerializeField]
	[HideInInspector]
	private float scrollWheelFactor;

	// Token: 0x04000318 RID: 792
	private Plane mPlane;

	// Token: 0x04000319 RID: 793
	private Vector3 mTargetPos;

	// Token: 0x0400031A RID: 794
	private Vector3 mLastPos;

	// Token: 0x0400031B RID: 795
	private UIPanel mPanel;

	// Token: 0x0400031C RID: 796
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x0400031D RID: 797
	private Vector3 mScroll = Vector3.zero;

	// Token: 0x0400031E RID: 798
	private Bounds mBounds;

	// Token: 0x0400031F RID: 799
	private int mTouchID;

	// Token: 0x04000320 RID: 800
	private bool mStarted;

	// Token: 0x04000321 RID: 801
	private bool mPressed;

	// Token: 0x020011E1 RID: 4577
	public enum DragEffect
	{
		// Token: 0x040063D4 RID: 25556
		None,
		// Token: 0x040063D5 RID: 25557
		Momentum,
		// Token: 0x040063D6 RID: 25558
		MomentumAndSpring
	}
}
