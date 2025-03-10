-- Physical Locations or persons

CREATE TABLE MilkSuppliers (
    AgentID INT IDENTITY(1,1) PRIMARY KEY,     -- Based on Village, Route.
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,           -- TBD on formatting - or further split with Street Address, City, State for filtering.
    MilkTypesAvailable NVARCHAR(MAX), -- JSON format: [{"MilkType": "Cow", "Cost": 50}, {"MilkType": "Buffalo", "Cost": 60}]
    OnRoute INT,
    FOREIGN KEY (OnRoute) REFERENCES Routes_(RouteID)
);