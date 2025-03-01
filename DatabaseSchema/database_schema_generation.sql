CREATE DATABASE MilkProcurementProcessingAndSales;
USE MilkProcurementProcessingAndSales;

-- Drop existing tables if they exist (use with caution)
DROP TABLE IF EXISTS ProfitabilityReports;
DROP TABLE IF EXISTS BatchCosting;
DROP TABLE IF EXISTS RejectedBatches;
DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS PlantCapabilities;
DROP TABLE IF EXISTS ProcessingCapabilities;
DROP TABLE IF EXISTS Batches;
DROP TABLE IF EXISTS Silos;
DROP TABLE IF EXISTS ProcessingPlants;
DROP TABLE IF EXISTS Goods;
DROP TABLE IF EXISTS TransportVehicles;
DROP TABLE IF EXISTS VehicleTypes;
DROP TABLE IF EXISTS ChillingCenters;
DROP TABLE IF EXISTS MilkSuppliers;
DROP TABLE IF EXISTS GoodsTypes;
DROP TABLE IF EXISTS BatchStatuses;
DROP TABLE IF EXISTS RejectedBatches;
DROP TABLE IF EXISTS MilkTypes;
DROP TABLE IF EXISTS Silos;



-- Enum Tables
CREATE TABLE VehicleTypes (
    TypeID INT AUTO_INCREMENT PRIMARY KEY,
    TypeName VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE GoodsTypes (
    GoodsID INT AUTO_INCREMENT PRIMARY KEY,
    GoodsName VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE BatchStatuses (
    StatusID INT AUTO_INCREMENT PRIMARY KEY,
    StatusName VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE DurationTypes (
    DurationID INT AUTO_INCREMENT PRIMARY KEY,
    DurationName VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE MilkTypes (
    MilkID INT AUTO_INCREMENT PRIMARY KEY,
    MilkName VARCHAR(50) UNIQUE NOT NULL,
    CostPerLitre DECIMAL(5,2) NOT NULL,
    BuffaloOrCow VARCHAR(2) NOT NULL
);


-- Inventory Management
CREATE TABLE Silos(
    SiloID INT AUTO_INCREMENT PRIMARY KEY,
    ProcessingPlantID INT NOT NULL,
    Capacity_Litres INT NOT NULL DEFAULT 20000,
    CurrentStock_Litres INT DEFAULT 0,
    LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProcessingPlantID) REFERENCES ProcessingPlants(ProcessingPlantID)
);

-- Physical Locations or persons

CREATE TABLE MilkSuppliers (
    AgentID INT AUTO_INCREMENT PRIMARY KEY,     -- Based on Village, Route.
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,           -- TBD on formatting - or further split with Street Address, City, State for filtering.
    MilkTypesAvailable TEXT -- JSON format: [{"MilkType": "Cow", "Cost": 50}, {"MilkType": "Buffalo", "Cost": 60}]
);

-- The route which a vehicle has to go around collecting or distributing the products.
CREATE TABLE Route (
    RouteID INT AUTO_INCREMENT PRIMARY KEY,     -- Based on village code, SDN-CHV-AG##-### -> Shadnagar-chevella-route_number

);

-- Chilling Centers
CREATE TABLE ChillingCenters (
    ChillingCenterID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,
    Capabilities TEXT, -- JSON format for future extensibility
    Cost
);

-- Transport Vehicles
CREATE TABLE TransportVehicles (
    VehicleID INT AUTO_INCREMENT PRIMARY KEY,
    VehicleNumber VARCHAR(50) NOT NULL UNIQUE,
    VehicleTypeID INT NOT NULL,
    GoodsID INT NOT NULL,
    Source VARCHAR(255),
    Destination VARCHAR(255),
    LastLocation VARCHAR(255),
    FOREIGN KEY (VehicleTypeID) REFERENCES VehicleTypes(TypeID),
    FOREIGN KEY (GoodsID) REFERENCES GoodsTypes(GoodsID),
    CarryingCapacity,
    VehicleCapacity,
    Cost
);

CREATE TABLE Driver(
    DriverId,
    DriverName,
    DriverPhoneNumber,
    DriverLicenseNumber,
    DriverLicenseValidUpTo,
    DriverBadgeNumber
);

-- Processing Plants
CREATE TABLE ProcessingPlants (
    PlantID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,
    Capabilities TEXT, -- JSON format to store processing capabilities
    Cost
);

-- Inventory Tracking
CREATE TABLE Inventory (
    InventoryID INT AUTO_INCREMENT PRIMARY KEY,
    ProcessingPlantID INT,
    ItemTypeID INT NOT NULL,
    MilkTypeID INT,
    FatPercentage DECIMAL(5,2),
    SNFPercentage DECIMAL(5,2),
    Quantity_Litres INT,
    Quantity_Kg INT,
    LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProcessingPlantID) REFERENCES ProcessingPlants(PlantID)
    FOREIGN KEY (MilkType) REFERENCES MilkTypes(MilkID)
);

-- Batch Processing
CREATE TABLE Batches (
    BatchID INT AUTO_INCREMENT PRIMARY KEY,
    PlantID INT,
    SiloID INT NOT NULL,
    BatchName VARCHAR(200),
    MilkType VARCHAR(100) NOT NULL,
    FatPercentage DECIMAL(5,2),
    SNFPercentage DECIMAL(5,2),
    SMP, -- Skim Milk Powder.
    MilkMixturePercentages,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    StatusID INT NOT NULL,
    FOREIGN KEY (PlantID) REFERENCES ProcessingPlants(PlantID),
    FOREIGN KEY (StatusID) REFERENCES BatchStatuses(StatusID),
    FOREIGN KEY (SiloID) REFERENCES Silos(SiloID)
);

-- Batch Status Tracking
CREATE TABLE BatchStatusTracking (
    BatchID INT,
    StatusID INT NOT NULL,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    Remarks TEXT,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID),
    FOREIGN KEY (StatusID) REFERENCES BatchStatuses(StatusID)
);

-- Correction Batches
CREATE TABLE CorrectionBatches (
    CorrectionBatchID INT AUTO_INCREMENT PRIMARY KEY,
    BatchID INT,
    Reason TEXT,
    CorrectionTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID),
    Correction
);

