using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

// Token: 0x020004B5 RID: 1205
public class LanguageManager : MonoBehaviour
{
	// Token: 0x06002620 RID: 9760 RVA: 0x00107F00 File Offset: 0x00106100
	private void Awake()
	{
		base.transform.name = "LanguageManager";
		Object.DontDestroyOnLoad(base.gameObject);
		LanguageManager.RefreshTexts();
		if (PlayerPrefs.HasKey("choosenLanguage"))
		{
			LanguageManager.chosenLanguage = PlayerPrefs.GetString("choosenLanguage");
		}
		Debug.Log("Chosen Languafe iz Language Mangaer: " + LanguageManager.chosenLanguage);
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x00107F5C File Offset: 0x0010615C
	public static void RefreshTexts()
	{
		IEnumerable<XElement> source = XElement.Parse(((TextAsset)Resources.Load("xmls/inGameText/Language" + LanguageManager.chosenLanguage)).ToString()).Elements();
		int num = source.Count<XElement>();
		Debug.Log("Ukupno ima " + num + " xml elemenata");
		LanguageManager.Collect = source.ElementAt(0).Value;
		LanguageManager.LogIn = source.ElementAt(1).Value;
		LanguageManager.Free = source.ElementAt(2).Value;
		LanguageManager.Coins = source.ElementAt(3).Value;
		LanguageManager.Music = source.ElementAt(4).Value;
		LanguageManager.Sound = source.ElementAt(5).Value;
		LanguageManager.Language = source.ElementAt(6).Value;
		LanguageManager.Settings = source.ElementAt(7).Value;
		LanguageManager.ResetProgress = source.ElementAt(8).Value;
		LanguageManager.ResetTutorials = source.ElementAt(9).Value;
		LanguageManager.LogOut = source.ElementAt(10).Value;
		LanguageManager.Leaderboard = source.ElementAt(11).Value;
		LanguageManager.InviteAndEarn = source.ElementAt(12).Value;
		LanguageManager.InviteFriendsAndEarn = source.ElementAt(13).Value;
		LanguageManager.FreeCoins = source.ElementAt(14).Value;
		LanguageManager.Shop = source.ElementAt(15).Value;
		LanguageManager.Customize = source.ElementAt(16).Value;
		LanguageManager.PowerUps = source.ElementAt(17).Value;
		LanguageManager.WatchVideo = source.ElementAt(18).Value;
		LanguageManager.Banana = source.ElementAt(19).Value;
		LanguageManager.DoubleCoins = source.ElementAt(20).Value;
		LanguageManager.CoinsMagnet = source.ElementAt(21).Value;
		LanguageManager.Shield = source.ElementAt(22).Value;
		LanguageManager.New = source.ElementAt(23).Value;
		LanguageManager.Preview = source.ElementAt(24).Value;
		LanguageManager.Buy = source.ElementAt(25).Value;
		LanguageManager.Equip = source.ElementAt(26).Value;
		LanguageManager.Unequip = source.ElementAt(27).Value;
		LanguageManager.ShareAndEarn = source.ElementAt(28).Value;
		LanguageManager.PostAndEarn = source.ElementAt(29).Value;
		LanguageManager.TweetAndEarn = source.ElementAt(30).Value;
		LanguageManager.LogInAndEarn = source.ElementAt(31).Value;
		LanguageManager.Level = source.ElementAt(32).Value;
		LanguageManager.Mission = source.ElementAt(33).Value;
		LanguageManager.Invite = source.ElementAt(34).Value;
		LanguageManager.BonusLevel = source.ElementAt(35).Value;
		LanguageManager.Unlock = source.ElementAt(36).Value;
		LanguageManager.No = source.ElementAt(37).Value;
		LanguageManager.Yes = source.ElementAt(38).Value;
		LanguageManager.NoVideo = source.ElementAt(39).Value;
		LanguageManager.NotEnoughBananas = source.ElementAt(40).Value;
		LanguageManager.Loading = source.ElementAt(41).Value;
		LanguageManager.Tip = source.ElementAt(42).Value;
		LanguageManager.Tips = source.ElementAt(43).Value;
		LanguageManager.ComingSoon = source.ElementAt(44).Value;
		LanguageManager.TapScreenToStart = source.ElementAt(45).Value;
		LanguageManager.Pause = source.ElementAt(46).Value;
		LanguageManager.KeepPlaying = source.ElementAt(47).Value;
		LanguageManager.LevelFailed = source.ElementAt(48).Value;
		LanguageManager.LevelCompleted = source.ElementAt(49).Value;
		LanguageManager.RateThisGame = source.ElementAt(50).Value;
		LanguageManager.DoYouLikeOurGame = source.ElementAt(51).Value;
		LanguageManager.Cancel = source.ElementAt(52).Value;
		LanguageManager.Rate = source.ElementAt(53).Value;
		LanguageManager.Congratulations = source.ElementAt(54).Value;
		LanguageManager.NewLevelsComingSoon = source.ElementAt(55).Value;
		LanguageManager.TutorialTapJump = source.ElementAt(56).Value;
		LanguageManager.TutorialGlide = source.ElementAt(57).Value;
		LanguageManager.TutorialSwipe = source.ElementAt(58).Value;
		LanguageManager.NoInternet = source.ElementAt(59).Value;
		LanguageManager.CheckInternet = source.ElementAt(60).Value;
		LanguageManager.HowWouldYouRate = source.ElementAt(61).Value;
		LanguageManager.Downloading = source.ElementAt(62).Value;
		LanguageManager.Maintenance = source.ElementAt(63).Value;
		LanguageManager.BeBackSoon = source.ElementAt(64).Value;
		LanguageManager.Ok = source.ElementAt(65).Value;
		LanguageManager.Reward = source.ElementAt(66).Value;
		LanguageManager.DailyReward = source.ElementAt(67).Value;
		LanguageManager.Day = source.ElementAt(68).Value;
		LanguageManager.Play = source.ElementAt(69).Value;
		LanguageManager.BananaIsland = source.ElementAt(70).Value;
		LanguageManager.SavannaIsland = source.ElementAt(71).Value;
		LanguageManager.JungleIsland = source.ElementAt(72).Value;
		LanguageManager.TempleIsland = source.ElementAt(73).Value;
		LanguageManager.VolcanoIsland = source.ElementAt(74).Value;
		LanguageManager.Completed = source.ElementAt(75).Value;
		LanguageManager.Share = source.ElementAt(76).Value;
		LanguageManager.FollowUsOnFacebook = source.ElementAt(77).Value;
		LanguageManager.LoadingTip1 = source.ElementAt(78).Value;
		LanguageManager.LoadingTip2 = source.ElementAt(79).Value;
		LanguageManager.LoadingTip3 = source.ElementAt(80).Value;
		LanguageManager.FrozenIsland = source.ElementAt(83).Value;
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x0010856C File Offset: 0x0010676C
	public string SplitTextIntoRows(string originalText, int rowLimit)
	{
		string text2;
		string text = text2 = "";
		originalText = originalText.Replace("\n", " ");
		if (originalText.Length < rowLimit)
		{
			return originalText;
		}
		bool flag = true;
		string[] array = originalText.Split(new char[]
		{
			' '
		});
		if (LanguageManager.chosenLanguage == "_jp" || LanguageManager.chosenLanguage == "_ch")
		{
			array = new string[originalText.Length];
			for (int i = 0; i < originalText.Length; i++)
			{
				array[i] = originalText[i].ToString();
			}
		}
		int j = 0;
		while (j < array.Length)
		{
			if (array.Length > j)
			{
				if (array[j].Length >= rowLimit)
				{
					text2 = text2 + array[j++] + ((LanguageManager.chosenLanguage != "_jp" && LanguageManager.chosenLanguage != "_ch") ? " " : "");
				}
				else
				{
					while (text2.Length + array[j].Length < rowLimit && j < array.Length)
					{
						text2 = text2 + array[j++] + ((LanguageManager.chosenLanguage != "_jp" && LanguageManager.chosenLanguage != "_ch") ? " " : "");
						if (j >= array.Length)
						{
							break;
						}
					}
				}
				if (flag)
				{
					text = text2;
					flag = false;
				}
				else
				{
					text = text + "\n" + text2;
				}
				text2 = "";
			}
		}
		return text;
	}

	// Token: 0x06002623 RID: 9763 RVA: 0x001086F1 File Offset: 0x001068F1
	private string SplitInHalf(string toSplit)
	{
		return toSplit;
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x001086F4 File Offset: 0x001068F4
	public IEnumerator helpfunk(TextMesh toAdjust)
	{
		string text = toAdjust.text;
		while (toAdjust.GetComponent<Renderer>().bounds.size.x * 1.1f < toAdjust.GetComponent<Collider>().bounds.size.x)
		{
			if (toAdjust.GetComponent<Renderer>().bounds.size.y * 1.1f >= toAdjust.GetComponent<Collider>().bounds.size.y)
			{
				break;
			}
			toAdjust.characterSize *= 1.1f;
		}
		while (toAdjust.GetComponent<Renderer>().bounds.size.x * 0.9f > toAdjust.GetComponent<Collider>().bounds.size.x && toAdjust.GetComponent<Renderer>().bounds.size.y * 0.9f > toAdjust.GetComponent<Collider>().bounds.size.y)
		{
			toAdjust.characterSize *= 0.9f;
		}
		string[] wholeText = text.Split(new char[]
		{
			' '
		});
		if (LanguageManager.chosenLanguage == "_jp" || LanguageManager.chosenLanguage == "_ch")
		{
			char[] array = text.ToCharArray();
			wholeText = new string[array.Length];
			for (int i = 0; i < wholeText.Length; i++)
			{
				wholeText[i] = array[i].ToString();
			}
		}
		bool again = false;
		float charSize = toAdjust.characterSize;
		do
		{
			again = false;
			toAdjust.characterSize = charSize;
			int counter = 1;
			toAdjust.text = wholeText[0];
			string previous = toAdjust.text;
			string helpText = "";
			while (counter < wholeText.Length)
			{
				int helpCounter = 0;
				while ((toAdjust.GetComponent<Renderer>().bounds.size.x < toAdjust.GetComponent<Collider>().bounds.size.x || toAdjust.text == wholeText[(counter < wholeText.Length) ? counter : (wholeText.Length - 1)]) && counter < wholeText.Length)
				{
					previous = toAdjust.text;
					toAdjust.text = toAdjust.text + ((LanguageManager.chosenLanguage == "_jp" || LanguageManager.chosenLanguage == "_ch") ? "" : " ") + wholeText[counter];
					int num = counter;
					counter = num + 1;
					num = helpCounter;
					helpCounter = num + 1;
					yield return null;
					yield return new WaitForSeconds(0.1f);
				}
				if (helpCounter == 0)
				{
					Debug.Log("again");
					again = true;
					break;
				}
				if (counter < wholeText.Length)
				{
					toAdjust.text = wholeText[counter - 1];
					Debug.Log(counter + "<" + wholeText.Length);
					helpText = helpText + previous + "\n";
				}
				else
				{
					helpText += toAdjust.text;
					Debug.Log(counter + ">=" + wholeText.Length);
				}
				Debug.Log(wholeText.Length + "##" + counter);
				yield return null;
				yield return new WaitForSeconds(0.1f);
			}
			toAdjust.text = helpText;
			charSize *= 0.9f;
			Debug.Log("enddd");
			previous = null;
			helpText = null;
		}
		while (toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y || again);
		yield break;
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x00108704 File Offset: 0x00106904
	public void AdjustFontSize(TextMesh toAdjust, bool rowSplit)
	{
		string text = toAdjust.text;
		while (toAdjust.GetComponent<Renderer>().bounds.size.x * 1.1f < toAdjust.GetComponent<Collider>().bounds.size.x && toAdjust.GetComponent<Renderer>().bounds.size.y * 1.1f < toAdjust.GetComponent<Collider>().bounds.size.y)
		{
			toAdjust.characterSize *= 1.1f;
		}
		if (!rowSplit)
		{
			while (toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x)
			{
				if (toAdjust.GetComponent<Renderer>().bounds.size.y <= toAdjust.GetComponent<Collider>().bounds.size.y)
				{
					return;
				}
				toAdjust.characterSize *= 0.9f;
			}
		}
		else
		{
			string[] array = text.Split(new char[]
			{
				' '
			});
			if (LanguageManager.chosenLanguage == "_jp" || LanguageManager.chosenLanguage == "_ch")
			{
				char[] array2 = text.ToCharArray();
				array = new string[array2.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array2[i].ToString();
				}
			}
			float num = toAdjust.characterSize;
			bool flag;
			do
			{
				flag = false;
				toAdjust.characterSize = num;
				int j = 1;
				toAdjust.text = array[0];
				string text2 = toAdjust.text;
				string text3 = "";
				while (j < array.Length)
				{
					int num2 = 0;
					while ((toAdjust.GetComponent<Renderer>().bounds.size.x < toAdjust.GetComponent<Collider>().bounds.size.x || toAdjust.text == array[(j < array.Length) ? j : (array.Length - 1)]) && j < array.Length)
					{
						text2 = toAdjust.text;
						toAdjust.text = toAdjust.text + ((LanguageManager.chosenLanguage == "_jp" || LanguageManager.chosenLanguage == "_ch") ? "" : " ") + array[j];
						j++;
						num2++;
					}
					if (num2 == 0)
					{
						Debug.Log("again");
						flag = true;
						break;
					}
					if (j < array.Length)
					{
						toAdjust.text = array[j - 1];
						Debug.Log(j + "<" + array.Length);
						text3 = text3 + text2 + "\n";
					}
					else
					{
						text3 += toAdjust.text;
						Debug.Log(j + ">=" + array.Length);
					}
					Debug.Log(array.Length + "##" + j);
				}
				toAdjust.text = text3;
				num *= 0.9f;
				Debug.Log("enddd");
			}
			while (toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y || toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x || flag);
			Debug.Log(string.Concat(new string[]
			{
				(toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y).ToString(),
				"###",
				(toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x).ToString(),
				"###",
				flag.ToString()
			}));
		}
	}

	// Token: 0x04001ED6 RID: 7894
	public static string chosenLanguage = "_en";

	// Token: 0x04001ED7 RID: 7895
	public static string Collect = "Collect";

	// Token: 0x04001ED8 RID: 7896
	public static string LogIn = "Log In";

	// Token: 0x04001ED9 RID: 7897
	public static string Free = "Free";

	// Token: 0x04001EDA RID: 7898
	public static string Coins = "Coins";

	// Token: 0x04001EDB RID: 7899
	public static string Music = "Music";

	// Token: 0x04001EDC RID: 7900
	public static string Sound = "Sound";

	// Token: 0x04001EDD RID: 7901
	public static string Language = "Language";

	// Token: 0x04001EDE RID: 7902
	public static string Settings = "Settings";

	// Token: 0x04001EDF RID: 7903
	public static string ResetProgress = "Reset Progress";

	// Token: 0x04001EE0 RID: 7904
	public static string ResetTutorials = "Reset Tutorials";

	// Token: 0x04001EE1 RID: 7905
	public static string LogOut = "Log Out";

	// Token: 0x04001EE2 RID: 7906
	public static string Leaderboard = "Leaderboard";

	// Token: 0x04001EE3 RID: 7907
	public static string InviteAndEarn = "Invite and earn";

	// Token: 0x04001EE4 RID: 7908
	public static string InviteFriendsAndEarn = "Invite friends and earn";

	// Token: 0x04001EE5 RID: 7909
	public static string FreeCoins = "Free Coins";

	// Token: 0x04001EE6 RID: 7910
	public static string Shop = "Shop";

	// Token: 0x04001EE7 RID: 7911
	public static string Customize = "Customize";

	// Token: 0x04001EE8 RID: 7912
	public static string PowerUps = "Power-Ups";

	// Token: 0x04001EE9 RID: 7913
	public static string WatchVideo = "Watch Video";

	// Token: 0x04001EEA RID: 7914
	public static string Banana = "Banana";

	// Token: 0x04001EEB RID: 7915
	public static string DoubleCoins = "Double Coins";

	// Token: 0x04001EEC RID: 7916
	public static string CoinsMagnet = "Coins Magnet";

	// Token: 0x04001EED RID: 7917
	public static string Shield = "Shield";

	// Token: 0x04001EEE RID: 7918
	public static string New = "New";

	// Token: 0x04001EEF RID: 7919
	public static string Preview = "Preview";

	// Token: 0x04001EF0 RID: 7920
	public static string Buy = "Buy";

	// Token: 0x04001EF1 RID: 7921
	public static string Equip = "Equip";

	// Token: 0x04001EF2 RID: 7922
	public static string Unequip = "Unequip";

	// Token: 0x04001EF3 RID: 7923
	public static string ShareAndEarn = "Share and earn";

	// Token: 0x04001EF4 RID: 7924
	public static string PostAndEarn = "Post and earn";

	// Token: 0x04001EF5 RID: 7925
	public static string TweetAndEarn = "Tweet and earn";

	// Token: 0x04001EF6 RID: 7926
	public static string LogInAndEarn = "Log In and earn";

	// Token: 0x04001EF7 RID: 7927
	public static string Level = "Level";

	// Token: 0x04001EF8 RID: 7928
	public static string Mission = "Mission";

	// Token: 0x04001EF9 RID: 7929
	public static string Invite = "Invite";

	// Token: 0x04001EFA RID: 7930
	public static string BonusLevel = "Bonus Level";

	// Token: 0x04001EFB RID: 7931
	public static string Unlock = "Unlock";

	// Token: 0x04001EFC RID: 7932
	public static string No = "No";

	// Token: 0x04001EFD RID: 7933
	public static string Yes = "Yes";

	// Token: 0x04001EFE RID: 7934
	public static string NoVideo = "No videos available, please try again later";

	// Token: 0x04001EFF RID: 7935
	public static string NotEnoughBananas = "Not enough bananas";

	// Token: 0x04001F00 RID: 7936
	public static string Loading = "Loading";

	// Token: 0x04001F01 RID: 7937
	public static string Tip = "Tip";

	// Token: 0x04001F02 RID: 7938
	public static string Tips = "Tips";

	// Token: 0x04001F03 RID: 7939
	public static string ComingSoon = "Coming Soon";

	// Token: 0x04001F04 RID: 7940
	public static string TapScreenToStart = "Tap Screen To Start!";

	// Token: 0x04001F05 RID: 7941
	public static string Pause = "Pause";

	// Token: 0x04001F06 RID: 7942
	public static string KeepPlaying = "Keep Playing";

	// Token: 0x04001F07 RID: 7943
	public static string LevelFailed = "Level Failed";

	// Token: 0x04001F08 RID: 7944
	public static string LevelCompleted = "Level Completed";

	// Token: 0x04001F09 RID: 7945
	public static string RateThisGame = "Rate this game";

	// Token: 0x04001F0A RID: 7946
	public static string DoYouLikeOurGame = "Do you like our game?";

	// Token: 0x04001F0B RID: 7947
	public static string Cancel = "Cancel";

	// Token: 0x04001F0C RID: 7948
	public static string Rate = "Rate";

	// Token: 0x04001F0D RID: 7949
	public static string Congratulations = "Congratulations!";

	// Token: 0x04001F0E RID: 7950
	public static string NewLevelsComingSoon = "New levels coming soon!";

	// Token: 0x04001F0F RID: 7951
	public static string TutorialTapJump = "1-TAP anywhere to JUMP\n2-TAP and HOLD to JUMP HIGHER";

	// Token: 0x04001F10 RID: 7952
	public static string TutorialGlide = "When in the AIR you can TAP and HOLD to GLIDE slowly to the ground";

	// Token: 0x04001F11 RID: 7953
	public static string TutorialSwipe = "SWIPE DOWN at great heights to perform a deadly super drop!";

	// Token: 0x04001F12 RID: 7954
	public static string NoInternet = "No internet";

	// Token: 0x04001F13 RID: 7955
	public static string CheckInternet = "Check your internet connection";

	// Token: 0x04001F14 RID: 7956
	public static string HowWouldYouRate = "How would you rate our game?";

	// Token: 0x04001F15 RID: 7957
	public static string Downloading = "Downloading...";

	// Token: 0x04001F16 RID: 7958
	public static string Maintenance = "Maintenance";

	// Token: 0x04001F17 RID: 7959
	public static string BeBackSoon = "We'll be back soon";

	// Token: 0x04001F18 RID: 7960
	public static string Ok = "OK";

	// Token: 0x04001F19 RID: 7961
	public static string Reward = "Reward";

	// Token: 0x04001F1A RID: 7962
	public static string DailyReward = "Daily Reward";

	// Token: 0x04001F1B RID: 7963
	public static string Day = "Day";

	// Token: 0x04001F1C RID: 7964
	public static string Play = "Play";

	// Token: 0x04001F1D RID: 7965
	public static string BananaIsland = "Banana Island";

	// Token: 0x04001F1E RID: 7966
	public static string SavannaIsland = "Savanna Island";

	// Token: 0x04001F1F RID: 7967
	public static string JungleIsland = "Jungle Island";

	// Token: 0x04001F20 RID: 7968
	public static string TempleIsland = "Temple Island";

	// Token: 0x04001F21 RID: 7969
	public static string VolcanoIsland = "Volcano Island";

	// Token: 0x04001F22 RID: 7970
	public static string Completed = "Completed";

	// Token: 0x04001F23 RID: 7971
	public static string Share = "Share";

	// Token: 0x04001F24 RID: 7972
	public static string FollowUsOnFacebook = "Follow us on Facebook";

	// Token: 0x04001F25 RID: 7973
	public static string LoadingTip1 = "Don't miss Tips and Tricks in the Loading Screen, they can be useful.";

	// Token: 0x04001F26 RID: 7974
	public static string LoadingTip2 = "Want to keep playing? Spend one of your bananas to continue running.";

	// Token: 0x04001F27 RID: 7975
	public static string LoadingTip3 = "Jump on enemies' heads to knock'em out.";

	// Token: 0x04001F28 RID: 7976
	public static string FrozenIsland = "Frozen Island";

	// Token: 0x04001F29 RID: 7977
	public static bool hasWhiteSpaces = true;
}
