-- Routes
--      The route which a vehicle has to go around collecting or distributing the products.
CREATE TABLE Routes_ (
    RouteID INT IDENTITY(1,1) PRIMARY KEY,
    RouteCode VARCHAR(100) NOT NULL UNIQUE, -- Based on village code, SDN-CHV-AG##-### -> Shadnagar-chevella-agent_number-route_number
    RouteName VARCHAR(200) NOT NULL,
    AssociatedProcessingPlant INT,
    FOREIGN KEY (AssociatedProcessingPlant) REFERENCES ProcessingPlants(PlantID)
);