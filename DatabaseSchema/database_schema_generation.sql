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

-- Duration Types is for profitability reports. WIP, don't use yet.
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

CREATE TABLE PowerFuelTypes (
    PowerFuelTypeID INT AUTO_INCREMENT PRIMARY KEY,
    PowerFuelTypeName VARCHAR(255), -- ENUM('Electricity', 'Steam', 'Diesel'), to be used in processing plants.
    Cost DECIMAL(10,2)
);

CREATE TABLE PackingMaterials (
    PackingMaterialID INT AUTO_INCREMENT PRIMARY KEY,
    PackingMaterialName VARCHAR(255),
    Cost DECIMAL(10,2),
    PurposeOfPackingMaterial VARCHAR(200)
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
    MilkTypesAvailable JSON, -- JSON format: [{"MilkType": "Cow", "Cost": 50}, {"MilkType": "Buffalo", "Cost": 60}]
    OnRoute INT,
    FOREIGN KEY (OnRoute) REFERENCES Routes(RouteID)
);

-- Routes
--      The route which a vehicle has to go around collecting or distributing the products.
CREATE TABLE Routes (
    RouteID INT AUTO_INCREMENT PRIMARY KEY,
    RouteCode VARCHAR(100) NOT NULL UNIQUE,          -- Based on village code, SDN-CHV-AG##-### -> Shadnagar-chevella-agent_number-route_number
    RouteName VARCHAR(200) NOT NULL,
    AssociatedProcessingPlant INT,
    FOREIGN KEY (AssociatedProcessingPlant) REFERENCES ProcessingPlants(ProcessingPlantID)
);

-- Chilling Centers
CREATE TABLE ChillingCenters (
    ChillingCenterID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,
    Capabilities TEXT, -- JSON format for future extensibility
    Cost DECIMAL(10,2),
    OnRoute INT,
    FOREIGN KEY (OnRoute) REFERENCES Routes(RouteID)
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
    CarryingCapacity DECIMAL(10,2),
    VehicleCapacity DECIMAL(10,2),
    Cost DECIMAL(10,2),
    DriverID INT,   -- Who's driving.
    RouteID INT, -- Which route it is on.
    FOREIGN KEY (DriverID) REFERENCES Drivers(DriverID),
    FOREIGN KEY (RouteID) REFERENCES Routes(RouteID),
    FOREIGN KEY (VehicleTypeID) REFERENCES VehicleTypes(TypeID),
    FOREIGN KEY (GoodsID) REFERENCES GoodsTypes(GoodsID),
);

CREATE TABLE Drivers (
    DriverID INT AUTO_INCREMENT PRIMARY KEY,
    DriverName VARCHAR(255) NOT NULL,
    DriverPhoneNumber VARCHAR(20),
    DriverLicenseNumber VARCHAR(50) UNIQUE,
    DriverLicenseValidUpTo DATE,
    DriverBadgeNumber VARCHAR(50) UNIQUE
);

-- Processing Plants
CREATE TABLE ProcessingPlants (
    PlantID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,
    Capabilities TEXT, -- JSON format to store processing capabilities
    Cost DECIMAL(10,2)
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
    SNFPercentage DECIMAL(5,2),             -- Solids Non Fat
    SMPPercentage DECIMAL(5,2),             -- Skim Milk Powder.
    MilkMixtureComposition INT NOT NULL,    -- TODO: Figure out a way to track the milk mixture composition without just inputing it.
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    StatusID INT NOT NULL,
    ForRoute NULL INT,                      -- Optional - can be null, doesn't have to be for a route.
    FOREIGN KEY (PlantID) REFERENCES ProcessingPlants(PlantID),
    FOREIGN KEY (StatusID) REFERENCES BatchStatuses(StatusID),
    FOREIGN KEY (SiloID) REFERENCES Silos(SiloID),
    FOREIGN KEY (ForRoute) REFERENCES Routes(RouteID)
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
    Reason TEXT,            -- Why did it need the correction. Need more meaningful name.
    CorrectionTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID),
    Correction              -- What correction has been made. Need more meaningful name.
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

CREATE TABLE MilkMixtureComposition (
    CompositionID INT AUTO_INCREMENT PRIMARY KEY,
    BatchID INT,
    MilkType VARCHAR(100),
    Percentage DECIMAL(5,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);

CREATE TABLE QualityControl (
    QualityID INT AUTO_INCREMENT PRIMARY KEY,
    BatchID INT,
    FatPercentage DECIMAL(5,2),
    SNFPercentage DECIMAL(5,2),
    AlcoholPercentage DECIMAL(5,2),
    LactometerReading DECIMAL(5,2),
    MBRT DECIMAL(5,2),
    HeatStability DECIMAL(5,2),
    ClotOnBoiling BOOLEAN,
    AdulterationTests JSON,
    ProtienPercentage DECIMAL(5,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);

CREATE TABLE TemperatureTracking (
    TempID INT AUTO_INCREMENT PRIMARY KEY,
    BatchID INT,
    Stage VARCHAR(255),
    Temperature DECIMAL(5,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);

CREATE TABLE CurdBatches (                          -- TODO: Think how to incorporate this into the generic Batches.
    CurdBatchID INT AUTO_INCREMENT PRIMARY KEY,
    BatchID INT,
    CurdType VARCHAR(255),
    Quantity DECIMAL(10,2),
    FOREIGN KEY (BatchID) REFERENCES Batches(BatchID)
);



