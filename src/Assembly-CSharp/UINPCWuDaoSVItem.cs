using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WXB;

// Token: 0x0200027E RID: 638
public class UINPCWuDaoSVItem : MonoBehaviour
{
	// Token: 0x0600172E RID: 5934 RVA: 0x0009E3F8 File Offset: 0x0009C5F8
	private void Awake()
	{
		this.RT = (base.transform as RectTransform);
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x0009E40C File Offset: 0x0009C60C
	public void SetWuDao(UINPCWuDaoData data)
	{
		this.LevelText.text = UINPCWuDaoSVItem.LevelName[data.Level];
		if (data.ID > 10)
		{
			this.TypeImage.sprite = this.WuDaoTypeSprites[data.ID - 11];
		}
		else
		{
			this.TypeImage.sprite = this.WuDaoTypeSprites[data.ID - 1];
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (int key in data.SkillIDList)
		{
			UIWuDaoSkillData uiwuDaoSkillData = UINPCData._WuDaoSkillDict[key];
			stringBuilder.AppendLine("#s34#cb47a39" + uiwuDaoSkillData.Name + " #n" + uiwuDaoSkillData.Desc);
			this.hasSkill = true;
		}
		if (!this.hasSkill)
		{
			this.HengTiaoImage.color = new Color(1f, 1f, 1f, 0.5f);
		}
		this.SkillText.text = stringBuilder.ToString();
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x0009E534 File Offset: 0x0009C734
	private void Update()
	{
		if (this.RT.sizeDelta.y != 80f + this.SkillText.preferredHeight)
		{
			this.RT.sizeDelta = new Vector2(this.RT.sizeDelta.x, 80f + this.SkillText.preferredHeight);
		}
	}

	// Token: 0x040011DC RID: 4572
	public Image TypeImage;

	// Token: 0x040011DD RID: 4573
	public Image HengTiaoImage;

	// Token: 0x040011DE RID: 4574
	public Text LevelText;

	// Token: 0x040011DF RID: 4575
	public SymbolText SkillText;

	// Token: 0x040011E0 RID: 4576
	private RectTransform RT;

	// Token: 0x040011E1 RID: 4577
	private bool hasSkill;

	// Token: 0x040011E2 RID: 4578
	public List<Sprite> WuDaoTypeSprites = new List<Sprite>();

	// Token: 0x040011E3 RID: 4579
	private static List<string> LevelName = new List<string>
	{
		"一窍不通",
		"初窥门径",
		"略有小成",
		"融会贯通",
		"道之真境",
		"大道已成"
	};
}
