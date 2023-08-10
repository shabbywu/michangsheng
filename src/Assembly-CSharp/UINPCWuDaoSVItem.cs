using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WXB;

public class UINPCWuDaoSVItem : MonoBehaviour
{
	public Image TypeImage;

	public Image HengTiaoImage;

	public Text LevelText;

	public SymbolText SkillText;

	private RectTransform RT;

	private bool hasSkill;

	public List<Sprite> WuDaoTypeSprites = new List<Sprite>();

	private static List<string> LevelName = new List<string> { "一窍不通", "初窥门径", "略有小成", "融会贯通", "道之真境", "大道已成" };

	private void Awake()
	{
		ref RectTransform rT = ref RT;
		Transform transform = ((Component)this).transform;
		rT = (RectTransform)(object)((transform is RectTransform) ? transform : null);
	}

	public void SetWuDao(UINPCWuDaoData data)
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		LevelText.text = LevelName[data.Level];
		if (data.ID > 10)
		{
			TypeImage.sprite = WuDaoTypeSprites[data.ID - 11];
		}
		else
		{
			TypeImage.sprite = WuDaoTypeSprites[data.ID - 1];
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (int skillID in data.SkillIDList)
		{
			UIWuDaoSkillData uIWuDaoSkillData = UINPCData._WuDaoSkillDict[skillID];
			stringBuilder.AppendLine("#s34#cb47a39" + uIWuDaoSkillData.Name + " #n" + uIWuDaoSkillData.Desc);
			hasSkill = true;
		}
		if (!hasSkill)
		{
			((Graphic)HengTiaoImage).color = new Color(1f, 1f, 1f, 0.5f);
		}
		((Text)SkillText).text = stringBuilder.ToString();
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (RT.sizeDelta.y != 80f + ((Text)SkillText).preferredHeight)
		{
			RT.sizeDelta = new Vector2(RT.sizeDelta.x, 80f + ((Text)SkillText).preferredHeight);
		}
	}
}
