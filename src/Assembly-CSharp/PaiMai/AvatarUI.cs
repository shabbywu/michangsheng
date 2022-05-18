using System;
using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A61 RID: 2657
	public class AvatarUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		// Token: 0x0600447C RID: 17532 RVA: 0x001D49B8 File Offset: 0x001D2BB8
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

		// Token: 0x0600447D RID: 17533 RVA: 0x00030FAD File Offset: 0x0002F1AD
		public void SayWord(string msg)
		{
			this._say.SayWord(msg, null, 2f);
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x001D4B30 File Offset: 0x001D2D30
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

		// Token: 0x0600447F RID: 17535 RVA: 0x00030FC1 File Offset: 0x0002F1C1
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this._avatar.CanSelect)
			{
				return;
			}
			this._selected.SetActive(true);
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x00030FDD File Offset: 0x0002F1DD
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this._avatar.CanSelect)
			{
				return;
			}
			this._selected.SetActive(false);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x00030FF9 File Offset: 0x0002F1F9
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!this._avatar.CanSelect)
			{
				return;
			}
			this._selected.SetActive(false);
			this._avatar.Select();
		}

		// Token: 0x04003C83 RID: 15491
		private PaiMaiAvatar _avatar;

		// Token: 0x04003C84 RID: 15492
		private GameObject _activeGo;

		// Token: 0x04003C85 RID: 15493
		private GameObject _noActiveGo;

		// Token: 0x04003C86 RID: 15494
		private Image _stateImg;

		// Token: 0x04003C87 RID: 15495
		private Text _state;

		// Token: 0x04003C88 RID: 15496
		private PaiMaiSay _say;

		// Token: 0x04003C89 RID: 15497
		private GameObject _selected;
	}
}
