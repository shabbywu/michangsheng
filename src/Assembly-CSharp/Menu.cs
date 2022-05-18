using System;
using UnityEngine;

// Token: 0x0200079A RID: 1946
public class Menu : MonoBehaviour
{
	// Token: 0x0600317F RID: 12671 RVA: 0x0018A6BC File Offset: 0x001888BC
	private void Start()
	{
		base.transform.Find("Easy").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.StartEasyGame)));
		base.transform.Find("Normal").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.StartNormalGame)));
		this.controller = GameObject.Find("GameController").GetComponent<GameController>();
	}

	// Token: 0x06003180 RID: 12672 RVA: 0x000243AB File Offset: 0x000225AB
	private void StartEasyGame()
	{
		this.controller.InitInteraction();
		this.controller.InitScene();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06003181 RID: 12673 RVA: 0x000243CE File Offset: 0x000225CE
	private void StartNormalGame()
	{
		this.controller.Multiples = 2;
		this.controller.InitInteraction();
		this.controller.InitScene();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04002DC7 RID: 11719
	private GameController controller;
}
