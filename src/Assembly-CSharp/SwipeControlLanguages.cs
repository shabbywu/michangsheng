using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000767 RID: 1895
public class SwipeControlLanguages : MonoBehaviour
{
	// Token: 0x0600303F RID: 12351 RVA: 0x00023B91 File Offset: 0x00021D91
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

	// Token: 0x06003040 RID: 12352 RVA: 0x00180CB0 File Offset: 0x0017EEB0
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

	// Token: 0x06003041 RID: 12353 RVA: 0x00023BA0 File Offset: 0x00021DA0
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003042 RID: 12354 RVA: 0x00023BA9 File Offset: 0x00021DA9
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x00180D6C File Offset: 0x0017EF6C
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

	// Token: 0x06003044 RID: 12356 RVA: 0x00023BB7 File Offset: 0x00021DB7
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003046 RID: 12358 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x00180E24 File Offset: 0x0017F024
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

	// Token: 0x04002BF9 RID: 11257
	public static bool controlEnabled;

	// Token: 0x04002BFA RID: 11258
	public bool skipAutoSetup;

	// Token: 0x04002BFB RID: 11259
	public bool allowInput = true;

	// Token: 0x04002BFC RID: 11260
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002BFD RID: 11261
	public float partWidth;

	// Token: 0x04002BFE RID: 11262
	private float partFactor = 1f;

	// Token: 0x04002BFF RID: 11263
	public int startValue;

	// Token: 0x04002C00 RID: 11264
	public int currentValue;

	// Token: 0x04002C01 RID: 11265
	public int maxValue;

	// Token: 0x04002C02 RID: 11266
	public Rect mouseRect;

	// Token: 0x04002C03 RID: 11267
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002C04 RID: 11268
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002C05 RID: 11269
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002C06 RID: 11270
	private bool touched;

	// Token: 0x04002C07 RID: 11271
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002C08 RID: 11272
	private int mouseStartArea;

	// Token: 0x04002C09 RID: 11273
	public float smoothValue;

	// Token: 0x04002C0A RID: 11274
	private float smoothStartPos;

	// Token: 0x04002C0B RID: 11275
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002C0C RID: 11276
	private float lastSmoothValue;

	// Token: 0x04002C0D RID: 11277
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002C0E RID: 11278
	private float realtimeStamp;

	// Token: 0x04002C0F RID: 11279
	private float xVelocity;

	// Token: 0x04002C10 RID: 11280
	public float maxSpeed = 20f;

	// Token: 0x04002C11 RID: 11281
	private Vector2 mStartPos;

	// Token: 0x04002C12 RID: 11282
	private Vector3 pos;

	// Token: 0x04002C13 RID: 11283
	private Vector2 tPos;

	// Token: 0x04002C14 RID: 11284
	public bool debug;
}
