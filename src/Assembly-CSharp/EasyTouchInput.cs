using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public class EasyTouchInput
{
	// Token: 0x06000F35 RID: 3893 RVA: 0x0000F878 File Offset: 0x0000DA78
	public int TouchCount()
	{
		return this.getTouchCount(false);
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x000A10C8 File Offset: 0x0009F2C8
	private int getTouchCount(bool realTouch)
	{
		int result = 0;
		if (realTouch || EasyTouch.instance.enableRemote)
		{
			result = Input.touchCount;
		}
		else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
		{
			result = 1;
			if (Input.GetKey(308) || Input.GetKey(EasyTouch.instance.twistKey) || Input.GetKey(306) || Input.GetKey(EasyTouch.instance.swipeKey))
			{
				result = 2;
			}
			if (Input.GetKeyUp(308) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp(306) || Input.GetKeyUp(EasyTouch.instance.swipeKey))
			{
				result = 2;
			}
		}
		return result;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x000A117C File Offset: 0x0009F37C
	public Finger GetMouseTouch(int fingerIndex, Finger myFinger)
	{
		Finger finger;
		if (myFinger != null)
		{
			finger = myFinger;
		}
		else
		{
			finger = new Finger();
			finger.gesture = EasyTouch.GestureType.None;
		}
		if (fingerIndex == 1 && (Input.GetKeyUp(308) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp(306) || Input.GetKeyUp(EasyTouch.instance.swipeKey)))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = this.oldFinger2Position;
			finger.deltaPosition = finger.position - this.oldFinger2Position;
			finger.tapCount = this.tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
			finger.phase = 3;
			return finger;
		}
		if (Input.GetMouseButton(0))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = this.GetPointerPosition(fingerIndex);
			if ((double)(Time.realtimeSinceStartup - this.tapeTime[fingerIndex]) > 0.5)
			{
				this.tapCount[fingerIndex] = 0;
			}
			if (Input.GetMouseButtonDown(0) || (fingerIndex == 1 && (Input.GetKeyDown(308) || Input.GetKeyDown(EasyTouch.instance.twistKey) || Input.GetKeyDown(306) || Input.GetKeyDown(EasyTouch.instance.swipeKey))))
			{
				finger.position = this.GetPointerPosition(fingerIndex);
				finger.deltaPosition = Vector2.zero;
				this.tapCount[fingerIndex] = this.tapCount[fingerIndex] + 1;
				finger.tapCount = this.tapCount[fingerIndex];
				this.startActionTime[fingerIndex] = Time.realtimeSinceStartup;
				this.deltaTime[fingerIndex] = this.startActionTime[fingerIndex];
				finger.deltaTime = 0f;
				finger.phase = 0;
				if (fingerIndex == 1)
				{
					this.oldFinger2Position = finger.position;
				}
				else
				{
					this.oldMousePosition[fingerIndex] = finger.position;
				}
				if (this.tapCount[fingerIndex] == 1)
				{
					this.tapeTime[fingerIndex] = Time.realtimeSinceStartup;
				}
				return finger;
			}
			finger.deltaPosition = finger.position - this.oldMousePosition[fingerIndex];
			finger.tapCount = this.tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
			if (finger.deltaPosition.sqrMagnitude < 1f)
			{
				finger.phase = 2;
			}
			else
			{
				finger.phase = 1;
			}
			this.oldMousePosition[fingerIndex] = finger.position;
			this.deltaTime[fingerIndex] = Time.realtimeSinceStartup;
			return finger;
		}
		else
		{
			if (Input.GetMouseButtonUp(0))
			{
				finger.fingerIndex = fingerIndex;
				finger.position = this.GetPointerPosition(fingerIndex);
				finger.deltaPosition = finger.position - this.oldMousePosition[fingerIndex];
				finger.tapCount = this.tapCount[fingerIndex];
				finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
				finger.phase = 3;
				this.oldMousePosition[fingerIndex] = finger.position;
				return finger;
			}
			return null;
		}
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x000A1460 File Offset: 0x0009F660
	public Vector2 GetSecondFingerPosition()
	{
		Vector2 result;
		result..ctor(-1f, -1f);
		if ((Input.GetKey(308) || Input.GetKey(EasyTouch.instance.twistKey)) && (Input.GetKey(306) || Input.GetKey(EasyTouch.instance.swipeKey)))
		{
			if (!this.bComplex)
			{
				this.bComplex = true;
				this.deltaFingerPosition = Input.mousePosition - this.oldFinger2Position;
			}
			result = this.GetComplex2finger();
			return result;
		}
		if (Input.GetKey(308) || Input.GetKey(EasyTouch.instance.twistKey))
		{
			result = this.GetPinchTwist2Finger();
			this.bComplex = false;
			return result;
		}
		if (Input.GetKey(306) || Input.GetKey(EasyTouch.instance.swipeKey))
		{
			result = this.GetComplex2finger();
			this.bComplex = false;
			return result;
		}
		return result;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0000F881 File Offset: 0x0000DA81
	private Vector2 GetPointerPosition(int index)
	{
		if (index == 0)
		{
			return Input.mousePosition;
		}
		return this.GetSecondFingerPosition();
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x000A1548 File Offset: 0x0009F748
	private Vector2 GetPinchTwist2Finger()
	{
		Vector2 result;
		if (this.complexCenter == Vector2.zero)
		{
			result.x = (float)Screen.width / 2f - (Input.mousePosition.x - (float)Screen.width / 2f);
			result.y = (float)Screen.height / 2f - (Input.mousePosition.y - (float)Screen.height / 2f);
		}
		else
		{
			result.x = this.complexCenter.x - (Input.mousePosition.x - this.complexCenter.x);
			result.y = this.complexCenter.y - (Input.mousePosition.y - this.complexCenter.y);
		}
		this.oldFinger2Position = result;
		return result;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000A161C File Offset: 0x0009F81C
	private Vector2 GetComplex2finger()
	{
		Vector2 vector;
		vector.x = Input.mousePosition.x - this.deltaFingerPosition.x;
		vector.y = Input.mousePosition.y - this.deltaFingerPosition.y;
		this.complexCenter = new Vector2((Input.mousePosition.x + vector.x) / 2f, (Input.mousePosition.y + vector.y) / 2f);
		this.oldFinger2Position = vector;
		return vector;
	}

	// Token: 0x04000BCD RID: 3021
	private Vector2[] oldMousePosition = new Vector2[2];

	// Token: 0x04000BCE RID: 3022
	private int[] tapCount = new int[2];

	// Token: 0x04000BCF RID: 3023
	private float[] startActionTime = new float[2];

	// Token: 0x04000BD0 RID: 3024
	private float[] deltaTime = new float[2];

	// Token: 0x04000BD1 RID: 3025
	private float[] tapeTime = new float[2];

	// Token: 0x04000BD2 RID: 3026
	private bool bComplex;

	// Token: 0x04000BD3 RID: 3027
	private Vector2 deltaFingerPosition;

	// Token: 0x04000BD4 RID: 3028
	private Vector2 oldFinger2Position;

	// Token: 0x04000BD5 RID: 3029
	private Vector2 complexCenter;
}
