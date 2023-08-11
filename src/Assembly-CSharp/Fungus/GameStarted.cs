using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using script.YarnEditor.Manager;

namespace Fungus;

[EventHandlerInfo("", "Game Started", "The block will execute when the game starts playing.")]
[AddComponentMenu("")]
public class GameStarted : EventHandler
{
	[Tooltip("Wait for a number of frames after startup before executing the Block. Can help fix startup order issues.")]
	[SerializeField]
	protected int waitForFrames = 1;

	protected virtual void Start()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		StoryManager inst = StoryManager.Inst;
		Scene activeScene = SceneManager.GetActiveScene();
		if (inst.CheckTrigger(((Scene)(ref activeScene)).name))
		{
			StoryManager.Inst.OldTalk = new UnityAction(CheckLater);
		}
		else
		{
			CheckLater();
		}
	}

	public void CheckLater()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.StreamData.FungusSaveMgr.IsNeedStop || !StoryManager.Inst.IsEnd)
		{
			return;
		}
		if (Tools.instance.getPlayer().StreamData.FungusSaveMgr.SaveFungusData != null)
		{
			FungusSaveMgr fungusSaveMgr = Tools.instance.getPlayer().StreamData.FungusSaveMgr;
			if (fungusSaveMgr.StopTalkName == parentBlock.GetFlowchart().GetParentName())
			{
				fungusSaveMgr.StopTalkName = "";
				return;
			}
		}
		player.StreamData.FungusSaveMgr.LastIsEnd = player.StreamData.FungusSaveMgr.IsEnd();
		player.StreamData.FungusSaveMgr.CurCommand = null;
		((MonoBehaviour)this).StartCoroutine(GameStartCoroutine());
	}

	protected virtual IEnumerator GameStartCoroutine()
	{
		for (int frameCount = waitForFrames; frameCount > 0; frameCount--)
		{
			yield return (object)new WaitForEndOfFrame();
		}
		ExecuteBlock();
	}
}
