using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006BB RID: 1723
public class LoadingManager : MonoBehaviour
{
	// Token: 0x06002B15 RID: 11029 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x06002B16 RID: 11030 RVA: 0x0014E668 File Offset: 0x0014C868
	private void Start()
	{
		if (LoadingManager.previousLevel == "PrepareManagersScene")
		{
			Application.LoadLevelAsync(LoadingManager.nextLevel);
		}
		if (LoadingManager.previousLevel == "LevelSelectScene")
		{
			Application.LoadLevelAsync(LoadingManager.nextLevel);
		}
		if (LoadingManager.previousLevel == "PlayScene")
		{
			GameObject.Find("StagesParserManager").SendMessage("CallSave");
		}
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x000214A3 File Offset: 0x0001F6A3
	private IEnumerator saveData()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x0014E6D4 File Offset: 0x0014C8D4
	private void Update()
	{
		if (LoadingManager.previousLevel == "PlayScene" && StagesParser.saving)
		{
			StagesParser.saving = false;
			Application.LoadLevelAsync(LoadingManager.nextLevel);
		}
		if (Input.GetMouseButtonDown(0))
		{
			GameObject.Find("text").GetComponent<Animation>().Stop();
			int num = Random.Range(0, 16);
			if (num < 2)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "Stop Clicking!!\nIm Loading!!!";
				GameObject.Find("text").GetComponent<Animation>().Play();
				return;
			}
			if (num < 4)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "Leave me Alone!!!\nIm Loading!!! ";
				GameObject.Find("text").GetComponent<Animation>().Play();
				return;
			}
			if (num < 6)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "I wont load any faster\nno matter how much you click!";
				GameObject.Find("text").GetComponent<Animation>().Play();
				return;
			}
			if (num < 8)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "I don't think we're\nin kansas anymore!";
				GameObject.Find("text").GetComponent<Animation>().Play();
				return;
			}
			if (num < 10)
			{
				GameObject.Find("text").GetComponent<TextMesh>().text = "These aren't the\ndroids you're looking for!";
				GameObject.Find("text").GetComponent<Animation>().Play();
				return;
			}
			GameObject.Find("text").GetComponent<TextMesh>().GetComponent<Renderer>().enabled = false;
		}
	}

	// Token: 0x04002547 RID: 9543
	public static string previousLevel = "";

	// Token: 0x04002548 RID: 9544
	public static string nextLevel = "";
}
