using System.Diagnostics;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;
using script.Steam.Utils;

namespace script.Steam.UI;

public class MoreModInfoUI : BasePanel
{
	private bool isInit;

	private ulong modId;

	public Text ModName;

	public Text ModTag;

	public Text Author;

	public Image Img;

	public Text DependencyPrefab;

	public Transform DependencyParent;

	public Text Desc;

	public FpBtn Up;

	public FpBtn Down;

	public FpBtn OpenFile;

	public MoreModInfoUI(GameObject gameObject)
	{
		_go = gameObject;
	}

	private void Init()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		ModName = Get<Text>("名称/Value");
		Up = Get<FpBtn>("点赞");
		Down = Get<FpBtn>("点踩");
		OpenFile = Get<FpBtn>("打开文件夹");
		OpenFile.mouseUpEvent.AddListener(new UnityAction(OpenModFile));
		Up.mouseUpEvent.AddListener((UnityAction)delegate
		{
			WorkShopMag.Inst.ModMagUI.Ctr.VoteMod(modId, flag: true);
			Up.SetCanClick(flag: false);
			Down.SetCanClick(flag: true);
		});
		Down.mouseUpEvent.AddListener((UnityAction)delegate
		{
			WorkShopMag.Inst.ModMagUI.Ctr.VoteMod(modId, flag: false);
			Down.SetCanClick(flag: false);
			Up.SetCanClick(flag: true);
		});
		ModTag = Get<Text>("类型/Value");
		Author = Get<Text>("作者/Value");
		Img = Get<Image>("封面/Value");
		DependencyPrefab = Get<Text>("依赖模组/Scroll View/Viewport/Content/Value");
		DependencyParent = ((Component)DependencyPrefab).transform.parent;
		Desc = Get<Text>("mod描述/Scroll View/Viewport/Value");
	}

	private void OpenModFile()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo($"{WorkshopTool.WorkshopRootPath}/{modId}");
		if (!directoryInfo.Exists)
		{
			UIPopTip.Inst.Pop("没有找到该模组文件夹");
		}
		else
		{
			Process.Start("Explorer.exe", directoryInfo.FullName);
		}
	}

	private void UpdateIsUpOrDown(ulong id)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Up.SetCanClick(flag: false);
		Down.SetCanClick(flag: false);
		SteamAPICall_t userItemVote = SteamUGC.GetUserItemVote(new PublishedFileId_t(id));
		CallResult<GetUserItemVoteResult_t>.Create((APIDispatchDelegate<GetUserItemVoteResult_t>)null).Set(userItemVote, (APIDispatchDelegate<GetUserItemVoteResult_t>)delegate(GetUserItemVoteResult_t t, bool failure)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if ((int)t.m_eResult == 1)
			{
				if (t.m_bVotedUp)
				{
					Up.SetCanClick(flag: false);
				}
				else
				{
					Up.SetCanClick(flag: true);
				}
				if (t.m_bVotedDown)
				{
					Down.SetCanClick(flag: false);
				}
				else
				{
					Down.SetCanClick(flag: true);
				}
			}
		});
	}

	public void Show(ModInfo modInfo)
	{
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		if (!isInit)
		{
			Init();
			isInit = true;
		}
		modId = modInfo.Id;
		UpdateIsUpOrDown(modInfo.Id);
		ClearDependency();
		ModName.SetTextWithEllipsis(modInfo.Name);
		ModTag.SetTextWithEllipsis(modInfo.Tags);
		Author.SetTextWithEllipsis(modInfo.Author);
		modInfo.ShowImg(delegate(Sprite sprite)
		{
			Img.sprite = sprite;
		});
		if (WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(modInfo.Id))
		{
			((Component)OpenFile).gameObject.SetActive(true);
		}
		else
		{
			((Component)OpenFile).gameObject.SetActive(false);
		}
		foreach (ulong dependency in modInfo.DependencyList)
		{
			GameObject gameObject = ((Component)DependencyPrefab).gameObject.Inst(DependencyParent).gameObject;
			gameObject.SetActive(true);
			Text component = gameObject.GetComponent<Text>();
			string value = ((!WorkShopMag.Inst.ModInfoDict.ContainsKey(dependency)) ? dependency.ToString() : WorkShopMag.Inst.ModInfoDict[dependency].Name);
			if (WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(dependency))
			{
				((Graphic)component).color = Color32.op_Implicit(new Color32((byte)60, (byte)115, (byte)111, byte.MaxValue));
			}
			else
			{
				((Graphic)component).color = Color32.op_Implicit(new Color32((byte)167, (byte)98, (byte)48, byte.MaxValue));
			}
			component.SetTextWithEllipsis(value);
		}
		Desc.SetText(modInfo.Desc);
		_go.SetActive(true);
	}

	private void ClearDependency()
	{
		Tools.ClearChild(DependencyParent);
	}
}
