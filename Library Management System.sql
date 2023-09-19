CREATE DATABASE library_management_system

ON(
NAME = dataf,
FILENAME = 'D:\dataf.mdf',
SIZE = 10MB,
MAXSIZE = 500MB,
FILEGROWTH = 10MB
)

LOG ON(
NAME = logf,
FILENAME = 'D:\logf.ldf',
SIZE = 10MB,
MAXSIZE = 500MB,
FILEGROWTH = 10MB
)

USE library_management_system

ALTER DATABASE library_management_system
MODIFY FILE(
NAME = dataf,
FILENAME = 'D:\Library_Management_System\dataf.mdf',
SIZE = 15MB,
MAXSIZE=501MB,
FILEGROWTH=5MB
)

ALTER DATABASE library_management_system
MODIFY FILE(
NAME = logf,
FILENAME = 'D:\Library_Management_System\logf.ldf',
SIZE = 15MB,
MAXSIZE=501MB,
FILEGROWTH=5MB
)
