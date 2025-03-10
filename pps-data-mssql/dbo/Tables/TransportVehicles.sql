-- Transport Vehicles
CREATE TABLE TransportVehicles (
    VehicleID INT IDENTITY(1,1) PRIMARY KEY,
    VehicleNumber VARCHAR(50) NOT NULL UNIQUE,
    VehicleTypeID INT NOT NULL,
    GoodsID INT NOT NULL,
    Source VARCHAR(255),
    Destination VARCHAR(255),
    LastLocation VARCHAR(255),
    CarryingCapacity DECIMAL(10,2),
    VehicleCapacity DECIMAL(10,2),
    Cost DECIMAL(10,2),
    DriverID INT,   -- Who's driving.
    RouteID INT, -- Which route it is on.
    FOREIGN KEY (DriverID) REFERENCES Drivers(DriverID),
    FOREIGN KEY (RouteID) REFERENCES Routes_(RouteID),
    FOREIGN KEY (VehicleTypeID) REFERENCES VehicleTypes(TypeID),
    FOREIGN KEY (GoodsID) REFERENCES GoodsTypes(GoodsID)
);