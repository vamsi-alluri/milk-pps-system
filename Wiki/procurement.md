# Procurement

- Milk comes from different collection agencies/farmers, let's call them collection "Agents", to chilling centers "ChillingCenters" that pool the milk and load them into trucks"TransportVehicles" heading to the processing plant"ProcessingPlants". 

### What each table should contain:
- **Agents**: name, location, address, milk types available (cost is allotted for each agent or a group of agents against each category of milk they sell, this is the cost to company, as I acquire milk from them)
- **ChillingCenters**: location, address(maybe split for easier searching), name, capabilities
TransportVehicles: Vehicle Number, Vehicle Type, Goods(that's being carried), Source, destination, last location
Vehicle Types: MilkContainer, MilkTanker, Truck...
- **Goods**: RawMilk, ProcessedMilk, Milk Products/Sweets, Packaging Items - And these should depend on which type of vehicle it is. *Container* is chillable, tanker is not, trucks could only carry solids/packed items.
- **ProcessingPlants**: name, location, address, capabilities. [[segway to processing plants](./Processing.md)]