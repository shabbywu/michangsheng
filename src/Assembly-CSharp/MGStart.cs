using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001C4 RID: 452
public class MGStart : MonoBehaviour
{
	// Token: 0x060012BE RID: 4798 RVA: 0x0007518F File Offset: 0x0007338F
	private void Start()
	{
		SceneManager.LoadScene("Mainmenu");
		SceneManager.LoadSceneAsync("login", 1);
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}
}
