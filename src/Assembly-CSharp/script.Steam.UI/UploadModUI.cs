using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using script.Steam.Utils;

namespace script.Steam.UI;

public class UploadModUI : BasePanel
{
	private bool isInit;

	public Dropdown VisibilitySelect;

	public Text ModPath;

	public Text ImgPath;

	public InputField ModName;

	public TMP_InputField ModDesc;

	public List<Toggle> Toggles;

	public Text Progress;

	public FpBtn UpLoadBtn;

	public Image UpLoadingImg;

	public UploadModProgress UploadModProgress;

	public UploadCtr UploadCtr { get; private set; }

	public UploadModUI(GameObject gameObject)
	{
		_go = gameObject;
		UploadCtr = new UploadCtr();
		UploadModProgress = Get<UploadModProgress>("上传过程");
	}

	public override void Show()
	{
		if (!isInit)
		{
			Init();
			isInit = true;
		}
		WorkShopMag.Inst.ModPoolUI.Hide();
		base.Show();
	}

	private void Init()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		VisibilitySelect = Get<Dropdown>("可见性/选项");
		((UnityEvent<int>)(object)VisibilitySelect.onValueChanged).AddListener((UnityAction<int>)delegate(int value)
		{
			if (UploadCtr.WorkShopItem == null)
			{
				UIPopTip.Inst.Pop("请先选择Mod文件");
			}
			else
			{
				UploadCtr.WorkShopItem.Visibility = value;
			}
		});
		ModPath = Get<Text>("Mod文件夹/路径/Value");
		ImgPath = Get<Text>("封面/路径/Value");
		ModName = Get<InputField>("标题/Value");
		ModDesc = Get<TMP_InputField>("描述/Value");
		Progress = Get<Text>("上传过程/Value");
		CreateTags();
		Get<FpBtn>("Mod文件夹/选择").mouseUpEvent.AddListener(new UnityAction(UploadCtr.SelectMod));
		Get<FpBtn>("封面/选择").mouseUpEvent.AddListener(new UnityAction(UploadCtr.SelectImg));
		UpLoadBtn = Get<FpBtn>("上传过程/上传按钮");
		UpLoadBtn.mouseUpEvent.AddListener(new UnityAction(UploadCtr.ClickUpload));
		UpLoadingImg = Get<Image>("上传过程/Mask");
	}

	public override void Hide()
	{
		if (isInit && UploadModProgress.IsUploading)
		{
			UIPopTip.Inst.Pop("正在上传Mod中，禁止关闭");
		}
		else
		{
			base.Hide();
		}
	}

	private void CreateTags()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Toggles = new List<Toggle>();
		GameObject val = Get("类型/0");
		Transform parent = val.transform.parent;
		float num = val.transform.localPosition.x;
		float y = val.transform.localPosition.y;
		foreach (string tag in WorkShopMag.Tags)
		{
			GameObject obj = val.Inst(parent);
			obj.transform.localPosition = Vector2.op_Implicit(new Vector2(num, y));
			Toggle component = obj.GetComponent<Toggle>();
			((Object)obj).name = tag;
			((UnityEvent<bool>)(object)component.onValueChanged).AddListener((UnityAction<bool>)delegate(bool arg0)
			{
				if (arg0)
				{
					UploadCtr.WorkShopItem.AddTags(tag);
				}
				else
				{
					UploadCtr.WorkShopItem.RemoveTag(tag);
				}
			});
			Toggles.Add(component);
			((Component)obj.transform.GetChild(1)).GetComponent<Text>().SetText(tag);
			obj.SetActive(true);
			num += 146f;
		}
	}

	public void UpdateUI()
	{
		VisibilitySelect.value = UploadCtr.WorkShopItem.Visibility;
		ModPath.SetTextWithEllipsis(UploadCtr.WorkShopItem.ModPath);
		ImgPath.SetTextWithEllipsis(UploadCtr.WorkShopItem.ImgPath);
		ModName.text = UploadCtr.WorkShopItem.Title;
		ModDesc.text = UploadCtr.WorkShopItem.Des;
		foreach (Toggle toggle in Toggles)
		{
			if (UploadCtr.WorkShopItem.Tags.Contains(((Object)((Component)toggle).gameObject).name))
			{
				toggle.isOn = true;
			}
			else
			{
				toggle.isOn = false;
			}
		}
	}
}
