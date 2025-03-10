-- Profitability Reports
CREATE TABLE ProfitabilityReports (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    ReportDate DATE NOT NULL,
    DurationID INT NOT NULL,
    TotalSalesRevenue DECIMAL(10,2) NOT NULL,
    TotalProcurementCost DECIMAL(10,2) NOT NULL,
    TotalProcessingCost DECIMAL(10,2) NOT NULL,
    TotalPackagingCost DECIMAL(10,2) NOT NULL,
    TotalTransportationCost DECIMAL(10,2) NOT NULL,
    TotalProfitOrLoss AS (TotalSalesRevenue - (TotalProcurementCost + TotalProcessingCost + TotalPackagingCost + TotalTransportationCost)) PERSISTED,
    FOREIGN KEY (DurationID) REFERENCES DurationTypes(DurationID)
);