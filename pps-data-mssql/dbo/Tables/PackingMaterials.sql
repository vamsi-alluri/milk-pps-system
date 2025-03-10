CREATE TABLE PackingMaterials (
    PackingMaterialID INT IDENTITY(1,1) PRIMARY KEY,
    PackingMaterialName VARCHAR(255),
    Cost DECIMAL(10,2),
    PurposeOfPackingMaterial VARCHAR(200)
);