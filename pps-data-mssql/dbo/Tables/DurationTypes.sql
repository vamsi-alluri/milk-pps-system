-- Duration Types is for profitability reports. WIP, don't use yet.
CREATE TABLE DurationTypes (
    DurationID INT IDENTITY(1,1) PRIMARY KEY,
    DurationName VARCHAR(50) UNIQUE NOT NULL
);