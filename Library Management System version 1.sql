-- Creating labirary management system database
-- First we need to create a database with transaction file and table file

CREATE DATABASE Library_Management_System

-- Now we will select the database that we created now

USE Library_Management_System

-- Then we will create database tables by using the following code
-- First We should have to create table for strong entities
-- Strong entities are entities that they are not dependent on other entites

-- First we will start by createing table for category

CREATE TABLE tblCategory(
Category_ID INT IDENTITY(1,1) NOT NULL,
Category_Name VARCHAR(50) NOT NULL,
PRIMARY KEY(Category_ID)
)

-- Then let's test our table by inserting some datas
-- we will insert data into category name only because category_id is idenetity which means it is auto increment by 1
INSERT INTO tblCategory(Category_Name)
VALUES('Computer Science'),
('Health Sciecne')

SELECT *
FROM tblCategory

-- create table for binding detail
CREATE TABLE tblBindingDetail(
Binding_ID INT IDENTITY(1,1) NOT NULL,
Binding_Name VARCHAR(50) NOT NULL,
PRIMARY KEY(Binding_ID)
)
-- insert data into binding detail
INSERT INTO tblBindingDetail(Binding_Name)
VALUES('Publisher1'),
('Publisher2')

SELECT * 
FROM tblBindingDetail

-- create staff memebers table
CREATE TABLE tblStaff(
Staff_Memeber_ID INT IDENTITY(1000,1) NOT NULL,
FullName VARCHAR(100) NOT NULL,
User_Name VARCHAR(30) NOT NULL UNIQUE,
SPassword VARCHAR(50) NOT NULL,
is_Admin INT NOT NULL DEFAULT 0,
Designation VARCHAR(50) NOT NULL,
CONSTRAINT CK_is_Admin CHECK(is_Admin=0 OR is_Admin=1),
PRIMARY KEY(Staff_Memeber_ID)
)

INSERT INTO tblStaff(FullName,User_Name,SPassword,is_Admin,Designation)
VALUES('Nancy Thomas','nancy@user.com','123456',1,'Manager'),
('Robert Gets','robert@user.com','123456',0,'Lib_Clr')

SELECT *
FROM tblStaff

-- Now we finished creating table for strong entity 
-- Now we will create tables for weak entities
-- Weak entity is an entity that is dependanet on other entity
-- we will use foreign key to create realtionship with strong entitys

-- Now we will create table for Book Details

CREATE TABLE tblBookDetail(
ISBN VARCHAR(15) NOT NULL,
Book_Title VARCHAR(100) NOT NULL,
Category INT NOT NULL,
Binding_ID INT NOT NULL,
Publcation_Year INT,
Actual_No_Of_Copy INT NOT NULL,
Current_No_Of_Copy INT NOT NULL,
CONSTRAINT FK_CAT FOREIGN KEY(Category) REFERENCES tblCategory(Category_ID) ON UPDATE CASCADE ON DELETE CASCADE,
CONSTRAINT FK_BIND FOREIGN KEY(Binding_ID) REFERENCES tblBindingDetail(Binding_ID) ON UPDATE CASCADE ON DELETE CASCADE,
PRIMARY KEY(ISBN)
)

SELECT * FROM tblCategory
INSERT INTO tblBookDetail VALUES
('123-445-1357','Database Systems',1,1,2015,15,15),
('650-341-3412','Starting Out With VB.Net',1,1,2019,20,20)

SELECT *
FROM tblBookDetail

-- LETS TEST RELATION SHIP IS APPLIED SUCCESFULLY
CREATE VIEW Book_Detail AS
SELECT b.ISBN, b.Book_Title,c.Category_Name,bind.Binding_Name,b.Publcation_Year,b.Actual_No_Of_Copy, b.Current_No_Of_Copy
FROM tblCategory c INNER JOIN tblBookDetail b 
ON c.Category_ID = b.Category INNER JOIN tblBindingDetail bind ON bind.Binding_ID = b.Binding_ID

-- NOW LETS CREATE TABLE FOR STUDENT

CREATE TABLE tblStudentDetail(
Stud_ID NVARCHAR(10) NOT NULL,
Full_Name VARCHAR(50) NOT NULL,
Gender VARCHAR(2) NOT NULL,
Date_Of_Birth DATE NOT NULL,
Department VARCHAR(30) NOT NULL,
Phone_Number VARCHAR(15) NOT NULL,
Registered_By INT NOT NULL,
CONSTRAINT ck_Gender CHECK(Gender='M' OR Gender='F'),
CONSTRAINT FK_ST_STUD FOREIGN KEY(Registered_By) REFERENCES tblStaff(Staff_Memeber_ID) ON UPDATE CASCADE ON DELETE NO ACTION,
PRIMARY KEY(Stud_ID)
)

INSERT INTO tblStudentDetail(Stud_ID,Full_Name,Gender,Date_Of_Birth,Department,Phone_Number,Registered_By)
VALUES('STUD0001','Robert Alison','M','9/9/2000','Computer Science','+94785895866',1002)

SELECT*
FROM tblStudentDetail

-- now lets create borrow detail table
CREATE TABLE tblBorrowDetail(
Borrow_ID INT IDENTITY(1,1) NOT NULL,
ISBN VARCHAR(15) NOT NULL,
Stud_ID NVARCHAR(10) NOT NULL,
BorrowDate DATE NOT NULL,
Actual_Return_Date DATE NOT NULL,
Issued_By INT NOT NULL,
CONSTRAINT CK_DATE CHECK(Actual_Return_Date>BorrowDate),
CONSTRAINT FK_BOOK_BORROW FOREIGN KEY(ISBN) REFERENCES tblBookDetail(ISBN) ON UPDATE CASCADE ON DELETE CASCADE,
CONSTRAINT FK_Student_BORROW FOREIGN KEY(Stud_ID) REFERENCES tblStudentDetail(Stud_ID) ON UPDATE CASCADE ON DELETE CASCADE,
CONSTRAINT FK_STAFF_BORROE FOREIGN KEY(Issued_By) REFERENCES tblStaff(Staff_Memeber_ID),
PRIMARY KEY(Borrow_ID)
)

select* from tblBorrowDetail
INSERT INTO tblBorrowDetail(ISBN,Stud_ID,BorrowDate,Actual_Return_Date,Issued_By)
VALUES('650-341-3412','STUD0001',GETDATE(),'5/10/2023',1002)

-- next time we will create vb.net windows form application for this database!!!
-- Now lets create database diagram
