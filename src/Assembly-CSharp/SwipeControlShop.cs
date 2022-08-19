using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
public class SwipeControlShop : MonoBehaviour
{
	// Token: 0x060028E4 RID: 10468 RVA: 0x00135F95 File Offset: 0x00134195
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

	// Token: 0x060028E5 RID: 10469 RVA: 0x00135FA4 File Offset: 0x001341A4
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

	// Token: 0x060028E6 RID: 10470 RVA: 0x0013605F File Offset: 0x0013425F
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028E7 RID: 10471 RVA: 0x00136068 File Offset: 0x00134268
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x00136078 File Offset: 0x00134278
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

	// Token: 0x060028E9 RID: 10473 RVA: 0x0013612F File Offset: 0x0013432F
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x00136140 File Offset: 0x00134340
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x00136170 File Offset: 0x00134370
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x00136190 File Offset: 0x00134390
	private void Update()
	{
		if (SwipeControlShop.controlEnabled)
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

	// Token: 0x040024CC RID: 9420
	public static bool controlEnabled;

	// Token: 0x040024CD RID: 9421
	public bool skipAutoSetup;

	// Token: 0x040024CE RID: 9422
	public bool allowInput = true;

	// Token: 0x040024CF RID: 9423
	public bool clickEdgeToSwitch = true;

	// Token: 0x040024D0 RID: 9424
	public float partWidth;

	// Token: 0x040024D1 RID: 9425
	private float partFactor = 1f;

	// Token: 0x040024D2 RID: 9426
	public int startValue;

	// Token: 0x040024D3 RID: 9427
	public int currentValue;

	// Token: 0x040024D4 RID: 9428
	public int maxValue;

	// Token: 0x040024D5 RID: 9429
	public Rect mouseRect;

	// Token: 0x040024D6 RID: 9430
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x040024D7 RID: 9431
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x040024D8 RID: 9432
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x040024D9 RID: 9433
	private bool touched;

	// Token: 0x040024DA RID: 9434
	private int[] fingerStartArea = new int[5];

	// Token: 0x040024DB RID: 9435
	private int mouseStartArea;

	// Token: 0x040024DC RID: 9436
	public float smoothValue;

	// Token: 0x040024DD RID: 9437
	private float smoothStartPos;

	// Token: 0x040024DE RID: 9438
	private float smoothDragOffset = 0.2f;

	// Token: 0x040024DF RID: 9439
	private float lastSmoothValue;

	// Token: 0x040024E0 RID: 9440
	private float[] prevSmoothValue = new float[5];

	// Token: 0x040024E1 RID: 9441
	private float realtimeStamp;

	// Token: 0x040024E2 RID: 9442
	private float xVelocity;

	// Token: 0x040024E3 RID: 9443
	public float maxSpeed = 20f;

	// Token: 0x040024E4 RID: 9444
	private Vector2 mStartPos;

	// Token: 0x040024E5 RID: 9445
	private Vector3 pos;

	// Token: 0x040024E6 RID: 9446
	private Vector2 tPos;

	// Token: 0x040024E7 RID: 9447
	public bool debug;
}
