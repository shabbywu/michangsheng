using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild : MonoBehaviour
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001A1DA File Offset: 0x000183DA
	public GameObject centeredObject
	{
		get
		{
			return this.mCenteredObject;
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0001A1E2 File Offset: 0x000183E2
	private void OnEnable()
	{
		this.Recenter();
		if (this.mScrollView)
		{
			this.mScrollView.onDragFinished = new UIScrollView.OnDragNotification(this.OnDragFinished);
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0001A20E File Offset: 0x0001840E
	private void OnDisable()
	{
		if (this.mScrollView)
		{
			UIScrollView uiscrollView = this.mScrollView;
			uiscrollView.onDragFinished = (UIScrollView.OnDragNotification)Delegate.Remove(uiscrollView.onDragFinished, new UIScrollView.OnDragNotification(this.OnDragFinished));
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0001A244 File Offset: 0x00018444
	private void OnDragFinished()
	{
		if (base.enabled)
		{
			this.Recenter();
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0001A254 File Offset: 0x00018454
	private void OnValidate()
	{
		this.nextPageThreshold = Mathf.Abs(this.nextPageThreshold);
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0001A268 File Offset: 0x00018468
	[ContextMenu("Execute")]
	public void Recenter()
	{
		if (this.mScrollView == null)
		{
			this.mScrollView = NGUITools.FindInParents<UIScrollView>(base.gameObject);
			if (this.mScrollView == null)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					base.GetType(),
					" requires ",
					typeof(UIScrollView),
					" on a parent object in order to work"
				}), this);
				base.enabled = false;
				return;
			}
			this.mScrollView.onDragFinished = new UIScrollView.OnDragNotification(this.OnDragFinished);
			if (this.mScrollView.horizontalScrollBar != null)
			{
				this.mScrollView.horizontalScrollBar.onDragFinished = new UIProgressBar.OnDragFinished(this.OnDragFinished);
			}
			if (this.mScrollView.verticalScrollBar != null)
			{
				this.mScrollView.verticalScrollBar.onDragFinished = new UIProgressBar.OnDragFinished(this.OnDragFinished);
			}
		}
		if (this.mScrollView.panel == null)
		{
			return;
		}
		Transform transform = base.transform;
		if (transform.childCount == 0)
		{
			return;
		}
		Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
		Vector3 vector = (worldCorners[2] + worldCorners[0]) * 0.5f;
		Vector3 vector2 = this.mScrollView.currentMomentum * this.mScrollView.momentumAmount;
		Vector3 vector3 = NGUIMath.SpringDampen(ref vector2, 9f, 2f);
		Vector3 vector4 = vector - vector3 * 0.05f;
		this.mScrollView.currentMomentum = Vector3.zero;
		float num = float.MaxValue;
		Transform target = null;
		int num2 = 0;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			Transform child = transform.GetChild(i);
			if (child.gameObject.activeInHierarchy)
			{
				float num3 = Vector3.SqrMagnitude(child.position - vector4);
				if (num3 < num)
				{
					num = num3;
					target = child;
					num2 = i;
				}
			}
			i++;
		}
		if (this.nextPageThreshold > 0f && UICamera.currentTouch != null && this.mCenteredObject != null && this.mCenteredObject.transform == transform.GetChild(num2))
		{
			Vector2 totalDelta = UICamera.currentTouch.totalDelta;
			UIScrollView.Movement movement = this.mScrollView.movement;
			float num4;
			if (movement != UIScrollView.Movement.Horizontal)
			{
				if (movement != UIScrollView.Movement.Vertical)
				{
					num4 = totalDelta.magnitude;
				}
				else
				{
					num4 = totalDelta.y;
				}
			}
			else
			{
				num4 = totalDelta.x;
			}
			if (num4 > this.nextPageThreshold)
			{
				if (num2 > 0)
				{
					target = transform.GetChild(num2 - 1);
				}
			}
			else if (num4 < -this.nextPageThreshold && num2 < transform.childCount - 1)
			{
				target = transform.GetChild(num2 + 1);
			}
		}
		this.CenterOn(target, vector);
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0001A540 File Offset: 0x00018740
	private void CenterOn(Transform target, Vector3 panelCenter)
	{
		if (target != null && this.mScrollView != null && this.mScrollView.panel != null)
		{
			Transform cachedTransform = this.mScrollView.panel.cachedTransform;
			this.mCenteredObject = target.gameObject;
			Vector3 vector = cachedTransform.InverseTransformPoint(target.position);
			Vector3 vector2 = cachedTransform.InverseTransformPoint(panelCenter);
			Vector3 vector3 = vector - vector2;
			if (!this.mScrollView.canMoveHorizontally)
			{
				vector3.x = 0f;
			}
			if (!this.mScrollView.canMoveVertically)
			{
				vector3.y = 0f;
			}
			vector3.z = 0f;
			SpringPanel.Begin(this.mScrollView.panel.cachedGameObject, cachedTransform.localPosition - vector3, this.springStrength).onFinished = this.onFinished;
		}
		else
		{
			this.mCenteredObject = null;
		}
		if (this.onCenter != null)
		{
			this.onCenter(this.mCenteredObject);
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0001A64C File Offset: 0x0001884C
	public void CenterOn(Transform target)
	{
		if (this.mScrollView != null && this.mScrollView.panel != null)
		{
			Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
			Vector3 panelCenter = (worldCorners[2] + worldCorners[0]) * 0.5f;
			this.CenterOn(target, panelCenter);
		}
	}

	// Token: 0x040002EC RID: 748
	public float springStrength = 8f;

	// Token: 0x040002ED RID: 749
	public float nextPageThreshold;

	// Token: 0x040002EE RID: 750
	public SpringPanel.OnFinished onFinished;

	// Token: 0x040002EF RID: 751
	public UICenterOnChild.OnCenterCallback onCenter;

	// Token: 0x040002F0 RID: 752
	private UIScrollView mScrollView;

	// Token: 0x040002F1 RID: 753
	private GameObject mCenteredObject;

	// Token: 0x020011DF RID: 4575
	// (Invoke) Token: 0x06007805 RID: 30725
	public delegate void OnCenterCallback(GameObject centeredObject);
}
