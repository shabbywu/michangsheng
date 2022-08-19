using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004E9 RID: 1257
public class SwipeControlCustomizationHats : MonoBehaviour
{
	// Token: 0x06002897 RID: 10391 RVA: 0x00133735 File Offset: 0x00131935
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

	// Token: 0x06002898 RID: 10392 RVA: 0x00133744 File Offset: 0x00131944
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

	// Token: 0x06002899 RID: 10393 RVA: 0x001337FF File Offset: 0x001319FF
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x00133808 File Offset: 0x00131A08
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x00133818 File Offset: 0x00131A18
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

	// Token: 0x0600289C RID: 10396 RVA: 0x001338CF File Offset: 0x00131ACF
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x0600289D RID: 10397 RVA: 0x001338E0 File Offset: 0x00131AE0
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x00133910 File Offset: 0x00131B10
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x00133930 File Offset: 0x00131B30
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

	// Token: 0x04002408 RID: 9224
	public static bool controlEnabled = false;

	// Token: 0x04002409 RID: 9225
	public bool skipAutoSetup;

	// Token: 0x0400240A RID: 9226
	public static bool allowInput = true;

	// Token: 0x0400240B RID: 9227
	public bool clickEdgeToSwitch = true;

	// Token: 0x0400240C RID: 9228
	public float partWidth;

	// Token: 0x0400240D RID: 9229
	private float partFactor = 1f;

	// Token: 0x0400240E RID: 9230
	public int startValue;

	// Token: 0x0400240F RID: 9231
	public int currentValue;

	// Token: 0x04002410 RID: 9232
	public int maxValue;

	// Token: 0x04002411 RID: 9233
	public Rect mouseRect;

	// Token: 0x04002412 RID: 9234
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x04002413 RID: 9235
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x04002414 RID: 9236
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x04002415 RID: 9237
	private bool touched;

	// Token: 0x04002416 RID: 9238
	private int[] fingerStartArea = new int[5];

	// Token: 0x04002417 RID: 9239
	private int mouseStartArea;

	// Token: 0x04002418 RID: 9240
	public float smoothValue;

	// Token: 0x04002419 RID: 9241
	private float smoothStartPos;

	// Token: 0x0400241A RID: 9242
	private float smoothDragOffset = 0.2f;

	// Token: 0x0400241B RID: 9243
	private float lastSmoothValue;

	// Token: 0x0400241C RID: 9244
	private float[] prevSmoothValue = new float[5];

	// Token: 0x0400241D RID: 9245
	private float realtimeStamp;

	// Token: 0x0400241E RID: 9246
	private float xVelocity;

	// Token: 0x0400241F RID: 9247
	public float maxSpeed = 20f;

	// Token: 0x04002420 RID: 9248
	private Vector2 mStartPos;

	// Token: 0x04002421 RID: 9249
	private Vector3 pos;

	// Token: 0x04002422 RID: 9250
	private Vector2 tPos;

	// Token: 0x04002423 RID: 9251
	public bool debug;
}
