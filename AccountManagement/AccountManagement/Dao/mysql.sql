start transaction;

create database AccountManagement;
use AccountManagement;

drop table Users;
drop table AccountInformation;

create table Users(
	Id	int primary key auto_increment,
	userId varchar(64) not null,
	password varchar(64) not null
);

create table AccountInformation(
	accountId int primary key auto_increment,
	userId  varchar(64) not null,
	loginAccount varchar(64) not null,
	loginPassword varchar(64),
	payAccount varchar(64),
	payPassword varchar(64),	
	emailAddr varchar(64),
	telephone varchar(64),
	validDate  varchar(64),
	creditSafeCode  varchar(64),
	safeQuestion1  varchar(100),
	safeQuestionAnswer1 varchar(100),
	safeQuestion2 varchar(100),
	safeQuestionAnswer2 varchar(100),
	safeQuestion3 varchar(100), 
	safeQuestionAnswer3 varchar(100),
	emergencyContact1 varchar(64),
	emergencyContactPhone1 varchar(20),
	emergencyContact2 varchar(64),
	emergencyContactPhone2 varchar(20),
	address varchar(200),
	accountType varchar(20),
	companyName varchar(64),
	companyCode varchar(20),
	shopName varchar(64),
	shopCode varchar(20),
	webSite varchar(100)
);

