using System;
using UnityEngine;

// Token: 0x020004E4 RID: 1252
public class ObjPowerUps : MonoBehaviour
{
	// Token: 0x06002875 RID: 10357 RVA: 0x0013256B File Offset: 0x0013076B
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

	// Token: 0x06002876 RID: 10358 RVA: 0x001325AC File Offset: 0x001307AC
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

	// Token: 0x06002877 RID: 10359 RVA: 0x001326D4 File Offset: 0x001308D4
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

	// Token: 0x040023B3 RID: 9139
	public static bool PowerUps;

	// Token: 0x040023B4 RID: 9140
	public SwipeControlPowerUps swipeCtrl;

	// Token: 0x040023B5 RID: 9141
	public Transform[] obj = new Transform[0];

	// Token: 0x040023B6 RID: 9142
	public float minXPos;

	// Token: 0x040023B7 RID: 9143
	public float maxXPos = 115f;

	// Token: 0x040023B8 RID: 9144
	private float xDist;

	// Token: 0x040023B9 RID: 9145
	private float xDistFactor;

	// Token: 0x040023BA RID: 9146
	private float swipeSmoothFactor = 1f;

	// Token: 0x040023BB RID: 9147
	public float xPosReal = -11f;

	// Token: 0x040023BC RID: 9148
	private float rememberYPos;
}
