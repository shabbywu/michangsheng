using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	public Color from = Color.white;

	public Color to = Color.white;

	private bool mCached;

	private UIWidget mWidget;

	private Material mMat;

	private Light mLight;

	[Obsolete("Use 'value' instead")]
	public Color color
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return value;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			this.value = value;
		}
	}

	public Color value
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			if (!mCached)
			{
				Cache();
			}
			if ((Object)(object)mWidget != (Object)null)
			{
				return mWidget.color;
			}
			if ((Object)(object)mLight != (Object)null)
			{
				return mLight.color;
			}
			if ((Object)(object)mMat != (Object)null)
			{
				return mMat.color;
			}
			return Color.black;
		}
		set
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			if (!mCached)
			{
				Cache();
			}
			if ((Object)(object)mWidget != (Object)null)
			{
				mWidget.color = value;
			}
			if ((Object)(object)mMat != (Object)null)
			{
				mMat.color = value;
			}
			if ((Object)(object)mLight != (Object)null)
			{
				mLight.color = value;
				((Behaviour)mLight).enabled = value.r + value.g + value.b > 0.01f;
			}
		}
	}

	private void Cache()
	{
		mCached = true;
		mWidget = ((Component)this).GetComponent<UIWidget>();
		Renderer component = ((Component)this).GetComponent<Renderer>();
		if ((Object)(object)component != (Object)null)
		{
			mMat = component.material;
		}
		mLight = ((Component)this).GetComponent<Light>();
		if ((Object)(object)mWidget == (Object)null && (Object)(object)mMat == (Object)null && (Object)(object)mLight == (Object)null)
		{
			mWidget = ((Component)this).GetComponentInChildren<UIWidget>();
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		value = Color.Lerp(from, to, factor);
	}

	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, isFinished: true);
			((Behaviour)tweenColor).enabled = false;
		}
		return tweenColor;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		from = value;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		to = value;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		value = from;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		value = to;
	}
}
