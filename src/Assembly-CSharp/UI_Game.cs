using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000489 RID: 1161
[Obsolete]
public class UI_Game : MonoBehaviour
{
	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06002496 RID: 9366 RVA: 0x000FC880 File Offset: 0x000FAA80
	// (remove) Token: 0x06002497 RID: 9367 RVA: 0x000FC8B4 File Offset: 0x000FAAB4
	public static event UI_Game.PickDelegate ItemPick;

	// Token: 0x04001D2C RID: 7468
	public InputField input_content;

	// Token: 0x04001D2D RID: 7469
	public Transform tran_text;

	// Token: 0x04001D2E RID: 7470
	public Scrollbar sb_vertical;

	// Token: 0x04001D2F RID: 7471
	public Text text_pos;

	// Token: 0x04001D30 RID: 7472
	public Transform tran_relive;

	// Token: 0x04001D31 RID: 7473
	public GameObject text_error;

	// Token: 0x04001D32 RID: 7474
	public static UI_Game instence;

	// Token: 0x04001D33 RID: 7475
	private Text text_content;

	// Token: 0x04001D34 RID: 7476
	public GameObject inventory;

	// Token: 0x04001D35 RID: 7477
	public GameObject characterSystem;

	// Token: 0x04001D36 RID: 7478
	public GameObject statePanel;

	// Token: 0x04001D37 RID: 7479
	public GameObject craftSystem;

	// Token: 0x04001D38 RID: 7480
	private Inventory craftSystemInventory;

	// Token: 0x04001D39 RID: 7481
	private Inventory mainInventory;

	// Token: 0x04001D3A RID: 7482
	private Inventory characterSystemInventory;

	// Token: 0x04001D3B RID: 7483
	private UI_AvatarState avatarState;

	// Token: 0x04001D3C RID: 7484
	private Tooltip toolTip;

	// Token: 0x04001D3D RID: 7485
	public GameObject FP_Camera;

	// Token: 0x04001D3E RID: 7486
	public GameObject MainGameUI;

	// Token: 0x04001D3F RID: 7487
	public GameObject LVUPUI;

	// Token: 0x04001D40 RID: 7488
	public GameObject HUDUI;

	// Token: 0x04001D41 RID: 7489
	public GameObject backgroundBtn;

	// Token: 0x04001D42 RID: 7490
	public GameObject talkUI;

	// Token: 0x04001D43 RID: 7491
	public GameObject iteamCollect;

	// Token: 0x04001D44 RID: 7492
	public GameObject in_GameUI;

	// Token: 0x04001D45 RID: 7493
	public GameObject Male_Player;

	// Token: 0x04001D46 RID: 7494
	private int talkUIStatus = 1;

	// Token: 0x04001D47 RID: 7495
	public Dictionary<string, GameObject> skillBtn;

	// Token: 0x020013B6 RID: 5046
	// (Invoke) Token: 0x06007CA8 RID: 31912
	public delegate void PickDelegate();
}
