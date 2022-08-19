using System;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
	// Token: 0x02000726 RID: 1830
	public class RunAwayUI : MonoBehaviour, IESCClose
	{
		// Token: 0x06003A60 RID: 14944 RVA: 0x00190F94 File Offset: 0x0018F194
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

		// Token: 0x06003A61 RID: 14945 RVA: 0x0019112C File Offset: 0x0018F32C
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

		// Token: 0x06003A62 RID: 14946 RVA: 0x001911C0 File Offset: 0x0018F3C0
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

		// Token: 0x06003A63 RID: 14947 RVA: 0x001912DC File Offset: 0x0018F4DC
		public void Close()
		{
			Tools.canClickFlag = true;
			Object.Destroy(base.gameObject);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x001912FA File Offset: 0x0018F4FA
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003288 RID: 12936
		[SerializeField]
		private Text Desc;

		// Token: 0x04003289 RID: 12937
		private Avatar avatar;

		// Token: 0x0400328A RID: 12938
		[SerializeField]
		private Transform Panel;
	}
}
