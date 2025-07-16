CREATE TABLE [dbo].[UserDepartments] (
    [UserID]      INT NOT NULL,
    [DepartmentID] INT NOT NULL,
    PRIMARY KEY (UserID, DepartmentID),
    FOREIGN KEY (UserID) REFERENCES [dbo].[UserIdentity](ID),
    FOREIGN KEY (DepartmentID) REFERENCES [dbo].[Departments](ID)
);
