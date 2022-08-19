using System;
using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000711 RID: 1809
	public class AvatarUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		// Token: 0x060039F0 RID: 14832 RVA: 0x0018D214 File Offset: 0x0018B414
		public void Init(PaiMaiAvatar data)
		{
			this._avatar = data;
			this._activeGo = base.transform.Find("Active").gameObject;
			this._noActiveGo = base.transform.Find("NoActive").gameObject;
			this._say = base.transform.Find("Say").GetComponent<PaiMaiSay>();
			base.transform.Find("NoActive/Name").GetComponent<Text>().text = data.Name;
			base.transform.Find("NoActive/Title/TitleText").GetComponent<Text>().text = data.Title;
			base.transform.Find("NoActive/Head/Viewport/Content/FaceBase/SkeletonGraphic").GetComponent<PlayerSetRandomFace>().SetNPCFace(data.NpcId);
			base.transform.Find("Active/Name").GetComponent<Text>().text = data.Name;
			base.transform.Find("Active/Title/TitleText").GetComponent<Text>().text = data.Title;
			base.transform.Find("Active/Head/Viewport/Content/FaceBase/SkeletonGraphic").GetComponent<PlayerSetRandomFace>().SetNPCFace(data.NpcId);
			this._state = base.transform.Find("Active/State/Text").GetComponent<Text>();
			this._stateImg = base.transform.Find("Active/State/img").GetComponent<Image>();
			this._selected = base.transform.Find("Active/BG/Selected").gameObject;
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x0018D38A File Offset: 0x0018B58A
		public void SayWord(string msg)
		{
			this._say.SayWord(msg, null, 2f);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x0018D3A0 File Offset: 0x0018B5A0
		public void SetState(PaiMaiAvatar.StateType state, bool isSay = false)
		{
			if (state == PaiMaiAvatar.StateType.放弃)
			{
				this._activeGo.SetActive(false);
				this._noActiveGo.SetActive(true);
			}
			else
			{
				this._activeGo.SetActive(true);
				this._noActiveGo.SetActive(false);
				this._stateImg.sprite = SingletonMono<PaiMaiUiMag>.Instance.StateSprites[state - PaiMaiAvatar.StateType.略感兴趣];
				this._state.text = string.Concat(new string[]
				{
					"<color=#",
					SingletonMono<PaiMaiUiMag>.Instance.StateColors[state],
					">",
					state.ToString(),
					"</color>"
				});
			}
			if (isSay)
			{
				int state2 = (int)this._avatar.State;
				int num = (int)state;
				int key = SingletonMono<PaiMaiUiMag>.Instance.WordDict[state2 * 10 + num][Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.WordDict[state2 * 10 + num].Count - 1)];
				this.SayWord(PaiMaiDuiHuaBiao.DataDict[key].Text);
			}
			this._avatar.State = state;
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x0018D4C9 File Offset: 0x0018B6C9
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this._avatar.CanSelect)
			{
				return;
			}
			this._selected.SetActive(true);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x0018D4E5 File Offset: 0x0018B6E5
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this._avatar.CanSelect)
			{
				return;
			}
			this._selected.SetActive(false);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x0018D501 File Offset: 0x0018B701
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!this._avatar.CanSelect)
			{
				return;
			}
			this._selected.SetActive(false);
			this._avatar.Select();
		}

		// Token: 0x040031FC RID: 12796
		private PaiMaiAvatar _avatar;

		// Token: 0x040031FD RID: 12797
		private GameObject _activeGo;

		// Token: 0x040031FE RID: 12798
		private GameObject _noActiveGo;

		// Token: 0x040031FF RID: 12799
		private Image _stateImg;

		// Token: 0x04003200 RID: 12800
		private Text _state;

		// Token: 0x04003201 RID: 12801
		private PaiMaiSay _say;

		// Token: 0x04003202 RID: 12802
		private GameObject _selected;
	}
}
