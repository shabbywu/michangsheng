using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Obsolete]
public class UI_Game : MonoBehaviour
{
	public delegate void PickDelegate();

	public InputField input_content;

	public Transform tran_text;

	public Scrollbar sb_vertical;

	public Text text_pos;

	public Transform tran_relive;

	public GameObject text_error;

	public static UI_Game instence;

	private Text text_content;

	public GameObject inventory;

	public GameObject characterSystem;

	public GameObject statePanel;

	public GameObject craftSystem;

	private Inventory craftSystemInventory;

	private Inventory mainInventory;

	private Inventory characterSystemInventory;

	private UI_AvatarState avatarState;

	private Tooltip toolTip;

	public GameObject FP_Camera;

	public GameObject MainGameUI;

	public GameObject LVUPUI;

	public GameObject HUDUI;

	public GameObject backgroundBtn;

	public GameObject talkUI;

	public GameObject iteamCollect;

	public GameObject in_GameUI;

	public GameObject Male_Player;

	private int talkUIStatus = 1;

	public Dictionary<string, GameObject> skillBtn;

	public static event PickDelegate ItemPick;
}
