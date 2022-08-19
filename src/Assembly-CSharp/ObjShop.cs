using System;
using UnityEngine;

// Token: 0x020004E6 RID: 1254
public class ObjShop : MonoBehaviour
{
	// Token: 0x0600287D RID: 10365 RVA: 0x00132980 File Offset: 0x00130B80
	private void Awake()
	{
		ObjShop.Shop = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x001329C0 File Offset: 0x00130BC0
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!this.swipeCtrl)
		{
			this.swipeCtrl = base.gameObject.AddComponent<SwipeControlShop>();
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

	// Token: 0x0600287F RID: 10367 RVA: 0x00132AE8 File Offset: 0x00130CE8
	private void Update()
	{
		if (ObjShop.Shop)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - this.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x040023C6 RID: 9158
	public static bool Shop;

	// Token: 0x040023C7 RID: 9159
	public SwipeControlShop swipeCtrl;

	// Token: 0x040023C8 RID: 9160
	public Transform[] obj = new Transform[0];

	// Token: 0x040023C9 RID: 9161
	public float minXPos;

	// Token: 0x040023CA RID: 9162
	public float maxXPos = 115f;

	// Token: 0x040023CB RID: 9163
	private float xDist;

	// Token: 0x040023CC RID: 9164
	private float xDistFactor;

	// Token: 0x040023CD RID: 9165
	private float swipeSmoothFactor = 1f;

	// Token: 0x040023CE RID: 9166
	public float xPosReal = -11f;

	// Token: 0x040023CF RID: 9167
	private float rememberYPos;
}
