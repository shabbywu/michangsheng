using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004EB RID: 1259
public class SwipeControlFreeCoins : MonoBehaviour
{
	// Token: 0x060028AD RID: 10413 RVA: 0x001342C1 File Offset: 0x001324C1
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

	// Token: 0x060028AE RID: 10414 RVA: 0x001342D0 File Offset: 0x001324D0
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

	// Token: 0x060028AF RID: 10415 RVA: 0x0013438B File Offset: 0x0013258B
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x00134394 File Offset: 0x00132594
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x001343A4 File Offset: 0x001325A4
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

	// Token: 0x060028B2 RID: 10418 RVA: 0x0013445B File Offset: 0x0013265B
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028B3 RID: 10419 RVA: 0x0013446C File Offset: 0x0013266C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028B4 RID: 10420 RVA: 0x0013449C File Offset: 0x0013269C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x001344BC File Offset: 0x001326BC
	private void Update()
	{
		if (SwipeControlFreeCoins.controlEnabled)
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

	// Token: 0x04002440 RID: 9280
	public static bool controlEnabled;

	// Token: 0x04002441 RID: 9281
	public bool skipAutoSetup;

	// Token: 0x04002442 RID: 9282
	public bool allowInput = true;

	// Token: 0x04002443 RID: 9283
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002444 RID: 9284
	public float partWidth;

	// Token: 0x04002445 RID: 9285
	private float partFactor = 1f;

	// Token: 0x04002446 RID: 9286
	public int startValue;

	// Token: 0x04002447 RID: 9287
	public int currentValue;

	// Token: 0x04002448 RID: 9288
	public int maxValue;

	// Token: 0x04002449 RID: 9289
	public Rect mouseRect;

	// Token: 0x0400244A RID: 9290
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x0400244B RID: 9291
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x0400244C RID: 9292
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x0400244D RID: 9293
	private bool touched;

	// Token: 0x0400244E RID: 9294
	private int[] fingerStartArea = new int[5];

	// Token: 0x0400244F RID: 9295
	private int mouseStartArea;

	// Token: 0x04002450 RID: 9296
	public float smoothValue;

	// Token: 0x04002451 RID: 9297
	private float smoothStartPos;

	// Token: 0x04002452 RID: 9298
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002453 RID: 9299
	private float lastSmoothValue;

	// Token: 0x04002454 RID: 9300
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002455 RID: 9301
	private float realtimeStamp;

	// Token: 0x04002456 RID: 9302
	private float xVelocity;

	// Token: 0x04002457 RID: 9303
	public float maxSpeed = 20f;

	// Token: 0x04002458 RID: 9304
	private Vector2 mStartPos;

	// Token: 0x04002459 RID: 9305
	private Vector3 pos;

	// Token: 0x0400245A RID: 9306
	private Vector2 tPos;

	// Token: 0x0400245B RID: 9307
	public bool debug;
}
