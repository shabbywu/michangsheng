using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004EC RID: 1260
public class SwipeControlLanguages : MonoBehaviour
{
	// Token: 0x060028B8 RID: 10424 RVA: 0x00134885 File Offset: 0x00132A85
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

	// Token: 0x060028B9 RID: 10425 RVA: 0x00134894 File Offset: 0x00132A94
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

	// Token: 0x060028BA RID: 10426 RVA: 0x0013494F File Offset: 0x00132B4F
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x00134958 File Offset: 0x00132B58
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x00134968 File Offset: 0x00132B68
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

	// Token: 0x060028BD RID: 10429 RVA: 0x00134A1F File Offset: 0x00132C1F
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028BE RID: 10430 RVA: 0x00134A30 File Offset: 0x00132C30
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x00134A60 File Offset: 0x00132C60
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x00134A80 File Offset: 0x00132C80
	private void Update()
	{
		if (SwipeControlLanguages.controlEnabled)
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

	// Token: 0x0400245C RID: 9308
	public static bool controlEnabled;

	// Token: 0x0400245D RID: 9309
	public bool skipAutoSetup;

	// Token: 0x0400245E RID: 9310
	public bool allowInput = true;

	// Token: 0x0400245F RID: 9311
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002460 RID: 9312
	public float partWidth;

	// Token: 0x04002461 RID: 9313
	private float partFactor = 1f;

	// Token: 0x04002462 RID: 9314
	public int startValue;

	// Token: 0x04002463 RID: 9315
	public int currentValue;

	// Token: 0x04002464 RID: 9316
	public int maxValue;

	// Token: 0x04002465 RID: 9317
	public Rect mouseRect;

	// Token: 0x04002466 RID: 9318
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002467 RID: 9319
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002468 RID: 9320
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002469 RID: 9321
	private bool touched;

	// Token: 0x0400246A RID: 9322
	private int[] fingerStartArea = new int[5];

	// Token: 0x0400246B RID: 9323
	private int mouseStartArea;

	// Token: 0x0400246C RID: 9324
	public float smoothValue;

	// Token: 0x0400246D RID: 9325
	private float smoothStartPos;

	// Token: 0x0400246E RID: 9326
	private float smoothDragOffset = 0.2f;

	// Token: 0x0400246F RID: 9327
	private float lastSmoothValue;

	// Token: 0x04002470 RID: 9328
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002471 RID: 9329
	private float realtimeStamp;

	// Token: 0x04002472 RID: 9330
	private float xVelocity;

	// Token: 0x04002473 RID: 9331
	public float maxSpeed = 20f;

	// Token: 0x04002474 RID: 9332
	private Vector2 mStartPos;

	// Token: 0x04002475 RID: 9333
	private Vector3 pos;

	// Token: 0x04002476 RID: 9334
	private Vector2 tPos;

	// Token: 0x04002477 RID: 9335
	public bool debug;
}
