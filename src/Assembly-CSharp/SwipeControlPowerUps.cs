using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004EE RID: 1262
public class SwipeControlPowerUps : MonoBehaviour
{
	// Token: 0x060028CE RID: 10446 RVA: 0x0013540D File Offset: 0x0013360D
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

	// Token: 0x060028CF RID: 10447 RVA: 0x0013541C File Offset: 0x0013361C
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

	// Token: 0x060028D0 RID: 10448 RVA: 0x001354D7 File Offset: 0x001336D7
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028D1 RID: 10449 RVA: 0x001354E0 File Offset: 0x001336E0
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x001354F0 File Offset: 0x001336F0
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

	// Token: 0x060028D3 RID: 10451 RVA: 0x001355A7 File Offset: 0x001337A7
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028D4 RID: 10452 RVA: 0x001355B8 File Offset: 0x001337B8
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x001355E8 File Offset: 0x001337E8
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x00135608 File Offset: 0x00133808
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

	// Token: 0x04002494 RID: 9364
	public static bool controlEnabled;

	// Token: 0x04002495 RID: 9365
	public bool skipAutoSetup;

	// Token: 0x04002496 RID: 9366
	public bool allowInput = true;

	// Token: 0x04002497 RID: 9367
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002498 RID: 9368
	public float partWidth;

	// Token: 0x04002499 RID: 9369
	private float partFactor = 1f;

	// Token: 0x0400249A RID: 9370
	public int startValue;

	// Token: 0x0400249B RID: 9371
	public int currentValue;

	// Token: 0x0400249C RID: 9372
	public int maxValue;

	// Token: 0x0400249D RID: 9373
	public Rect mouseRect;

	// Token: 0x0400249E RID: 9374
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x0400249F RID: 9375
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x040024A0 RID: 9376
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x040024A1 RID: 9377
	private bool touched;

	// Token: 0x040024A2 RID: 9378
	private int[] fingerStartArea = new int[5];

	// Token: 0x040024A3 RID: 9379
	private int mouseStartArea;

	// Token: 0x040024A4 RID: 9380
	public float smoothValue;

	// Token: 0x040024A5 RID: 9381
	private float smoothStartPos;

	// Token: 0x040024A6 RID: 9382
	private float smoothDragOffset = 0.2f;

	// Token: 0x040024A7 RID: 9383
	private float lastSmoothValue;

	// Token: 0x040024A8 RID: 9384
	private float[] prevSmoothValue = new float[5];

	// Token: 0x040024A9 RID: 9385
	private float realtimeStamp;

	// Token: 0x040024AA RID: 9386
	private float xVelocity;

	// Token: 0x040024AB RID: 9387
	public float maxSpeed = 20f;

	// Token: 0x040024AC RID: 9388
	private Vector2 mStartPos;

	// Token: 0x040024AD RID: 9389
	private Vector3 pos;

	// Token: 0x040024AE RID: 9390
	private Vector2 tPos;

	// Token: 0x040024AF RID: 9391
	public bool debug;
}
