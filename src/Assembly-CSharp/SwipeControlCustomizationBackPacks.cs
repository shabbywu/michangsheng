using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200075F RID: 1887
public class SwipeControlCustomizationBackPacks : MonoBehaviour
{
	// Token: 0x06002FFB RID: 12283 RVA: 0x00023A4F File Offset: 0x00021C4F
	private IEnumerator Start()
	{
		if (this.clickEdgeToSwitch && !this.allowInput)
		{
			Debug.LogWarning("You have enabled clickEdgeToSwitch, but it will not work because allowInput is disabled!", this);
		}
		yield return new WaitForSeconds(0.2f);
		if (!this.skipAutoSetup)
		{
			this.Setup();
		}
		yield break;
	}

	// Token: 0x06002FFC RID: 12284 RVA: 0x0017F5E0 File Offset: 0x0017D7E0
	public void Setup()
	{
		this.partFactor = 1f / this.partWidth;
		this.smoothValue = (float)this.currentValue;
		this.currentValue = this.startValue;
		if (this.mouseRect != new Rect(0f, 0f, 0f, 0f))
		{
			this.SetMouseRect(this.mouseRect);
		}
		if (this.leftEdgeRectForClickSwitch == new Rect(0f, 0f, 0f, 0f))
		{
			this.CalculateEdgeRectsFromMouseRect();
		}
		if (this.matrix == Matrix4x4.zero)
		{
			this.matrix = Matrix4x4.identity.inverse;
		}
	}

	// Token: 0x06002FFD RID: 12285 RVA: 0x00023A5E File Offset: 0x00021C5E
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06002FFE RID: 12286 RVA: 0x00023A67 File Offset: 0x00021C67
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x0017F69C File Offset: 0x0017D89C
	public void CalculateEdgeRectsFromMouseRect(Rect myRect)
	{
		this.leftEdgeRectForClickSwitch.x = myRect.x;
		this.leftEdgeRectForClickSwitch.y = myRect.y;
		this.leftEdgeRectForClickSwitch.width = myRect.width * 0.5f;
		this.leftEdgeRectForClickSwitch.height = myRect.height;
		this.rightEdgeRectForClickSwitch.x = myRect.x + myRect.width * 0.5f;
		this.rightEdgeRectForClickSwitch.y = myRect.y;
		this.rightEdgeRectForClickSwitch.width = myRect.width * 0.5f;
		this.rightEdgeRectForClickSwitch.height = myRect.height;
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x00023A75 File Offset: 0x00021C75
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003002 RID: 12290 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003003 RID: 12291 RVA: 0x0017F754 File Offset: 0x0017D954
	private void Update()
	{
		if (SwipeControlCustomizationBackPacks.controlEnabled)
		{
			this.touched = false;
			if (this.allowInput && (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
			{
				this.pos = new Vector3(Input.mousePosition[0], Input.mousePosition[1], 0f);
				this.tPos = this.matrix.inverse.MultiplyPoint3x4(this.pos);
				if (Input.GetMouseButtonDown(0) && this.mouseRect.Contains(this.tPos))
				{
					this.mouseStartArea = 1;
				}
				if (this.mouseStartArea == 1)
				{
					this.touched = true;
					if (Input.GetMouseButtonDown(0))
					{
						this.mStartPos = this.tPos;
						this.smoothStartPos = this.smoothValue + this.tPos.y * this.partFactor;
						this.FillArrayWithValue(this.prevSmoothValue, this.smoothValue);
					}
					this.smoothValue = this.smoothStartPos - this.tPos.y * this.partFactor;
					if (this.smoothValue < -0.12f)
					{
						this.smoothValue = -0.12f;
					}
					else if (this.smoothValue > (float)this.maxValue + 0.12f)
					{
						this.smoothValue = (float)this.maxValue + 0.12f;
					}
					if (Input.GetMouseButtonUp(0))
					{
						if ((this.tPos - this.mStartPos).sqrMagnitude < 25f)
						{
							if (this.clickEdgeToSwitch)
							{
								if (this.leftEdgeRectForClickSwitch.Contains(this.tPos))
								{
									this.currentValue--;
									if (this.currentValue < 0)
									{
										this.currentValue = 0;
									}
								}
								else if (this.rightEdgeRectForClickSwitch.Contains(this.tPos))
								{
									this.currentValue++;
									if (this.currentValue > this.maxValue)
									{
										this.currentValue = this.maxValue;
									}
								}
							}
						}
						else if ((float)this.currentValue - (this.smoothValue + (this.smoothValue - this.GetAvgValue(this.prevSmoothValue))) > this.smoothDragOffset || (float)this.currentValue - (this.smoothValue + (this.smoothValue - this.GetAvgValue(this.prevSmoothValue))) < -this.smoothDragOffset)
						{
							this.currentValue = (int)Mathf.Round(this.smoothValue + (this.smoothValue - this.GetAvgValue(this.prevSmoothValue)));
							this.xVelocity = this.smoothValue - this.GetAvgValue(this.prevSmoothValue);
							if (this.currentValue > this.maxValue)
							{
								this.currentValue = this.maxValue;
							}
							else if (this.currentValue < 0)
							{
								this.currentValue = 0;
							}
						}
						this.mouseStartArea = 0;
					}
					for (int i = 1; i < this.prevSmoothValue.Length; i++)
					{
						this.prevSmoothValue[i] = this.prevSmoothValue[i - 1];
					}
					this.prevSmoothValue[0] = this.smoothValue;
				}
			}
			if (!this.touched)
			{
				this.smoothValue = Mathf.SmoothDamp(this.smoothValue, (float)this.currentValue, ref this.xVelocity, 0.3f, this.maxSpeed, Time.realtimeSinceStartup - this.realtimeStamp);
			}
			this.realtimeStamp = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x04002B7D RID: 11133
	public static bool controlEnabled;

	// Token: 0x04002B7E RID: 11134
	public bool skipAutoSetup;

	// Token: 0x04002B7F RID: 11135
	public bool allowInput = true;

	// Token: 0x04002B80 RID: 11136
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002B81 RID: 11137
	public float partWidth;

	// Token: 0x04002B82 RID: 11138
	private float partFactor = 1f;

	// Token: 0x04002B83 RID: 11139
	public int startValue;

	// Token: 0x04002B84 RID: 11140
	public int currentValue;

	// Token: 0x04002B85 RID: 11141
	public int maxValue;

	// Token: 0x04002B86 RID: 11142
	public Rect mouseRect;

	// Token: 0x04002B87 RID: 11143
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002B88 RID: 11144
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002B89 RID: 11145
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002B8A RID: 11146
	private bool touched;

	// Token: 0x04002B8B RID: 11147
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002B8C RID: 11148
	private int mouseStartArea;

	// Token: 0x04002B8D RID: 11149
	public float smoothValue;

	// Token: 0x04002B8E RID: 11150
	private float smoothStartPos;

	// Token: 0x04002B8F RID: 11151
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002B90 RID: 11152
	private float lastSmoothValue;

	// Token: 0x04002B91 RID: 11153
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002B92 RID: 11154
	private float realtimeStamp;

	// Token: 0x04002B93 RID: 11155
	private float xVelocity;

	// Token: 0x04002B94 RID: 11156
	public float maxSpeed = 20f;

	// Token: 0x04002B95 RID: 11157
	private Vector2 mStartPos;

	// Token: 0x04002B96 RID: 11158
	private Vector3 pos;

	// Token: 0x04002B97 RID: 11159
	private Vector2 tPos;

	// Token: 0x04002B98 RID: 11160
	public bool debug;
}
