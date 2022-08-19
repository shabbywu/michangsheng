using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B96 RID: 2966
	public abstract class GateBase : Entity
	{
		// Token: 0x06005268 RID: 21096 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x0022F7A1 File Offset: 0x0022D9A1
		public GateBase()
		{
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0022F7BC File Offset: 0x0022D9BC
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_GateBase(this.id, this.className);
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0022F7D5 File Offset: 0x0022D9D5
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_GateBase(this.id, this.className);
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0022F7EE File Offset: 0x0022D9EE
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0022F7F7 File Offset: 0x0022D9F7
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0022F7FF File Offset: 0x0022D9FF
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0022F808 File Offset: 0x0022DA08
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Gate"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0022F888 File Offset: 0x0022DA88
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Gate"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				if (properUtype2 <= 40002)
				{
					if (properUtype2 != 89)
					{
						switch (properUtype2)
						{
						case 40000:
						{
							Vector3 position = this.position;
							this.position = stream.readVector3();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onPositionChanged(position);
								}
							}
							else if (this.inWorld)
							{
								this.onPositionChanged(position);
							}
							break;
						}
						case 40001:
						{
							Vector3 direction = this.direction;
							this.direction = stream.readVector3();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onDirectionChanged(direction);
								}
							}
							else if (this.inWorld)
							{
								this.onDirectionChanged(direction);
							}
							break;
						}
						case 40002:
							stream.readUint32();
							break;
						}
					}
					else
					{
						uint oldValue = this.dialogID;
						this.dialogID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDialogIDChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onDialogIDChanged(oldValue);
						}
					}
				}
				else
				{
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue2 = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue2);
						}
						break;
					}
					case 41004:
					{
						uint oldValue3 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue3);
						}
						break;
					}
					case 41005:
					{
						uint oldValue4 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue4);
						}
						break;
					}
					case 41006:
					{
						uint oldValue5 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue5);
						}
						break;
					}
					case 41007:
					{
						byte oldValue6 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue6);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue6);
						}
						break;
					}
					default:
						if (properUtype2 == 51007)
						{
							uint oldValue7 = this.entityNO;
							this.entityNO = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onEntityNOChanged(oldValue7);
								}
							}
							else if (this.inWorld)
							{
								this.onEntityNOChanged(oldValue7);
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0022FC18 File Offset: 0x0022DE18
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Gate"].idpropertys;
			uint oldValue = this.dialogID;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDialogIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDialogIDChanged(oldValue);
			}
			Vector3 direction = this.direction;
			Property property2 = idpropertys[2];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue2 = this.entityNO;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEntityNOChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onEntityNOChanged(oldValue2);
			}
			uint oldValue3 = this.modelID;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue3);
			}
			byte oldValue4 = this.modelScale;
			Property property5 = idpropertys[7];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue4);
			}
			string oldValue5 = this.name;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue5);
			}
			Vector3 position = this.position;
			Property property7 = idpropertys[1];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue6 = this.uid;
			Property property8 = idpropertys[9];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue6);
			}
			uint oldValue7 = this.utype;
			Property property9 = idpropertys[10];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue7);
					return;
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue7);
			}
		}

		// Token: 0x04004FE1 RID: 20449
		public EntityBaseEntityCall_GateBase baseEntityCall;

		// Token: 0x04004FE2 RID: 20450
		public EntityCellEntityCall_GateBase cellEntityCall;

		// Token: 0x04004FE3 RID: 20451
		public uint dialogID;

		// Token: 0x04004FE4 RID: 20452
		public uint entityNO;

		// Token: 0x04004FE5 RID: 20453
		public uint modelID;

		// Token: 0x04004FE6 RID: 20454
		public byte modelScale = 30;

		// Token: 0x04004FE7 RID: 20455
		public string name = "";

		// Token: 0x04004FE8 RID: 20456
		public uint uid;

		// Token: 0x04004FE9 RID: 20457
		public uint utype;
	}
}
