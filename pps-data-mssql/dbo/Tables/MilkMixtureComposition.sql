CREATE TABLE MilkMixtureComposition (
    CompositionID INT IDENTITY(1,1) PRIMARY KEY,
    BatchID INT,
    MilkType VARCHAR(100),
    Percentage DECIMAL(5,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);