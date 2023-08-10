using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
	public static string previousLevel = "";

	public static string nextLevel = "";

	private void Awake()
	{
	}

	private void Start()
	{
		if (previousLevel == "PrepareManagersScene")
		{
			Application.LoadLevelAsync(nextLevel);
		}
		if (previousLevel == "LevelSelectScene")
		{
			Application.LoadLevelAsync(nextLevel);
		}
		if (previousLevel == "PlayScene")
		{
			GameObject.Find("StagesParserManager").SendMessage("CallSave");
		}
	}

	private IEnumerator saveData()
	{
		yield return null;
	}

	private void Update()
	{
		if (previousLevel == "PlayScene" && StagesParser.saving)
		{
			StagesParser.saving = false;
			Application.LoadLevelAsync(nextLevel);
		}
		if (Input.GetMouseButtonDown(0))
		{
			GameObject.Find("text").GetComponent<Animation>().Stop();
			int num = Random.Range(0, 16);
			if (num < 2)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "Stop Clicking!!\nIm Loading!!!";
				GameObject.Find("text").GetComponent<Animation>().Play();
			}
			else if (num < 4)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "Leave me Alone!!!\nIm Loading!!! ";
				GameObject.Find("text").GetComponent<Animation>().Play();
			}
			else if (num < 6)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "I wont load any faster\nno matter how much you click!";
				GameObject.Find("text").GetComponent<Animation>().Play();
			}
			else if (num < 8)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "I don't think we're\nin kansas anymore!";
				GameObject.Find("text").GetComponent<Animation>().Play();
			}
			else if (num < 10)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "These aren't the\ndroids you're looking for!";
				GameObject.Find("text").GetComponent<Animation>().Play();
			}
			else
			{
				((Component)GameObject.Find("text").GetComponent<TextMesh>()).GetComponent<Renderer>().enabled = false;
			}
		}
	}
}
