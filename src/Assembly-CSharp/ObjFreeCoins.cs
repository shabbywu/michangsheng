using System;
using UnityEngine;

// Token: 0x020004E1 RID: 1249
public class ObjFreeCoins : MonoBehaviour
{
	// Token: 0x06002869 RID: 10345 RVA: 0x00131F7B File Offset: 0x0013017B
	private void Awake()
	{
		ObjFreeCoins.FreeCoins = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x00131FBC File Offset: 0x001301BC
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!this.swipeCtrl)
		{
			this.swipeCtrl = base.gameObject.AddComponent<SwipeControlFreeCoins>();
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

	// Token: 0x0600286B RID: 10347 RVA: 0x001320E4 File Offset: 0x001302E4
	private void Update()
	{
		if (ObjFreeCoins.FreeCoins)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - this.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x04002397 RID: 9111
	public static bool FreeCoins;

	// Token: 0x04002398 RID: 9112
	public SwipeControlFreeCoins swipeCtrl;

	// Token: 0x04002399 RID: 9113
	public Transform[] obj = new Transform[0];

	// Token: 0x0400239A RID: 9114
	public float minXPos;

	// Token: 0x0400239B RID: 9115
	public float maxXPos = 115f;

	// Token: 0x0400239C RID: 9116
	private float xDist;

	// Token: 0x0400239D RID: 9117
	private float xDistFactor;

	// Token: 0x0400239E RID: 9118
	private float swipeSmoothFactor = 1f;

	// Token: 0x0400239F RID: 9119
	public float xPosReal = -11f;

	// Token: 0x040023A0 RID: 9120
	private float rememberYPos;
}
