using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
public class GameController : MonoBehaviour
{
	// Token: 0x060025F5 RID: 9717 RVA: 0x00106CE9 File Offset: 0x00104EE9
	private void Start()
	{
		this.multiples = 1;
		this.basePointPerMatch = 100;
		this.InitMenu();
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x060025F7 RID: 9719 RVA: 0x00106D10 File Offset: 0x00104F10
	// (set) Token: 0x060025F6 RID: 9718 RVA: 0x00106D00 File Offset: 0x00104F00
	public int Multiples
	{
		get
		{
			return this.multiples;
		}
		set
		{
			this.multiples *= value;
		}
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x00106D18 File Offset: 0x00104F18
	public void InitMenu()
	{
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("StartPanel"));
		gameObject.AddComponent<Menu>();
		gameObject.transform.Find("NoticeLabel").gameObject.SetActive(false);
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x00106D64 File Offset: 0x00104F64
	public void InitInteraction()
	{
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("InteractionPanel"));
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		gameObject.AddComponent<Interaction>();
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x00106DB0 File Offset: 0x00104FB0
	public void InitScene()
	{
		string text;
		if (Application.platform == 11)
		{
			text = Application.persistentDataPath;
		}
		else
		{
			text = Application.dataPath;
		}
		FileSystemInfo fileSystemInfo = new FileInfo(text + "\\data.json");
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("BackgroundPanel"));
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		GameObject gameObject2 = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("ScenePanel"));
		gameObject2.name = gameObject2.name.Replace("(Clone)", "");
		GameObject gameObject3 = gameObject2.transform.Find("Player").gameObject;
		HandCards handCards = gameObject3.AddComponent<HandCards>();
		handCards.cType = CharacterType.Player;
		gameObject3.AddComponent<PlayCard>();
		GameObject gameObject4 = gameObject2.transform.Find("ComputerOne").gameObject;
		HandCards handCards2 = gameObject4.AddComponent<HandCards>();
		handCards2.cType = CharacterType.ComputerOne;
		gameObject4.AddComponent<SimpleSmartCard>();
		gameObject4.transform.Find("ComputerNotice").gameObject.SetActive(false);
		GameObject gameObject5 = gameObject2.transform.Find("ComputerTwo").gameObject;
		HandCards handCards3 = gameObject5.AddComponent<HandCards>();
		handCards3.cType = CharacterType.ComputerTwo;
		gameObject5.AddComponent<SimpleSmartCard>();
		gameObject5.transform.Find("ComputerNotice").gameObject.SetActive(false);
		gameObject2.transform.Find("Desk").gameObject.transform.Find("NoticeLabel").gameObject.SetActive(false);
		if (!fileSystemInfo.Exists)
		{
			handCards.Integration = 1000;
			handCards2.Integration = 1000;
			handCards3.Integration = 1000;
		}
		else
		{
			GameData dataWithoutBOM = this.GetDataWithoutBOM(text);
			handCards.Integration = dataWithoutBOM.playerIntegration;
			handCards2.Integration = dataWithoutBOM.computerOneIntegration;
			handCards3.Integration = dataWithoutBOM.computerTwoIntegration;
		}
		GameController.UpdateIntegration(CharacterType.Player);
		GameController.UpdateIntegration(CharacterType.ComputerOne);
		GameController.UpdateIntegration(CharacterType.ComputerTwo);
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x00106FA8 File Offset: 0x001051A8
	public void DealCards()
	{
		Deck.Instance.Shuffle();
		CharacterType characterType = CharacterType.Player;
		for (int i = 0; i < 51; i++)
		{
			if (characterType == CharacterType.Desk)
			{
				characterType = CharacterType.Player;
			}
			this.DealTo(characterType);
			characterType++;
		}
		for (int j = 0; j < 3; j++)
		{
			this.DealTo(CharacterType.Desk);
		}
		for (int k = 1; k < 5; k++)
		{
			this.MakeHandCardsSprite((CharacterType)k, false);
		}
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x00107008 File Offset: 0x00105208
	public void GameOver()
	{
		Identity accessIdentity = GameObject.Find(OrderController.Instance.Type.ToString()).GetComponent<HandCards>().AccessIdentity;
		for (int i = 1; i < 4; i++)
		{
			this.StatisticalIntegral((CharacterType)i, accessIdentity);
		}
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("GameOverPanel"));
		gameObject.AddComponent<Restart>();
		int integration = GameObject.Find(CharacterType.Player.ToString()).GetComponent<HandCards>().Integration;
		int integration2 = GameObject.Find(CharacterType.ComputerOne.ToString()).GetComponent<HandCards>().Integration;
		int integration3 = GameObject.Find(CharacterType.ComputerTwo.ToString()).GetComponent<HandCards>().Integration;
		if (integration > 0)
		{
			gameObject.GetComponent<Restart>().SetTimeToNext(3f);
			if (integration2 <= 0)
			{
				base.StartCoroutine(this.ChangeComputer(CharacterType.ComputerOne));
			}
			if (integration3 <= 0)
			{
				base.StartCoroutine(this.ChangeComputer(CharacterType.ComputerTwo));
			}
			GameController.DisplayOverInfo(true, gameObject, accessIdentity);
			GameData data = new GameData
			{
				playerIntegration = ((integration > 0) ? integration : 1000),
				computerOneIntegration = ((integration2 > 0) ? integration2 : 1000),
				computerTwoIntegration = ((integration3 > 0) ? integration3 : 1000)
			};
			this.SaveDataWithUTF8(data);
			return;
		}
		GameController.DisplayOverInfo(false, gameObject, accessIdentity);
		GameData data2 = new GameData
		{
			playerIntegration = 1000,
			computerOneIntegration = 1000,
			computerTwoIntegration = 1000
		};
		this.SaveDataWithUTF8(data2);
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x001071A4 File Offset: 0x001053A4
	private void StatisticalIntegral(CharacterType type, Identity winner)
	{
		HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
		int num = component.Multiples * this.Multiples * this.basePointPerMatch * (int)(component.AccessIdentity + 1);
		if (component.AccessIdentity != winner)
		{
			num = -num;
		}
		component.Integration += num;
		GameController.UpdateIntegration(type);
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x00107207 File Offset: 0x00105407
	private IEnumerator ChangeComputer(CharacterType type)
	{
		GameController.DisplayDeskNotice(true);
		yield return new WaitForSeconds(3f);
		Object.Destroy(GameObject.Find(type.ToString()));
		this.MatchNewComputer(type);
		yield break;
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x00107220 File Offset: 0x00105420
	private void MatchNewComputer(CharacterType type)
	{
		this.BackToDeck();
		this.DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		OrderController.Instance.ResetSmartCard();
		GameController.DisplayDeskNotice(false);
		GameObject gameObject = NGUITools.AddChild(GameObject.Find("ScenePanel"), (GameObject)Resources.Load(type.ToString()));
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		gameObject.AddComponent<HandCards>().cType = type;
		gameObject.AddComponent<HandCards>().Integration = 1000;
		gameObject.transform.Find("ComputerNotice").gameObject.SetActive(false);
		gameObject.AddComponent<SimpleSmartCard>();
		if (Random.Range(1, 3) == 1)
		{
			gameObject.transform.Find("HeadPortrait").gameObject.GetComponent<UISprite>().spriteName = "role1";
			return;
		}
		gameObject.transform.Find("HeadPortrait").gameObject.GetComponent<UISprite>().spriteName = "role";
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x00107328 File Offset: 0x00105528
	private void SaveDataWithUTF8(GameData data)
	{
		string str;
		if (Application.platform == 11)
		{
			str = Application.persistentDataPath;
		}
		else
		{
			str = Application.dataPath;
		}
		FileStream fileStream = new FileStream(str + "\\data.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
		new XmlSerializer(data.GetType()).Serialize(streamWriter, data);
		streamWriter.Close();
		fileStream.Close();
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x00107390 File Offset: 0x00105590
	private GameData GetDataWithoutBOM(string fileName)
	{
		object obj = new GameData();
		Stream stream = new FileStream(fileName + "\\data.json", FileMode.Open, FileAccess.Read, FileShare.None);
		StreamReader streamReader = new StreamReader(stream, true);
		GameData result = new XmlSerializer(obj.GetType()).Deserialize(streamReader) as GameData;
		streamReader.Close();
		stream.Close();
		return result;
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x001073E0 File Offset: 0x001055E0
	public void BackToDeck()
	{
		HandCards[] array = new HandCards[]
		{
			GameObject.Find("Player").GetComponent<HandCards>(),
			GameObject.Find("ComputerOne").GetComponent<HandCards>(),
			GameObject.Find("ComputerTwo").GetComponent<HandCards>()
		};
		for (int i = 0; i < array.Length; i++)
		{
			while (array[i].CardsCount != 0)
			{
				Card card = array[i][array[i].CardsCount - 1];
				Deck.Instance.AddCard(card);
				array[i].PopCard(card);
			}
		}
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x0010746C File Offset: 0x0010566C
	public void DestroyAllSprites()
	{
		CardSprite[][] array = new CardSprite[][]
		{
			GameObject.Find("Player").GetComponentsInChildren<CardSprite>(),
			GameObject.Find("ComputerOne").GetComponentsInChildren<CardSprite>(),
			GameObject.Find("ComputerTwo").GetComponentsInChildren<CardSprite>()
		};
		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array[i].Length; j++)
			{
				array[i][j].Destroy();
			}
		}
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x001074E4 File Offset: 0x001056E4
	public void CardsOnTable(CharacterType type)
	{
		HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
		component.Multiples = 2;
		CardSprite[] componentsInChildren = GameObject.Find("Desk").GetComponentsInChildren<CardSprite>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Destroy();
		}
		while (DeskCardsCache.Instance.CardsCount != 0)
		{
			Card card = DeskCardsCache.Instance.Deal();
			component.AddCard(card);
		}
		this.MakeHandCardsSprite(type, true);
		GameController.UpdateIndentity(type, Identity.Landlord);
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x00107568 File Offset: 0x00105768
	private void DealTo(CharacterType person)
	{
		if (person == CharacterType.Desk)
		{
			Card card = Deck.Instance.Deal();
			DeskCardsCache.Instance.AddCard(card);
			return;
		}
		HandCards component = GameObject.Find(person.ToString()).GetComponent<HandCards>();
		Card card2 = Deck.Instance.Deal();
		component.AddCard(card2);
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x001075B8 File Offset: 0x001057B8
	private void MakeSprite(CharacterType type, Card card, bool selected)
	{
		if (!card.isSprite)
		{
			GameObject prefab = Resources.Load("poker") as GameObject;
			CardSprite component = NGUITools.AddChild(GameObject.Find(type.ToString()), prefab).gameObject.GetComponent<CardSprite>();
			component.Poker = card;
			component.Select = selected;
		}
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x0010760C File Offset: 0x0010580C
	private void MakeHandCardsSprite(CharacterType type, bool isSelected)
	{
		if (type == CharacterType.Desk)
		{
			DeskCardsCache instance = DeskCardsCache.Instance;
			for (int i = 0; i < instance.CardsCount; i++)
			{
				this.MakeSprite(type, instance[i], isSelected);
			}
		}
		else
		{
			HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
			component.Sort();
			for (int j = 0; j < component.CardsCount; j++)
			{
				if (!component[j].isSprite)
				{
					this.MakeSprite(type, component[j], isSelected);
				}
			}
			GameController.UpdateLeftCardsCount(component.cType, component.CardsCount);
		}
		GameController.AdjustCardSpritsPosition(type);
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x001076A8 File Offset: 0x001058A8
	public static void AdjustCardSpritsPosition(CharacterType type)
	{
		if (type == CharacterType.Desk)
		{
			DeskCardsCache instance = DeskCardsCache.Instance;
			CardSprite[] componentsInChildren = GameObject.Find(type.ToString()).GetComponentsInChildren<CardSprite>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					if (componentsInChildren[j].Poker == instance[i])
					{
						componentsInChildren[j].GoToPosition(GameObject.Find(type.ToString()), i, 100, 0);
					}
				}
			}
			return;
		}
		HandCards component = GameObject.Find(type.ToString()).GetComponent<HandCards>();
		CardSprite[] componentsInChildren2 = GameObject.Find(type.ToString()).GetComponentsInChildren<CardSprite>();
		for (int k = 0; k < component.CardsCount; k++)
		{
			for (int l = 0; l < componentsInChildren2.Length; l++)
			{
				if (componentsInChildren2[l].Poker == component[k])
				{
					componentsInChildren2[l].GoToPosition(GameObject.Find(type.ToString()), k, 100, 0);
				}
			}
		}
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x001077B4 File Offset: 0x001059B4
	public static int GetWeight(Card[] cards, CardsType rule)
	{
		int num = 0;
		if (rule == CardsType.ThreeAndOne || rule == CardsType.ThreeAndTwo)
		{
			for (int i = 0; i < cards.Length; i++)
			{
				if (i < cards.Length - 2 && cards[i].GetCardWeight == cards[i + 1].GetCardWeight && cards[i].GetCardWeight == cards[i + 2].GetCardWeight)
				{
					num = (int)(num + cards[i].GetCardWeight);
					num *= 3;
					break;
				}
			}
		}
		else
		{
			for (int j = 0; j < cards.Length; j++)
			{
				num = (int)(num + cards[j].GetCardWeight);
			}
		}
		return num;
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x00107838 File Offset: 0x00105A38
	public static void UpdateLeftCardsCount(CharacterType type, int cardsCount)
	{
		GameObject.Find(type.ToString()).transform.Find("LeftPoker").gameObject.GetComponent<UILabel>().text = "剩余扑克:" + cardsCount;
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x00107888 File Offset: 0x00105A88
	public static void UpdateIndentity(CharacterType type, Identity identity)
	{
		GameObject gameObject = GameObject.Find(type.ToString()).transform.Find("Identity").gameObject;
		GameObject.Find(type.ToString()).GetComponent<HandCards>().AccessIdentity = identity;
		gameObject.GetComponent<UISprite>().spriteName = "Identity_" + identity.ToString();
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x001078FC File Offset: 0x00105AFC
	public static void UpdateIntegration(CharacterType type)
	{
		int integration = GameObject.Find(type.ToString()).GetComponent<HandCards>().Integration;
		GameObject.Find(type.ToString()).transform.Find("IntegrationLabel").gameObject.GetComponent<UILabel>().text = "积分:" + integration;
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x00107966 File Offset: 0x00105B66
	public static void DisplayDeskNotice(bool show)
	{
		GameObject.Find("Desk").transform.Find("NoticeLabel").gameObject.SetActive(show);
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x0010798C File Offset: 0x00105B8C
	public static void DisplayOverInfo(bool enough, GameObject gameovePanel, Identity winner)
	{
		if (enough)
		{
			gameovePanel.transform.Find("Button").gameObject.SetActive(false);
			gameovePanel.GetComponent<Restart>().SetTimeToNext(3f);
		}
		if (winner == Identity.Farmer)
		{
			gameovePanel.GetComponentInChildren<UISprite>().spriteName = "role1";
			gameovePanel.GetComponentInChildren<UILabel>().text = "农民获得胜利";
			return;
		}
		gameovePanel.GetComponentInChildren<UISprite>().spriteName = "role";
		gameovePanel.GetComponentInChildren<UILabel>().text = "地主获得胜利";
	}

	// Token: 0x04001EC2 RID: 7874
	public int basePointPerMatch;

	// Token: 0x04001EC3 RID: 7875
	private int multiples;
}
