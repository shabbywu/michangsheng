using System;

namespace KBEngine
{
	// Token: 0x02000B6C RID: 2924
	public class EntityCall
	{
		// Token: 0x060051BE RID: 20926 RVA: 0x00223557 File Offset: 0x00221757
		public EntityCall(int eid, string ename)
		{
			this.id = eid;
			this.className = ename;
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void __init__()
		{
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x00223578 File Offset: 0x00221778
		public virtual bool isBase()
		{
			return this.type == EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00223583 File Offset: 0x00221783
		public virtual bool isCell()
		{
			return this.type == EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00223590 File Offset: 0x00221790
		public Bundle newCall()
		{
			if (this.bundle == null)
			{
				this.bundle = ObjectPool<Bundle>.createObject();
			}
			if (this.isCell())
			{
				this.bundle.newMessage(Messages.messages["Baseapp_onRemoteCallCellMethodFromClient"]);
			}
			else
			{
				this.bundle.newMessage(Messages.messages["Entity_onRemoteMethodCall"]);
			}
			this.bundle.writeInt32(this.id);
			return this.bundle;
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x00223605 File Offset: 0x00221805
		public void sendCall(Bundle inbundle)
		{
			if (inbundle == null)
			{
				inbundle = this.bundle;
			}
			inbundle.send(KBEngineApp.app.networkInterface());
			if (inbundle == this.bundle)
			{
				this.bundle = null;
			}
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00223634 File Offset: 0x00221834
		public Bundle newCall(string methodName, ushort entitycomponentPropertyID = 0)
		{
			if (KBEngineApp.app.currserver == "loginapp")
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::newCall(",
					methodName,
					"), currserver=!",
					KBEngineApp.app.currserver
				}));
				return null;
			}
			ScriptModule scriptModule = null;
			if (!EntityDef.moduledefs.TryGetValue(this.className, out scriptModule))
			{
				Dbg.ERROR_MSG(this.className + "::newCall: entity-module(" + this.className + ") error, can not find from EntityDef.moduledefs");
				return null;
			}
			Method method;
			if (this.isCell())
			{
				method = scriptModule.cell_methods[methodName];
			}
			else
			{
				method = scriptModule.base_methods[methodName];
			}
			ushort methodUtype = method.methodUtype;
			this.newCall();
			this.bundle.writeUint16(entitycomponentPropertyID);
			this.bundle.writeUint16(methodUtype);
			return this.bundle;
		}

		// Token: 0x04004FA9 RID: 20393
		public int id;

		// Token: 0x04004FAA RID: 20394
		public string className = "";

		// Token: 0x04004FAB RID: 20395
		public EntityCall.ENTITYCALL_TYPE type;

		// Token: 0x04004FAC RID: 20396
		public Bundle bundle;

		// Token: 0x020015F3 RID: 5619
		public enum ENTITYCALL_TYPE
		{
			// Token: 0x040070E3 RID: 28899
			ENTITYCALL_TYPE_CELL,
			// Token: 0x040070E4 RID: 28900
			ENTITYCALL_TYPE_BASE
		}
	}
}
