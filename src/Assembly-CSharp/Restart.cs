using System;
using UnityEngine;

// Token: 0x0200079B RID: 1947
public class Restart : MonoBehaviour
{
	// Token: 0x06003183 RID: 12675 RVA: 0x0018A748 File Offset: 0x00188948
	private void Start()
	{
		this.controller = GameObject.Find("GameController").GetComponent<GameController>();
		base.transform.Find("Button").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.RestartGame)));
	}

	// Token: 0x06003184 RID: 12676 RVA: 0x000243FD File Offset: 0x000225FD
	public void SetTimeToNext(float sec)
	{
		base.Invoke("Next", sec);
	}

	// Token: 0x06003185 RID: 12677 RVA: 0x0018A7A0 File Offset: 0x001889A0
	private void RestartGame()
	{
		this.controller.BackToDeck();
		this.controller.DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		Object.Destroy(GameObject.Find("InteractionPanel").gameObject);
		Object.Destroy(GameObject.Find("ScenePanel").gameObject);
		Object.Destroy(GameObject.Find("BackgroundPanel").gameObject);
		Object.Destroy(base.gameObject);
		OrderController.Instance.ResetButton();
		OrderController.Instance.ResetSmartCard();
		GameObject gameObject = NGUITools.AddChild(UICamera.mainCamera.gameObject, (GameObject)Resources.Load("StartPanel"));
		gameObject.AddComponent<Menu>();
		gameObject.transform.Find("NoticeLabel").gameObject.SetActive(true);
	}

	// Token: 0x06003186 RID: 12678 RVA: 0x0018A868 File Offset: 0x00188A68
	private void Next()
	{
		this.controller.BackToDeck();
		this.controller.DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		GameObject.Find("InteractionPanel").transform.Find("DealBtn").gameObject.SetActive(true);
		Object.Destroy(base.gameObject);
		this.ResetDisplay();
	}

	// Token: 0x06003187 RID: 12679 RVA: 0x0018A8CC File Offset: 0x00188ACC
	private void ResetDisplay()
	{
		for (int i = 1; i < 4; i++)
		{
			CharacterType characterType = (CharacterType)i;
			if (GameObject.Find(characterType.ToString()))
			{
				GameController.UpdateLeftCardsCount((CharacterType)i, 0);
				GameController.UpdateIndentity((CharacterType)i, Identity.Farmer);
			}
		}
	}

	// Token: 0x04002DC8 RID: 11720
	private GameController controller;
}
