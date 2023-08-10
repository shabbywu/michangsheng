using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Progress Bar")]
public class UIProgressBar : UIWidgetContainer
{
	public enum FillDirection
	{
		LeftToRight,
		RightToLeft,
		BottomToTop,
		TopToBottom
	}

	public delegate void OnDragFinished();

	public static UIProgressBar current;

	public OnDragFinished onDragFinished;

	public Transform thumb;

	[HideInInspector]
	[SerializeField]
	protected UIWidget mBG;

	[HideInInspector]
	[SerializeField]
	protected UIWidget mFG;

	[HideInInspector]
	[SerializeField]
	protected float mValue = 1f;

	[HideInInspector]
	[SerializeField]
	protected FillDirection mFill;

	protected Transform mTrans;

	protected bool mIsDirty;

	protected Camera mCam;

	protected float mOffset;

	public int numberOfSteps;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	public Transform cachedTransform
	{
		get
		{
			if ((Object)(object)mTrans == (Object)null)
			{
				mTrans = ((Component)this).transform;
			}
			return mTrans;
		}
	}

	public Camera cachedCamera
	{
		get
		{
			if ((Object)(object)mCam == (Object)null)
			{
				mCam = NGUITools.FindCameraForLayer(((Component)this).gameObject.layer);
			}
			return mCam;
		}
	}

	public UIWidget foregroundWidget
	{
		get
		{
			return mFG;
		}
		set
		{
			if ((Object)(object)mFG != (Object)(object)value)
			{
				mFG = value;
				mIsDirty = true;
			}
		}
	}

	public UIWidget backgroundWidget
	{
		get
		{
			return mBG;
		}
		set
		{
			if ((Object)(object)mBG != (Object)(object)value)
			{
				mBG = value;
				mIsDirty = true;
			}
		}
	}

	public FillDirection fillDirection
	{
		get
		{
			return mFill;
		}
		set
		{
			if (mFill != value)
			{
				mFill = value;
				ForceUpdate();
			}
		}
	}

	public float value
	{
		get
		{
			if (numberOfSteps > 1)
			{
				return Mathf.Round(mValue * (float)(numberOfSteps - 1)) / (float)(numberOfSteps - 1);
			}
			return mValue;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (mValue == num)
			{
				return;
			}
			float num2 = this.value;
			mValue = num;
			if (num2 != this.value)
			{
				ForceUpdate();
				if ((Object)(object)current == (Object)null && NGUITools.GetActive((Behaviour)(object)this) && EventDelegate.IsValid(onChange))
				{
					current = this;
					EventDelegate.Execute(onChange);
					current = null;
				}
			}
		}
	}

	public float alpha
	{
		get
		{
			if ((Object)(object)mFG != (Object)null)
			{
				return mFG.alpha;
			}
			if ((Object)(object)mBG != (Object)null)
			{
				return mBG.alpha;
			}
			return 1f;
		}
		set
		{
			if ((Object)(object)mFG != (Object)null)
			{
				mFG.alpha = value;
				if ((Object)(object)((Component)mFG).GetComponent<Collider>() != (Object)null)
				{
					((Component)mFG).GetComponent<Collider>().enabled = mFG.alpha > 0.001f;
				}
				else if ((Object)(object)((Component)mFG).GetComponent<Collider2D>() != (Object)null)
				{
					((Behaviour)((Component)mFG).GetComponent<Collider2D>()).enabled = mFG.alpha > 0.001f;
				}
			}
			if ((Object)(object)mBG != (Object)null)
			{
				mBG.alpha = value;
				if ((Object)(object)((Component)mBG).GetComponent<Collider>() != (Object)null)
				{
					((Component)mBG).GetComponent<Collider>().enabled = mBG.alpha > 0.001f;
				}
				else if ((Object)(object)((Component)mBG).GetComponent<Collider2D>() != (Object)null)
				{
					((Behaviour)((Component)mBG).GetComponent<Collider2D>()).enabled = mBG.alpha > 0.001f;
				}
			}
			if (!((Object)(object)thumb != (Object)null))
			{
				return;
			}
			UIWidget component = ((Component)thumb).GetComponent<UIWidget>();
			if ((Object)(object)component != (Object)null)
			{
				component.alpha = value;
				if ((Object)(object)((Component)component).GetComponent<Collider>() != (Object)null)
				{
					((Component)component).GetComponent<Collider>().enabled = component.alpha > 0.001f;
				}
				else if ((Object)(object)((Component)component).GetComponent<Collider2D>() != (Object)null)
				{
					((Behaviour)((Component)component).GetComponent<Collider2D>()).enabled = component.alpha > 0.001f;
				}
			}
		}
	}

	protected bool isHorizontal
	{
		get
		{
			if (mFill != 0)
			{
				return mFill == FillDirection.RightToLeft;
			}
			return true;
		}
	}

	protected bool isInverted
	{
		get
		{
			if (mFill != FillDirection.RightToLeft)
			{
				return mFill == FillDirection.TopToBottom;
			}
			return true;
		}
	}

	protected void Start()
	{
		Upgrade();
		if (Application.isPlaying)
		{
			if ((Object)(object)mBG != (Object)null)
			{
				mBG.autoResizeBoxCollider = true;
			}
			OnStart();
			if ((Object)(object)current == (Object)null && onChange != null)
			{
				current = this;
				EventDelegate.Execute(onChange);
				current = null;
			}
		}
		ForceUpdate();
	}

	protected virtual void Upgrade()
	{
	}

	protected virtual void OnStart()
	{
	}

