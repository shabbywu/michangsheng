using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class LingGanMag : MonoBehaviour
{
	// Token: 0x06001DDB RID: 7643 RVA: 0x000D26B4 File Offset: 0x000D08B4
	private void Awake()
	{
		LingGanMag.inst = this;
		this.colors = new List<string>();
		this.colors.Add("8df1ec");
		this.colors.Add("64c97a");
		this.colors.Add("e9e4a4");
		this.colors.Add("aaab96");
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x000D2714 File Offset: 0x000D0914
	public void UpdateData()
	{
		Avatar player = Tools.instance.getPlayer();
		this.curState.text = "[" + this.colors[player.LunDaoState - 1] + "]" + jsonData.instance.LunDaoStateData[player.LunDaoState.ToString()]["ZhuangTaiInfo"].Str;
		this.num.text = string.Format("{0}/{1}", player.LingGan, player.GetLingGanMax());
		this.sprite.sprite2D = this.sprites[player.LunDaoState - 1];
	}

	// Token: 0x0400187B RID: 6267
	public static LingGanMag inst;

	// Token: 0x0400187C RID: 6268
	[SerializeField]
	private List<Sprite> sprites;

	// Token: 0x0400187D RID: 6269
	private List<string> colors;

	// Token: 0x0400187E RID: 6270
	public UILabel curState;

	// Token: 0x0400187F RID: 6271
	public UI2DSprite sprite;

	// Token: 0x04001880 RID: 6272
	public UILabel num;
}
