using System;
using UnityEngine;

// Token: 0x02000752 RID: 1874
public class SetRandomStarsManager : MonoBehaviour
{
	// Token: 0x06002FBB RID: 12219 RVA: 0x000235C6 File Offset: 0x000217C6
	private void Start()
	{
		StagesParser.NemaRequiredStars_VratiULevele = false;
		this.currSet = StagesParser.currSetIndex;
		this.currStage = StagesParser.currStageIndex;
		this.gameManager = base.GetComponent<Manage>();
	}

	// Token: 0x06002FBC RID: 12220 RVA: 0x0017D618 File Offset: 0x0017B818
	public void GoBack()
	{
		this.prevoiousSetIndex = StagesParser.currSetIndex;
		int starsGained = this.gameManager.starsGained;
		if (StagesParser.bonusLevel)
		{
			string[] array = PlayerPrefs.GetString("BonusLevel").Split(new char[]
			{
				'_'
			});
			string text = array[StagesParser.currSetIndex];
			string[] array2 = text.Split(new char[]
			{
				'#'
			});
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
			int num = int.Parse(StagesParser.allLevels[this.currSet * 20 + this.currStage].Split(new char[]
			{
				'#'
			})[2]);
			if (Manage.points > num)
			{
				string text3 = string.Empty;
				StagesParser.allLevels[this.currSet * 20 + this.currStage] = string.Concat(new object[]
				{
					(this.currSet * 20 + this.currStage + 1).ToString(),
					"#",
					starsGained,
					"#",
					Manage.points
				});
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
					string[] array3 = StagesParser.allLevels[this.currSet * 20 + this.currStage + 1].Split(new char[]
					{
						'#'
					});
					if (this.currStage < 19 && int.Parse(array3[1]) == -1)
					{
						text3 = string.Empty;
						StagesParser.allLevels[this.currSet * 20 + this.currStage + 1] = (this.currSet * 20 + this.currStage + 2).ToString() + "#0#0";
						for (int l = 0; l < StagesParser.allLevels.Length; l++)
						{
							text3 += StagesParser.allLevels[l];
							text3 += "_";
						}
						text3 = text3.Remove(text3.Length - 1);
						PlayerPrefs.SetString("AllLevels", text3);
						PlayerPrefs.Save();
						StagesParser.zadnjiOtkljucanNivo = this.currStage + 2;
					}
					if (StagesParser.maxLevel < this.currSet * 20 + this.currStage + 1)
					{
						StagesParser.maxLevel = this.currSet * 20 + this.currStage + 1;
					}
				}
			}
			StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex] = StagesParser.currStageIndex + 1;
			PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + StagesParser.currSetIndex.ToString(), StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]);
			PlayerPrefs.Save();
			string[] array4 = StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20 + 19].Split(new char[]
			{
				'#'
			});
			Debug.Log("ISPRED USLOV ZA NIVO: " + StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20 + 19]);
			if (int.Parse(array4[1]) > 0)
			{
				this.uslovNivo = true;
			}
			StagesParser.RecountTotalStars();
			float num2 = 0f;
			int totalSets = StagesParser.totalSets;
			for (int m = 0; m <= StagesParser.lastUnlockedWorldIndex + 1; m++)
			{
				if (m < totalSets)
				{
					num2 = (float)StagesParser.SetsInGame[m].StarRequirement;
				}
			}
			PlayerPrefs.SetInt("CurrentStars", StagesParser.currentStarsNEW);
			PlayerPrefs.Save();
			Debug.Log(string.Concat(new object[]
			{
				"potrebne zvezdice: ",
				num2,
				", a ima gi: ",
				StagesParser.currentStarsNEW,
				", last unlocked world index: ",
				StagesParser.lastUnlockedWorldIndex
			}));
			Debug.Log(string.Concat(new string[]
			{
				"Unlocked worlds: ",
				StagesParser.unlockedWorlds[0].ToString(),
				", ",
				StagesParser.unlockedWorlds[1].ToString(),
				", ",
				StagesParser.unlockedWorlds[2].ToString(),
				", ",
				StagesParser.unlockedWorlds[3].ToString(),
				", ",
				StagesParser.unlockedWorlds[4].ToString(),
				", ",
				StagesParser.unlockedWorlds[5].ToString()
			}));
			if (num2 <= (float)StagesParser.currentStarsNEW && StagesParser.lastUnlockedWorldIndex + 1 < StagesParser.totalSets && !StagesParser.unlockedWorlds[StagesParser.lastUnlockedWorldIndex + 1])
			{
				this.uslovZvezdice = true;
			}
			Debug.Log("uslov nivo: " + this.uslovNivo.ToString() + ", uslov zvezdice: " + this.uslovZvezdice.ToString());
			if (this.uslovNivo && this.uslovZvezdice)
			{
				Debug.Log("ULETEO OVDE: IMA USLOVE ZA NIVO I ZVEZDICE");
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
				Debug.Log(string.Concat(new object[]
				{
					"StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex*20] = ",
					StagesParser.allLevels[StagesParser.lastUnlockedWorldIndex * 20],
					", treba upisat': ",
					StagesParser.lastUnlockedWorldIndex * 20 + 1,
					"#0#0"
				}));
				Debug.Log("svi nivoji: " + text4);
			}
			else
			{
				StagesParser.NemaRequiredStars_VratiULevele = true;
			}
		}
		StagesParser.saving = true;
	}

	// Token: 0x04002AE8 RID: 10984
	private int currSet;

	// Token: 0x04002AE9 RID: 10985
	private int currStage;

	// Token: 0x04002AEA RID: 10986
	private int prevoiousSetIndex;

	// Token: 0x04002AEB RID: 10987
	private Manage gameManager;

	// Token: 0x04002AEC RID: 10988
	private bool uslovNivo;

	// Token: 0x04002AED RID: 10989
	private bool uslovZvezdice;
}
