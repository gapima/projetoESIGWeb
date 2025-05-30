--------------------------------------------------------
--  Arquivo criado - segunda-feira-maio-26-2025   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table CARGO_VENCIMENTOS
--------------------------------------------------------

  CREATE TABLE "SYSTEM"."CARGO_VENCIMENTOS" 
   (	"ID" NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"CARGO_ID" NUMBER, 
	"VENCIMENTO_ID" NUMBER
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into SYSTEM.CARGO_VENCIMENTOS
SET DEFINE OFF;
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('43','2','2');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('3','2','9');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('50','2','52');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('5','3','3');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('6','3','9');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('51','3','52');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('8','4','4');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('9','4','9');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('52','4','52');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('11','4','6');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('12','5','5');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('13','5','9');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('53','5','52');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('15','5','7');
Insert into SYSTEM.CARGO_VENCIMENTOS (ID,CARGO_ID,VENCIMENTO_ID) values ('22','1','1');
--------------------------------------------------------
--  DDL for Index SYS_C008343
--------------------------------------------------------

  CREATE UNIQUE INDEX "SYSTEM"."SYS_C008343" ON "SYSTEM"."CARGO_VENCIMENTOS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index IDX_CV_CARGO
--------------------------------------------------------

  CREATE INDEX "SYSTEM"."IDX_CV_CARGO" ON "SYSTEM"."CARGO_VENCIMENTOS" ("CARGO_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index IDX_CV_VENCIMENTO
--------------------------------------------------------

  CREATE INDEX "SYSTEM"."IDX_CV_VENCIMENTO" ON "SYSTEM"."CARGO_VENCIMENTOS" ("VENCIMENTO_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table CARGO_VENCIMENTOS
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."CARGO_VENCIMENTOS" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."CARGO_VENCIMENTOS" MODIFY ("CARGO_ID" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."CARGO_VENCIMENTOS" MODIFY ("VENCIMENTO_ID" NOT NULL ENABLE);
  ALTER TABLE "SYSTEM"."CARGO_VENCIMENTOS" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table CARGO_VENCIMENTOS
--------------------------------------------------------

  ALTER TABLE "SYSTEM"."CARGO_VENCIMENTOS" ADD CONSTRAINT "FK_CV_CARGO" FOREIGN KEY ("CARGO_ID")
	  REFERENCES "SYSTEM"."CARGO" ("ID") ENABLE;
  ALTER TABLE "SYSTEM"."CARGO_VENCIMENTOS" ADD CONSTRAINT "FK_CV_VENCIMENTO" FOREIGN KEY ("VENCIMENTO_ID")
	  REFERENCES "SYSTEM"."VENCIMENTOS" ("ID") ENABLE;
