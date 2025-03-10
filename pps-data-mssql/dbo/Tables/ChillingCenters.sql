-- Chilling Centers
CREATE TABLE ChillingCenters (
    ChillingCenterID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,
    Capabilities NVARCHAR(MAX), -- JSON
    Cost DECIMAL(10,2),
    OnRoute INT,
    FOREIGN KEY (OnRoute) REFERENCES Routes_(RouteID)
);