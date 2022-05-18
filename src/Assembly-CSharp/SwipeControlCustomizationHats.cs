using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000761 RID: 1889
public class SwipeControlCustomizationHats : MonoBehaviour
{
	// Token: 0x0600300C RID: 12300 RVA: 0x00023A9C File Offset: 0x00021C9C
	private IEnumerator Start()
	{
		if (this.clickEdgeToSwitch && !SwipeControlCustomizationHats.allowInput)
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

	// Token: 0x0600300D RID: 12301 RVA: 0x0017FB98 File Offset: 0x0017DD98
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

	// Token: 0x0600300E RID: 12302 RVA: 0x00023AAB File Offset: 0x00021CAB
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x0600300F RID: 12303 RVA: 0x00023AB4 File Offset: 0x00021CB4
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003010 RID: 12304 RVA: 0x0017FC54 File Offset: 0x0017DE54
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

	// Token: 0x06003011 RID: 12305 RVA: 0x00023AC2 File Offset: 0x00021CC2
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003012 RID: 12306 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003013 RID: 12307 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06003014 RID: 12308 RVA: 0x0017FD0C File Offset: 0x0017DF0C
	private void Update()
	{
		if (SwipeControlCustomizationHats.controlEnabled)
		{
			this.touched = false;
			if (SwipeControlCustomizationHats.allowInput && (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
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

	// Token: 0x04002B9C RID: 11164
	public static bool controlEnabled = false;

	// Token: 0x04002B9D RID: 11165
	public bool skipAutoSetup;

	// Token: 0x04002B9E RID: 11166
	public static bool allowInput = true;

	// Token: 0x04002B9F RID: 11167
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002BA0 RID: 11168
	public float partWidth;

	// Token: 0x04002BA1 RID: 11169
	private float partFactor = 1f;

	// Token: 0x04002BA2 RID: 11170
	public int startValue;

	// Token: 0x04002BA3 RID: 11171
	public int currentValue;

	// Token: 0x04002BA4 RID: 11172
	public int maxValue;

	// Token: 0x04002BA5 RID: 11173
	public Rect mouseRect;

	// Token: 0x04002BA6 RID: 11174
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002BA7 RID: 11175
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002BA8 RID: 11176
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002BA9 RID: 11177
	private bool touched;

	// Token: 0x04002BAA RID: 11178
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002BAB RID: 11179
	private int mouseStartArea;

	// Token: 0x04002BAC RID: 11180
	public float smoothValue;

	// Token: 0x04002BAD RID: 11181
	private float smoothStartPos;

	// Token: 0x04002BAE RID: 11182
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002BAF RID: 11183
	private float lastSmoothValue;

	// Token: 0x04002BB0 RID: 11184
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002BB1 RID: 11185
	private float realtimeStamp;

	// Token: 0x04002BB2 RID: 11186
	private float xVelocity;

	// Token: 0x04002BB3 RID: 11187
	public float maxSpeed = 20f;

	// Token: 0x04002BB4 RID: 11188
	private Vector2 mStartPos;

	// Token: 0x04002BB5 RID: 11189
	private Vector3 pos;

	// Token: 0x04002BB6 RID: 11190
	private Vector2 tPos;

	// Token: 0x04002BB7 RID: 11191
	public bool debug;
}
