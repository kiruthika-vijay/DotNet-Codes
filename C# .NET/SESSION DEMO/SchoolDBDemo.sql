-- Create Student Table
CREATE TABLE Student (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
	Phone VARCHAR(10) NOT NULL,
	DOB DATE NOT NULL
);

-- Create Teacher Table
CREATE TABLE Teacher (
    TeacherId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
	Phone VARCHAR(10) NOT NULL,
	HireDate DATE NOT NULL
);

-- Create Course Table
CREATE TABLE Course (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
	TeacherId INT,
	FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId)
);

-- Create Fee Table
CREATE TABLE Fee (
    FeeId INT PRIMARY KEY IDENTITY(1,1),
    Amount DECIMAL(10, 2) NOT NULL,
    DueDate DATE NOT NULL,
    StudentId INT,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId)
);

-- Create StudentCourse Junction Table
CREATE TABLE StudentCourse (
    StudentId INT,
    CourseId INT,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
);
