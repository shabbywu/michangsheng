using UnityEngine;

public class Menu : MonoBehaviour
{
	private GameController controller;

	private void Start()
	{
		((Component)((Component)this).transform.Find("Easy")).gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(StartEasyGame));
		((Component)((Component)this).transform.Find("Normal")).gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(StartNormalGame));
		controller = GameObject.Find("GameController").GetComponent<GameController>();
	}

	private void StartEasyGame()
	{
		controller.InitInteraction();
		controller.InitScene();
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void StartNormalGame()
	{
		controller.Multiples = 2;
		controller.InitInteraction();
		controller.InitScene();
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
