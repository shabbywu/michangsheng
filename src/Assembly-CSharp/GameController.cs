using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x020006A3 RID: 1699
public class GameController : MonoBehaviour
{
	// Token: 0x06002A6D RID: 10861 RVA: 0x00020FAC File Offset: 0x0001F1AC
	private void Start()
	{
		this.multiples = 1;
		this.basePointPerMatch = 100;
		this.InitMenu();
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06002A6F RID: 10863 RVA: 0x00020FD3 File Offset: 0x0001F1D3
	// (set) Token: 0x06002A6E RID: 10862 RVA: 0x00020FC3 File Offset: 0x0001F1C3
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

	// Token: 0x06002A70 RID: 10864 RVA: 0x001470C8 File Offset: 0x001452C8
	public void InitMenu()
	{
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("StartPanel"));
		gameObject.AddComponent<Menu>();
		gameObject.transform.Find("NoticeLabel").gameObject.SetActive(false);
	}

	// Token: 0x06002A71 RID: 10865 RVA: 0x00147114 File Offset: 0x00145314
	public void InitInteraction()
	{
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("InteractionPanel"));
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		gameObject.AddComponent<Interaction>();
	}

	// Token: 0x06002A72 RID: 10866 RVA: 0x00147160 File Offset: 0x00145360
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

	// Token: 0x06002A73 RID: 10867 RVA: 0x00147358 File Offset: 0x00145558
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

	// Token: 0x06002A74 RID: 10868 RVA: 0x001473B8 File Offset: 0x001455B8
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

	// Token: 0x06002A75 RID: 10869 RVA: 0x00147554 File Offset: 0x00145754
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

	// Token: 0x06002A76 RID: 10870 RVA: 0x00020FDB File Offset: 0x0001F1DB
	private IEnumerator ChangeComputer(CharacterType type)
	{
		GameController.DisplayDeskNotice(true);
		yield return new WaitForSeconds(3f);
		Object.Destroy(GameObject.Find(type.ToString()));
		this.MatchNewComputer(type);
		yield break;
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x001475B8 File Offset: 0x001457B8
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

	// Token: 0x06002A78 RID: 10872 RVA: 0x001476C0 File Offset: 0x001458C0
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

	// Token: 0x06002A79 RID: 10873 RVA: 0x00147728 File Offset: 0x00145928
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

	// Token: 0x06002A7A RID: 10874 RVA: 0x00147778 File Offset: 0x00145978
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

	// Token: 0x06002A7B RID: 10875 RVA: 0x00147804 File Offset: 0x00145A04
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

	// Token: 0x06002A7C RID: 10876 RVA: 0x0014787C File Offset: 0x00145A7C
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

	// Token: 0x06002A7D RID: 10877 RVA: 0x00147900 File Offset: 0x00145B00
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

	// Token: 0x06002A7E RID: 10878 RVA: 0x00147950 File Offset: 0x00145B50
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

	// Token: 0x06002A7F RID: 10879 RVA: 0x001479A4 File Offset: 0x00145BA4
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

	// Token: 0x06002A80 RID: 10880 RVA: 0x00147A40 File Offset: 0x00145C40
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

	// Token: 0x06002A81 RID: 10881 RVA: 0x00147B4C File Offset: 0x00145D4C
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

	// Token: 0x06002A82 RID: 10882 RVA: 0x00147BD0 File Offset: 0x00145DD0
	public static void UpdateLeftCardsCount(CharacterType type, int cardsCount)
	{
		GameObject.Find(type.ToString()).transform.Find("LeftPoker").gameObject.GetComponent<UILabel>().text = "剩余扑克:" + cardsCount;
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x00147C20 File Offset: 0x00145E20
	public static void UpdateIndentity(CharacterType type, Identity identity)
	{
		GameObject gameObject = GameObject.Find(type.ToString()).transform.Find("Identity").gameObject;
		GameObject.Find(type.ToString()).GetComponent<HandCards>().AccessIdentity = identity;
		gameObject.GetComponent<UISprite>().spriteName = "Identity_" + identity.ToString();
	}

	// Token: 0x06002A84 RID: 10884 RVA: 0x00147C94 File Offset: 0x00145E94
	public static void UpdateIntegration(CharacterType type)
	{
		int integration = GameObject.Find(type.ToString()).GetComponent<HandCards>().Integration;
		GameObject.Find(type.ToString()).transform.Find("IntegrationLabel").gameObject.GetComponent<UILabel>().text = "积分:" + integration;
	}

	// Token: 0x06002A85 RID: 10885 RVA: 0x00020FF1 File Offset: 0x0001F1F1
	public static void DisplayDeskNotice(bool show)
	{
		GameObject.Find("Desk").transform.Find("NoticeLabel").gameObject.SetActive(show);
	}

	// Token: 0x06002A86 RID: 10886 RVA: 0x00147D00 File Offset: 0x00145F00
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

	// Token: 0x04002431 RID: 9265
	public int basePointPerMatch;

	// Token: 0x04002432 RID: 9266
	private int multiples;
}
