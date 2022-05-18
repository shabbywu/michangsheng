using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0000D743 File Offset: 0x0000B943
	public static bool isVisible
	{
		get
		{
			return UITooltip.mInstance != null && UITooltip.mInstance.mTarget == 1f;
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0000D765 File Offset: 0x0000B965
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0000D76D File Offset: 0x0000B96D
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00092310 File Offset: 0x00090510
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

	// Token: 0x06000B69 RID: 2921 RVA: 0x00092378 File Offset: 0x00090578
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

	// Token: 0x06000B6A RID: 2922 RVA: 0x0009246C File Offset: 0x0009066C
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

	// Token: 0x06000B6B RID: 2923 RVA: 0x000924AC File Offset: 0x000906AC
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

	// Token: 0x06000B6C RID: 2924 RVA: 0x0000D775 File Offset: 0x0000B975
	public static void ShowText(string tooltipText)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x0400081E RID: 2078
	protected static UITooltip mInstance;

	// Token: 0x0400081F RID: 2079
	public Camera uiCamera;

	// Token: 0x04000820 RID: 2080
	public UILabel text;

	// Token: 0x04000821 RID: 2081
	public UISprite background;

	// Token: 0x04000822 RID: 2082
	public float appearSpeed = 10f;

	// Token: 0x04000823 RID: 2083
	public bool scalingTransitions = true;

	// Token: 0x04000824 RID: 2084
	protected Transform mTrans;

	// Token: 0x04000825 RID: 2085
	protected float mTarget;

	// Token: 0x04000826 RID: 2086
	protected float mCurrent;

	// Token: 0x04000827 RID: 2087
	protected Vector3 mPos;

	// Token: 0x04000828 RID: 2088
	protected Vector3 mSize = Vector3.zero;

	// Token: 0x04000829 RID: 2089
	protected UIWidget[] mWidgets;
}
