using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	public enum Scaling
	{
		PixelPerfect,
		FixedSize,
		FixedSizeOnMobiles
	}

	public static List<UIRoot> list = new List<UIRoot>();

	public Scaling scalingStyle;

	public int manualHeight = 720;

	public int minimumHeight = 320;

	public int maximumHeight = 1536;

	public bool adjustByDPI;

	public bool shrinkPortraitUI;

	private Transform mTrans;

	public int activeHeight
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			if (scalingStyle == Scaling.FixedSize)
			{
				return manualHeight;
			}
			Vector2 screenSize = NGUITools.screenSize;
			float num = screenSize.x / screenSize.y;
			if (screenSize.y < (float)minimumHeight)
			{
				screenSize.y = minimumHeight;
				screenSize.x = screenSize.y * num;
			}
			else if (screenSize.y > (float)maximumHeight)
			{
				screenSize.y = maximumHeight;
				screenSize.x = screenSize.y * num;
			}
			int num2 = Mathf.RoundToInt((shrinkPortraitUI && screenSize.y > screenSize.x) ? (screenSize.y / num) : screenSize.y);
			if (!adjustByDPI)
			{
				return num2;
			}
			return NGUIMath.AdjustByDPI(num2);
		}
	}

	public float pixelSizeAdjustment => GetPixelSizeAdjustment(Mathf.RoundToInt(NGUITools.screenSize.y));

	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(go);
		if (!((Object)(object)uIRoot != (Object)null))
		{
			return 1f;
		}
		return uIRoot.pixelSizeAdjustment;
	}

	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (scalingStyle == Scaling.FixedSize)
		{
			return (float)manualHeight / (float)height;
		}
		if (height < minimumHeight)
		{
			return (float)minimumHeight / (float)height;
		}
		if (height > maximumHeight)
		{
			return (float)maximumHeight / (float)height;
		}
		return 1f;
	}

	protected virtual void Awake()
	{
		mTrans = ((Component)this).transform;
	}

	protected virtual void OnEnable()
	{
		list.Add(this);
	}

	protected virtual void OnDisable()
	{
		list.Remove(this);
	}

	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = ((Component)this).GetComponentInChildren<UIOrthoCamera>();
		if ((Object)(object)componentInChildren != (Object)null)
		{
			Debug.LogWarning((object)"UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", (Object)(object)componentInChildren);
			Camera component = ((Component)componentInChildren).gameObject.GetComponent<Camera>();
			((Behaviour)componentInChildren).enabled = false;
			if ((Object)(object)component != (Object)null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			Update();
		}
	}

	private void Update()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)mTrans != (Object)null))
		{
			return;
		}
		float num = activeHeight;
		if (num > 0f)
		{
			float num2 = 2f / num;
			Vector3 localScale = mTrans.localScale;
			if (!(Mathf.Abs(localScale.x - num2) <= float.Epsilon) || !(Mathf.Abs(localScale.y - num2) <= float.Epsilon) || !(Mathf.Abs(localScale.z - num2) <= float.Epsilon))
			{
				mTrans.localScale = new Vector3(num2, num2, num2);
			}
		}
	}

	public static void Broadcast(string funcName)
	{
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			UIRoot uIRoot = list[i];
			if ((Object)(object)uIRoot != (Object)null)
			{
				((Component)uIRoot).BroadcastMessage(funcName, (SendMessageOptions)1);
			}
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError((object)"SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			UIRoot uIRoot = list[i];
			if ((Object)(object)uIRoot != (Object)null)
			{
				((Component)uIRoot).BroadcastMessage(funcName, param, (SendMessageOptions)1);
			}
		}
	}
}
