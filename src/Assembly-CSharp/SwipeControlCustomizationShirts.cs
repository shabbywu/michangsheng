using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004EA RID: 1258
public class SwipeControlCustomizationShirts : MonoBehaviour
{
	// Token: 0x060028A2 RID: 10402 RVA: 0x00133CFC File Offset: 0x00131EFC
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

	// Token: 0x060028A3 RID: 10403 RVA: 0x00133D0C File Offset: 0x00131F0C
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

	// Token: 0x060028A4 RID: 10404 RVA: 0x00133DC7 File Offset: 0x00131FC7
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x00133DD0 File Offset: 0x00131FD0
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x00133DE0 File Offset: 0x00131FE0
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

	// Token: 0x060028A7 RID: 10407 RVA: 0x00133E97 File Offset: 0x00132097
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x00133EA8 File Offset: 0x001320A8
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x00133ED8 File Offset: 0x001320D8
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x00133EF8 File Offset: 0x001320F8
	private void Update()
	{
		if (SwipeControlCustomizationShirts.controlEnabled)
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

	// Token: 0x04002424 RID: 9252
	public static bool controlEnabled;

	// Token: 0x04002425 RID: 9253
	public bool skipAutoSetup;

	// Token: 0x04002426 RID: 9254
	public bool allowInput = true;

	// Token: 0x04002427 RID: 9255
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002428 RID: 9256
	public float partWidth;

	// Token: 0x04002429 RID: 9257
	private float partFactor = 1f;

	// Token: 0x0400242A RID: 9258
	public int startValue;

	// Token: 0x0400242B RID: 9259
	public int currentValue;

	// Token: 0x0400242C RID: 9260
	public int maxValue;

	// Token: 0x0400242D RID: 9261
	public Rect mouseRect;

	// Token: 0x0400242E RID: 9262
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x0400242F RID: 9263
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002430 RID: 9264
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002431 RID: 9265
	private bool touched;

	// Token: 0x04002432 RID: 9266
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002433 RID: 9267
	private int mouseStartArea;

	// Token: 0x04002434 RID: 9268
	public float smoothValue;

	// Token: 0x04002435 RID: 9269
	private float smoothStartPos;

	// Token: 0x04002436 RID: 9270
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002437 RID: 9271
	private float lastSmoothValue;

	// Token: 0x04002438 RID: 9272
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002439 RID: 9273
	private float realtimeStamp;

	// Token: 0x0400243A RID: 9274
	private float xVelocity;

	// Token: 0x0400243B RID: 9275
	public float maxSpeed = 20f;

	// Token: 0x0400243C RID: 9276
	private Vector2 mStartPos;

	// Token: 0x0400243D RID: 9277
	private Vector3 pos;

	// Token: 0x0400243E RID: 9278
	private Vector2 tPos;

	// Token: 0x0400243F RID: 9279
	public bool debug;
}
