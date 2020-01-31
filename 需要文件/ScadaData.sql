USE [master]
GO

IF EXISTS(SELECT * FROM sysdatabases WHERE NAME='ScadaData')
BEGIN
    DROP DATABASE ScadaData   --如果数据库存在先删掉数据库
END
GO

CREATE DATABASE ScadaData
ON
PRIMARY  --创建主数据库文件
(
    NAME='ScadaData',
    FILENAME='E:\ScadaData.mdf',
    SIZE=5MB,
    MaxSize=100MB,
    FileGrowth=10MB
)
LOG ON --创建日志文件
(
    NAME='ScadaDataLog',
    FileName='E:\ScadaData.ldf',
    Size=2MB,
    MaxSize=20MB,
    FileGrowth=1MB
)
GO
use ScadaData
--添加表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME='ActualData')

CREATE TABLE ActualData
(
    Id INT IDENTITY(1,1) not NULL,
    InsertTime DateTime NULL,
    VarName NVARCHAR(20)  NULL,
    Value NVARCHAR(20) NULL,
    Remark NVARCHAR(50)  NULL
)

use ScadaData
--添加表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME='ReportData')

CREATE TABLE ReportData
(
    Id INT IDENTITY(1,1) not NULL,
    InsertTime DateTime NULL,
    LQT_Level NVARCHAR(20)  NULL,
    LQT_InPre NVARCHAR(20) NULL,
    LQT_InTemp NVARCHAR(20)  NULL,
	 LQT_OutPre NVARCHAR(20)  NULL,
    LQT_OutTemp NVARCHAR(20) NULL,
    LQT_BSPre NVARCHAR(20)  NULL,
	 LQB1_Current NVARCHAR(20)  NULL,
    LQB1_Fre NVARCHAR(20) NULL,
    LQB2_Current NVARCHAR(20)  NULL,
	 LQB2_Fre NVARCHAR(20)  NULL,
    KYJ1_OutTemp NVARCHAR(20) NULL,
 KYJ2_OutTemp NVARCHAR(20)  NULL,
    KYJ3_OutTemp NVARCHAR(20) NULL,
 CQG1_OutPre NVARCHAR(20)  NULL,
    CQG2_OutPre NVARCHAR(20) NULL,
 CQG3_OutPre NVARCHAR(20)  NULL,
    Env_Temp NVARCHAR(20) NULL,
	 FQG_Temp NVARCHAR(20)  NULL,
    FQG_Pre NVARCHAR(20) NULL
)

use ScadaData
--添加表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME='AlarmData')

CREATE TABLE AlarmData
(
    Id INT IDENTITY(1,1) not NULL,
    InsertTime DateTime NULL,
    VarName NVARCHAR(20)  NULL,
    AlarmState NVARCHAR(20) NULL,
    Priority Int  NULL,
	 AlarmType NVARCHAR(20)  NULL,
    Value float NULL,
    AlarmValue float NULL,
	 Operator NVARCHAR(20)  NULL,
    Note NVARCHAR(150) NULL
)

use ScadaData
--添加表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME='SysAdmins')

CREATE TABLE SysAdmins
(
    LongId INT IDENTITY(10000,1) not NULL,
    LoginName NVARCHAR(20) NULL,
    LoginPwd NVARCHAR(20)  NULL,
    Role int NULL
)

insert into SysAdmins( LoginName,LoginPwd,Role) Values('Administrator','123',1)