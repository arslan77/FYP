
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/26/2016 01:35:42
-- Generated from EDMX file: C:\Users\Arslan Arshad\Documents\Visual Studio 2015\Projects\Attendance System\Attendance System\Models\Project.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Attendance_Class]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendances] DROP CONSTRAINT [FK_Attendance_Class];
GO
IF OBJECT_ID(N'[dbo].[FK_Attendance_Student]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendances] DROP CONSTRAINT [FK_Attendance_Student];
GO
IF OBJECT_ID(N'[dbo].[FK_Attendance_Subject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendances] DROP CONSTRAINT [FK_Attendance_Subject];
GO
IF OBJECT_ID(N'[dbo].[FK_Attendance_Teacher]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendances] DROP CONSTRAINT [FK_Attendance_Teacher];
GO
IF OBJECT_ID(N'[dbo].[FK_Class_Section]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Classes] DROP CONSTRAINT [FK_Class_Section];
GO
IF OBJECT_ID(N'[dbo].[FK_Class_Session]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Classes] DROP CONSTRAINT [FK_Class_Session];
GO
IF OBJECT_ID(N'[dbo].[FK_Teacher_Course_Allocation_Class]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teacher_Course_Allocation] DROP CONSTRAINT [FK_Teacher_Course_Allocation_Class];
GO
IF OBJECT_ID(N'[dbo].[FK_Dicipline_Department]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Diciplines] DROP CONSTRAINT [FK_Dicipline_Department];
GO
IF OBJECT_ID(N'[dbo].[FK_Session_Dicipline]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_Session_Dicipline];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_Guardian]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Student_Guardian];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_Session]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Student_Session];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_Userinfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Student_Userinfo];
GO
IF OBJECT_ID(N'[dbo].[FK_Teacher_Course_Allocation_Subject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teacher_Course_Allocation] DROP CONSTRAINT [FK_Teacher_Course_Allocation_Subject];
GO
IF OBJECT_ID(N'[dbo].[FK_Teacher_Course_Allocation_Teacher]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teacher_Course_Allocation] DROP CONSTRAINT [FK_Teacher_Course_Allocation_Teacher];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_Allocation_Class]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Student_Allocation] DROP CONSTRAINT [FK_Student_Allocation_Class];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_Allocation_Student]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Student_Allocation] DROP CONSTRAINT [FK_Student_Allocation_Student];
GO
IF OBJECT_ID(N'[dbo].[FK_Teacher_Userinfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teacher_Userinfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Attendances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendances];
GO
IF OBJECT_ID(N'[dbo].[Classes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Classes];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Diciplines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Diciplines];
GO
IF OBJECT_ID(N'[dbo].[Guardians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Guardians];
GO
IF OBJECT_ID(N'[dbo].[Sections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sections];
GO
IF OBJECT_ID(N'[dbo].[Sessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sessions];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[Subjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subjects];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Teachers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teachers];
GO
IF OBJECT_ID(N'[dbo].[Teacher_Course_Allocation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teacher_Course_Allocation];
GO
IF OBJECT_ID(N'[dbo].[Userinfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Userinfoes];
GO
IF OBJECT_ID(N'[dbo].[Student_Allocation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Student_Allocation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attendances'
CREATE TABLE [dbo].[Attendances] (
    [classID] int  NOT NULL,
    [rollNo] varchar(30)  NOT NULL,
    [status] varchar(20)  NOT NULL,
    [date] datetime  NOT NULL,
    [teacherID] int  NOT NULL,
    [courseCode] varchar(50)  NOT NULL
);
GO

-- Creating table 'Classes'
CREATE TABLE [dbo].[Classes] (
    [classID] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NOT NULL,
    [sectionID] int  NULL,
    [sessionID] int  NOT NULL,
    [isMorning] bit  NOT NULL,
    [isTheory] bit  NOT NULL,
    [isSupply] bit  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [deptID] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [location] varchar(100)  NOT NULL
);
GO

-- Creating table 'Diciplines'
CREATE TABLE [dbo].[Diciplines] (
    [dicipID] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [deptID] int  NOT NULL,
    [totalSemester] int  NOT NULL
);
GO

-- Creating table 'Guardians'
CREATE TABLE [dbo].[Guardians] (
    [guardianID] int IDENTITY(1,1) NOT NULL,
    [guardianName] varchar(50)  NOT NULL,
    [relationship] varchar(50)  NOT NULL,
    [mobileNumber] varchar(50)  NOT NULL
);
GO

-- Creating table 'Sections'
CREATE TABLE [dbo].[Sections] (
    [sectionID] int IDENTITY(1,1) NOT NULL,
    [sessionID] int  NOT NULL,
    [name] varchar(50)  NOT NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [sessionID] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [dicipID] int  NOT NULL,
    [year] int  NOT NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [rollNo] varchar(30)  NOT NULL,
    [sessionID] int  NOT NULL,
    [fatherName] varchar(100)  NOT NULL,
    [fullName] varchar(100)  NOT NULL,
    [guardianID] int  NULL,
    [gender] varchar(10)  NOT NULL,
    [username] varchar(100)  NULL
);
GO

-- Creating table 'Subjects'
CREATE TABLE [dbo].[Subjects] (
    [courseCode] varchar(50)  NOT NULL,
    [name] varchar(100)  NOT NULL,
    [theoryHours] int  NOT NULL,
    [labHours] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Teachers'
CREATE TABLE [dbo].[Teachers] (
    [teacherID] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NOT NULL,
    [email] varchar(100)  NOT NULL,
    [username] varchar(100)  NULL
);
GO

-- Creating table 'Teacher_Course_Allocation'
CREATE TABLE [dbo].[Teacher_Course_Allocation] (
    [teacherID] int  NOT NULL,
    [classID] int  NOT NULL,
    [courseCode] varchar(50)  NOT NULL,
    [isCurrent] bit  NOT NULL
);
GO

-- Creating table 'Userinfoes'
CREATE TABLE [dbo].[Userinfoes] (
    [username] varchar(100)  NOT NULL,
    [passwordHash] nvarchar(120)  NOT NULL,
    [role] varchar(50)  NOT NULL,
    [email] varchar(100)  NOT NULL,
    [token] varchar(150)  NOT NULL,
    [imagePath] varchar(max)  NULL
);
GO

-- Creating table 'Student_Allocation'
CREATE TABLE [dbo].[Student_Allocation] (
    [Classes_classID] int  NOT NULL,
    [Students_rollNo] varchar(30)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [classID], [rollNo], [date], [teacherID], [courseCode] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [PK_Attendances]
    PRIMARY KEY CLUSTERED ([classID], [rollNo], [date], [teacherID], [courseCode] ASC);
GO

-- Creating primary key on [classID] in table 'Classes'
ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [PK_Classes]
    PRIMARY KEY CLUSTERED ([classID] ASC);
GO

-- Creating primary key on [deptID] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([deptID] ASC);
GO

-- Creating primary key on [dicipID] in table 'Diciplines'
ALTER TABLE [dbo].[Diciplines]
ADD CONSTRAINT [PK_Diciplines]
    PRIMARY KEY CLUSTERED ([dicipID] ASC);
GO

-- Creating primary key on [guardianID] in table 'Guardians'
ALTER TABLE [dbo].[Guardians]
ADD CONSTRAINT [PK_Guardians]
    PRIMARY KEY CLUSTERED ([guardianID] ASC);
GO

-- Creating primary key on [sectionID] in table 'Sections'
ALTER TABLE [dbo].[Sections]
ADD CONSTRAINT [PK_Sections]
    PRIMARY KEY CLUSTERED ([sectionID] ASC);
GO

-- Creating primary key on [sessionID] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([sessionID] ASC);
GO

-- Creating primary key on [rollNo] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([rollNo] ASC);
GO

-- Creating primary key on [courseCode] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [PK_Subjects]
    PRIMARY KEY CLUSTERED ([courseCode] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [teacherID] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [PK_Teachers]
    PRIMARY KEY CLUSTERED ([teacherID] ASC);
GO

-- Creating primary key on [teacherID], [classID], [courseCode] in table 'Teacher_Course_Allocation'
ALTER TABLE [dbo].[Teacher_Course_Allocation]
ADD CONSTRAINT [PK_Teacher_Course_Allocation]
    PRIMARY KEY CLUSTERED ([teacherID], [classID], [courseCode] ASC);
GO

-- Creating primary key on [username] in table 'Userinfoes'
ALTER TABLE [dbo].[Userinfoes]
ADD CONSTRAINT [PK_Userinfoes]
    PRIMARY KEY CLUSTERED ([username] ASC);
GO

-- Creating primary key on [Classes_classID], [Students_rollNo] in table 'Student_Allocation'
ALTER TABLE [dbo].[Student_Allocation]
ADD CONSTRAINT [PK_Student_Allocation]
    PRIMARY KEY CLUSTERED ([Classes_classID], [Students_rollNo] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [classID] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [FK_Attendance_Class]
    FOREIGN KEY ([classID])
    REFERENCES [dbo].[Classes]
        ([classID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [rollNo] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [FK_Attendance_Student]
    FOREIGN KEY ([rollNo])
    REFERENCES [dbo].[Students]
        ([rollNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attendance_Student'
CREATE INDEX [IX_FK_Attendance_Student]
ON [dbo].[Attendances]
    ([rollNo]);
GO

-- Creating foreign key on [courseCode] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [FK_Attendance_Subject]
    FOREIGN KEY ([courseCode])
    REFERENCES [dbo].[Subjects]
        ([courseCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attendance_Subject'
CREATE INDEX [IX_FK_Attendance_Subject]
ON [dbo].[Attendances]
    ([courseCode]);
GO

-- Creating foreign key on [teacherID] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [FK_Attendance_Teacher]
    FOREIGN KEY ([teacherID])
    REFERENCES [dbo].[Teachers]
        ([teacherID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attendance_Teacher'
CREATE INDEX [IX_FK_Attendance_Teacher]
ON [dbo].[Attendances]
    ([teacherID]);
GO

-- Creating foreign key on [sectionID] in table 'Classes'
ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [FK_Class_Section]
    FOREIGN KEY ([sectionID])
    REFERENCES [dbo].[Sections]
        ([sectionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Class_Section'
CREATE INDEX [IX_FK_Class_Section]
ON [dbo].[Classes]
    ([sectionID]);
GO

-- Creating foreign key on [sessionID] in table 'Classes'
ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [FK_Class_Session]
    FOREIGN KEY ([sessionID])
    REFERENCES [dbo].[Sessions]
        ([sessionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Class_Session'
CREATE INDEX [IX_FK_Class_Session]
ON [dbo].[Classes]
    ([sessionID]);
GO

-- Creating foreign key on [classID] in table 'Teacher_Course_Allocation'
ALTER TABLE [dbo].[Teacher_Course_Allocation]
ADD CONSTRAINT [FK_Teacher_Course_Allocation_Class]
    FOREIGN KEY ([classID])
    REFERENCES [dbo].[Classes]
        ([classID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Teacher_Course_Allocation_Class'
CREATE INDEX [IX_FK_Teacher_Course_Allocation_Class]
ON [dbo].[Teacher_Course_Allocation]
    ([classID]);
GO

-- Creating foreign key on [deptID] in table 'Diciplines'
ALTER TABLE [dbo].[Diciplines]
ADD CONSTRAINT [FK_Dicipline_Department]
    FOREIGN KEY ([deptID])
    REFERENCES [dbo].[Departments]
        ([deptID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Dicipline_Department'
CREATE INDEX [IX_FK_Dicipline_Department]
ON [dbo].[Diciplines]
    ([deptID]);
GO

-- Creating foreign key on [dicipID] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_Session_Dicipline]
    FOREIGN KEY ([dicipID])
    REFERENCES [dbo].[Diciplines]
        ([dicipID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Session_Dicipline'
CREATE INDEX [IX_FK_Session_Dicipline]
ON [dbo].[Sessions]
    ([dicipID]);
GO

-- Creating foreign key on [guardianID] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_Student_Guardian]
    FOREIGN KEY ([guardianID])
    REFERENCES [dbo].[Guardians]
        ([guardianID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Student_Guardian'
CREATE INDEX [IX_FK_Student_Guardian]
ON [dbo].[Students]
    ([guardianID]);
GO

-- Creating foreign key on [sessionID] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_Student_Session]
    FOREIGN KEY ([sessionID])
    REFERENCES [dbo].[Sessions]
        ([sessionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Student_Session'
CREATE INDEX [IX_FK_Student_Session]
ON [dbo].[Students]
    ([sessionID]);
GO

-- Creating foreign key on [username] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_Student_Userinfo]
    FOREIGN KEY ([username])
    REFERENCES [dbo].[Userinfoes]
        ([username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Student_Userinfo'
CREATE INDEX [IX_FK_Student_Userinfo]
ON [dbo].[Students]
    ([username]);
GO

-- Creating foreign key on [courseCode] in table 'Teacher_Course_Allocation'
ALTER TABLE [dbo].[Teacher_Course_Allocation]
ADD CONSTRAINT [FK_Teacher_Course_Allocation_Subject]
    FOREIGN KEY ([courseCode])
    REFERENCES [dbo].[Subjects]
        ([courseCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Teacher_Course_Allocation_Subject'
CREATE INDEX [IX_FK_Teacher_Course_Allocation_Subject]
ON [dbo].[Teacher_Course_Allocation]
    ([courseCode]);
GO

-- Creating foreign key on [teacherID] in table 'Teacher_Course_Allocation'
ALTER TABLE [dbo].[Teacher_Course_Allocation]
ADD CONSTRAINT [FK_Teacher_Course_Allocation_Teacher]
    FOREIGN KEY ([teacherID])
    REFERENCES [dbo].[Teachers]
        ([teacherID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Classes_classID] in table 'Student_Allocation'
ALTER TABLE [dbo].[Student_Allocation]
ADD CONSTRAINT [FK_Student_Allocation_Class]
    FOREIGN KEY ([Classes_classID])
    REFERENCES [dbo].[Classes]
        ([classID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Students_rollNo] in table 'Student_Allocation'
ALTER TABLE [dbo].[Student_Allocation]
ADD CONSTRAINT [FK_Student_Allocation_Student]
    FOREIGN KEY ([Students_rollNo])
    REFERENCES [dbo].[Students]
        ([rollNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Student_Allocation_Student'
CREATE INDEX [IX_FK_Student_Allocation_Student]
ON [dbo].[Student_Allocation]
    ([Students_rollNo]);
GO

-- Creating foreign key on [username] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [FK_Teacher_Userinfo]
    FOREIGN KEY ([username])
    REFERENCES [dbo].[Userinfoes]
        ([username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Teacher_Userinfo'
CREATE INDEX [IX_FK_Teacher_Userinfo]
ON [dbo].[Teachers]
    ([username]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------