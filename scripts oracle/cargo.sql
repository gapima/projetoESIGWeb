--------------------------------------------------------
--  Arquivo criado - segunda-feira-maio-26-2025   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table CARGO
--------------------------------------------------------

  CREATE TABLE "SYSTEM"."CARGO" 
   (	"ID" NUMBER(*,0), 
	"NOME" VARCHAR2(100 BYTE)
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into SYSTEM.CARGO
SET DEFINE OFF;
Insert into SYSTEM.CARGO (ID,NOME) values ('1','Estagiario');
Insert into SYSTEM.CARGO (ID,NOME) values ('2','Tecnico');
Insert into SYSTEM.CARGO (ID,NOME) values ('3','Analista');
Insert into SYSTEM.CARGO (ID,NOME) values ('4','Coordenador');
Insert into SYSTEM.CARGO (ID,NOME) values ('5','Gerente');
--------------------------------------------------------
--  DDL for Index SYS_C008315
--------------------------------------------------------

  CREATE UNIQUE INDEX "SYSTEM"."SYS_C008315" ON "SYSTEM"."CARGO" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table CARGO
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."CARGO" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
