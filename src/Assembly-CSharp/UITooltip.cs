using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0003FE7D File Offset: 0x0003E07D
	public static bool isVisible
	{
		get
		{
			return UITooltip.mInstance != null && UITooltip.mInstance.mTarget == 1f;
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0003FE9F File Offset: 0x0003E09F
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0003FEA7 File Offset: 0x0003E0A7
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0003FEB0 File Offset: 0x0003E0B0
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0003FF18 File Offset: 0x0003E118
	protected virtual void Update()
	{
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, RealTime.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 vector = this.mSize * 0.25f;
				vector.y = -vector.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - vector, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0004000C File Offset: 0x0003E20C
	protected virtual void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0004004C File Offset: 0x0003E24C
	protected virtual void SetText(string tooltipText)
	{
		if (!(this.text != null) || string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 0f;
			return;
		}
		this.mTarget = 1f;
		this.text.text = tooltipText;
		this.mPos = Input.mousePosition;
		Transform transform = this.text.transform;
		Vector3 localPosition = transform.localPosition;
		Vector3 localScale = transform.localScale;
		this.mSize = this.text.printedSize;
		this.mSize.x = this.mSize.x * localScale.x;
		this.mSize.y = this.mSize.y * localScale.y;
		if (this.background != null)
		{
			Vector4 border = this.background.border;
			this.mSize.x = this.mSize.x + (border.x + border.z + (localPosition.x - border.x) * 2f);
			this.mSize.y = this.mSize.y + (border.y + border.w + (-localPosition.y - border.y) * 2f);
			this.background.width = Mathf.RoundToInt(this.mSize.x);
			this.background.height = Mathf.RoundToInt(this.mSize.y);
		}
		if (this.uiCamera != null)
		{
			this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
			this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
			float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
			float num2 = (float)Screen.height * 0.5f / num;
			Vector2 vector;
			vector..ctor(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
			this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
			this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
			this.mPos = this.mTrans.localPosition;
			this.mPos.x = Mathf.Round(this.mPos.x);
			this.mPos.y = Mathf.Round(this.mPos.y);
			this.mTrans.localPosition = this.mPos;
			return;
		}
		if (this.mPos.x + this.mSize.x > (float)Screen.width)
		{
			this.mPos.x = (float)Screen.width - this.mSize.x;
		}
		if (this.mPos.y - this.mSize.y < 0f)
		{
			this.mPos.y = this.mSize.y;
		}
		this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
		this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x000403CC File Offset: 0x0003E5CC
	public static void ShowText(string tooltipText)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x0400067A RID: 1658
	protected static UITooltip mInstance;

	// Token: 0x0400067B RID: 1659
	public Camera uiCamera;

	// Token: 0x0400067C RID: 1660
	public UILabel text;

	// Token: 0x0400067D RID: 1661
	public UISprite background;

	// Token: 0x0400067E RID: 1662
	public float appearSpeed = 10f;

	// Token: 0x0400067F RID: 1663
	public bool scalingTransitions = true;

	// Token: 0x04000680 RID: 1664
	protected Transform mTrans;

	// Token: 0x04000681 RID: 1665
	protected float mTarget;

	// Token: 0x04000682 RID: 1666
	protected float mCurrent;

	// Token: 0x04000683 RID: 1667
	protected Vector3 mPos;

	// Token: 0x04000684 RID: 1668
	protected Vector3 mSize = Vector3.zero;

	// Token: 0x04000685 RID: 1669
	protected UIWidget[] mWidgets;
}
