using System;
using UnityEngine;

// Token: 0x0200053A RID: 1338
public class PlayTutorialCircle : MonoBehaviour
{
	// Token: 0x0600222C RID: 8748 RVA: 0x0001C084 File Offset: 0x0001A284
	private void Awake()
	{
		PlayTutorialCircle.Inst = this;
	}

	// Token: 0x0600222D RID: 8749 RVA: 0x0001C08C File Offset: 0x0001A28C
	public void SetShow(bool show)
	{
		this.BG.SetActive(show);
		this.Hand.SetActive(show);
	}

	// Token: 0x04001D94 RID: 7572
	public static PlayTutorialCircle Inst;

	// Token: 0x04001D95 RID: 7573
	public GameObject BG;

	// Token: 0x04001D96 RID: 7574
	public GameObject Hand;
}
