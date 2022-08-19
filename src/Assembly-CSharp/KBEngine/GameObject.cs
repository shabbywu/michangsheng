using System;

namespace KBEngine
{
	// Token: 0x02000C77 RID: 3191
	public class GameObject : if_Entity_error_use______git_submodule_update_____kbengine_plugins_______open_this_file_and_I_will_tell_you
	{
		// Token: 0x06005812 RID: 22546 RVA: 0x00248F4C File Offset: 0x0024714C
		public virtual void onHPChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP");
			Event.fireOut("set_HP", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x00246801 File Offset: 0x00244A01
		public virtual void onMPChanged(int oldValue)
		{
			this.getDefinedProperty("MP");
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x00248F80 File Offset: 0x00247180
		public virtual void onHP_MaxChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP_Max");
			Event.fireOut("set_HP_Max", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x00248FB1 File Offset: 0x002471B1
		public virtual void onMP_MaxChanged(int oldValue)
		{
			this.getDefinedProperty("MP_Max");
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x00248FC0 File Offset: 0x002471C0
		public virtual void onLevelChanged(ushort oldValue)
		{
			object definedProperty = this.getDefinedProperty("level");
			Event.fireOut("set_level", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x00248FF4 File Offset: 0x002471F4
		public virtual void onNameChanged(string oldValue)
		{
			object definedProperty = this.getDefinedProperty("name");
			Event.fireOut("set_name", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x00249028 File Offset: 0x00247228
		public virtual void onStateChanged(sbyte oldValue)
		{
			object definedProperty = this.getDefinedProperty("state");
			Event.fireOut("set_state", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSpaceUTypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x00249059 File Offset: 0x00247259
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
			this.getDefinedProperty("moveSpeed");
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x00249067 File Offset: 0x00247267
		public virtual void set_modelScale(object old)
		{
			this.getDefinedProperty("modelScale");
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x00249075 File Offset: 0x00247275
		public virtual void set_modelID(object old)
		{
			this.getDefinedProperty("modelID");
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void set_forbids(object old)
		{
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x00249084 File Offset: 0x00247284
		public virtual void recvDamage(int attackerID, int skillID, int damageType, int damage)
		{
			Entity entity = KBEngineApp.app.findEntity(attackerID);
			Event.fireOut("recvDamage", new object[]
			{
				this,
				entity,
				skillID,
				damageType,
				damage
			});
		}
	}
}
