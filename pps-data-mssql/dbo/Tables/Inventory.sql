-- Inventory Tracking
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProcessingPlantID INT,
    ItemTypeID INT NOT NULL,
    MilkTypeID INT,
    FatPercentage DECIMAL(5,2),
    SNFPercentage DECIMAL(5,2),
    Quantity_Litres INT,
    Quantity_Kg INT,
    LastUpdated DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProcessingPlantID) REFERENCES ProcessingPlants(PlantID),
    FOREIGN KEY (MilkTypeID) REFERENCES MilkTypes(MilkID)
);