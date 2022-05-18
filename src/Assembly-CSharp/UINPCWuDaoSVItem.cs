using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WXB;

// Token: 0x020003A5 RID: 933
public class UINPCWuDaoSVItem : MonoBehaviour
{
	// Token: 0x060019FE RID: 6654 RVA: 0x00016512 File Offset: 0x00014712
	private void Awake()
	{
		this.RT = (base.transform as RectTransform);
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000E5C58 File Offset: 0x000E3E58
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

	// Token: 0x06001A00 RID: 6656 RVA: 0x000E5D80 File Offset: 0x000E3F80
	private void Update()
	{
		if (this.RT.sizeDelta.y != 80f + this.SkillText.preferredHeight)
		{
			this.RT.sizeDelta = new Vector2(this.RT.sizeDelta.x, 80f + this.SkillText.preferredHeight);
		}
	}

	// Token: 0x0400154F RID: 5455
	public Image TypeImage;

	// Token: 0x04001550 RID: 5456
	public Image HengTiaoImage;

	// Token: 0x04001551 RID: 5457
	public Text LevelText;

	// Token: 0x04001552 RID: 5458
	public SymbolText SkillText;

	// Token: 0x04001553 RID: 5459
	private RectTransform RT;

	// Token: 0x04001554 RID: 5460
	private bool hasSkill;

	// Token: 0x04001555 RID: 5461
	public List<Sprite> WuDaoTypeSprites = new List<Sprite>();

	// Token: 0x04001556 RID: 5462
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
