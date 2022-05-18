using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000765 RID: 1893
public class SwipeControlFreeCoins : MonoBehaviour
{
	// Token: 0x0600302E RID: 12334 RVA: 0x00023B44 File Offset: 0x00021D44
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

	// Token: 0x0600302F RID: 12335 RVA: 0x001806F8 File Offset: 0x0017E8F8
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

	// Token: 0x06003030 RID: 12336 RVA: 0x00023B53 File Offset: 0x00021D53
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003031 RID: 12337 RVA: 0x00023B5C File Offset: 0x00021D5C
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003032 RID: 12338 RVA: 0x001807B4 File Offset: 0x0017E9B4
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

	// Token: 0x06003033 RID: 12339 RVA: 0x00023B6A File Offset: 0x00021D6A
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003035 RID: 12341 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003036 RID: 12342 RVA: 0x0018086C File Offset: 0x0017EA6C
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

	// Token: 0x04002BDA RID: 11226
	public static bool controlEnabled;

	// Token: 0x04002BDB RID: 11227
	public bool skipAutoSetup;

	// Token: 0x04002BDC RID: 11228
	public bool allowInput = true;

	// Token: 0x04002BDD RID: 11229
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002BDE RID: 11230
	public float partWidth;

	// Token: 0x04002BDF RID: 11231
	private float partFactor = 1f;

	// Token: 0x04002BE0 RID: 11232
	public int startValue;

	// Token: 0x04002BE1 RID: 11233
	public int currentValue;

	// Token: 0x04002BE2 RID: 11234
	public int maxValue;

	// Token: 0x04002BE3 RID: 11235
	public Rect mouseRect;

	// Token: 0x04002BE4 RID: 11236
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002BE5 RID: 11237
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002BE6 RID: 11238
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002BE7 RID: 11239
	private bool touched;

	// Token: 0x04002BE8 RID: 11240
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002BE9 RID: 11241
	private int mouseStartArea;

	// Token: 0x04002BEA RID: 11242
	public float smoothValue;

	// Token: 0x04002BEB RID: 11243
	private float smoothStartPos;

	// Token: 0x04002BEC RID: 11244
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002BED RID: 11245
	private float lastSmoothValue;

	// Token: 0x04002BEE RID: 11246
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002BEF RID: 11247
	private float realtimeStamp;

	// Token: 0x04002BF0 RID: 11248
	private float xVelocity;

	// Token: 0x04002BF1 RID: 11249
	public float maxSpeed = 20f;

	// Token: 0x04002BF2 RID: 11250
	private Vector2 mStartPos;

	// Token: 0x04002BF3 RID: 11251
	private Vector3 pos;

	// Token: 0x04002BF4 RID: 11252
	private Vector2 tPos;

	// Token: 0x04002BF5 RID: 11253
	public bool debug;
}
