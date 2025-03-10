-- Inventory Management
CREATE TABLE Silos(
    SiloID INT IDENTITY(1,1) PRIMARY KEY,
    ProcessingPlantID INT NOT NULL,
    Capacity_Litres INT NOT NULL DEFAULT 20000,
    CurrentStock_Litres INT DEFAULT 0,
    LastUpdated DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProcessingPlantID) REFERENCES ProcessingPlants(PlantID)
);