-- Processing Plants
CREATE TABLE ProcessingPlants (
    PlantID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Address TEXT,
    Capabilities NVARCHAR(MAX), -- JSON
    Cost DECIMAL(10,2)
);