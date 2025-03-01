USE MilkProcessingDB;

-- Insert default vehicle types
INSERT INTO VehicleTypes (TypeName) VALUES
('MilkContainer'),
('MilkTanker'),
('Truck');

-- Insert default goods types
INSERT INTO GoodsTypes (GoodsName) VALUES
('RawMilk'),
('ProcessedMilk'),
('Milk Products/Sweets'),
('Packaging Items');

-- Insert default batch statuses
INSERT INTO BatchStatuses (StatusName) VALUES
('Pending Approval'),
('Approved'),
('Rejected'),
('Processed'),
('Completed');

-- Insert default duration types
INSERT INTO DurationTypes (DurationName) VALUES
('Daily'),
('Weekly'),
('Monthly'),
('Per 10 Days'),
('Yearly');
