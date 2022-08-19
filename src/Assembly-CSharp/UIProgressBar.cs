using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000070 RID: 112
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Progress Bar")]
public class UIProgressBar : UIWidgetContainer
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001EB58 File Offset: 0x0001CD58
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001EB7A File Offset: 0x0001CD7A
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001EBA6 File Offset: 0x0001CDA6
	// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001EBAE File Offset: 0x0001CDAE
	public UIWidget foregroundWidget
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001EBCC File Offset: 0x0001CDCC
	// (set) Token: 0x0600059A RID: 1434 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
	public UIWidget backgroundWidget
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001EBF2 File Offset: 0x0001CDF2
	// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001EBFA File Offset: 0x0001CDFA
	public UIProgressBar.FillDirection fillDirection
	{
		get
		{
			return this.mFill;
		}
		set
		{
			if (this.mFill != value)
			{
				this.mFill = value;
				this.ForceUpdate();
			}
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001EC12 File Offset: 0x0001CE12
	// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001EC44 File Offset: 0x0001CE44
	public float value
	{
		get
		{
			if (this.numberOfSteps > 1)
			{
				return Mathf.Round(this.mValue * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
			}
			return this.mValue;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mValue != num)
			{
				float value2 = this.value;
				this.mValue = num;
				if (value2 != this.value)
				{
					this.ForceUpdate();
					if (UIProgressBar.current == null && NGUITools.GetActive(this) && EventDelegate.IsValid(this.onChange))
					{
						UIProgressBar.current = this;
						EventDelegate.Execute(this.onChange);
						UIProgressBar.current = null;
					}
				}
			}
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001ECB5 File Offset: 0x0001CEB5
	// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001ECF0 File Offset: 0x0001CEF0
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 1f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				if (this.mFG.GetComponent<Collider>() != null)
				{
					this.mFG.GetComponent<Collider>().enabled = (this.mFG.alpha > 0.001f);
				}
				else if (this.mFG.GetComponent<Collider2D>() != null)
				{
					this.mFG.GetComponent<Collider2D>().enabled = (this.mFG.alpha > 0.001f);
				}
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				if (this.mBG.GetComponent<Collider>() != null)
				{
					this.mBG.GetComponent<Collider>().enabled = (this.mBG.alpha > 0.001f);
				}
				else if (this.mBG.GetComponent<Collider2D>() != null)
				{
					this.mBG.GetComponent<Collider2D>().enabled = (this.mBG.alpha > 0.001f);
				}
			}
			if (this.thumb != null)
			{
				UIWidget component = this.thumb.GetComponent<UIWidget>();
				if (component != null)
				{
					component.alpha = value;
					if (component.GetComponent<Collider>() != null)
					{
						component.GetComponent<Collider>().enabled = (component.alpha > 0.001f);
						return;
					}
					if (component.GetComponent<Collider2D>() != null)
					{
						component.GetComponent<Collider2D>().enabled = (component.alpha > 0.001f);
					}
				}
			}
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001EE80 File Offset: 0x0001D080
	protected bool isHorizontal
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.LeftToRight || this.mFill == UIProgressBar.FillDirection.RightToLeft;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001EE95 File Offset: 0x0001D095
	protected bool isInverted
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.RightToLeft || this.mFill == UIProgressBar.FillDirection.TopToBottom;
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0001EEAC File Offset: 0x0001D0AC
	protected void Start()
	{
		this.Upgrade();
		if (Application.isPlaying)
		{
			if (this.mBG != null)
			{
				this.mBG.autoResizeBoxCollider = true;
			}
			this.OnStart();
			if (UIProgressBar.current == null && this.onChange != null)
			{
				UIProgressBar.current = this;
				EventDelegate.Execute(this.onChange);
				UIProgressBar.current = null;
			}
		}
		this.ForceUpdate();
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00004095 File Offset: 0x00002295
	protected virtual void Upgrade()
	{
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00004095 File Offset: 0x00002295
	protected virtual void OnStart()
	{
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0001EF18 File Offset: 0x0001D118
	protected void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0001EF28 File Offset: 0x0001D128
	protected void OnValidate()
	{
		if (NGUITools.GetActive(this))
		{
			this.Upgrade();
			this.mIsDirty = true;
			float num = Mathf.Clamp01(this.mValue);
			if (this.mValue != num)
			{
				this.mValue = num;
			}
			if (this.numberOfSteps < 0)
			{
				this.numberOfSteps = 0;
			}
			else if (this.numberOfSteps > 20)
			{
				this.numberOfSteps = 20;
			}
			this.ForceUpdate();
			return;
		}
		float num2 = Mathf.Clamp01(this.mValue);
		if (this.mValue != num2)
		{
			this.mValue = num2;
		}
		if (this.numberOfSteps < 0)
		{
			this.numberOfSteps = 0;
			return;
		}
		if (this.numberOfSteps > 20)
		{
			this.numberOfSteps = 20;
		}
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
	protected float ScreenToValue(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane;
		plane..ctor(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			return this.value;
		}
		return this.LocalToValue(cachedTransform.InverseTransformPoint(ray.GetPoint(num)));
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0001F040 File Offset: 0x0001D240
	protected virtual float LocalToValue(Vector2 localPos)
	{
		if (!(this.mFG != null))
		{
			return this.value;
		}
		Vector3[] localCorners = this.mFG.localCorners;
		Vector3 vector = localCorners[2] - localCorners[0];
		if (this.isHorizontal)
		{
			float num = (localPos.x - localCorners[0].x) / vector.x;
			if (!this.isInverted)
			{
				return num;
			}
			return 1f - num;
		}
		else
		{
			float num2 = (localPos.y - localCorners[0].y) / vector.y;
			if (!this.isInverted)
			{
				return num2;
			}
			return 1f - num2;
		}
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
	public virtual void ForceUpdate()
	{
		this.mIsDirty = false;
		if (this.mFG != null)
		{
			UIBasicSprite uibasicSprite = this.mFG as UIBasicSprite;
			if (this.isHorizontal)
			{
				if (uibasicSprite != null && uibasicSprite.type == UIBasicSprite.Type.Filled)
				{
					if (uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
					{
						uibasicSprite.fillDirection = UIBasicSprite.FillDirection.Horizontal;
						uibasicSprite.invert = this.isInverted;
					}
					uibasicSprite.fillAmount = this.value;
				}
				else
				{
					this.mFG.drawRegion = (this.isInverted ? new Vector4(1f - this.value, 0f, 1f, 1f) : new Vector4(0f, 0f, this.value, 1f));
				}
			}
			else if (uibasicSprite != null && uibasicSprite.type == UIBasicSprite.Type.Filled)
			{
				if (uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
				{
					uibasicSprite.fillDirection = UIBasicSprite.FillDirection.Vertical;
					uibasicSprite.invert = this.isInverted;
				}
				uibasicSprite.fillAmount = this.value;
			}
			else
			{
				this.mFG.drawRegion = (this.isInverted ? new Vector4(0f, 1f - this.value, 1f, 1f) : new Vector4(0f, 0f, 1f, this.value));
			}
		}
		if (this.thumb != null && (this.mFG != null || this.mBG != null))
		{
			Vector3[] array = (this.mFG != null) ? this.mFG.localCorners : this.mBG.localCorners;
			Vector4 vector = (this.mFG != null) ? this.mFG.border : this.mBG.border;
			Vector3[] array2 = array;
			int num = 0;
			array2[num].x = array2[num].x + vector.x;
			Vector3[] array3 = array;
			int num2 = 1;
			array3[num2].x = array3[num2].x + vector.x;
			Vector3[] array4 = array;
			int num3 = 2;
			array4[num3].x = array4[num3].x - vector.z;
			Vector3[] array5 = array;
			int num4 = 3;
			array5[num4].x = array5[num4].x - vector.z;
			Vector3[] array6 = array;
			int num5 = 0;
			array6[num5].y = array6[num5].y + vector.y;
			Vector3[] array7 = array;
			int num6 = 1;
			array7[num6].y = array7[num6].y - vector.w;
			Vector3[] array8 = array;
			int num7 = 2;
			array8[num7].y = array8[num7].y - vector.w;
			Vector3[] array9 = array;
			int num8 = 3;
			array9[num8].y = array9[num8].y + vector.y;
			Transform transform = (this.mFG != null) ? this.mFG.cachedTransform : this.mBG.cachedTransform;
			for (int i = 0; i < 4; i++)
			{
				array[i] = transform.TransformPoint(array[i]);
			}
			if (this.isHorizontal)
			{
				Vector3 vector2 = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 vector3 = Vector3.Lerp(array[2], array[3], 0.5f);
				this.SetThumbPosition(Vector3.Lerp(vector2, vector3, this.isInverted ? (1f - this.value) : this.value));
				return;
			}
			Vector3 vector4 = Vector3.Lerp(array[0], array[3], 0.5f);
			Vector3 vector5 = Vector3.Lerp(array[1], array[2], 0.5f);
			this.SetThumbPosition(Vector3.Lerp(vector4, vector5, this.isInverted ? (1f - this.value) : this.value));
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0001F494 File Offset: 0x0001D694
	protected void SetThumbPosition(Vector3 worldPos)
	{
		Transform parent = this.thumb.parent;
		if (parent != null)
		{
			worldPos = parent.InverseTransformPoint(worldPos);
			worldPos.x = Mathf.Round(worldPos.x);
			worldPos.y = Mathf.Round(worldPos.y);
			worldPos.z = 0f;
			if (Vector3.Distance(this.thumb.localPosition, worldPos) > 0.001f)
			{
				this.thumb.localPosition = worldPos;
				return;
			}
		}
		else if (Vector3.Distance(this.thumb.position, worldPos) > 1E-05f)
		{
			this.thumb.position = worldPos;
		}
	}

	// Token: 0x040003B5 RID: 949
	public static UIProgressBar current;

	// Token: 0x040003B6 RID: 950
	public UIProgressBar.OnDragFinished onDragFinished;

	// Token: 0x040003B7 RID: 951
	public Transform thumb;

	// Token: 0x040003B8 RID: 952
	[HideInInspector]
	[SerializeField]
	protected UIWidget mBG;

	// Token: 0x040003B9 RID: 953
	[HideInInspector]
	[SerializeField]
	protected UIWidget mFG;

	// Token: 0x040003BA RID: 954
	[HideInInspector]
	[SerializeField]
	protected float mValue = 1f;

	// Token: 0x040003BB RID: 955
	[HideInInspector]
	[SerializeField]
	protected UIProgressBar.FillDirection mFill;

	// Token: 0x040003BC RID: 956
	protected Transform mTrans;

	// Token: 0x040003BD RID: 957
	protected bool mIsDirty;

	// Token: 0x040003BE RID: 958
	protected Camera mCam;

	// Token: 0x040003BF RID: 959
	protected float mOffset;

	// Token: 0x040003C0 RID: 960
	public int numberOfSteps;

	// Token: 0x040003C1 RID: 961
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x020011EC RID: 4588
	public enum FillDirection
	{
		// Token: 0x040063FD RID: 25597
		LeftToRight,
		// Token: 0x040063FE RID: 25598
		RightToLeft,
		// Token: 0x040063FF RID: 25599
		BottomToTop,
		// Token: 0x04006400 RID: 25600
		TopToBottom
	}

	// Token: 0x020011ED RID: 4589
	// (Invoke) Token: 0x06007817 RID: 30743
	public delegate void OnDragFinished();
}
