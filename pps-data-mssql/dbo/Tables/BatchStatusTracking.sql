-- Batch Status Tracking
CREATE TABLE BatchStatusTracking (
    BatchID INT IDENTITY(1,1) PRIMARY KEY,
    StatusID INT NOT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Remarks TEXT,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID),
    FOREIGN KEY (StatusID) REFERENCES BatchStatuses(StatusID)
);