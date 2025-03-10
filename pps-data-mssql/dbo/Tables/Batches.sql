-- Batch Processing
CREATE TABLE Batches (
    BatchID INT IDENTITY(1,1) PRIMARY KEY,
    PlantID INT,
    SiloID INT NOT NULL,
    BatchName VARCHAR(200),
    MilkType VARCHAR(100) NOT NULL,
    FatPercentage DECIMAL(5,2),
    SNFPercentage DECIMAL(5,2),             -- Solids Non Fat
    SMPPercentage DECIMAL(5,2),             -- Skim Milk Powder.
    MilkMixtureComposition INT NOT NULL,    -- TODO: Figure out a way to track the milk mixture composition without just inputing it.
    CreatedAt DATETIME DEFAULT GETDATE(),
    StatusID INT NOT NULL,
    ForRoute INT NULL,                      -- Optional - can be null, doesn't have to be for a route.
    FOREIGN KEY (PlantID) REFERENCES ProcessingPlants(PlantID),
    FOREIGN KEY (StatusID) REFERENCES BatchStatuses(StatusID),
    FOREIGN KEY (SiloID) REFERENCES Silos(SiloID),
    FOREIGN KEY (ForRoute) REFERENCES Routes_(RouteID)
);