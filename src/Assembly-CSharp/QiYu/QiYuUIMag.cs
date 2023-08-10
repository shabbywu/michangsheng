using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QiYu;

public class QiYuUIMag : MonoBehaviour
{
	[SerializeField]
	private Transform Panel;

	[SerializeField]
	private Text EventName;

	[SerializeField]
	private Text EventContent;

	[SerializeField]
	private Transform OptionList;

	[SerializeField]
	private GameObject Option;

	[SerializeField]
	private GameObject OkBtn;

	public int EventId;

	public static QiYuUIMag Inst;

	public void Show(int id)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Inst = this;
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(Panel, Vector3.one, 0.5f);
		EventId = id;
		PanelMamager.CanOpenOrClose = false;
		Tools.canClickFlag = false;
		Init();
	}

	private void Init()
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		AllMapShiJianOptionJsonData data = AllMapShiJianOptionJsonData.DataDict[EventId];
		EventName.text = data.EventName;
		EventContent.text = "\u3000\u3000" + data.desc.Replace("{huanhang}", "\n");
		if (data.option1 > 0)
		{
			Option.Inst(OptionList).GetComponent<QiYuOption>().Init(data.optionDesc1, (UnityAction)delegate
			{
				OptionAction(data.option1);
				((Component)OptionList).gameObject.SetActive(false);
			});
		}
		if (data.option2 > 0)
		{
			Option.Inst(OptionList).GetComponent<QiYuOption>().Init(data.optionDesc2, (UnityAction)delegate
			{
				OptionAction(data.option2);
				((Component)OptionList).gameObject.SetActive(false);
			});
		}
		if (data.option3 > 0)
		{
			Option.Inst(OptionList).GetComponent<QiYuOption>().Init(data.optionDesc3, (UnityAction)delegate
			{
				OptionAction(data.option3);
				((Component)OptionList).gameObject.SetActive(false);
			});
		}
	}

	private void OptionAction(int optionId)
	{
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		AllMapOptionJsonData data = AllMapOptionJsonData.DataDict[optionId];
		Avatar player = Tools.instance.getPlayer();
		if (data.value1 > 0)
		{
			player.AddTime(data.value1);
		}
		if (data.value2 != 0)
		{
			player.AddMoney(data.value2);
		}
		if (data.value3 != 0)
		{
			player.addEXP(data.value3);
		}
		if (data.value4 != 0)
		{
			player.xinjin += data.value4;
		}
		if (data.value5 != 0)
		{
			player.AddHp(data.value5);
		}
		if (data.value6.Count > 0)
		{
			for (int i = 0; i < data.value6.Count; i++)
			{
				player.addItem(data.value6[i], data.value7[i], Tools.CreateItemSeid(data.value6[i]));
			}
		}
		if (data.value8 > 0)
		{
			OkBtn.GetComponent<FpBtn>().mouseUpEvent.AddListener((UnityAction)delegate
			{
				ResManager.inst.LoadTalk("TalkPrefab/Talk" + data.value8).Inst();
			});
		}
		if (data.value10 > 0)
		{
			player.wuDaoMag.AddLingGuangByJsonID(data.value10);
		}
		if (data.value9 > 0)
		{
			ResManager.inst.LoadPrefab("QiYuPanel").Inst().GetComponent<QiYuUIMag>()
				.Show(data.value9);
			Close();
		}
		EventContent.text = "\u3000\u3000" + data.desc.Replace("{huanhang}", "\n");
		OkBtn.SetActive(true);
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnDestroy()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.canClickFlag = true;
		Inst = null;
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)27) && OkBtn.activeSelf)
		{
			Close();
		}
	}
}
