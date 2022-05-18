using System;
using UnityEngine;

// Token: 0x0200075C RID: 1884
public class ObjShop : MonoBehaviour
{
	// Token: 0x06002FE6 RID: 12262 RVA: 0x0002398E File Offset: 0x00021B8E
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

	// Token: 0x06002FE7 RID: 12263 RVA: 0x0017EE20 File Offset: 0x0017D020
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

	// Token: 0x06002FE8 RID: 12264 RVA: 0x0017EF48 File Offset: 0x0017D148
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

	// Token: 0x04002B54 RID: 11092
	public static bool Shop;

	// Token: 0x04002B55 RID: 11093
	public SwipeControlShop swipeCtrl;

	// Token: 0x04002B56 RID: 11094
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B57 RID: 11095
	public float minXPos;

	// Token: 0x04002B58 RID: 11096
	public float maxXPos = 115f;

	// Token: 0x04002B59 RID: 11097
	private float xDist;

	// Token: 0x04002B5A RID: 11098
	private float xDistFactor;

	// Token: 0x04002B5B RID: 11099
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B5C RID: 11100
	public float xPosReal = -11f;

	// Token: 0x04002B5D RID: 11101
	private float rememberYPos;
}
