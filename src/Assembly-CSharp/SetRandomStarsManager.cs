using UnityEngine;

public class SetRandomStarsManager : MonoBehaviour
{
	private int currSet;

	private int currStage;

	private int prevoiousSetIndex;

	private Manage gameManager;

	private bool uslovNivo;

	private bool uslovZvezdice;

	private void Start()
	{
		StagesParser.NemaRequiredStars_VratiULevele = false;
		currSet = StagesParser.currSetIndex;
		currStage = StagesParser.currStageIndex;
		gameManager = ((Component)this).GetComponent<Manage>();
	}

	public void GoBack()
	{
		prevoiousSetIndex = StagesParser.currSetIndex;
		int starsGained = gameManager.starsGained;
		if (StagesParser.bonusLevel)
		{
			string[] array = PlayerPrefs.GetString("BonusLevel").Split(new char[1] { '_' });
			string text = array[StagesParser.currSetIndex];
			string[] array2 = text.Split(new char[1] { '#' });
			array2[StagesParser.bonusID - 1] = "1";
			string text2 = string.Empty;
			text = string.Empty;
			for (int i = 0; i < array2.Length; i++)
			{
				text = text + array2[i] + "#";
			}
			text = text.Remove(text.Length - 1);
			array[StagesParser.currSetIndex] = text;
			for (int j = 0; j < StagesParser.totalSets; j++)
			{
				text2 = text2 + array[j] + "_";
			}
			text2 = text2.Remove(text2.Length - 1);
			PlayerPrefs.SetString("BonusLevel", text2);
			PlayerPrefs.Save();
			StagesParser.bonusLevels = text2;
			StagesParser.ServerUpdate = 1;
		}
		else
		{
			int num = int.Parse(StagesParser.allLevels[currSet * 20 + currStage].Split(new char[1] { '#' })[2]);
			if (Manage.points > num)
			{
				string text3 = string.Empty;
				StagesParser.allLevels[currSet * 20 + currStage] = (currSet * 20 + currStage + 1).ToString() + "#" + starsGained + "#" + Manage.points;
				for (int k = 0; k < StagesParser.allLevels.Length; k++)
				{
					text3 += StagesParser.allLevels[k];
					text3 += "_";
				}
				text3 = text3.Remove(text3.Length - 1);
				PlayerPrefs.SetString("AllLevels", text3);
				PlayerPrefs.Save();
				if (StagesParser.currSetIndex != 5 || StagesParser.currStageIndex != 19)
				{
					string[] array3 = StagesParser.allLevels[currSet * 20 + currStage + 1].Split(new char[1] { '#' });
					if (currStage < 19 && int.Parse(array3[1]) == -1)
					{
						text3 = string.Empty;
						StagesParser.allLevels[currSet * 20 + currStage + 1] = currSet * 20 + currStage + 2 + "#0#0";
						for (int l = 0; l < StagesParser.allLevels.Length; l++)
						{
							text3 += StagesParser.allLevels[l];
							text3 += "_";
						}
						text3 = text3.Remove(text3.Length - 1);
						PlayerPrefs.SetString("AllLevels", text3);
						PlayerPrefs.Save();
						StagesParser.zadnjiOtkljucanNivo = currStage + 2;
					}
					if (StagesParser.maxLevel < currSet * 20 + currStage + 1)
					{
						StagesParser.maxLevel = currSet * 20 + currStage + 1;
					}
				}
			}
			StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex] = StagesParser.currStageIndex + 1;
			PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + StagesParser.currSetIndex, StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]);
			PlayerPrefs.Save();
			string[] array4 = StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20 + 19].Split(new char[1] { '#' });
			Debug.Log((object)("ISPRED USLOV ZA NIVO: " + StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20 + 19]));
			if (int.Parse(array4[1]) > 0)
			{
				uslovNivo = true;
			}
			StagesParser.RecountTotalStars();
			float num2 = 0f;
			int totalSets = StagesParser.totalSets;
			for (int m = 0; m <= StagesParser.lastUnlockedWorldIndex + 1; m++)
			{
				if (m < totalSets)
				{
					num2 = StagesParser.SetsInGame[m].StarRequirement;
				}
			}
			PlayerPrefs.SetInt("CurrentStars", StagesParser.currentStarsNEW);
			PlayerPrefs.Save();
			Debug.Log((object)("potrebne zvezdice: " + num2 + ", a ima gi: " + StagesParser.currentStarsNEW + ", last unlocked world index: " + StagesParser.lastUnlockedWorldIndex));
			Debug.Log((object)("Unlocked worlds: " + StagesParser.unlockedWorlds[0] + ", " + StagesParser.unlockedWorlds[1] + ", " + StagesParser.unlockedWorlds[2] + ", " + StagesParser.unlockedWorlds[3] + ", " + StagesParser.unlockedWorlds[4] + ", " + StagesParser.unlockedWorlds[5]));
			if (num2 <= (float)StagesParser.currentStarsNEW && StagesParser.lastUnlockedWorldIndex + 1 < StagesParser.totalSets && !StagesParser.unlockedWorlds[StagesParser.lastUnlockedWorldIndex + 1])
			{
				uslovZvezdice = true;
			}
			Debug.Log((object)("uslov nivo: " + uslovNivo + ", uslov zvezdice: " + uslovZvezdice));
			if (uslovNivo && uslovZvezdice)
			{
				Debug.Log((object)"ULETEO OVDE: IMA USLOVE ZA NIVO I ZVEZDICE");
				StagesParser.unlockedWorlds[StagesParser.lastUnlockedWorldIndex + 1] = true;
				StagesParser.openedButNotPlayed[StagesParser.lastUnlockedWorldIndex + 1] = true;
				StagesParser.lastUnlockedWorldIndex++;
				StagesParser.isJustOpened = true;
				StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20] = StagesParser.lastUnlockedWorldIndex * 20 + 1 + "#0#0";
				StagesParser.StarsPoNivoima[StagesParser.lastUnlockedWorldIndex * 20] = 0;
				if (StagesParser.lastUnlockedWorldIndex == 5 && FB.IsLoggedIn)
				{
					for (int n = 0; n < FacebookManager.ListaStructPrijatelja.Count; n++)
					{
						if (FacebookManager.ListaStructPrijatelja[n].PrijateljID.Equals(FacebookManager.User) && FacebookManager.ListaStructPrijatelja[n].scores.Count < StagesParser.allLevels.Length)
						{
							for (int num3 = FacebookManager.ListaStructPrijatelja[n].scores.Count; num3 < StagesParser.allLevels.Length; num3++)
							{
								FacebookManager.ListaStructPrijatelja[n].scores.Add(0);
							}
						}
					}
				}
				string text4 = string.Empty;
				for (int num4 = 0; num4 < StagesParser.allLevels.Length; num4++)
				{
					text4 += StagesParser.allLevels[num4];
					text4 += "_";
				}
				text4 = text4.Remove(text4.Length - 1);
				PlayerPrefs.SetString("AllLevels", text4);
				PlayerPrefs.Save();
				Debug.Log((object)("StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex*20] = " + StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20] + ", treba upisat': " + (StagesParser.lastUnlockedWorldIndex * 20 + 1) + "#0#0"));
				Debug.Log((object)("svi nivoji: " + text4));
			}
			else
			{
				StagesParser.NemaRequiredStars_VratiULevele = true;
			}
		}
		StagesParser.saving = true;
	}
}
