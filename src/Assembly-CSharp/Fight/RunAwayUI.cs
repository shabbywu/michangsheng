using System;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
	// Token: 0x02000A7D RID: 2685
	public class RunAwayUI : MonoBehaviour, IESCClose
	{
		// Token: 0x06004502 RID: 17666 RVA: 0x001D8488 File Offset: 0x001D6688
		public void Show()
		{
			this.avatar = Tools.instance.getPlayer();
			int num = this.avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(Tools.instance.MonstarID)]["dunSu"].n;
			if (num > 0 && num <= jsonData.instance.RunawayJsonData.Count - 1)
			{
				this.setRunawayText(num);
				this.avatar.AddTime((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunTime"].n, 0, 0);
				try
				{
					this.randomMapIndex((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunDistance"].n);
					goto IL_EF;
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
					goto IL_EF;
				}
			}
			if (num > 10)
			{
				this.setRunawayText(11);
			}
			IL_EF:
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			this.Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.Panel, Vector3.one, 0.5f);
			Tools.canClickFlag = false;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x001D8620 File Offset: 0x001D6820
		private void setRunawayText(int type)
		{
			int staticDunSu = Tools.instance.getPlayer().getStaticDunSu();
			string newValue = (staticDunSu > 0) ? ("运起" + Tools.instance.getStaticSkillName(staticDunSu, false) + "，") : "";
			RunawayJsonData runawayJsonData = RunawayJsonData.DataDict[type];
			string text = "\u3000\u3000" + runawayJsonData.Text;
			text = text.Replace("运起（dunshu），", newValue);
			text = text.Replace("X", runawayJsonData.RunTime.ToString());
			this.Desc.text = text;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x001D86B4 File Offset: 0x001D68B4
		private void randomMapIndex(int Num)
		{
			int nowMapIndex = this.avatar.NowMapIndex;
			int i = 0;
			while (i < Num)
			{
				BaseMapCompont baseMapCompont = AllMapManage.instance.mapIndex[this.avatar.NowMapIndex];
				if (i == 0)
				{
					int num = jsonData.instance.getRandom() % baseMapCompont.nextIndex.Count;
					goto IL_73;
				}
				if (baseMapCompont.nextIndex.Count > 1)
				{
					int num = jsonData.instance.getRandom() % (baseMapCompont.nextIndex.Count - 1);
					goto IL_73;
				}
				IL_D2:
				i++;
				continue;
				IL_73:
				int num2 = 0;
				foreach (int num3 in baseMapCompont.nextIndex)
				{
					if (nowMapIndex != num3)
					{
						int num;
						if (num == num2)
						{
							nowMapIndex = this.avatar.NowMapIndex;
							this.avatar.NowMapIndex = num3;
							break;
						}
						num2++;
					}
				}
				goto IL_D2;
			}
			AllMapManage.instance.mapIndex[this.avatar.NowMapIndex].AvatarMoveToThis();
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x0003161B File Offset: 0x0002F81B
		public void Close()
		{
			Tools.canClickFlag = true;
			Object.Destroy(base.gameObject);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00031639 File Offset: 0x0002F839
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003D23 RID: 15651
		[SerializeField]
		private Text Desc;

		// Token: 0x04003D24 RID: 15652
		private Avatar avatar;

		// Token: 0x04003D25 RID: 15653
		[SerializeField]
		private Transform Panel;
	}
}
