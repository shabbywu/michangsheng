using UnityEngine;

public class PlayTutorialCircle : MonoBehaviour
{
	public static PlayTutorialCircle Inst;

	public GameObject BG;

	public GameObject Hand;

	private void Awake()
	{
		Inst = this;
	}

	public void SetShow(bool show)
	{
		BG.SetActive(show);
		Hand.SetActive(show);
	}
}
