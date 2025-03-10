-- Procurement Cost Tracking
CREATE TABLE BatchCosting (
    BatchID INT IDENTITY(1,1) PRIMARY KEY,
    ProcurementCost DECIMAL(10,2),      -- Farmer + Agent Milk cost, Chilling Centre expenses, transport. power and fuel, man power cost.
    ProcessingCost DECIMAL(10,2),       -- power and fuel, man power cost, Consumables(acids and cleaning agents) and maintenance(regular and breakdowns).
    PackagingCost DECIMAL(10,2),        -- power and fuel.
    TransportationCost DECIMAL(10,2),
    TotalCost AS (ProcurementCost + ProcessingCost + PackagingCost + TransportationCost) PERSISTED,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);