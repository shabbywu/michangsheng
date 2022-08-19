using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A94 RID: 2708
	public class UITianJiDaBi2Player : MonoBehaviour
	{
		// Token: 0x06004BD4 RID: 19412 RVA: 0x00205014 File Offset: 0x00203214
		public void InitData(DaBiPlayer left, DaBiPlayer right, Match match)
		{
			this.LeftName.text = left.Name;
			this.RightName.text = right.Name;
			this.LeftFire.NumberText.text = left.BigScore.ToString();
			this.RightFire.NumberText.text = right.BigScore.ToString();
			this.LeftWinFail.text = "";
			this.RightWinFail.text = "";
			this.LeftWinFail.gameObject.SetActive(false);
			this.RightWinFail.gameObject.SetActive(false);
			this.LeftFire.SetNormal();
			this.RightFire.SetNormal();
			this.JianAnimCtl.Play("UITianJiDaBiJianIdleAnim");
			if (left.IsWanJia)
			{
				this.LeftName.color = new Color(0.078431375f, 0.39607844f, 0.3529412f);
			}
			if (right.IsWanJia)
			{
				this.RightName.color = new Color(0.078431375f, 0.39607844f, 0.3529412f);
			}
			this.LeftNameBtn.mouseEnterEvent.RemoveAllListeners();
			this.RightNameBtn.mouseEnterEvent.RemoveAllListeners();
			this.LeftNameBtn.mouseOutEvent.RemoveAllListeners();
			this.RightNameBtn.mouseOutEvent.RemoveAllListeners();
			this.LeftNameBtn.mouseUpEvent.RemoveAllListeners();
			this.RightNameBtn.mouseUpEvent.RemoveAllListeners();
			this.LeftNameBtn.mouseEnterEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					this.LeftNameHighlight.SetActive(true);
				}
			});
			this.RightNameBtn.mouseEnterEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					this.RightNameHiglight.SetActive(true);
				}
			});
			this.LeftNameBtn.mouseOutEvent.AddListener(delegate()
			{
				this.LeftNameHighlight.SetActive(false);
			});
			this.RightNameBtn.mouseOutEvent.AddListener(delegate()
			{
				this.RightNameHiglight.SetActive(false);
			});
			this.LeftNameBtn.mouseUpEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					UITianJiDaBiPlayerInfo.Show(left, match);
				}
			});
			this.RightNameBtn.mouseUpEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					UITianJiDaBiPlayerInfo.Show(right, match);
				}
			});
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00205278 File Offset: 0x00203478
		public void SetWinFail(DaBiPlayer left, DaBiPlayer right)
		{
			FightRecord fightRecord = left.FightRecords[left.FightRecords.Count - 1];
			FightRecord fightRecord2 = right.FightRecords[right.FightRecords.Count - 1];
			this.SetLeftWinFail(fightRecord.WinID == left.ID, true);
			this.SetRightWinFail(fightRecord2.WinID == right.ID, true);
			this.JianAnimCtl.Play("UITianJiDaBiJianEndAnim");
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x002052F4 File Offset: 0x002034F4
		public void SetLeftWinFail(bool win, bool showText = true)
		{
			if (win)
			{
				this.LeftWinFail.text = "胜";
				this.LeftWinFail.color = this.winColor;
				this.LeftFire.SetWin();
			}
			else
			{
				this.LeftWinFail.text = "负";
				this.LeftWinFail.color = this.failColor;
				this.LeftFire.SetFail();
			}
			this.LeftWinFail.gameObject.SetActive(showText);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00205370 File Offset: 0x00203570
		public void SetRightWinFail(bool win, bool showText = true)
		{
			if (win)
			{
				this.RightWinFail.text = "胜";
				this.RightWinFail.color = this.winColor;
				this.RightFire.SetWin();
			}
			else
			{
				this.RightWinFail.text = "负";
				this.RightWinFail.color = this.failColor;
				this.RightFire.SetFail();
			}
			this.RightWinFail.gameObject.SetActive(showText);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x002053EB File Offset: 0x002035EB
		public void PlayFightAnim(Action onAnimEnd)
		{
			base.StartCoroutine(this.FightAnimC(onAnimEnd));
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x002053FB File Offset: 0x002035FB
		private IEnumerator FightAnimC(Action onAnimEnd)
		{
			this.canClickName = false;
			float num = Random.Range(0f, 1f);
			yield return new WaitForSeconds(num);
			this.JianAnimCtl.Play("UITianJiDaBiJianFightAnim");
			this.LeftFire.FireAnim.Play("UITianJiDaBiFireFightAnim");
			this.RightFire.FireAnim.Play("UITianJiDaBiFireFightAnim");
			yield return new WaitForSeconds(2f);
			if (onAnimEnd != null)
			{
				onAnimEnd();
			}
			this.canClickName = true;
			yield break;
		}

		// Token: 0x04004AE6 RID: 19174
		public UITianJiDaBiFire LeftFire;

		// Token: 0x04004AE7 RID: 19175
		public UITianJiDaBiFire RightFire;

		// Token: 0x04004AE8 RID: 19176
		public GameObject LeftNameHighlight;

		// Token: 0x04004AE9 RID: 19177
		public GameObject RightNameHiglight;

		// Token: 0x04004AEA RID: 19178
		public FpBtn LeftNameBtn;

		// Token: 0x04004AEB RID: 19179
		public FpBtn RightNameBtn;

		// Token: 0x04004AEC RID: 19180
		public Text LeftName;

		// Token: 0x04004AED RID: 19181
		public Text RightName;

		// Token: 0x04004AEE RID: 19182
		public Text LeftWinFail;

		// Token: 0x04004AEF RID: 19183
		public Text RightWinFail;

		// Token: 0x04004AF0 RID: 19184
		public Animator JianAnimCtl;

		// Token: 0x04004AF1 RID: 19185
		private Color winColor = new Color(0.73333335f, 0.38431373f, 0.19607843f);

		// Token: 0x04004AF2 RID: 19186
		private Color failColor = new Color(0.30588236f, 0.39607844f, 0.49803922f);

		// Token: 0x04004AF3 RID: 19187
		private bool canClickName = true;
	}
}
