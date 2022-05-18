using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200076D RID: 1901
public class SwipeControlSettingsTabs : MonoBehaviour
{
	// Token: 0x06003072 RID: 12402 RVA: 0x00023C78 File Offset: 0x00021E78
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

	// Token: 0x06003073 RID: 12403 RVA: 0x00181DD8 File Offset: 0x0017FFD8
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

	// Token: 0x06003074 RID: 12404 RVA: 0x00023C87 File Offset: 0x00021E87
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x00023C90 File Offset: 0x00021E90
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06003076 RID: 12406 RVA: 0x00181E94 File Offset: 0x00180094
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

	// Token: 0x06003077 RID: 12407 RVA: 0x00023C9E File Offset: 0x00021E9E
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06003078 RID: 12408 RVA: 0x0017F14C File Offset: 0x0017D34C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06003079 RID: 12409 RVA: 0x0017F17C File Offset: 0x0017D37C
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x0600307A RID: 12410 RVA: 0x00181F4C File Offset: 0x0018014C
	private void Update()
	{
		if (SwipeControlSettingsTabs.controlEnabled)
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

	// Token: 0x04002C56 RID: 11350
	public static bool controlEnabled;

	// Token: 0x04002C57 RID: 11351
	public bool skipAutoSetup;

	// Token: 0x04002C58 RID: 11352
	public bool allowInput = true;

	// Token: 0x04002C59 RID: 11353
	public bool clickEdgeToSwitch = true;

	// Token: 0x04002C5A RID: 11354
	public float partWidth;

	// Token: 0x04002C5B RID: 11355
	private float partFactor = 1f;

	// Token: 0x04002C5C RID: 11356
	public int startValue;

	// Token: 0x04002C5D RID: 11357
	public int currentValue;

	// Token: 0x04002C5E RID: 11358
	public int maxValue;

	// Token: 0x04002C5F RID: 11359
	public Rect mouseRect;

	// Token: 0x04002C60 RID: 11360
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002C61 RID: 11361
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002C62 RID: 11362
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002C63 RID: 11363
	private bool touched;

	// Token: 0x04002C64 RID: 11364
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002C65 RID: 11365
	private int mouseStartArea;

	// Token: 0x04002C66 RID: 11366
	public float smoothValue;

	// Token: 0x04002C67 RID: 11367
	private float smoothStartPos;

	// Token: 0x04002C68 RID: 11368
	private float smoothDragOffset = 0.2f;

	// Token: 0x04002C69 RID: 11369
	private float lastSmoothValue;

	// Token: 0x04002C6A RID: 11370
	private float[] prevSmoothValue = new float[5];

	// Token: 0x04002C6B RID: 11371
	private float realtimeStamp;

	// Token: 0x04002C6C RID: 11372
	private float xVelocity;

	// Token: 0x04002C6D RID: 11373
	public float maxSpeed = 20f;

	// Token: 0x04002C6E RID: 11374
	private Vector2 mStartPos;

	// Token: 0x04002C6F RID: 11375
	private Vector3 pos;

	// Token: 0x04002C70 RID: 11376
	private Vector2 tPos;

	// Token: 0x04002C71 RID: 11377
	public bool debug;
}
