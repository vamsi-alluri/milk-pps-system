CREATE TABLE TemperatureTracking (
    TempID INT IDENTITY(1,1) PRIMARY KEY,
    BatchID INT,
    Stage VARCHAR(255),
    Temperature DECIMAL(5,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);