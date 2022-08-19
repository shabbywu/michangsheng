using System;

// Token: 0x020004C3 RID: 1219
public class MissionTemplate
{
	// Token: 0x060026D3 RID: 9939 RVA: 0x001154B8 File Offset: 0x001136B8
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

	// Token: 0x060026D4 RID: 9940 RVA: 0x00115690 File Offset: 0x00113890
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

	// Token: 0x04002099 RID: 8345
	public int baboons;

	// Token: 0x0400209A RID: 8346
	public int fly_baboons;

	// Token: 0x0400209B RID: 8347
	public int boomerang_baboons;

	// Token: 0x0400209C RID: 8348
	public int gorilla;

	// Token: 0x0400209D RID: 8349
	public int fly_gorilla;

	// Token: 0x0400209E RID: 8350
	public int koplje_gorilla;

	// Token: 0x0400209F RID: 8351
	public int diamonds;

	// Token: 0x040020A0 RID: 8352
	public int coins;

	// Token: 0x040020A1 RID: 8353
	public int distance;

	// Token: 0x040020A2 RID: 8354
	public int barrels;

	// Token: 0x040020A3 RID: 8355
	public int red_diamonds;

	// Token: 0x040020A4 RID: 8356
	public int blue_diamonds;

	// Token: 0x040020A5 RID: 8357
	public int green_diamonds;

	// Token: 0x040020A6 RID: 8358
	public int points;

	// Token: 0x040020A7 RID: 8359
	public string level = string.Empty;

	// Token: 0x040020A8 RID: 8360
	public string description_en = string.Empty;

	// Token: 0x040020A9 RID: 8361
	public string description_us = string.Empty;

	// Token: 0x040020AA RID: 8362
	public string description_es = string.Empty;

	// Token: 0x040020AB RID: 8363
	public string description_ru = string.Empty;

	// Token: 0x040020AC RID: 8364
	public string description_pt = string.Empty;

	// Token: 0x040020AD RID: 8365
	public string description_pt_br = string.Empty;

	// Token: 0x040020AE RID: 8366
	public string description_fr = string.Empty;

	// Token: 0x040020AF RID: 8367
	public string description_tha = string.Empty;

	// Token: 0x040020B0 RID: 8368
	public string description_zh = string.Empty;

	// Token: 0x040020B1 RID: 8369
	public string description_tzh = string.Empty;

	// Token: 0x040020B2 RID: 8370
	public string description_ger = string.Empty;

	// Token: 0x040020B3 RID: 8371
	public string description_it = string.Empty;

	// Token: 0x040020B4 RID: 8372
	public string description_srb = string.Empty;

	// Token: 0x040020B5 RID: 8373
	public string description_tur = string.Empty;

	// Token: 0x040020B6 RID: 8374
	public string description_kor = string.Empty;
}
