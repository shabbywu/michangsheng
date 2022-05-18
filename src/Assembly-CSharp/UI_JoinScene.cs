using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005E9 RID: 1513
public class UI_JoinScene : MonoBehaviour
{
	// Token: 0x060025FD RID: 9725 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x0001E5BD File Offset: 0x0001C7BD
	public void MoveToScence()
	{
		if (this.SceneName != "")
		{
			SceneManager.LoadScene(this.SceneName);
		}
	}

	// Token: 0x04002084 RID: 8324
	public string SceneName = "";
}
