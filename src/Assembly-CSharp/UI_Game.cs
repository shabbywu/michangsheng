using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000656 RID: 1622
[Obsolete]
public class UI_Game : MonoBehaviour
{
	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06002863 RID: 10339 RVA: 0x0013BD88 File Offset: 0x00139F88
	// (remove) Token: 0x06002864 RID: 10340 RVA: 0x0013BDBC File Offset: 0x00139FBC
	public static event UI_Game.PickDelegate ItemPick;

	// Token: 0x04002209 RID: 8713
	public InputField input_content;

	// Token: 0x0400220A RID: 8714
	public Transform tran_text;

	// Token: 0x0400220B RID: 8715
	public Scrollbar sb_vertical;

	// Token: 0x0400220C RID: 8716
	public Text text_pos;

	// Token: 0x0400220D RID: 8717
	public Transform tran_relive;

	// Token: 0x0400220E RID: 8718
	public GameObject text_error;

	// Token: 0x0400220F RID: 8719
	public static UI_Game instence;

	// Token: 0x04002210 RID: 8720
	private Text text_content;

	// Token: 0x04002211 RID: 8721
	public GameObject inventory;

	// Token: 0x04002212 RID: 8722
	public GameObject characterSystem;

	// Token: 0x04002213 RID: 8723
	public GameObject statePanel;

	// Token: 0x04002214 RID: 8724
	public GameObject craftSystem;

	// Token: 0x04002215 RID: 8725
	private Inventory craftSystemInventory;

	// Token: 0x04002216 RID: 8726
	private Inventory mainInventory;

	// Token: 0x04002217 RID: 8727
	private Inventory characterSystemInventory;

	// Token: 0x04002218 RID: 8728
	private UI_AvatarState avatarState;

	// Token: 0x04002219 RID: 8729
	private Tooltip toolTip;

	// Token: 0x0400221A RID: 8730
	public GameObject FP_Camera;

	// Token: 0x0400221B RID: 8731
	public GameObject MainGameUI;

	// Token: 0x0400221C RID: 8732
	public GameObject LVUPUI;

	// Token: 0x0400221D RID: 8733
	public GameObject HUDUI;

	// Token: 0x0400221E RID: 8734
	public GameObject backgroundBtn;

	// Token: 0x0400221F RID: 8735
	public GameObject talkUI;

	// Token: 0x04002220 RID: 8736
	public GameObject iteamCollect;

	// Token: 0x04002221 RID: 8737
	public GameObject in_GameUI;

	// Token: 0x04002222 RID: 8738
	public GameObject Male_Player;

	// Token: 0x04002223 RID: 8739
	private int talkUIStatus = 1;

	// Token: 0x04002224 RID: 8740
	public Dictionary<string, GameObject> skillBtn;

	// Token: 0x02000657 RID: 1623
	// (Invoke) Token: 0x06002868 RID: 10344
	public delegate void PickDelegate();
}
