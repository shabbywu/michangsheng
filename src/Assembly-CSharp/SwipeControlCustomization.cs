using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200075D RID: 1885
public class SwipeControlCustomization : MonoBehaviour
{
	// Token: 0x06002FEA RID: 12266 RVA: 0x00023A02 File Offset: 0x00021C02
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

	// Token: 0x06002FEB RID: 12267 RVA: 0x0017EFD8 File Offset: 0x0017D1D8
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

	// Token: 0x06002FEC RID: 12268 RVA: 0x00023A11 File Offset: 0x00021C11
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06002FED RID: 12269 RVA: 0x00023A1A File Offset: 0x00021C1A
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06002FEE RID: 12270 RVA: 0x0017F094 File Offset: 0x0017D294
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

	// Token: 0x06002FEF RID: 12271 RVA: 0x00023A28 File Offset: 0x00021C28
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06002FF0 RID: 12272 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06002FF1 RID: 12273 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06002FF2 RID: 12274 RVA: 0x0017F19C File Offset: 0x0017D39C
	private void Update()
	{
		if (SwipeControlCustomization.controlEnabled)
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

	// Token: 0x04002B5E RID: 11102
	public static bool controlEnabled;

	// Token: 0x04002B5F RID: 11103
	public bool skipAutoSetup;

	// Token: 0x04002B60 RID: 11104
	public bool allowInput = true;

	// Token: 0x04002B61 RID: 11105
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002B62 RID: 11106
	public float partWidth;

	// Token: 0x04002B63 RID: 11107
	private float partFactor = 1f;

	// Token: 0x04002B64 RID: 11108
	public int startValue;

	// Token: 0x04002B65 RID: 11109
	public int currentValue;

	// Token: 0x04002B66 RID: 11110
	public int maxValue;

	// Token: 0x04002B67 RID: 11111
	public Rect mouseRect;

	// Token: 0x04002B68 RID: 11112
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002B69 RID: 11113
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002B6A RID: 11114
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002B6B RID: 11115
	private bool touched;

	// Token: 0x04002B6C RID: 11116
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002B6D RID: 11117
	private int mouseStartArea;

	// Token: 0x04002B6E RID: 11118
	public float smoothValue;

	// Token: 0x04002B6F RID: 11119
	private float smoothStartPos;

	// Token: 0x04002B70 RID: 11120
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002B71 RID: 11121
	private float lastSmoothValue;

	// Token: 0x04002B72 RID: 11122
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002B73 RID: 11123
	private float realtimeStamp;

	// Token: 0x04002B74 RID: 11124
	private float xVelocity;

	// Token: 0x04002B75 RID: 11125
	public float maxSpeed = 20f;

	// Token: 0x04002B76 RID: 11126
	private Vector2 mStartPos;

	// Token: 0x04002B77 RID: 11127
	private Vector3 pos;

	// Token: 0x04002B78 RID: 11128
	private Vector2 tPos;

	// Token: 0x04002B79 RID: 11129
	public bool debug;
}
