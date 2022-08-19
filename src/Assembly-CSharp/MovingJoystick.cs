using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class MovingJoystick
{
	// Token: 0x06000D74 RID: 3444 RVA: 0x00050B50 File Offset: 0x0004ED50
	public float Axis2Angle(bool inDegree = true)
	{
		float num = Mathf.Atan2(this.joystickAxis.x, this.joystickAxis.y);
		if (inDegree)
		{
			return num * 57.29578f;
		}
		return num;
	}

	// Token: 0x04000976 RID: 2422
	public string joystickName;

	// Token: 0x04000977 RID: 2423
	public Vector2 joystickAxis;

	// Token: 0x04000978 RID: 2424
	public Vector2 joystickValue;

	// Token: 0x04000979 RID: 2425
	public EasyJoystick joystick;
}
