CREATE TABLE QualityControl (
    QualityID INT IDENTITY(1,1) PRIMARY KEY,
    BatchID INT,
    FatPercentage DECIMAL(5,2),
    SNFPercentage DECIMAL(5,2),
    AlcoholPercentage DECIMAL(5,2),
    LactometerReading DECIMAL(5,2),
    MBRT DECIMAL(5,2),
    HeatStability DECIMAL(5,2),
    ClotOnBoiling BIT,
    AdulterationTests NVARCHAR(MAX), -- As JSON
    ProtienPercentage DECIMAL(5,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);