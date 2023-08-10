using UnityEngine;

public class Restart : MonoBehaviour
{
	private GameController controller;

	private void Start()
	{
		controller = GameObject.Find("GameController").GetComponent<GameController>();
		((Component)((Component)this).transform.Find("Button")).gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(RestartGame));
	}

	public void SetTimeToNext(float sec)
	{
		((MonoBehaviour)this).Invoke("Next", sec);
	}

	private void RestartGame()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		controller.BackToDeck();
		controller.DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		Object.Destroy((Object)(object)GameObject.Find("InteractionPanel").gameObject);
		Object.Destroy((Object)(object)GameObject.Find("ScenePanel").gameObject);
		Object.Destroy((Object)(object)GameObject.Find("BackgroundPanel").gameObject);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		OrderController.Instance.ResetButton();
		OrderController.Instance.ResetSmartCard();
		GameObject obj = NGUITools.AddChild(((Component)UICamera.mainCamera).gameObject, (GameObject)Resources.Load("StartPanel"));
		obj.AddComponent<Menu>();
		((Component)obj.transform.Find("NoticeLabel")).gameObject.SetActive(true);
	}

	private void Next()
	{
		controller.BackToDeck();
		controller.DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		((Component)GameObject.Find("InteractionPanel").transform.Find("DealBtn")).gameObject.SetActive(true);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ResetDisplay();
	}

	private void ResetDisplay()
	{
		for (int i = 1; i < 4; i++)
		{
			CharacterType characterType = (CharacterType)i;
			if (Object.op_Implicit((Object)(object)GameObject.Find(characterType.ToString())))
			{
				GameController.UpdateLeftCardsCount((CharacterType)i, 0);
				GameController.UpdateIndentity((CharacterType)i, Identity.Farmer);
			}
		}
	}
}
