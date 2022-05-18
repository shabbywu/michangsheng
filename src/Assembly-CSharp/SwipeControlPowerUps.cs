using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200076B RID: 1899
public class SwipeControlPowerUps : MonoBehaviour
{
	// Token: 0x06003061 RID: 12385 RVA: 0x00023C2B File Offset: 0x00021E2B
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

	// Token: 0x06003062 RID: 12386 RVA: 0x00181820 File Offset: 0x0017FA20
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

	// Token: 0x06003063 RID: 12387 RVA: 0x00023C3A File Offset: 0x00021E3A
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003064 RID: 12388 RVA: 0x00023C43 File Offset: 0x00021E43
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003065 RID: 12389 RVA: 0x001818DC File Offset: 0x0017FADC
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

	// Token: 0x06003066 RID: 12390 RVA: 0x00023C51 File Offset: 0x00021E51
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003067 RID: 12391 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003068 RID: 12392 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003069 RID: 12393 RVA: 0x00181994 File Offset: 0x0017FB94
	private void Update()
	{
		if (SwipeControlPowerUps.controlEnabled)
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

	// Token: 0x04002C37 RID: 11319
	public static bool controlEnabled;

	// Token: 0x04002C38 RID: 11320
	public bool skipAutoSetup;

	// Token: 0x04002C39 RID: 11321
	public bool allowInput = true;

	// Token: 0x04002C3A RID: 11322
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002C3B RID: 11323
	public float partWidth;

	// Token: 0x04002C3C RID: 11324
	private float partFactor = 1f;

	// Token: 0x04002C3D RID: 11325
	public int startValue;

	// Token: 0x04002C3E RID: 11326
	public int currentValue;

	// Token: 0x04002C3F RID: 11327
	public int maxValue;

	// Token: 0x04002C40 RID: 11328
	public Rect mouseRect;

	// Token: 0x04002C41 RID: 11329
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002C42 RID: 11330
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002C43 RID: 11331
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002C44 RID: 11332
	private bool touched;

	// Token: 0x04002C45 RID: 11333
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002C46 RID: 11334
	private int mouseStartArea;

	// Token: 0x04002C47 RID: 11335
	public float smoothValue;

	// Token: 0x04002C48 RID: 11336
	private float smoothStartPos;

	// Token: 0x04002C49 RID: 11337
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002C4A RID: 11338
	private float lastSmoothValue;

	// Token: 0x04002C4B RID: 11339
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002C4C RID: 11340
	private float realtimeStamp;

	// Token: 0x04002C4D RID: 11341
	private float xVelocity;

	// Token: 0x04002C4E RID: 11342
	public float maxSpeed = 20f;

	// Token: 0x04002C4F RID: 11343
	private Vector2 mStartPos;

	// Token: 0x04002C50 RID: 11344
	private Vector3 pos;

	// Token: 0x04002C51 RID: 11345
	private Vector2 tPos;

	// Token: 0x04002C52 RID: 11346
	public bool debug;
}
