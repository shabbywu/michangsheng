using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FDB RID: 4059
	public abstract class NPCBase : Entity
	{
		// Token: 0x06005FF0 RID: 24560 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x00042C71 File Offset: 0x00040E71
		public NPCBase()
		{
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x00042C94 File Offset: 0x00040E94
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_NPCBase(this.id, this.className);
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x00042CAD File Offset: 0x00040EAD
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_NPCBase(this.id, this.className);
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x00042CC6 File Offset: 0x00040EC6
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x00042CCF File Offset: 0x00040ECF
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x00042CD7 File Offset: 0x00040ED7
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x0026671C File Offset: 0x0026491C
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["NPC"];
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

		// Token: 0x06006003 RID: 24579 RVA: 0x0026679C File Offset: 0x0026499C
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["NPC"];
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
				if (properUtype2 <= 82)
				{
					if (properUtype2 != 81)
					{
						if (properUtype2 == 82)
						{
							byte oldValue = this.moveSpeed;
							this.moveSpeed = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onMoveSpeedChanged(oldValue);
								}
							}
							else if (this.inWorld)
							{
								this.onMoveSpeedChanged(oldValue);
							}
						}
					}
					else
					{
						uint oldValue2 = this.dialogID;
						this.dialogID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDialogIDChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onDialogIDChanged(oldValue2);
						}
					}
				}
				else
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
					default:
						switch (properUtype2)
						{
						case 41003:
						{
							string oldValue3 = this.name;
							this.name = stream.readUnicode();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onNameChanged(oldValue3);
								}
							}
							else if (this.inWorld)
							{
								this.onNameChanged(oldValue3);
							}
							break;
						}
						case 41004:
						{
							uint oldValue4 = this.uid;
							this.uid = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUidChanged(oldValue4);
								}
							}
							else if (this.inWorld)
							{
								this.onUidChanged(oldValue4);
							}
							break;
						}
						case 41005:
						{
							uint oldValue5 = this.utype;
							this.utype = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUtypeChanged(oldValue5);
								}
							}
							else if (this.inWorld)
							{
								this.onUtypeChanged(oldValue5);
							}
							break;
						}
						case 41006:
						{
							uint oldValue6 = this.modelID;
							this.modelID = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelIDChanged(oldValue6);
								}
							}
							else if (this.inWorld)
							{
								this.onModelIDChanged(oldValue6);
							}
							break;
						}
						case 41007:
						{
							byte oldValue7 = this.modelScale;
							this.modelScale = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelScaleChanged(oldValue7);
								}
							}
							else if (this.inWorld)
							{
								this.onModelScaleChanged(oldValue7);
							}
							break;
						}
						default:
							if (properUtype2 == 51007)
							{
								uint oldValue8 = this.entityNO;
								this.entityNO = stream.readUint32();
								if (property.isBase())
								{
									if (this.inited)
									{
										this.onEntityNOChanged(oldValue8);
									}
								}
								else if (this.inWorld)
								{
									this.onEntityNOChanged(oldValue8);
								}
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x00266B7C File Offset: 0x00264D7C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["NPC"].idpropertys;
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
			byte oldValue5 = this.moveSpeed;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMoveSpeedChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onMoveSpeedChanged(oldValue5);
			}
			string oldValue6 = this.name;
			Property property7 = idpropertys[9];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue6);
			}
			Vector3 position = this.position;
			Property property8 = idpropertys[1];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue7 = this.uid;
			Property property9 = idpropertys[10];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue7);
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue7);
			}
			uint oldValue8 = this.utype;
			Property property10 = idpropertys[11];
			if (property10.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue8);
					return;
				}
			}
			else if (this.inWorld && (!property10.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue8);
			}
		}

		// Token: 0x04005B6A RID: 23402
		public EntityBaseEntityCall_NPCBase baseEntityCall;

		// Token: 0x04005B6B RID: 23403
		public EntityCellEntityCall_NPCBase cellEntityCall;

		// Token: 0x04005B6C RID: 23404
		public uint dialogID;

		// Token: 0x04005B6D RID: 23405
		public uint entityNO;

		// Token: 0x04005B6E RID: 23406
		public uint modelID;

		// Token: 0x04005B6F RID: 23407
		public byte modelScale = 30;

		// Token: 0x04005B70 RID: 23408
		public byte moveSpeed = 50;

		// Token: 0x04005B71 RID: 23409
		public string name = "";

		// Token: 0x04005B72 RID: 23410
		public uint uid;

		// Token: 0x04005B73 RID: 23411
		public uint utype;
	}
}
