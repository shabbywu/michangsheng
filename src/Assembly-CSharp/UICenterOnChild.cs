using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild : MonoBehaviour
{
	public delegate void OnCenterCallback(GameObject centeredObject);

	public float springStrength = 8f;

	public float nextPageThreshold;

	public SpringPanel.OnFinished onFinished;

	public OnCenterCallback onCenter;

	private UIScrollView mScrollView;

	private GameObject mCenteredObject;

	public GameObject centeredObject => mCenteredObject;

	private void OnEnable()
	{
		Recenter();
		if (Object.op_Implicit((Object)(object)mScrollView))
		{
			mScrollView.onDragFinished = OnDragFinished;
		}
	}

	private void OnDisable()
	{
		if (Object.op_Implicit((Object)(object)mScrollView))
		{
			UIScrollView uIScrollView = mScrollView;
			uIScrollView.onDragFinished = (UIScrollView.OnDragNotification)Delegate.Remove(uIScrollView.onDragFinished, new UIScrollView.OnDragNotification(OnDragFinished));
		}
	}

	private void OnDragFinished()
	{
		if (((Behaviour)this).enabled)
		{
			Recenter();
		}
	}

	private void OnValidate()
	{
		nextPageThreshold = Mathf.Abs(nextPageThreshold);
	}

	[ContextMenu("Execute")]
	public void Recenter()
	{
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mScrollView == (Object)null)
		{
			mScrollView = NGUITools.FindInParents<UIScrollView>(((Component)this).gameObject);
			if ((Object)(object)mScrollView == (Object)null)
			{
				Debug.LogWarning((object)string.Concat(((object)this).GetType(), " requires ", typeof(UIScrollView), " on a parent object in order to work"), (Object)(object)this);
				((Behaviour)this).enabled = false;
				return;
			}
			mScrollView.onDragFinished = OnDragFinished;
			if ((Object)(object)mScrollView.horizontalScrollBar != (Object)null)
			{
				mScrollView.horizontalScrollBar.onDragFinished = OnDragFinished;
			}
			if ((Object)(object)mScrollView.verticalScrollBar != (Object)null)
			{
				mScrollView.verticalScrollBar.onDragFinished = OnDragFinished;
			}
		}
		if ((Object)(object)mScrollView.panel == (Object)null)
		{
			return;
		}
		Transform transform = ((Component)this).transform;
		if (transform.childCount == 0)
		{
			return;
		}
		Vector3[] worldCorners = mScrollView.panel.worldCorners;
		Vector3 val = (worldCorners[2] + worldCorners[0]) * 0.5f;
		Vector3 velocity = mScrollView.currentMomentum * mScrollView.momentumAmount;
		Vector3 val2 = NGUIMath.SpringDampen(ref velocity, 9f, 2f);
		Vector3 val3 = val - val2 * 0.05f;
		mScrollView.currentMomentum = Vector3.zero;
		float num = float.MaxValue;
		Transform target = null;
		int num2 = 0;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (((Component)child).gameObject.activeInHierarchy)
			{
				float num3 = Vector3.SqrMagnitude(child.position - val3);
				if (num3 < num)
				{
					num = num3;
					target = child;
					num2 = i;
				}
			}
		}
		if (nextPageThreshold > 0f && UICamera.currentTouch != null && (Object)(object)mCenteredObject != (Object)null && (Object)(object)mCenteredObject.transform == (Object)(object)transform.GetChild(num2))
		{
			Vector2 totalDelta = UICamera.currentTouch.totalDelta;
			float num4 = 0f;
			num4 = mScrollView.movement switch
			{
				UIScrollView.Movement.Horizontal => totalDelta.x, 
				UIScrollView.Movement.Vertical => totalDelta.y, 
				_ => ((Vector2)(ref totalDelta)).magnitude, 
			};
			if (num4 > nextPageThreshold)
			{
				if (num2 > 0)
				{
					target = transform.GetChild(num2 - 1);
				}
			}
			else if (num4 < 0f - nextPageThreshold && num2 < transform.childCount - 1)
			{
				target = transform.GetChild(num2 + 1);
			}
		}
		CenterOn(target, val);
	}

	private void CenterOn(Transform target, Vector3 panelCenter)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)target != (Object)null && (Object)(object)mScrollView != (Object)null && (Object)(object)mScrollView.panel != (Object)null)
		{
			Transform cachedTransform = mScrollView.panel.cachedTransform;
			mCenteredObject = ((Component)target).gameObject;
			Vector3 val = cachedTransform.InverseTransformPoint(target.position);
			Vector3 val2 = cachedTransform.InverseTransformPoint(panelCenter);
			Vector3 val3 = val - val2;
			if (!mScrollView.canMoveHorizontally)
			{
				val3.x = 0f;
			}
			if (!mScrollView.canMoveVertically)
			{
				val3.y = 0f;
			}
			val3.z = 0f;
			SpringPanel.Begin(mScrollView.panel.cachedGameObject, cachedTransform.localPosition - val3, springStrength).onFinished = onFinished;
		}
		else
		{
			mCenteredObject = null;
		}
		if (onCenter != null)
		{
			onCenter(mCenteredObject);
		}
	}

	public void CenterOn(Transform target)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mScrollView != (Object)null && (Object)(object)mScrollView.panel != (Object)null)
		{
			Vector3[] worldCorners = mScrollView.panel.worldCorners;
			Vector3 panelCenter = (worldCorners[2] + worldCorners[0]) * 0.5f;
			CenterOn(target, panelCenter);
		}
	}
}
