using System;
using UnityEngine;

// Token: 0x0200050A RID: 1290
public class Restart : MonoBehaviour
{
	// Token: 0x0600297E RID: 10622 RVA: 0x0013D41C File Offset: 0x0013B61C
	private void Start()
	{
		this.controller = GameObject.Find("GameController").GetComponent<GameController>();
		base.transform.Find("Button").gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.RestartGame)));
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x0013D473 File Offset: 0x0013B673
	public void SetTimeToNext(float sec)
	{
		base.Invoke("Next", sec);
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x0013D484 File Offset: 0x0013B684
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

	// Token: 0x06002981 RID: 10625 RVA: 0x0013D54C File Offset: 0x0013B74C
	private void Next()
	{
		this.controller.BackToDeck();
		this.controller.DestroyAllSprites();
		DeskCardsCache.Instance.Clear();
		GameObject.Find("InteractionPanel").transform.Find("DealBtn").gameObject.SetActive(true);
		Object.Destroy(base.gameObject);
		this.ResetDisplay();
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x0013D5B0 File Offset: 0x0013B7B0
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

	// Token: 0x040025E0 RID: 9696
	private GameController controller;
}
