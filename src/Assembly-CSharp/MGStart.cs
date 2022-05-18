using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002CB RID: 715
public class MGStart : MonoBehaviour
{
	// Token: 0x06001576 RID: 5494 RVA: 0x000136F4 File Offset: 0x000118F4
	private void Start()
	{
		SceneManager.LoadScene("Mainmenu");
		SceneManager.LoadSceneAsync("login", 1);
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}
}
