using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000432 RID: 1074
public class UI_JoinScene : MonoBehaviour
{
	// Token: 0x0600223E RID: 8766 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x000EBFA5 File Offset: 0x000EA1A5
	public void MoveToScence()
	{
		if (this.SceneName != "")
		{
			SceneManager.LoadScene(this.SceneName);
		}
	}

	// Token: 0x04001BB8 RID: 7096
	public string SceneName = "";
}
