using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004E7 RID: 1255
public class SwipeControlCustomization : MonoBehaviour
{
	// Token: 0x06002881 RID: 10369 RVA: 0x00132BAB File Offset: 0x00130DAB
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

	// Token: 0x06002882 RID: 10370 RVA: 0x00132BBC File Offset: 0x00130DBC
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

	// Token: 0x06002883 RID: 10371 RVA: 0x00132C77 File Offset: 0x00130E77
	public void SetMouseRect(Rect myRect)
	{
		this.mouseRect = myRect;
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x00132C80 File Offset: 0x00130E80
	public void CalculateEdgeRectsFromMouseRect()
	{
		this.CalculateEdgeRectsFromMouseRect(this.mouseRect);
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x00132C90 File Offset: 0x00130E90
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

	// Token: 0x06002886 RID: 10374 RVA: 0x00132D47 File Offset: 0x00130F47
	public void SetEdgeRects(Rect leftRect, Rect rightRect)
	{
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x00132D58 File Offset: 0x00130F58
	private float GetAvgValue(float[] arr)
	{
		float num = 0f;
		for (int i = 0; i < arr.Length; i++)
		{
			num += arr[i];
		}
		return num / (float)arr.Length;
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x00132D88 File Offset: 0x00130F88
	private void FillArrayWithValue(float[] arr, float val)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = val;
		}
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x00132DA8 File Offset: 0x00130FA8
	private void Update()
	{
		if (SwipeControlCustomization.controlEnabled)
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

	// Token: 0x040023D0 RID: 9168
	public static bool controlEnabled;

	// Token: 0x040023D1 RID: 9169
	public bool skipAutoSetup;

	// Token: 0x040023D2 RID: 9170
	public bool allowInput = true;

	// Token: 0x040023D3 RID: 9171
	public bool clickEdgeToSwitch = true;

	// Token: 0x040023D4 RID: 9172
	public float partWidth;

	// Token: 0x040023D5 RID: 9173
	private float partFactor = 1f;

	// Token: 0x040023D6 RID: 9174
	public int startValue;

	// Token: 0x040023D7 RID: 9175
	public int currentValue;

	// Token: 0x040023D8 RID: 9176
	public int maxValue;

	// Token: 0x040023D9 RID: 9177
	public Rect mouseRect;

	// Token: 0x040023DA RID: 9178
	public Rect leftEdgeRectForClickSwitch;

	// Token: 0x040023DB RID: 9179
	public Rect rightEdgeRectForClickSwitch;

	// Token: 0x040023DC RID: 9180
	public Matrix4x4 matrix = Matrix4x4.identity;

	// Token: 0x040023DD RID: 9181
	private bool touched;

	// Token: 0x040023DE RID: 9182
	private int[] fingerStartArea = new int[5];

	// Token: 0x040023DF RID: 9183
	private int mouseStartArea;

	// Token: 0x040023E0 RID: 9184
	public float smoothValue;

	// Token: 0x040023E1 RID: 9185
	private float smoothStartPos;

	// Token: 0x040023E2 RID: 9186
	private float smoothDragOffset = 0.2f;

	// Token: 0x040023E3 RID: 9187
	private float lastSmoothValue;

	// Token: 0x040023E4 RID: 9188
	private float[] prevSmoothValue = new float[5];

	// Token: 0x040023E5 RID: 9189
	private float realtimeStamp;

	// Token: 0x040023E6 RID: 9190
	private float xVelocity;

	// Token: 0x040023E7 RID: 9191
	public float maxSpeed = 20f;

	// Token: 0x040023E8 RID: 9192
	private Vector2 mStartPos;

	// Token: 0x040023E9 RID: 9193
	private Vector3 pos;

	// Token: 0x040023EA RID: 9194
	private Vector2 tPos;

	// Token: 0x040023EB RID: 9195
	public bool debug;
}
