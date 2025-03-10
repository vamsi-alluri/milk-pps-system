CREATE TABLE CurdBatches (                          -- TODO: Think how to incorporate this into the generic Batches.
    CurdBatchID INT IDENTITY(1,1) PRIMARY KEY,
    BatchID INT,
    CurdType VARCHAR(255),
    Quantity DECIMAL(10,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);