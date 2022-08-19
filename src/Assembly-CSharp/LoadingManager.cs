using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class LoadingManager : MonoBehaviour
{
	// Token: 0x0600264F RID: 9807 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x0010A340 File Offset: 0x00108540
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

	// Token: 0x06002651 RID: 9809 RVA: 0x0010A3AA File Offset: 0x001085AA
	private IEnumerator saveData()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x0010A3B4 File Offset: 0x001085B4
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

	// Token: 0x04001F94 RID: 8084
	public static string previousLevel = "";

	// Token: 0x04001F95 RID: 8085
	public static string nextLevel = "";
}
