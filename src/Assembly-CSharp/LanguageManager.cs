using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

// Token: 0x020006A9 RID: 1705
public class LanguageManager : MonoBehaviour
{
	// Token: 0x06002A9E RID: 10910 RVA: 0x001481A8 File Offset: 0x001463A8
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

	// Token: 0x06002A9F RID: 10911 RVA: 0x00148204 File Offset: 0x00146404
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

	// Token: 0x06002AA0 RID: 10912 RVA: 0x00148814 File Offset: 0x00146A14
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

	// Token: 0x06002AA1 RID: 10913 RVA: 0x00010DC9 File Offset: 0x0000EFC9
	private string SplitInHalf(string toSplit)
	{
		return toSplit;
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x00021170 File Offset: 0x0001F370
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

	// Token: 0x06002AA3 RID: 10915 RVA: 0x0014899C File Offset: 0x00146B9C
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

	// Token: 0x04002449 RID: 9289
	public static string chosenLanguage = "_en";

	// Token: 0x0400244A RID: 9290
	public static string Collect = "Collect";

	// Token: 0x0400244B RID: 9291
	public static string LogIn = "Log In";

	// Token: 0x0400244C RID: 9292
	public static string Free = "Free";

	// Token: 0x0400244D RID: 9293
	public static string Coins = "Coins";

	// Token: 0x0400244E RID: 9294
	public static string Music = "Music";

	// Token: 0x0400244F RID: 9295
	public static string Sound = "Sound";

	// Token: 0x04002450 RID: 9296
	public static string Language = "Language";

	// Token: 0x04002451 RID: 9297
	public static string Settings = "Settings";

	// Token: 0x04002452 RID: 9298
	public static string ResetProgress = "Reset Progress";

	// Token: 0x04002453 RID: 9299
	public static string ResetTutorials = "Reset Tutorials";

	// Token: 0x04002454 RID: 9300
	public static string LogOut = "Log Out";

	// Token: 0x04002455 RID: 9301
	public static string Leaderboard = "Leaderboard";

	// Token: 0x04002456 RID: 9302
	public static string InviteAndEarn = "Invite and earn";

	// Token: 0x04002457 RID: 9303
	public static string InviteFriendsAndEarn = "Invite friends and earn";

	// Token: 0x04002458 RID: 9304
	public static string FreeCoins = "Free Coins";

	// Token: 0x04002459 RID: 9305
	public static string Shop = "Shop";

	// Token: 0x0400245A RID: 9306
	public static string Customize = "Customize";

	// Token: 0x0400245B RID: 9307
	public static string PowerUps = "Power-Ups";

	// Token: 0x0400245C RID: 9308
	public static string WatchVideo = "Watch Video";

	// Token: 0x0400245D RID: 9309
	public static string Banana = "Banana";

	// Token: 0x0400245E RID: 9310
	public static string DoubleCoins = "Double Coins";

	// Token: 0x0400245F RID: 9311
	public static string CoinsMagnet = "Coins Magnet";

	// Token: 0x04002460 RID: 9312
	public static string Shield = "Shield";

	// Token: 0x04002461 RID: 9313
	public static string New = "New";

	// Token: 0x04002462 RID: 9314
	public static string Preview = "Preview";

	// Token: 0x04002463 RID: 9315
	public static string Buy = "Buy";

	// Token: 0x04002464 RID: 9316
	public static string Equip = "Equip";

	// Token: 0x04002465 RID: 9317
	public static string Unequip = "Unequip";

	// Token: 0x04002466 RID: 9318
	public static string ShareAndEarn = "Share and earn";

	// Token: 0x04002467 RID: 9319
	public static string PostAndEarn = "Post and earn";

	// Token: 0x04002468 RID: 9320
	public static string TweetAndEarn = "Tweet and earn";

	// Token: 0x04002469 RID: 9321
	public static string LogInAndEarn = "Log In and earn";

	// Token: 0x0400246A RID: 9322
	public static string Level = "Level";

	// Token: 0x0400246B RID: 9323
	public static string Mission = "Mission";

	// Token: 0x0400246C RID: 9324
	public static string Invite = "Invite";

	// Token: 0x0400246D RID: 9325
	public static string BonusLevel = "Bonus Level";

	// Token: 0x0400246E RID: 9326
	public static string Unlock = "Unlock";

	// Token: 0x0400246F RID: 9327
	public static string No = "No";

	// Token: 0x04002470 RID: 9328
	public static string Yes = "Yes";

	// Token: 0x04002471 RID: 9329
	public static string NoVideo = "No videos available, please try again later";

	// Token: 0x04002472 RID: 9330
	public static string NotEnoughBananas = "Not enough bananas";

	// Token: 0x04002473 RID: 9331
	public static string Loading = "Loading";

	// Token: 0x04002474 RID: 9332
	public static string Tip = "Tip";

	// Token: 0x04002475 RID: 9333
	public static string Tips = "Tips";

	// Token: 0x04002476 RID: 9334
	public static string ComingSoon = "Coming Soon";

	// Token: 0x04002477 RID: 9335
	public static string TapScreenToStart = "Tap Screen To Start!";

	// Token: 0x04002478 RID: 9336
	public static string Pause = "Pause";

	// Token: 0x04002479 RID: 9337
	public static string KeepPlaying = "Keep Playing";

	// Token: 0x0400247A RID: 9338
	public static string LevelFailed = "Level Failed";

	// Token: 0x0400247B RID: 9339
	public static string LevelCompleted = "Level Completed";

	// Token: 0x0400247C RID: 9340
	public static string RateThisGame = "Rate this game";

	// Token: 0x0400247D RID: 9341
	public static string DoYouLikeOurGame = "Do you like our game?";

	// Token: 0x0400247E RID: 9342
	public static string Cancel = "Cancel";

	// Token: 0x0400247F RID: 9343
	public static string Rate = "Rate";

	// Token: 0x04002480 RID: 9344
	public static string Congratulations = "Congratulations!";

	// Token: 0x04002481 RID: 9345
	public static string NewLevelsComingSoon = "New levels coming soon!";

	// Token: 0x04002482 RID: 9346
	public static string TutorialTapJump = "1-TAP anywhere to JUMP\n2-TAP and HOLD to JUMP HIGHER";

	// Token: 0x04002483 RID: 9347
	public static string TutorialGlide = "When in the AIR you can TAP and HOLD to GLIDE slowly to the ground";

	// Token: 0x04002484 RID: 9348
	public static string TutorialSwipe = "SWIPE DOWN at great heights to perform a deadly super drop!";

	// Token: 0x04002485 RID: 9349
	public static string NoInternet = "No internet";

	// Token: 0x04002486 RID: 9350
	public static string CheckInternet = "Check your internet connection";

	// Token: 0x04002487 RID: 9351
	public static string HowWouldYouRate = "How would you rate our game?";

	// Token: 0x04002488 RID: 9352
	public static string Downloading = "Downloading...";

	// Token: 0x04002489 RID: 9353
	public static string Maintenance = "Maintenance";

	// Token: 0x0400248A RID: 9354
	public static string BeBackSoon = "We'll be back soon";

	// Token: 0x0400248B RID: 9355
	public static string Ok = "OK";

	// Token: 0x0400248C RID: 9356
	public static string Reward = "Reward";

	// Token: 0x0400248D RID: 9357
	public static string DailyReward = "Daily Reward";

	// Token: 0x0400248E RID: 9358
	public static string Day = "Day";

	// Token: 0x0400248F RID: 9359
	public static string Play = "Play";

	// Token: 0x04002490 RID: 9360
	public static string BananaIsland = "Banana Island";

	// Token: 0x04002491 RID: 9361
	public static string SavannaIsland = "Savanna Island";

	// Token: 0x04002492 RID: 9362
	public static string JungleIsland = "Jungle Island";

	// Token: 0x04002493 RID: 9363
	public static string TempleIsland = "Temple Island";

	// Token: 0x04002494 RID: 9364
	public static string VolcanoIsland = "Volcano Island";

	// Token: 0x04002495 RID: 9365
	public static string Completed = "Completed";

	// Token: 0x04002496 RID: 9366
	public static string Share = "Share";

	// Token: 0x04002497 RID: 9367
	public static string FollowUsOnFacebook = "Follow us on Facebook";

	// Token: 0x04002498 RID: 9368
	public static string LoadingTip1 = "Don't miss Tips and Tricks in the Loading Screen, they can be useful.";

	// Token: 0x04002499 RID: 9369
	public static string LoadingTip2 = "Want to keep playing? Spend one of your bananas to continue running.";

	// Token: 0x0400249A RID: 9370
	public static string LoadingTip3 = "Jump on enemies' heads to knock'em out.";

	// Token: 0x0400249B RID: 9371
	public static string FrozenIsland = "Frozen Island";

	// Token: 0x0400249C RID: 9372
	public static bool hasWhiteSpaces = true;
}
