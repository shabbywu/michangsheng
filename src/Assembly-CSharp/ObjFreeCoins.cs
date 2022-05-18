using System;
using UnityEngine;

// Token: 0x02000757 RID: 1879
public class ObjFreeCoins : MonoBehaviour
{
	// Token: 0x06002FD2 RID: 12242 RVA: 0x00023810 File Offset: 0x00021A10
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

	// Token: 0x06002FD3 RID: 12243 RVA: 0x0017E594 File Offset: 0x0017C794
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

	// Token: 0x06002FD4 RID: 12244 RVA: 0x0017E6BC File Offset: 0x0017C8BC
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

	// Token: 0x04002B25 RID: 11045
	public static bool FreeCoins;

	// Token: 0x04002B26 RID: 11046
	public SwipeControlFreeCoins swipeCtrl;

	// Token: 0x04002B27 RID: 11047
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B28 RID: 11048
	public float minXPos;

	// Token: 0x04002B29 RID: 11049
	public float maxXPos = 115f;

	// Token: 0x04002B2A RID: 11050
	private float xDist;

	// Token: 0x04002B2B RID: 11051
	private float xDistFactor;

	// Token: 0x04002B2C RID: 11052
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B2D RID: 11053
	public float xPosReal = -11f;

	// Token: 0x04002B2E RID: 11054
	private float rememberYPos;
}
