using System;
using UnityEngine;

// Token: 0x02000758 RID: 1880
public class ObjLanguages : MonoBehaviour
{
	// Token: 0x06002FD6 RID: 12246 RVA: 0x00023884 File Offset: 0x00021A84
	private void Awake()
	{
		ObjLanguages.Languages = false;
	}

	// Token: 0x06002FD7 RID: 12247 RVA: 0x0017E74C File Offset: 0x0017C94C
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

	// Token: 0x06002FD8 RID: 12248 RVA: 0x0017E874 File Offset: 0x0017CA74
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

	// Token: 0x04002B2F RID: 11055
	public static bool Languages;

	// Token: 0x04002B30 RID: 11056
	public SwipeControlLanguages swipeCtrl;

	// Token: 0x04002B31 RID: 11057
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B32 RID: 11058
	public float minXPos;

	// Token: 0x04002B33 RID: 11059
	public float maxXPos = 115f;

	// Token: 0x04002B34 RID: 11060
	private float xDist;

	// Token: 0x04002B35 RID: 11061
	private float xDistFactor;

	// Token: 0x04002B36 RID: 11062
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B37 RID: 11063
	private float rememberYPos;
}