	protected void Update()
	{
		if (mIsDirty)
		{
			ForceUpdate();
		}
	}

	protected void OnValidate()
	{
		if (NGUITools.GetActive((Behaviour)(object)this))
		{
			Upgrade();
			mIsDirty = true;
			float num = Mathf.Clamp01(mValue);
			if (mValue != num)
			{
				mValue = num;
			}
			if (numberOfSteps < 0)
			{
				numberOfSteps = 0;
			}
			else if (numberOfSteps > 20)
			{
				numberOfSteps = 20;
			}
			ForceUpdate();
		}
		else
		{
			float num2 = Mathf.Clamp01(mValue);
			if (mValue != num2)
			{
				mValue = num2;
			}
			if (numberOfSteps < 0)
			{
				numberOfSteps = 0;
			}
			else if (numberOfSteps > 20)
			{
				numberOfSteps = 20;
			}
		}
	}

	protected float ScreenToValue(Vector2 screenPos)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Transform val = cachedTransform;
		Plane val2 = default(Plane);
		((Plane)(ref val2))._002Ector(val.rotation * Vector3.back, val.position);
		Ray val3 = cachedCamera.ScreenPointToRay(Vector2.op_Implicit(screenPos));
		float num = default(float);
		if (!((Plane)(ref val2)).Raycast(val3, ref num))
		{
			return value;
		}
		return LocalToValue(Vector2.op_Implicit(val.InverseTransformPoint(((Ray)(ref val3)).GetPoint(num))));
	}

	protected virtual float LocalToValue(Vector2 localPos)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mFG != (Object)null)
		{
			Vector3[] localCorners = mFG.localCorners;
			Vector3 val = localCorners[2] - localCorners[0];
			if (isHorizontal)
			{
				float num = (localPos.x - localCorners[0].x) / val.x;
				if (!isInverted)
				{
					return num;
				}
				return 1f - num;
			}
			float num2 = (localPos.y - localCorners[0].y) / val.y;
			if (!isInverted)
			{
				return num2;
			}
			return 1f - num2;
		}
		return value;
	}

	public virtual void ForceUpdate()
	{
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		mIsDirty = false;
		if ((Object)(object)mFG != (Object)null)
		{
			UIBasicSprite uIBasicSprite = mFG as UIBasicSprite;
			if (isHorizontal)
			{
				if ((Object)(object)uIBasicSprite != (Object)null && uIBasicSprite.type == UIBasicSprite.Type.Filled)
				{
					if (uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
					{
						uIBasicSprite.fillDirection = UIBasicSprite.FillDirection.Horizontal;
						uIBasicSprite.invert = isInverted;
					}
					uIBasicSprite.fillAmount = value;
				}
				else
				{
					mFG.drawRegion = (isInverted ? new Vector4(1f - value, 0f, 1f, 1f) : new Vector4(0f, 0f, value, 1f));
				}
			}
			else if ((Object)(object)uIBasicSprite != (Object)null && uIBasicSprite.type == UIBasicSprite.Type.Filled)
			{
				if (uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
				{
					uIBasicSprite.fillDirection = UIBasicSprite.FillDirection.Vertical;
					uIBasicSprite.invert = isInverted;
				}
				uIBasicSprite.fillAmount = value;
			}
			else
			{
				mFG.drawRegion = (isInverted ? new Vector4(0f, 1f - value, 1f, 1f) : new Vector4(0f, 0f, 1f, value));
			}
		}
		if ((Object)(object)thumb != (Object)null && ((Object)(object)mFG != (Object)null || (Object)(object)mBG != (Object)null))
		{
			Vector3[] array = (((Object)(object)mFG != (Object)null) ? mFG.localCorners : mBG.localCorners);
			Vector4 val = (((Object)(object)mFG != (Object)null) ? mFG.border : mBG.border);
			array[0].x += val.x;
			array[1].x += val.x;
			array[2].x -= val.z;
			array[3].x -= val.z;
			array[0].y += val.y;
			array[1].y -= val.w;
			array[2].y -= val.w;
			array[3].y += val.y;
			Transform val2 = (((Object)(object)mFG != (Object)null) ? mFG.cachedTransform : mBG.cachedTransform);
			for (int i = 0; i < 4; i++)
			{
				array[i] = val2.TransformPoint(array[i]);
			}
			if (isHorizontal)
			{
				Vector3 val3 = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 val4 = Vector3.Lerp(array[2], array[3], 0.5f);
				SetThumbPosition(Vector3.Lerp(val3, val4, isInverted ? (1f - value) : value));
			}
			else
			{
				Vector3 val5 = Vector3.Lerp(array[0], array[3], 0.5f);
				Vector3 val6 = Vector3.Lerp(array[1], array[2], 0.5f);
				SetThumbPosition(Vector3.Lerp(val5, val6, isInverted ? (1f - value) : value));
			}
		}
	}

	protected void SetThumbPosition(Vector3 worldPos)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Transform parent = thumb.parent;
		if ((Object)(object)parent != (Object)null)
		{
			worldPos = parent.InverseTransformPoint(worldPos);
			worldPos.x = Mathf.Round(worldPos.x);
			worldPos.y = Mathf.Round(worldPos.y);
			worldPos.z = 0f;
			if (Vector3.Distance(thumb.localPosition, worldPos) > 0.001f)
			{
				thumb.localPosition = worldPos;
			}
		}
		else if (Vector3.Distance(thumb.position, worldPos) > 1E-05f)
		{
			thumb.position = worldPos;
		}
	}
}
