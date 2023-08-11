using UnityEngine;

public class Interaction : MonoBehaviour
{
	private GameObject deal;

	private GameObject play;

	private GameObject disard;

	private GameObject grab;

	private GameObject disgrab;

	private GameController controller;

	private void Start()
	{
		deal = ((Component)((Component)this).gameObject.transform.Find("DealBtn")).gameObject;
		play = ((Component)((Component)this).gameObject.transform.Find("PlayBtn")).gameObject;
		disard = ((Component)((Component)this).gameObject.transform.Find("DiscardBtn")).gameObject;
		grab = ((Component)((Component)this).gameObject.transform.Find("GrabBtn")).gameObject;
		disgrab = ((Component)((Component)this).gameObject.transform.Find("DisgrabBtn")).gameObject;
		controller = GameObject.Find("GameController").GetComponent<GameController>();
		deal.GetComponent<UIButton>().onClick.Add(new EventDelegate(DealCallBack));
		play.GetComponent<UIButton>().onClick.Add(new EventDelegate(PlayCallBack));
		disard.GetComponent<UIButton>().onClick.Add(new EventDelegate(DiscardCallBack));
		grab.GetComponent<UIButton>().onClick.Add(new EventDelegate(GrabLordCallBack));
		disgrab.GetComponent<UIButton>().onClick.Add(new EventDelegate(DisgrabLordCallBack));
		OrderController.Instance.activeButton += ActiveCardButton;
		play.SetActive(false);
		disard.SetActive(false);
		grab.SetActive(false);
		disgrab.SetActive(false);
	}

	private void ActiveCardButton(bool canReject)
	{
		play.SetActive(true);
		disard.SetActive(true);
		disard.GetComponent<UIButton>().isEnabled = canReject;
	}

	public void DealCallBack()
	{
		controller.DealCards();
		grab.SetActive(true);
		disgrab.SetActive(true);
		deal.SetActive(false);
	}

	private void PlayCallBack()
	{
		if (GameObject.Find("Player").GetComponent<PlayCard>().CheckSelectCards())
		{
			play.SetActive(false);
			disard.SetActive(false);
		}
	}

	private void DiscardCallBack()
	{
		OrderController.Instance.Turn();
		play.SetActive(false);
		disard.SetActive(false);
	}

	private void GrabLordCallBack()
	{
		controller.CardsOnTable(CharacterType.Player);
		OrderController.Instance.Init(CharacterType.Player);
		grab.SetActive(false);
		disgrab.SetActive(false);
	}

	private void DisgrabLordCallBack()
	{
		int type = Random.Range(2, 4);
		controller.CardsOnTable((CharacterType)type);
		OrderController.Instance.Init((CharacterType)type);
		grab.SetActive(false);
		disgrab.SetActive(false);
	}
}
