-- Correction Batches
CREATE TABLE CorrectionBatches (
    CorrectionBatchID INT IDENTITY(1,1) PRIMARY KEY,
    BatchID INT,
    Reason TEXT,            -- Why did it need the correction. Need more meaningful name.
    CorrectionTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID),
    Correction VARCHAR(200)              -- What correction has been made. Need more meaningful name.
);