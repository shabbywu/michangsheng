using System;

// Token: 0x020006E9 RID: 1769
public class MissionTemplate
{
	// Token: 0x06002C79 RID: 11385 RVA: 0x0015BC30 File Offset: 0x00159E30
	public MissionTemplate()
	{
		this.baboons = 0;
		this.fly_baboons = 0;
		this.boomerang_baboons = 0;
		this.gorilla = 0;
		this.fly_gorilla = 0;
		this.koplje_gorilla = 0;
		this.diamonds = 0;
		this.coins = 0;
		this.distance = 0;
		this.barrels = 0;
		this.red_diamonds = 0;
		this.blue_diamonds = 0;
		this.green_diamonds = 0;
		this.points = 0;
		this.level = string.Empty;
		this.description_en = string.Empty;
		this.description_us = string.Empty;
		this.description_es = string.Empty;
		this.description_ru = string.Empty;
		this.description_pt = string.Empty;
		this.description_pt_br = string.Empty;
		this.description_fr = string.Empty;
		this.description_tha = string.Empty;
		this.description_zh = string.Empty;
		this.description_tzh = string.Empty;
		this.description_ger = string.Empty;
		this.description_it = string.Empty;
		this.description_srb = string.Empty;
		this.description_tur = string.Empty;
		this.description_kor = string.Empty;
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x0015BE08 File Offset: 0x0015A008
	public string IspisiDescriptionNaIspravnomJeziku()
	{
		if (LanguageManager.chosenLanguage.Equals("_en"))
		{
			return this.description_en;
		}
		if (LanguageManager.chosenLanguage.Equals("_us"))
		{
			return this.description_us;
		}
		if (LanguageManager.chosenLanguage.Equals("_es"))
		{
			return this.description_es;
		}
		if (LanguageManager.chosenLanguage.Equals("_ru"))
		{
			return this.description_ru;
		}
		if (LanguageManager.chosenLanguage.Equals("_pt"))
		{
			return this.description_pt;
		}
		if (LanguageManager.chosenLanguage.Equals("_br"))
		{
			return this.description_pt_br;
		}
		if (LanguageManager.chosenLanguage.Equals("_fr"))
		{
			return this.description_fr;
		}
		if (LanguageManager.chosenLanguage.Equals("_th"))
		{
			return this.description_tha;
		}
		if (LanguageManager.chosenLanguage.Equals("_ch"))
		{
			return this.description_zh;
		}
		if (LanguageManager.chosenLanguage.Equals("_tch"))
		{
			return this.description_tzh;
		}
		if (LanguageManager.chosenLanguage.Equals("_de"))
		{
			return this.description_ger;
		}
		if (LanguageManager.chosenLanguage.Equals("_it"))
		{
			return this.description_it;
		}
		if (LanguageManager.chosenLanguage.Equals("_srb"))
		{
			return this.description_srb;
		}
		if (LanguageManager.chosenLanguage.Equals("_tr"))
		{
			return this.description_tur;
		}
		if (LanguageManager.chosenLanguage.Equals("_ko"))
		{
			return this.description_kor;
		}
		return string.Empty;
	}

	// Token: 0x040026D1 RID: 9937
	public int baboons;

	// Token: 0x040026D2 RID: 9938
	public int fly_baboons;

	// Token: 0x040026D3 RID: 9939
	public int boomerang_baboons;

	// Token: 0x040026D4 RID: 9940
	public int gorilla;

	// Token: 0x040026D5 RID: 9941
	public int fly_gorilla;

	// Token: 0x040026D6 RID: 9942
	public int koplje_gorilla;

	// Token: 0x040026D7 RID: 9943
	public int diamonds;

	// Token: 0x040026D8 RID: 9944
	public int coins;

	// Token: 0x040026D9 RID: 9945
	public int distance;

	// Token: 0x040026DA RID: 9946
	public int barrels;

	// Token: 0x040026DB RID: 9947
	public int red_diamonds;

	// Token: 0x040026DC RID: 9948
	public int blue_diamonds;

	// Token: 0x040026DD RID: 9949
	public int green_diamonds;

	// Token: 0x040026DE RID: 9950
	public int points;

	// Token: 0x040026DF RID: 9951
	public string level = string.Empty;

	// Token: 0x040026E0 RID: 9952
	public string description_en = string.Empty;

	// Token: 0x040026E1 RID: 9953
	public string description_us = string.Empty;

	// Token: 0x040026E2 RID: 9954
	public string description_es = string.Empty;

	// Token: 0x040026E3 RID: 9955
	public string description_ru = string.Empty;

	// Token: 0x040026E4 RID: 9956
	public string description_pt = string.Empty;

	// Token: 0x040026E5 RID: 9957
	public string description_pt_br = string.Empty;

	// Token: 0x040026E6 RID: 9958
	public string description_fr = string.Empty;

	// Token: 0x040026E7 RID: 9959
	public string description_tha = string.Empty;

	// Token: 0x040026E8 RID: 9960
	public string description_zh = string.Empty;

	// Token: 0x040026E9 RID: 9961
	public string description_tzh = string.Empty;

	// Token: 0x040026EA RID: 9962
	public string description_ger = string.Empty;

	// Token: 0x040026EB RID: 9963
	public string description_it = string.Empty;

	// Token: 0x040026EC RID: 9964
	public string description_srb = string.Empty;

	// Token: 0x040026ED RID: 9965
	public string description_tur = string.Empty;

	// Token: 0x040026EE RID: 9966
	public string description_kor = string.Empty;
}
