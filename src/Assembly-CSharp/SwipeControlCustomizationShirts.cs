using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000763 RID: 1891
public class SwipeControlCustomizationShirts : MonoBehaviour
{
	// Token: 0x0600301D RID: 12317 RVA: 0x00023AF7 File Offset: 0x00021CF7
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

	// Token: 0x0600301E RID: 12318 RVA: 0x00180140 File Offset: 0x0017E340
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

	// Token: 0x0600301F RID: 12319 RVA: 0x00023B06 File Offset: 0x00021D06
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003020 RID: 12320 RVA: 0x00023B0F File Offset: 0x00021D0F
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003021 RID: 12321 RVA: 0x001801FC File Offset: 0x0017E3FC
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

	// Token: 0x06003022 RID: 12322 RVA: 0x00023B1D File Offset: 0x00021D1D
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003023 RID: 12323 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003024 RID: 12324 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003025 RID: 12325 RVA: 0x001802B4 File Offset: 0x0017E4B4
	private void Update()
	{
		if (SwipeControlCustomizationShirts.controlEnabled)
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

	// Token: 0x04002BBB RID: 11195
	public static bool controlEnabled;

	// Token: 0x04002BBC RID: 11196
	public bool skipAutoSetup;

	// Token: 0x04002BBD RID: 11197
	public bool allowInput = true;

	// Token: 0x04002BBE RID: 11198
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002BBF RID: 11199
	public float partWidth;

	// Token: 0x04002BC0 RID: 11200
	private float partFactor = 1f;

	// Token: 0x04002BC1 RID: 11201
	public int startValue;

	// Token: 0x04002BC2 RID: 11202
	public int currentValue;

	// Token: 0x04002BC3 RID: 11203
	public int maxValue;

	// Token: 0x04002BC4 RID: 11204
	public Rect mouseRect;

	// Token: 0x04002BC5 RID: 11205
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002BC6 RID: 11206
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002BC7 RID: 11207
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002BC8 RID: 11208
	private bool touched;

	// Token: 0x04002BC9 RID: 11209
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002BCA RID: 11210
	private int mouseStartArea;

	// Token: 0x04002BCB RID: 11211
	public float smoothValue;

	// Token: 0x04002BCC RID: 11212
	private float smoothStartPos;

	// Token: 0x04002BCD RID: 11213
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002BCE RID: 11214
	private float lastSmoothValue;

	// Token: 0x04002BCF RID: 11215
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002BD0 RID: 11216
	private float realtimeStamp;

	// Token: 0x04002BD1 RID: 11217
	private float xVelocity;

	// Token: 0x04002BD2 RID: 11218
	public float maxSpeed = 20f;

	// Token: 0x04002BD3 RID: 11219
	private Vector2 mStartPos;

	// Token: 0x04002BD4 RID: 11220
	private Vector3 pos;

	// Token: 0x04002BD5 RID: 11221
	private Vector2 tPos;

	// Token: 0x04002BD6 RID: 11222
	public bool debug;
}
