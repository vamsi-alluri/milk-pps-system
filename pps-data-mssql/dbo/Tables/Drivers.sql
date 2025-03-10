CREATE TABLE Drivers (
    DriverID INT IDENTITY(1,1) PRIMARY KEY,
    DriverName VARCHAR(255) NOT NULL,
    DriverPhoneNumber VARCHAR(20),
    DriverLicenseNumber VARCHAR(50) UNIQUE,
    DriverLicenseValidUpTo DATE,
    DriverBadgeNumber VARCHAR(50) UNIQUE
);