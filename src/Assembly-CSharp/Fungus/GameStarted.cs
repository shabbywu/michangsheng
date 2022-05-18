using System;
using System.Collections;
using KBEngine;
using script.YarnEditor.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x0200131B RID: 4891
	[EventHandlerInfo("", "Game Started", "The block will execute when the game starts playing.")]
	[AddComponentMenu("")]
	public class GameStarted : EventHandler
	{
		// Token: 0x06007736 RID: 30518 RVA: 0x002B4FA0 File Offset: 0x002B31A0
		protected virtual void Start()
		{
			if (StoryManager.Inst.CheckTrigger(SceneManager.GetActiveScene().name))
			{
				StoryManager.Inst.OldTalk = new UnityAction(this.CheckLater);
				return;
			}
			this.CheckLater();
		}

		// Token: 0x06007737 RID: 30519 RVA: 0x002B4FE4 File Offset: 0x002B31E4
		public void CheckLater()
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.StreamData.FungusSaveMgr.IsNeedStop)
			{
				return;
			}
			if (!StoryManager.Inst.IsEnd)
			{
				return;
			}
			if (Tools.instance.getPlayer().StreamData.FungusSaveMgr.SaveFungusData != null)
			{
				FungusSaveMgr fungusSaveMgr = Tools.instance.getPlayer().StreamData.FungusSaveMgr;
				if (fungusSaveMgr.StopTalkName == this.parentBlock.GetFlowchart().GetParentName())
				{
					fungusSaveMgr.StopTalkName = "";
					return;
				}
			}
			player.StreamData.FungusSaveMgr.CurCommand = null;
			base.StartCoroutine(this.GameStartCoroutine());
		}

		// Token: 0x06007738 RID: 30520 RVA: 0x000513B8 File Offset: 0x0004F5B8
		protected virtual IEnumerator GameStartCoroutine()
		{
			int num;
			for (int frameCount = this.waitForFrames; frameCount > 0; frameCount = num - 1)
			{
				yield return new WaitForEndOfFrame();
				num = frameCount;
			}
			this.ExecuteBlock();
			yield break;
		}

		// Token: 0x040067E7 RID: 26599
		[Tooltip("Wait for a number of frames after startup before executing the Block. Can help fix startup order issues.")]
		[SerializeField]
		protected int waitForFrames = 1;
	}
}
