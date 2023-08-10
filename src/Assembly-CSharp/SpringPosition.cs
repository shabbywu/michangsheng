using UnityEngine;

[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	public delegate void OnFinished();

	public static SpringPosition current;

	public Vector3 target = Vector3.zero;

	public float strength = 10f;

	public bool worldSpace;

	public bool ignoreTimeScale;

	public bool updateScrollView;

	public OnFinished onFinished;

	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	private Transform mTrans;

	private float mThreshold;

	private UIScrollView mSv;

	private void Start()
	{
		mTrans = ((Component)this).transform;
		if (updateScrollView)
		{
			mSv = NGUITools.FindInParents<UIScrollView>(((Component)this).gameObject);
		}
	}

	private void Update()
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = (ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
		Vector3 val;
		if (worldSpace)
		{
			if (mThreshold == 0f)
			{
				val = target - mTrans.position;
				mThreshold = ((Vector3)(ref val)).sqrMagnitude * 0.001f;
			}
			mTrans.position = NGUIMath.SpringLerp(mTrans.position, target, strength, deltaTime);
			float num = mThreshold;
			val = target - mTrans.position;
			if (num >= ((Vector3)(ref val)).sqrMagnitude)
			{
				mTrans.position = target;
				NotifyListeners();
				((Behaviour)this).enabled = false;
			}
		}
		else
		{
			if (mThreshold == 0f)
			{
				val = target - mTrans.localPosition;
				mThreshold = ((Vector3)(ref val)).sqrMagnitude * 1E-05f;
			}
			mTrans.localPosition = NGUIMath.SpringLerp(mTrans.localPosition, target, strength, deltaTime);
			float num2 = mThreshold;
			val = target - mTrans.localPosition;
			if (num2 >= ((Vector3)(ref val)).sqrMagnitude)
			{
				mTrans.localPosition = target;
				NotifyListeners();
				((Behaviour)this).enabled = false;
			}
		}
		if ((Object)(object)mSv != (Object)null)
		{
			mSv.UpdateScrollbars(recalculateBounds: true);
		}
	}

	private void NotifyListeners()
	{
		current = this;
		if (onFinished != null)
		{
			onFinished();
		}
		if ((Object)(object)eventReceiver != (Object)null && !string.IsNullOrEmpty(callWhenFinished))
		{
			eventReceiver.SendMessage(callWhenFinished, (object)this, (SendMessageOptions)1);
		}
		current = null;
	}

	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if ((Object)(object)springPosition == (Object)null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!((Behaviour)springPosition).enabled)
		{
			springPosition.mThreshold = 0f;
			((Behaviour)springPosition).enabled = true;
		}
		return springPosition;
	}
}
