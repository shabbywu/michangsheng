using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class SwipeControlSettingsTabs : MonoBehaviour
{
	// Token: 0x060028D9 RID: 10457 RVA: 0x001359D1 File Offset: 0x00133BD1
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

	// Token: 0x060028DA RID: 10458 RVA: 0x001359E0 File Offset: 0x00133BE0
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

	// Token: 0x060028DB RID: 10459 RVA: 0x00135A9B File Offset: 0x00133C9B
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x00135AA4 File Offset: 0x00133CA4
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x00135AB4 File Offset: 0x00133CB4
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

	// Token: 0x060028DE RID: 10462 RVA: 0x00135B6B File Offset: 0x00133D6B
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x00135B7C File Offset: 0x00133D7C
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x00135BAC File Offset: 0x00133DAC
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x00135BCC File Offset: 0x00133DCC
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

	// Token: 0x040024B0 RID: 9392
	public static bool controlEnabled;

	// Token: 0x040024B1 RID: 9393
	public bool skipAutoSetup;

	// Token: 0x040024B2 RID: 9394
	public bool allowInput = true;

	// Token: 0x040024B3 RID: 9395
	public bool clickEdgeToSwitch = true;

	// Token: 0x040024B4 RID: 9396
	public float partWidth;

	// Token: 0x040024B5 RID: 9397
	private float partFactor = 1f;

	// Token: 0x040024B6 RID: 9398
	public int startValue;

	// Token: 0x040024B7 RID: 9399
	public int currentValue;

	// Token: 0x040024B8 RID: 9400
	public int maxValue;

	// Token: 0x040024B9 RID: 9401
	public Rect mouseRect;

	// Token: 0x040024BA RID: 9402
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x040024BB RID: 9403
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x040024BC RID: 9404
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x040024BD RID: 9405
	private bool touched;

	// Token: 0x040024BE RID: 9406
	private int[] fingerStartArea = new int[5];

	// Token: 0x040024BF RID: 9407
	private int mouseStartArea;

	// Token: 0x040024C0 RID: 9408
	public float smoothValue;

	// Token: 0x040024C1 RID: 9409
	private float smoothStartPos;

	// Token: 0x040024C2 RID: 9410
	private float smoothDragOffset = 0.2f;

	// Token: 0x040024C3 RID: 9411
	private float lastSmoothValue;

	// Token: 0x040024C4 RID: 9412
	private float[] prevSmoothValue = new float[5];

	// Token: 0x040024C5 RID: 9413
	private float realtimeStamp;

	// Token: 0x040024C6 RID: 9414
	private float xVelocity;

	// Token: 0x040024C7 RID: 9415
	public float maxSpeed = 20f;

	// Token: 0x040024C8 RID: 9416
	private Vector2 mStartPos;

	// Token: 0x040024C9 RID: 9417
	private Vector3 pos;

	// Token: 0x040024CA RID: 9418
	private Vector2 tPos;

	// Token: 0x040024CB RID: 9419
	public bool debug;
}
