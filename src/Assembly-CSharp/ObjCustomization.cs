using System;
using UnityEngine;

// Token: 0x02000753 RID: 1875
public class ObjCustomization : MonoBehaviour
{
	// Token: 0x06002FBE RID: 12222 RVA: 0x000235F0 File Offset: 0x000217F0
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

	// Token: 0x06002FBF RID: 12223 RVA: 0x0017DD98 File Offset: 0x0017BF98
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

	// Token: 0x06002FC0 RID: 12224 RVA: 0x0017DEB0 File Offset: 0x0017C0B0
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

	// Token: 0x04002AEE RID: 10990
	public static bool Customization;

	// Token: 0x04002AEF RID: 10991
	public SwipeControlCustomization swipeCtrl;

	// Token: 0x04002AF0 RID: 10992
	public Transform[] obj = new Transform[0];

	// Token: 0x04002AF1 RID: 10993
	public float minXPos;

	// Token: 0x04002AF2 RID: 10994
	public float maxXPos = 115f;

	// Token: 0x04002AF3 RID: 10995
	private float xDist;

	// Token: 0x04002AF4 RID: 10996
	private float xDistFactor;

	// Token: 0x04002AF5 RID: 10997
	public static int HatsNumber = 8;

	// Token: 0x04002AF6 RID: 10998
	public static int ShirtsNumber = 8;

	// Token: 0x04002AF7 RID: 10999
	public static int BackBacksNumber = 8;

	// Token: 0x04002AF8 RID: 11000
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002AF9 RID: 11001
	public float xPosReal = -11f;

	// Token: 0x04002AFA RID: 11002
	private float rememberYPos;
}
