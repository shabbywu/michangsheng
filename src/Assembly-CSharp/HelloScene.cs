using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002FB RID: 763
public class HelloScene : MonoBehaviour
{
	// Token: 0x060016F9 RID: 5881 RVA: 0x0001454C File Offset: 0x0001274C
	private void Start()
	{
		this.scene = SceneManager.LoadSceneAsync("MainMenu");
		this.scene.allowSceneActivation = false;
		base.Invoke("GameStart", 6f);
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x0001457A File Offset: 0x0001277A
	public void GameStart()
	{
		this.scene.allowSceneActivation = true;
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400125B RID: 4699
	private AsyncOperation scene;
}
