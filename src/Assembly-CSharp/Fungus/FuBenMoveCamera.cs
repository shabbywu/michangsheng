using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F10 RID: 3856
	[CommandInfo("YSFuBen", "FuBenMoveCamera", "移动摄像机位置", 0)]
	[AddComponentMenu("")]
	public class FuBenMoveCamera : Command
	{
		// Token: 0x06006D6B RID: 28011 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x002A32DC File Offset: 0x002A14DC
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(this.MapID.Value))
			{
				Component component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
				this.wait();
				Vector2 vector = AllMapManage.instance.mapIndex[this.MapID.Value].transform.position;
				TweenSettingsExtensions.SetSpeedBased<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(component.transform, vector, 30f, false)).onComplete = delegate()
				{
					this.removeWait();
				};
			}
			this.Continue();
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x002A3394 File Offset: 0x002A1594
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06006D6E RID: 28014 RVA: 0x002A33C4 File Offset: 0x002A15C4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B28 RID: 23336
		[Tooltip("场景名称")]
		[SerializeField]
		protected string ScenceName;

		// Token: 0x04005B29 RID: 23337
		[Tooltip("摄像机移动地点的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MapID;

		// Token: 0x04005B2A RID: 23338
		[VariableProperty(new Type[]
		{
			typeof(FloatVariable)
		})]
		[Tooltip("摄像机移动速度")]
		[SerializeField]
		protected FloatVariable speed;
	}
}
