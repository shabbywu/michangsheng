using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000769 RID: 1897
public class SwipeControlLeaderboard : MonoBehaviour
{
	// Token: 0x06003050 RID: 12368 RVA: 0x00023BDE File Offset: 0x00021DDE
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

	// Token: 0x06003051 RID: 12369 RVA: 0x00181268 File Offset: 0x0017F468
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

	// Token: 0x06003052 RID: 12370 RVA: 0x00023BED File Offset: 0x00021DED
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003053 RID: 12371 RVA: 0x00023BF6 File Offset: 0x00021DF6
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003054 RID: 12372 RVA: 0x00181324 File Offset: 0x0017F524
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

	// Token: 0x06003055 RID: 12373 RVA: 0x00023C04 File Offset: 0x00021E04
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x001813DC File Offset: 0x0017F5DC
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

	// Token: 0x04002C18 RID: 11288
	public static bool controlEnabled;

	// Token: 0x04002C19 RID: 11289
	public bool skipAutoSetup;

	// Token: 0x04002C1A RID: 11290
	public bool allowInput = true;

	// Token: 0x04002C1B RID: 11291
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002C1C RID: 11292
	public float partWidth;

	// Token: 0x04002C1D RID: 11293
	private float partFactor = 1f;

	// Token: 0x04002C1E RID: 11294
	public int startValue;

	// Token: 0x04002C1F RID: 11295
	public int currentValue;

	// Token: 0x04002C20 RID: 11296
	public int maxValue;

	// Token: 0x04002C21 RID: 11297
	public Rect mouseRect;

	// Token: 0x04002C22 RID: 11298
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002C23 RID: 11299
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002C24 RID: 11300
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002C25 RID: 11301
	private bool touched;

	// Token: 0x04002C26 RID: 11302
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002C27 RID: 11303
	private int mouseStartArea;

	// Token: 0x04002C28 RID: 11304
	public float smoothValue;

	// Token: 0x04002C29 RID: 11305
	private float smoothStartPos;

	// Token: 0x04002C2A RID: 11306
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002C2B RID: 11307
	private float lastSmoothValue;

	// Token: 0x04002C2C RID: 11308
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002C2D RID: 11309
	private float realtimeStamp;

	// Token: 0x04002C2E RID: 11310
	private float xVelocity;

	// Token: 0x04002C2F RID: 11311
	public float maxSpeed = 20f;

	// Token: 0x04002C30 RID: 11312
	private Vector2 mStartPos;

	// Token: 0x04002C31 RID: 11313
	private Vector3 pos;

	// Token: 0x04002C32 RID: 11314
	private Vector2 tPos;

	// Token: 0x04002C33 RID: 11315
	public bool debug;
}
