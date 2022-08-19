using System;
using UnityEngine;

// Token: 0x020004DD RID: 1245
public class ObjCustomization : MonoBehaviour
{
	// Token: 0x06002855 RID: 10325 RVA: 0x00131561 File Offset: 0x0012F761
	private void Awake()
	{
		ObjCustomization.Customization = false;
		if (Application.loadedLevel != 1)
		{
			this.minXPos -= 94.2f;
			this.maxXPos -= 94.2f;
			this.xPosReal = -33.5f;
		}
	}

	// Token: 0x06002856 RID: 10326 RVA: 0x001315A0 File Offset: 0x0012F7A0
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!this.swipeCtrl)
		{
			this.swipeCtrl = base.gameObject.AddComponent<SwipeControlCustomization>();
		}
		this.swipeCtrl.skipAutoSetup = true;
		this.swipeCtrl.clickEdgeToSwitch = false;
		this.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height));
		this.swipeCtrl.maxValue = this.obj.Length - 1;
		this.swipeCtrl.currentValue = 0;
		this.swipeCtrl.startValue = 0;
		this.swipeCtrl.partWidth = (float)(Screen.width / this.swipeCtrl.maxValue);
		this.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)this.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002857 RID: 10327 RVA: 0x001316B8 File Offset: 0x0012F8B8
	private void Update()
	{
		if (ObjCustomization.Customization)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.xPosReal - Mathf.Clamp(Mathf.Abs((float)i - this.swipeCtrl.smoothValue), 0f, 1f), this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) + this.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
				if (ShopManagerFull.AktivanCustomizationTab == 1)
				{
					ShopManagerFull.AktivanItemSesir = this.swipeCtrl.currentValue;
				}
				else if (ShopManagerFull.AktivanCustomizationTab == 2)
				{
					ShopManagerFull.AktivanItemMajica = this.swipeCtrl.currentValue;
				}
				else if (ShopManagerFull.AktivanCustomizationTab == 3)
				{
					ShopManagerFull.AktivanItemRanac = this.swipeCtrl.currentValue;
				}
			}
		}
	}

	// Token: 0x04002360 RID: 9056
	public static bool Customization;

	// Token: 0x04002361 RID: 9057
	public SwipeControlCustomization swipeCtrl;

	// Token: 0x04002362 RID: 9058
	public Transform[] obj = new Transform[0];

	// Token: 0x04002363 RID: 9059
	public float minXPos;

	// Token: 0x04002364 RID: 9060
	public float maxXPos = 115f;

	// Token: 0x04002365 RID: 9061
	private float xDist;

	// Token: 0x04002366 RID: 9062
	private float xDistFactor;

	// Token: 0x04002367 RID: 9063
	public static int HatsNumber = 8;

	// Token: 0x04002368 RID: 9064
	public static int ShirtsNumber = 8;

	// Token: 0x04002369 RID: 9065
	public static int BackBacksNumber = 8;

	// Token: 0x0400236A RID: 9066
	private float swipeSmoothFactor = 1f;

	// Token: 0x0400236B RID: 9067
	public float xPosReal = -11f;

	// Token: 0x0400236C RID: 9068
	private float rememberYPos;
}
