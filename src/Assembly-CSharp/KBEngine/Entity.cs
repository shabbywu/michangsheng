using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B6B RID: 2923
	public class Entity
	{
		// Token: 0x0600519D RID: 20893 RVA: 0x00004095 File Offset: 0x00002295
		public static void clear()
		{
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual object getDefinedProperty(string name)
		{
			return null;
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00222DFD File Offset: 0x00220FFD
		public void destroy()
		{
			this.detachComponents();
			this.onDestroy();
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDestroy()
		{
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x00222E0B File Offset: 0x0022100B
		public bool isPlayer()
		{
			return this.id == KBEngineApp.app.entity_id;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRemoteMethodCall(MemoryStream stream)
		{
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUpdatePropertys(MemoryStream stream)
		{
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onGetBase()
		{
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onGetCell()
		{
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onLoseCell()
		{
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onComponentsEnterworld()
		{
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onComponentsLeaveworld()
		{
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual EntityCall getBaseEntityCall()
		{
			return null;
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual EntityCall getCellEntityCall()
		{
			return null;
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void __init__()
		{
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void callPropertysSetMethods()
		{
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void attachComponents()
		{
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void detachComponents()
		{
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x00222E20 File Offset: 0x00221020
		public void baseCall(string methodname, params object[] arguments)
		{
			if (KBEngineApp.app.currserver == "loginapp")
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::baseCall(",
					methodname,
					"), currserver=!",
					KBEngineApp.app.currserver
				}));
				return;
			}
			ScriptModule scriptModule = null;
			if (!EntityDef.moduledefs.TryGetValue(this.className, out scriptModule))
			{
				Dbg.ERROR_MSG("entity::baseCall:  entity-module(" + this.className + ") error, can not find from EntityDef.moduledefs");
				return;
			}
			Method method = null;
			if (!scriptModule.base_methods.TryGetValue(methodname, out method))
			{
				Dbg.ERROR_MSG(this.className + "::baseCall(" + methodname + "), not found method!");
				return;
			}
			ushort methodUtype = method.methodUtype;
			if (arguments.Length != method.args.Count)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					this.className,
					"::baseCall(",
					methodname,
					"): args(",
					arguments.Length,
					"!= ",
					method.args.Count,
					") size is error!"
				}));
				return;
			}
			EntityCall baseEntityCall = this.getBaseEntityCall();
			baseEntityCall.newCall();
			baseEntityCall.bundle.writeUint16(0);
			baseEntityCall.bundle.writeUint16(methodUtype);
			try
			{
				for (int i = 0; i < method.args.Count; i++)
				{
					if (!method.args[i].isSameType(arguments[i]))
					{
						throw new Exception(string.Concat(new object[]
						{
							"arg",
							i,
							": ",
							method.args[i].ToString()
						}));
					}
					method.args[i].addToStream(baseEntityCall.bundle, arguments[i]);
				}
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::baseCall(method=",
					methodname,
					"): args is error(",
					ex.Message,
					")!"
				}));
				baseEntityCall.bundle = null;
				return;
			}
			baseEntityCall.sendCall(null);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x0022306C File Offset: 0x0022126C
		public void cellCall(string methodname, params object[] arguments)
		{
			if (KBEngineApp.app.currserver == "loginapp")
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::cellCall(",
					methodname,
					"), currserver=!",
					KBEngineApp.app.currserver
				}));
				return;
			}
			ScriptModule scriptModule = null;
			if (!EntityDef.moduledefs.TryGetValue(this.className, out scriptModule))
			{
				Dbg.ERROR_MSG("entity::cellCall:  entity-module(" + this.className + ") error, can not find from EntityDef.moduledefs!");
				return;
			}
			Method method = null;
			if (!scriptModule.cell_methods.TryGetValue(methodname, out method))
			{
				Dbg.ERROR_MSG(this.className + "::cellCall(" + methodname + "), not found method!");
				return;
			}
			ushort methodUtype = method.methodUtype;
			if (arguments.Length != method.args.Count)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					this.className,
					"::cellCall(",
					methodname,
					"): args(",
					arguments.Length,
					"!= ",
					method.args.Count,
					") size is error!"
				}));
				return;
			}
			EntityCall cellEntityCall = this.getCellEntityCall();
			if (cellEntityCall == null)
			{
				Dbg.ERROR_MSG(this.className + "::cellCall(" + methodname + "): no cell!");
				return;
			}
			cellEntityCall.newCall();
			cellEntityCall.bundle.writeUint16(0);
			cellEntityCall.bundle.writeUint16(methodUtype);
			try
			{
				for (int i = 0; i < method.args.Count; i++)
				{
					if (!method.args[i].isSameType(arguments[i]))
					{
						throw new Exception(string.Concat(new object[]
						{
							"arg",
							i,
							": ",
							method.args[i].ToString()
						}));
					}
					method.args[i].addToStream(cellEntityCall.bundle, arguments[i]);
				}
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::cellCall(",
					methodname,
					"): args is error(",
					ex.Message,
					")!"
				}));
				cellEntityCall.bundle = null;
				return;
			}
			cellEntityCall.sendCall(null);
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x002232D8 File Offset: 0x002214D8
		public void enterWorld()
		{
			this.inWorld = true;
			try
			{
				this.onEnterWorld();
				this.onComponentsEnterworld();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onEnterWorld: error=" + ex.ToString());
			}
			Event.fireOut("onEnterWorld", new object[]
			{
				this
			});
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEnterWorld()
		{
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0022333C File Offset: 0x0022153C
		public void leaveWorld()
		{
			this.inWorld = false;
			try
			{
				this.onLeaveWorld();
				this.onComponentsLeaveworld();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onLeaveWorld: error=" + ex.ToString());
			}
			Event.fireOut("onLeaveWorld", new object[]
			{
				this
			});
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onLeaveWorld()
		{
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x002233A0 File Offset: 0x002215A0
		public virtual void enterSpace()
		{
			this.inWorld = true;
			try
			{
				this.onEnterSpace();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onEnterSpace: error=" + ex.ToString());
			}
			Event.fireOut("onEnterSpace", new object[]
			{
				this
			});
			Event.fireOut("set_position", new object[]
			{
				this
			});
			Event.fireOut("set_direction", new object[]
			{
				this
			});
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEnterSpace()
		{
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00223428 File Offset: 0x00221628
		public virtual void leaveSpace()
		{
			this.inWorld = false;
			try
			{
				this.onLeaveSpace();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onLeaveSpace: error=" + ex.ToString());
			}
			Event.fireOut("onLeaveSpace", new object[]
			{
				this
			});
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onLeaveSpace()
		{
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00223488 File Offset: 0x00221688
		public virtual void onPositionChanged(Vector3 oldValue)
		{
			if (this.isPlayer())
			{
				KBEngineApp.app.entityServerPos(this.position);
			}
			if (this.inWorld)
			{
				Event.fireOut("set_position", new object[]
				{
					this
				});
			}
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUpdateVolatileData()
		{
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x002234C0 File Offset: 0x002216C0
		public virtual void onDirectionChanged(Vector3 oldValue)
		{
			if (this.inWorld)
			{
				this.direction.x = this.direction.x * 360f / 6.2831855f;
				this.direction.y = this.direction.y * 360f / 6.2831855f;
				this.direction.z = this.direction.z * 360f / 6.2831855f;
				Event.fireOut("set_direction", new object[]
				{
					this
				});
				return;
			}
			this.direction = oldValue;
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onControlled(bool isControlled_)
		{
		}

		// Token: 0x04004F9D RID: 20381
		public Vector3 _entityLastLocalPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04004F9E RID: 20382
		public Vector3 _entityLastLocalDir = new Vector3(0f, 0f, 0f);

		// Token: 0x04004F9F RID: 20383
		public int id;

		// Token: 0x04004FA0 RID: 20384
		public string className = "";

		// Token: 0x04004FA1 RID: 20385
		public Vector3 position = new Vector3(0f, 0f, 0f);

		// Token: 0x04004FA2 RID: 20386
		public Vector3 direction = new Vector3(0f, 0f, 0f);

		// Token: 0x04004FA3 RID: 20387
		public float velocity;

		// Token: 0x04004FA4 RID: 20388
		public bool isOnGround = true;

		// Token: 0x04004FA5 RID: 20389
		public object renderObj;

		// Token: 0x04004FA6 RID: 20390
		public bool inWorld;

		// Token: 0x04004FA7 RID: 20391
		public bool isControlled;

		// Token: 0x04004FA8 RID: 20392
		public bool inited;
	}
}