-- Procurement Cost Tracking
CREATE TABLE BatchCosting (
    BatchID INT PRIMARY KEY,
    ProcurementCost DECIMAL(10,2),      -- Farmer + Agent Milk cost, Chilling Centre expenses, transport. power and fuel, man power cost.
    ProcessingCost DECIMAL(10,2),       -- power and fuel, man power cost, Consumables(acids and cleaning agents) and maintenance(regular and breakdowns).
    PackagingCost DECIMAL(10,2),        -- power and fuel.
    TransportationCost DECIMAL(10,2),
    TotalCost DECIMAL(10,2) GENERATED ALWAYS AS (ProcurementCost + ProcessingCost + PackagingCost + TransportationCost) STORED,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);

-- Profitability Reports
CREATE TABLE ProfitabilityReports (
    ReportID INT AUTO_INCREMENT PRIMARY KEY,
    ReportDate DATE NOT NULL,
    DurationID INT NOT NULL,
    TotalSalesRevenue DECIMAL(10,2) NOT NULL,
    TotalProcurementCost DECIMAL(10,2) NOT NULL,
    TotalProcessingCost DECIMAL(10,2) NOT NULL,
    TotalPackagingCost DECIMAL(10,2) NOT NULL,
    TotalTransportationCost DECIMAL(10,2) NOT NULL,
    TotalProfitOrLoss DECIMAL(10,2) GENERATED ALWAYS AS (TotalSalesRevenue - (TotalProcurementCost + TotalProcessingCost + TotalPackagingCost + TotalTransportationCost)) STORED,
    FOREIGN KEY (DurationID) REFERENCES DurationTypes(DurationID)
);

CREATE TABLE QualityTracking(
    
);

-- TODO:

-- Power and fuel subtopics: electricity(government power and solar), steam(timber, brickets, coal and others), diesel.

-- PackingMaterial:
-- Film (milk, curd, ghee, paneer), buckets(10kg, 5kg, 1kg), cups

-- Quality Control: fat%, snf%, alcohol% [acceptable 68 - 70 range] -> Shelf Life, 
-- Lactometer Reading -> SNF, MBRT(Methane Blue Reduction Test) [acceptable raw: atleast 1 hr or above; processed: atleast 5hrs], HS(Heat Stability) [accepted 6 and above], COB (Clot on Boiling), Acidity[acceptable .126 to .140], Protein[CM: min 3.4%, BM: min 3.55%], 
-- Adultretion Test -> Ranges should be negative: Urea, Sugar(sucrose, maltose), Salt, Pesticide, Antibiotic, Malamine, Starch. PT Test[atleast 2hrs]
-- Temperatures - raw milk, pasteurization, packing.
-- Curd Batches,


