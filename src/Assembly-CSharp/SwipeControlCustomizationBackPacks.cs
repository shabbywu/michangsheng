using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004E8 RID: 1256
public class SwipeControlCustomizationBackPacks : MonoBehaviour
{
	// Token: 0x0600288C RID: 10380 RVA: 0x00133171 File Offset: 0x00131371
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

	// Token: 0x0600288D RID: 10381 RVA: 0x00133180 File Offset: 0x00131380
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

	// Token: 0x0600288E RID: 10382 RVA: 0x0013323B File Offset: 0x0013143B
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x00133244 File Offset: 0x00131444
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x00133254 File Offset: 0x00131454
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

	// Token: 0x06002891 RID: 10385 RVA: 0x0013330B File Offset: 0x0013150B
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x0013331C File Offset: 0x0013151C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x0013334C File Offset: 0x0013154C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x0013336C File Offset: 0x0013156C
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

	// Token: 0x040023EC RID: 9196
	public static bool controlEnabled;

	// Token: 0x040023ED RID: 9197
	public bool skipAutoSetup;

	// Token: 0x040023EE RID: 9198
	public bool allowInput = true;

	// Token: 0x040023EF RID: 9199
	public bool clickEdgeToSwitch = true;

	// Token: 0x040023F0 RID: 9200
	public float partWidth;

	// Token: 0x040023F1 RID: 9201
	private float partFactor = 1f;

	// Token: 0x040023F2 RID: 9202
	public int startValue;

	// Token: 0x040023F3 RID: 9203
	public int currentValue;

	// Token: 0x040023F4 RID: 9204
	public int maxValue;

	// Token: 0x040023F5 RID: 9205
	public Rect mouseRect;

	// Token: 0x040023F6 RID: 9206
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x040023F7 RID: 9207
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x040023F8 RID: 9208
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x040023F9 RID: 9209
	private bool touched;

	// Token: 0x040023FA RID: 9210
	private int[] fingerStartArea = new int[5];

	// Token: 0x040023FB RID: 9211
	private int mouseStartArea;

	// Token: 0x040023FC RID: 9212
	public float smoothValue;

	// Token: 0x040023FD RID: 9213
	private float smoothStartPos;

	// Token: 0x040023FE RID: 9214
	private float smoothDragOffset = 0.2f;

	// Token: 0x040023FF RID: 9215
	private float lastSmoothValue;

	// Token: 0x04002400 RID: 9216
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002401 RID: 9217
	private float realtimeStamp;

	// Token: 0x04002402 RID: 9218
	private float xVelocity;

	// Token: 0x04002403 RID: 9219
	public float maxSpeed = 20f;

	// Token: 0x04002404 RID: 9220
	private Vector2 mStartPos;

	// Token: 0x04002405 RID: 9221
	private Vector3 pos;

	// Token: 0x04002406 RID: 9222
	private Vector2 tPos;

	// Token: 0x04002407 RID: 9223
	public bool debug;
}
