using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200076F RID: 1903
public class SwipeControlShop : MonoBehaviour
{
	// Token: 0x06003083 RID: 12419 RVA: 0x00023CC5 File Offset: 0x00021EC5
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

	// Token: 0x06003084 RID: 12420 RVA: 0x00182390 File Offset: 0x00180590
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

	// Token: 0x06003085 RID: 12421 RVA: 0x00023CD4 File Offset: 0x00021ED4
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003086 RID: 12422 RVA: 0x00023CDD File Offset: 0x00021EDD
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003087 RID: 12423 RVA: 0x0018244C File Offset: 0x0018064C
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

	// Token: 0x06003088 RID: 12424 RVA: 0x00023CEB File Offset: 0x00021EEB
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003089 RID: 12425 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x0600308A RID: 12426 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x0600308B RID: 12427 RVA: 0x00182504 File Offset: 0x00180704
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

	// Token: 0x04002C75 RID: 11381
	public static bool controlEnabled;

	// Token: 0x04002C76 RID: 11382
	public bool skipAutoSetup;

	// Token: 0x04002C77 RID: 11383
	public bool allowInput = true;

	// Token: 0x04002C78 RID: 11384
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002C79 RID: 11385
	public float partWidth;

	// Token: 0x04002C7A RID: 11386
	private float partFactor = 1f;

	// Token: 0x04002C7B RID: 11387
	public int startValue;

	// Token: 0x04002C7C RID: 11388
	public int currentValue;

	// Token: 0x04002C7D RID: 11389
	public int maxValue;

	// Token: 0x04002C7E RID: 11390
	public Rect mouseRect;

	// Token: 0x04002C7F RID: 11391
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002C80 RID: 11392
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002C81 RID: 11393
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002C82 RID: 11394
	private bool touched;

	// Token: 0x04002C83 RID: 11395
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002C84 RID: 11396
	private int mouseStartArea;

	// Token: 0x04002C85 RID: 11397
	public float smoothValue;

	// Token: 0x04002C86 RID: 11398
	private float smoothStartPos;

	// Token: 0x04002C87 RID: 11399
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002C88 RID: 11400
	private float lastSmoothValue;

	// Token: 0x04002C89 RID: 11401
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002C8A RID: 11402
	private float realtimeStamp;

	// Token: 0x04002C8B RID: 11403
	private float xVelocity;

	// Token: 0x04002C8C RID: 11404
	public float maxSpeed = 20f;

	// Token: 0x04002C8D RID: 11405
	private Vector2 mStartPos;

	// Token: 0x04002C8E RID: 11406
	private Vector3 pos;

	// Token: 0x04002C8F RID: 11407
	private Vector2 tPos;

	// Token: 0x04002C90 RID: 11408
	public bool debug;
}
