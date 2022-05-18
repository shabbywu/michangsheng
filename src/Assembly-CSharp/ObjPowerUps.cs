using System;
using UnityEngine;

// Token: 0x0200075A RID: 1882
public class ObjPowerUps : MonoBehaviour
{
	// Token: 0x06002FDE RID: 12254 RVA: 0x000238E8 File Offset: 0x00021AE8
	private void Awake()
	{
		ObjPowerUps.PowerUps = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x06002FDF RID: 12255 RVA: 0x0017EAB0 File Offset: 0x0017CCB0
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!this.swipeCtrl)
		{
			this.swipeCtrl = base.gameObject.AddComponent<SwipeControlPowerUps>();
		}
		this.swipeCtrl.skipAutoSetup = true;
		this.swipeCtrl.clickEdgeToSwitch = false;
		this.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		this.swipeCtrl.maxValue = this.obj.Length - 1;
		this.swipeCtrl.currentValue = this.obj.Length - 1;
		this.swipeCtrl.startValue = this.obj.Length - 1;
		this.swipeCtrl.partWidth = (float)(Screen.width / this.swipeCtrl.maxValue);
		this.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)this.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002FE0 RID: 12256 RVA: 0x0017EBD8 File Offset: 0x0017CDD8
	private void Update()
	{
		if (ObjPowerUps.PowerUps)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - this.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x04002B41 RID: 11073
	public static bool PowerUps;

	// Token: 0x04002B42 RID: 11074
	public SwipeControlPowerUps swipeCtrl;

	// Token: 0x04002B43 RID: 11075
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B44 RID: 11076
	public float minXPos;

	// Token: 0x04002B45 RID: 11077
	public float maxXPos = 115f;

	// Token: 0x04002B46 RID: 11078
	private float xDist;

	// Token: 0x04002B47 RID: 11079
	private float xDistFactor;

	// Token: 0x04002B48 RID: 11080
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B49 RID: 11081
	public float xPosReal = -11f;

	// Token: 0x04002B4A RID: 11082
	private float rememberYPos;
}
