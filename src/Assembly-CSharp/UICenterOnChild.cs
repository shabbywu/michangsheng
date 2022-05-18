using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild : MonoBehaviour
{
	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000519 RID: 1305 RVA: 0x00008892 File Offset: 0x00006A92
	public GameObject centeredObject
	{
		get
		{
			return this.mCenteredObject;
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0000889A File Offset: 0x00006A9A
	private void OnEnable()
	{
		this.Recenter();
		if (this.mScrollView)
		{
			this.mScrollView.onDragFinished = new UIScrollView.OnDragNotification(this.OnDragFinished);
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000088C6 File Offset: 0x00006AC6
	private void OnDisable()
	{
		if (this.mScrollView)
		{
			UIScrollView uiscrollView = this.mScrollView;
			uiscrollView.onDragFinished = (UIScrollView.OnDragNotification)Delegate.Remove(uiscrollView.onDragFinished, new UIScrollView.OnDragNotification(this.OnDragFinished));
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000088FC File Offset: 0x00006AFC
	private void OnDragFinished()
	{
		if (base.enabled)
		{
			this.Recenter();
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0000890C File Offset: 0x00006B0C
	private void OnValidate()
	{
		this.nextPageThreshold = Mathf.Abs(this.nextPageThreshold);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00070EB4 File Offset: 0x0006F0B4
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

	// Token: 0x0600051F RID: 1311 RVA: 0x0007118C File Offset: 0x0006F38C
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

	// Token: 0x06000520 RID: 1312 RVA: 0x00071298 File Offset: 0x0006F498
	public void CenterOn(Transform target)
	{
		if (this.mScrollView != null && this.mScrollView.panel != null)
		{
			Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
			Vector3 panelCenter = (worldCorners[2] + worldCorners[0]) * 0.5f;
			this.CenterOn(target, panelCenter);
		}
	}

	// Token: 0x0400036B RID: 875
	public float springStrength = 8f;

	// Token: 0x0400036C RID: 876
	public float nextPageThreshold;

	// Token: 0x0400036D RID: 877
	public SpringPanel.OnFinished onFinished;

	// Token: 0x0400036E RID: 878
	public UICenterOnChild.OnCenterCallback onCenter;

	// Token: 0x0400036F RID: 879
	private UIScrollView mScrollView;

	// Token: 0x04000370 RID: 880
	private GameObject mCenteredObject;

	// Token: 0x02000078 RID: 120
	// (Invoke) Token: 0x06000523 RID: 1315
	public delegate void OnCenterCallback(GameObject centeredObject);
}
