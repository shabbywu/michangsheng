using System;
using UnityEngine;

// Token: 0x02000759 RID: 1881
public class ObjLeaderboard : MonoBehaviour
{
	// Token: 0x06002FDA RID: 12250 RVA: 0x000238B6 File Offset: 0x00021AB6
	private void Awake()
	{
		ObjLeaderboard.Leaderboard = false;
	}

	// Token: 0x06002FDB RID: 12251 RVA: 0x0017E904 File Offset: 0x0017CB04
	private void Start()
	{
		this.xDist = this.maxXPos - this.minXPos;
		this.xDistFactor = 1f / this.xDist;
		if (!ObjLeaderboard.swipeCtrl)
		{
			ObjLeaderboard.swipeCtrl = base.gameObject.AddComponent<SwipeControlLeaderboard>();
		}
		ObjLeaderboard.swipeCtrl.skipAutoSetup = true;
		ObjLeaderboard.swipeCtrl.clickEdgeToSwitch = false;
		ObjLeaderboard.swipeCtrl.SetMouseRect(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		ObjLeaderboard.swipeCtrl.maxValue = this.obj.Length - 1;
		ObjLeaderboard.swipeCtrl.currentValue = this.obj.Length - 2;
		ObjLeaderboard.swipeCtrl.startValue = this.obj.Length - 2;
		ObjLeaderboard.swipeCtrl.partWidth = (float)(Screen.width / ObjLeaderboard.swipeCtrl.maxValue);
		ObjLeaderboard.swipeCtrl.Setup();
		this.swipeSmoothFactor = 1f / (float)ObjLeaderboard.swipeCtrl.maxValue;
		this.rememberYPos = this.obj[0].position.y;
	}

	// Token: 0x06002FDC RID: 12252 RVA: 0x0017EA20 File Offset: 0x0017CC20
	private void Update()
	{
		if (ObjLeaderboard.Leaderboard)
		{
			for (int i = 0; i < this.obj.Length; i++)
			{
				this.obj[i].position = new Vector3(this.obj[i].position.x, this.minXPos - (float)i * (this.xDist * this.swipeSmoothFactor) - ObjLeaderboard.swipeCtrl.smoothValue * this.swipeSmoothFactor * this.xDist, this.obj[i].position.z);
			}
		}
	}

	// Token: 0x04002B38 RID: 11064
	public static bool Leaderboard;

	// Token: 0x04002B39 RID: 11065
	public static SwipeControlLeaderboard swipeCtrl;

	// Token: 0x04002B3A RID: 11066
	public Transform[] obj = new Transform[0];

	// Token: 0x04002B3B RID: 11067
	public float minXPos;

	// Token: 0x04002B3C RID: 11068
	public float maxXPos = 55f;

	// Token: 0x04002B3D RID: 11069
	private float xDist;

	// Token: 0x04002B3E RID: 11070
	private float xDistFactor;

	// Token: 0x04002B3F RID: 11071
	private float swipeSmoothFactor = 1f;

	// Token: 0x04002B40 RID: 11072
	private float rememberYPos;
}
