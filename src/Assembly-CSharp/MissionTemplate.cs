public class MissionTemplate
{
	public int baboons;

	public int fly_baboons;

	public int boomerang_baboons;

	public int gorilla;

	public int fly_gorilla;

	public int koplje_gorilla;

	public int diamonds;

	public int coins;

	public int distance;

	public int barrels;

	public int red_diamonds;

	public int blue_diamonds;

	public int green_diamonds;

	public int points;

	public string level = string.Empty;

	public string description_en = string.Empty;

	public string description_us = string.Empty;

	public string description_es = string.Empty;

	public string description_ru = string.Empty;

	public string description_pt = string.Empty;

	public string description_pt_br = string.Empty;

	public string description_fr = string.Empty;

	public string description_tha = string.Empty;

	public string description_zh = string.Empty;

	public string description_tzh = string.Empty;

	public string description_ger = string.Empty;

	public string description_it = string.Empty;

	public string description_srb = string.Empty;

	public string description_tur = string.Empty;

	public string description_kor = string.Empty;

	public MissionTemplate()
	{
		baboons = 0;
		fly_baboons = 0;
		boomerang_baboons = 0;
		gorilla = 0;
		fly_gorilla = 0;
		koplje_gorilla = 0;
		diamonds = 0;
		coins = 0;
		distance = 0;
		barrels = 0;
		red_diamonds = 0;
		blue_diamonds = 0;
		green_diamonds = 0;
		points = 0;
		level = string.Empty;
		description_en = string.Empty;
		description_us = string.Empty;
		description_es = string.Empty;
		description_ru = string.Empty;
		description_pt = string.Empty;
		description_pt_br = string.Empty;
		description_fr = string.Empty;
		description_tha = string.Empty;
		description_zh = string.Empty;
		description_tzh = string.Empty;
		description_ger = string.Empty;
		description_it = string.Empty;
		description_srb = string.Empty;
		description_tur = string.Empty;
		description_kor = string.Empty;
	}

	public string IspisiDescriptionNaIspravnomJeziku()
	{
		if (LanguageManager.chosenLanguage.Equals("_en"))
		{
			return description_en;
		}
		if (LanguageManager.chosenLanguage.Equals("_us"))
		{
			return description_us;
		}
		if (LanguageManager.chosenLanguage.Equals("_es"))
		{
			return description_es;
		}
		if (LanguageManager.chosenLanguage.Equals("_ru"))
		{
			return description_ru;
		}
		if (LanguageManager.chosenLanguage.Equals("_pt"))
		{
			return description_pt;
		}
		if (LanguageManager.chosenLanguage.Equals("_br"))
		{
			return description_pt_br;
		}
		if (LanguageManager.chosenLanguage.Equals("_fr"))
		{
			return description_fr;
		}
		if (LanguageManager.chosenLanguage.Equals("_th"))
		{
			return description_tha;
		}
		if (LanguageManager.chosenLanguage.Equals("_ch"))
		{
			return description_zh;
		}
		if (LanguageManager.chosenLanguage.Equals("_tch"))
		{
			return description_tzh;
		}
		if (LanguageManager.chosenLanguage.Equals("_de"))
		{
			return description_ger;
		}
		if (LanguageManager.chosenLanguage.Equals("_it"))
		{
			return description_it;
		}
		if (LanguageManager.chosenLanguage.Equals("_srb"))
		{
			return description_srb;
		}
		if (LanguageManager.chosenLanguage.Equals("_tr"))
		{
			return description_tur;
		}
		if (LanguageManager.chosenLanguage.Equals("_ko"))
		{
			return description_kor;
		}
		return string.Empty;
	}
}
