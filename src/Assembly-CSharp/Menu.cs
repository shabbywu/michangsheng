using System;
using UnityEngine;

// Token: 0x02000509 RID: 1289
public class Menu : MonoBehaviour
{
	// Token: 0x0600297A RID: 10618 RVA: 0x0013D33C File Offset: 0x0013B53C
	private void Start()
	{
		base.transform.Find("Easy").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.StartEasyGame)));
		base.transform.Find("Normal").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.StartNormalGame)));
		this.controller = GameObject.Find("GameController").GetComponent<GameController>();
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x0013D3C8 File Offset: 0x0013B5C8
	private void StartEasyGame()
	{
		this.controller.InitInteraction();
		this.controller.InitScene();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x0013D3EB File Offset: 0x0013B5EB
	private void StartNormalGame()
	{
		this.controller.Multiples = 2;
		this.controller.InitInteraction();
		this.controller.InitScene();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040025DF RID: 9695
	private GameController controller;
}
