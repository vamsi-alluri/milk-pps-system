INSERT INTO [Roles] ([Name], [RoleLevel])
VALUES 
  ('User', 1),
  ('Manager', 2),
  ('Admin', 3);


INSERT INTO [Departments] ([Name])
VALUES 
  ('Finance'),
  ('HR'),
  ('Operations'),
  ('Audit');

INSERT INTO [UserIdentity] ([Username], [PasswordHash], [Active], [RoleID], [IsPasswordStale])
VALUES 
  ('alice', '$2a$12$oCS3kRfDVXZj7l8tU5bikuh0w3NQHg57yLwfHVIXmmuIXp6lAlWFC', 1, (SELECT ID FROM Roles WHERE Name = 'Admin'), 0),
  ('bob',   '$2a$12$dRDQfD2yGCDu53EG8scRVus8QABNoMEogPSjEcg81bnE0ckgXj0Aa', 1, (SELECT ID FROM Roles WHERE Name = 'Manager'), 0),
  ('carol', '$2a$12$NPLOsxQBY9XtlvD0iUEEXOwaSuAnZ/uAMN8LfTsyUMVVdetO6er6m', 1, (SELECT ID FROM Roles WHERE Name = 'User'), 0);

INSERT INTO [UserDepartments] ([UserID], [DepartmentID])
VALUES 
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'alice'), (SELECT ID FROM Departments WHERE Name = 'Finance')),
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'bob'),   (SELECT ID FROM Departments WHERE Name = 'Operations')),
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'carol'), (SELECT ID FROM Departments WHERE Name = 'HR'));

INSERT INTO [UserAccessScopes] ([UserID], [DepartmentID])
VALUES 
  -- Alice (Admin) can access everything
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'alice'), (SELECT ID FROM Departments WHERE Name = 'Finance')),
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'alice'), (SELECT ID FROM Departments WHERE Name = 'HR')),
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'alice'), (SELECT ID FROM Departments WHERE Name = 'Operations')),
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'alice'), (SELECT ID FROM Departments WHERE Name = 'Audit')),

  -- Bob (Manager) can access Operations + Audit
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'bob'), (SELECT ID FROM Departments WHERE Name = 'Operations')),
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'bob'), (SELECT ID FROM Departments WHERE Name = 'Audit')),

  -- Carol (User) can only access HR
  ((SELECT ID FROM [UserIdentity] WHERE Username = 'carol'), (SELECT ID FROM Departments WHERE Name = 'HR'));
