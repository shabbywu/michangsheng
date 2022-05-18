using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x0200050D RID: 1293
public class LingGanMag : MonoBehaviour
{
	// Token: 0x06002154 RID: 8532 RVA: 0x00116390 File Offset: 0x00114590
	private void Awake()
	{
		LingGanMag.inst = this;
		this.colors = new List<string>();
		this.colors.Add("8df1ec");
		this.colors.Add("64c97a");
		this.colors.Add("e9e4a4");
		this.colors.Add("aaab96");
	}

	// Token: 0x06002155 RID: 8533 RVA: 0x001163F0 File Offset: 0x001145F0
	public void UpdateData()
	{
		Avatar player = Tools.instance.getPlayer();
		this.curState.text = "[" + this.colors[player.LunDaoState - 1] + "]" + jsonData.instance.LunDaoStateData[player.LunDaoState.ToString()]["ZhuangTaiInfo"].Str;
		this.num.text = string.Format("{0}/{1}", player.LingGan, player.GetLingGanMax());
		this.sprite.sprite2D = this.sprites[player.LunDaoState - 1];
	}

	// Token: 0x04001CD9 RID: 7385
	public static LingGanMag inst;

	// Token: 0x04001CDA RID: 7386
	[SerializeField]
	private List<Sprite> sprites;

	// Token: 0x04001CDB RID: 7387
	private List<string> colors;

	// Token: 0x04001CDC RID: 7388
	public UILabel curState;

	// Token: 0x04001CDD RID: 7389
	public UI2DSprite sprite;

	// Token: 0x04001CDE RID: 7390
	public UILabel num;
}
