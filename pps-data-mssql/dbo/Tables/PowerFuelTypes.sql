CREATE TABLE PowerFuelTypes (
    PowerFuelTypeID INT IDENTITY(1,1) PRIMARY KEY,
    PowerFuelTypeName VARCHAR(255), -- ENUM('Electricity', 'Steam', 'Diesel'), to be used in processing plants.
    Cost DECIMAL(10,2)
);