using System;
using UnityEngine;

// Token: 0x020004E2 RID: 1250
public class ObjLanguages : MonoBehaviour
{
	// Token: 0x0600286D RID: 10349 RVA: 0x001321A7 File Offset: 0x001303A7
	private void Awake()
	{
		ObjLanguages.Languages = false;
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x001321B0 File Offset: 0x001303B0
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!this.swipeCtrl)
		{
			this.swipeCtrl = base.gameObject.AddComponent<SwipeControlLanguages>();
		}
		this.swipeCtrl.skipAutoSetup = true;
		this.swipeCtrl.clickEdgeToSwitch = false;
		this.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		this.swipeCtrl.maxValue = this.obj.Length - 1;
		this.swipeCtrl.currentValue = this.obj.Length - 2;
		this.swipeCtrl.startValue = this.obj.Length - 2;
		this.swipeCtrl.partWidth = (float)(Screen.width / this.swipeCtrl.maxValue);
		this.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)this.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x001322D8 File Offset: 0x001304D8
	private void Update()
	{
		if (ObjLanguages.Languages)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - this.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x040023A1 RID: 9121
	public static bool Languages;

	// Token: 0x040023A2 RID: 9122
	public SwipeControlLanguages swipeCtrl;

	// Token: 0x040023A3 RID: 9123
	public Transform[] obj = new Transform[0];

	// Token: 0x040023A4 RID: 9124
	public float minXPos;

	// Token: 0x040023A5 RID: 9125
	public float maxXPos = 115f;

	// Token: 0x040023A6 RID: 9126
	private float xDist;

	// Token: 0x040023A7 RID: 9127
	private float xDistFactor;

	// Token: 0x040023A8 RID: 9128
	private float swipeSmoothFactor = 1f;

	// Token: 0x040023A9 RID: 9129
	private float rememberYPos;
}
