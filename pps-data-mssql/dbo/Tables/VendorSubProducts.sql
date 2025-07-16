CREATE TABLE [dbo].[VendorSubProducts]
(
	Id INT PRIMARY KEY IDENTITY(1,1),
    VendorId INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    ProductType NVARCHAR(100),  -- Optional categorization
    Cost DECIMAL(18, 2),

    CONSTRAINT FK_VendorSubProducts_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id)
        ON DELETE CASCADE
)
