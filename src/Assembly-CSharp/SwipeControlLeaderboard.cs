using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004ED RID: 1261
public class SwipeControlLeaderboard : MonoBehaviour
{
	// Token: 0x060028C3 RID: 10435 RVA: 0x00134E49 File Offset: 0x00133049
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

	// Token: 0x060028C4 RID: 10436 RVA: 0x00134E58 File Offset: 0x00133058
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

	// Token: 0x060028C5 RID: 10437 RVA: 0x00134F13 File Offset: 0x00133113
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x00134F1C File Offset: 0x0013311C
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x00134F2C File Offset: 0x0013312C
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

	// Token: 0x060028C8 RID: 10440 RVA: 0x00134FE3 File Offset: 0x001331E3
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x00134FF4 File Offset: 0x001331F4
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028CA RID: 10442 RVA: 0x00135024 File Offset: 0x00133224
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028CB RID: 10443 RVA: 0x00135044 File Offset: 0x00133244
	private void Update()
	{
		if (SwipeControlLeaderboard.controlEnabled)
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

	// Token: 0x04002478 RID: 9336
	public static bool controlEnabled;

	// Token: 0x04002479 RID: 9337
	public bool skipAutoSetup;

	// Token: 0x0400247A RID: 9338
	public bool allowInput = true;

	// Token: 0x0400247B RID: 9339
	public bool clickEdgeToSwitch = true;

	// Token: 0x0400247C RID: 9340
	public float partWidth;

	// Token: 0x0400247D RID: 9341
	private float partFactor = 1f;

	// Token: 0x0400247E RID: 9342
	public int startValue;

	// Token: 0x0400247F RID: 9343
	public int currentValue;

	// Token: 0x04002480 RID: 9344
	public int maxValue;

	// Token: 0x04002481 RID: 9345
	public Rect mouseRect;

	// Token: 0x04002482 RID: 9346
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002483 RID: 9347
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002484 RID: 9348
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002485 RID: 9349
	private bool touched;

	// Token: 0x04002486 RID: 9350
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002487 RID: 9351
	private int mouseStartArea;

	// Token: 0x04002488 RID: 9352
	public float smoothValue;

	// Token: 0x04002489 RID: 9353
	private float smoothStartPos;

	// Token: 0x0400248A RID: 9354
	private float smoothDragOffset = 0.2f;

	// Token: 0x0400248B RID: 9355
	private float lastSmoothValue;

	// Token: 0x0400248C RID: 9356
	private float[] prevSmoothValue = new float[5];

	// Token: 0x0400248D RID: 9357
	private float realtimeStamp;

	// Token: 0x0400248E RID: 9358
	private float xVelocity;

	// Token: 0x0400248F RID: 9359
	public float maxSpeed = 20f;

	// Token: 0x04002490 RID: 9360
	private Vector2 mStartPos;

	// Token: 0x04002491 RID: 9361
	private Vector3 pos;

	// Token: 0x04002492 RID: 9362
	private Vector2 tPos;

	// Token: 0x04002493 RID: 9363
	public bool debug;
}
