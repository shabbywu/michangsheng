using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000098 RID: 152
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Progress Bar")]
public class UIProgressBar : UIWidgetContainer
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000095F3 File Offset: 0x000077F3
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

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00009615 File Offset: 0x00007815
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

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00009641 File Offset: 0x00007841
	// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00009649 File Offset: 0x00007849
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

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00009667 File Offset: 0x00007867
	// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000966F File Offset: 0x0000786F
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

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000968D File Offset: 0x0000788D
	// (set) Token: 0x060005FC RID: 1532 RVA: 0x00009695 File Offset: 0x00007895
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

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060005FD RID: 1533 RVA: 0x000096AD File Offset: 0x000078AD
	// (set) Token: 0x060005FE RID: 1534 RVA: 0x00074BA4 File Offset: 0x00072DA4
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

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060005FF RID: 1535 RVA: 0x000096DE File Offset: 0x000078DE
	// (set) Token: 0x06000600 RID: 1536 RVA: 0x00074C18 File Offset: 0x00072E18
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

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000601 RID: 1537 RVA: 0x00009719 File Offset: 0x00007919
	protected bool isHorizontal
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.LeftToRight || this.mFill == UIProgressBar.FillDirection.RightToLeft;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000972E File Offset: 0x0000792E
	protected bool isInverted
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.RightToLeft || this.mFill == UIProgressBar.FillDirection.TopToBottom;
		}
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00074DA8 File Offset: 0x00072FA8
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

	// Token: 0x06000604 RID: 1540 RVA: 0x000042DD File Offset: 0x000024DD
	protected virtual void Upgrade()
	{
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000042DD File Offset: 0x000024DD
	protected virtual void OnStart()
	{
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x00009744 File Offset: 0x00007944
	protected void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00074E14 File Offset: 0x00073014
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

	// Token: 0x06000608 RID: 1544 RVA: 0x00074EBC File Offset: 0x000730BC
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

	// Token: 0x06000609 RID: 1545 RVA: 0x00074F2C File Offset: 0x0007312C
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

	// Token: 0x0600060A RID: 1546 RVA: 0x00074FD4 File Offset: 0x000731D4
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

	// Token: 0x0600060B RID: 1547 RVA: 0x00075380 File Offset: 0x00073580
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

	// Token: 0x04000462 RID: 1122
	public static UIProgressBar current;

	// Token: 0x04000463 RID: 1123
	public UIProgressBar.OnDragFinished onDragFinished;

	// Token: 0x04000464 RID: 1124
	public Transform thumb;

	// Token: 0x04000465 RID: 1125
	[HideInInspector]
	[SerializeField]
	protected UIWidget mBG;

	// Token: 0x04000466 RID: 1126
	[HideInInspector]
	[SerializeField]
	protected UIWidget mFG;

	// Token: 0x04000467 RID: 1127
	[HideInInspector]
	[SerializeField]
	protected float mValue = 1f;

	// Token: 0x04000468 RID: 1128
	[HideInInspector]
	[SerializeField]
	protected UIProgressBar.FillDirection mFill;

	// Token: 0x04000469 RID: 1129
	protected Transform mTrans;

	// Token: 0x0400046A RID: 1130
	protected bool mIsDirty;

	// Token: 0x0400046B RID: 1131
	protected Camera mCam;

	// Token: 0x0400046C RID: 1132
	protected float mOffset;

	// Token: 0x0400046D RID: 1133
	public int numberOfSteps;

	// Token: 0x0400046E RID: 1134
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x02000099 RID: 153
	public enum FillDirection
	{
		// Token: 0x04000470 RID: 1136
		LeftToRight,
		// Token: 0x04000471 RID: 1137
		RightToLeft,
		// Token: 0x04000472 RID: 1138
		BottomToTop,
		// Token: 0x04000473 RID: 1139
		TopToBottom
	}

	// Token: 0x0200009A RID: 154
	// (Invoke) Token: 0x0600060E RID: 1550
	public delegate void OnDragFinished();
}
