using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PaiMai;

public class AvatarUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	private PaiMaiAvatar _avatar;

	private GameObject _activeGo;

	private GameObject _noActiveGo;

	private Image _stateImg;

	private Text _state;

	private PaiMaiSay _say;

	private GameObject _selected;

	public void Init(PaiMaiAvatar data)
	{
		_avatar = data;
		_activeGo = ((Component)((Component)this).transform.Find("Active")).gameObject;
		_noActiveGo = ((Component)((Component)this).transform.Find("NoActive")).gameObject;
		_say = ((Component)((Component)this).transform.Find("Say")).GetComponent<PaiMaiSay>();
		((Component)((Component)this).transform.Find("NoActive/Name")).GetComponent<Text>().text = data.Name;
		((Component)((Component)this).transform.Find("NoActive/Title/TitleText")).GetComponent<Text>().text = data.Title;
		((Component)((Component)this).transform.Find("NoActive/Head/Viewport/Content/FaceBase/SkeletonGraphic")).GetComponent<PlayerSetRandomFace>().SetNPCFace(data.NpcId);
		((Component)((Component)this).transform.Find("Active/Name")).GetComponent<Text>().text = data.Name;
		((Component)((Component)this).transform.Find("Active/Title/TitleText")).GetComponent<Text>().text = data.Title;
		((Component)((Component)this).transform.Find("Active/Head/Viewport/Content/FaceBase/SkeletonGraphic")).GetComponent<PlayerSetRandomFace>().SetNPCFace(data.NpcId);
		_state = ((Component)((Component)this).transform.Find("Active/State/Text")).GetComponent<Text>();
		_stateImg = ((Component)((Component)this).transform.Find("Active/State/img")).GetComponent<Image>();
		_selected = ((Component)((Component)this).transform.Find("Active/BG/Selected")).gameObject;
	}

	public void SayWord(string msg)
	{
		_say.SayWord(msg, null, 2f);
	}

	public void SetState(PaiMaiAvatar.StateType state, bool isSay = false)
	{
		if (state == PaiMaiAvatar.StateType.放弃)
		{
			_activeGo.SetActive(false);
			_noActiveGo.SetActive(true);
		}
		else
		{
			_activeGo.SetActive(true);
			_noActiveGo.SetActive(false);
			_stateImg.sprite = SingletonMono<PaiMaiUiMag>.Instance.StateSprites[(int)(state - 2)];
			_state.text = "<color=#" + SingletonMono<PaiMaiUiMag>.Instance.StateColors[state] + ">" + state.ToString() + "</color>";
		}
		if (isSay)
		{
			int state2 = (int)_avatar.State;
			int num = (int)state;
			int key = SingletonMono<PaiMaiUiMag>.Instance.WordDict[state2 * 10 + num][Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.WordDict[state2 * 10 + num].Count - 1)];
			SayWord(PaiMaiDuiHuaBiao.DataDict[key].Text);
		}
		_avatar.State = state;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (_avatar.CanSelect)
		{
			_selected.SetActive(true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (_avatar.CanSelect)
		{
			_selected.SetActive(false);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_avatar.CanSelect)
		{
			_selected.SetActive(false);
			_avatar.Select();
		}
	}
}
