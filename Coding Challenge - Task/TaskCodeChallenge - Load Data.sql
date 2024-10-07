INSERT INTO Tasks (Title, Description, DueDate, Priority, Status) VALUES
('Fix Bug #101', 'Fix the login authentication issue for the web app', '2024-09-20', 'High', 'InProgress'),
('Update Documentation', 'Update API documentation for the new release', '2024-09-25', 'Medium', 'Pending'),
('Create Marketing Plan', 'Develop a marketing strategy for the product launch', '2024-10-05', 'High', 'Pending'),
('Design New Logo', 'Design a new company logo', '2024-09-30', 'Medium', 'InProgress'),
('Implement Payment Gateway', 'Integrate Razorpay payment gateway into the system', '2024-09-22', 'High', 'Completed'),
('Refactor Codebase', 'Refactor the codebase for better performance and maintainability', '2024-09-27', 'Medium', 'Pending'),
('Prepare Budget Report', 'Create the budget report for Q4', '2024-10-01', 'Low', 'InProgress'),
('Setup Server', 'Setup the production server for the application deployment', '2024-09-18', 'High', 'Completed'),
('Conduct User Testing', 'Perform user acceptance testing for the new features', '2024-09-24', 'Medium', 'Pending'),
('Plan Team Meeting', 'Schedule and prepare for the weekly team meeting', '2024-09-17', 'Low', 'Completed');

INSERT INTO Users (Email, Username, Password, Role) VALUES
('john.doe@example.com', 'john_doe', 'Password123', 'Customer'),
('jane.smith@example.com', 'jane_smith', 'SecurePass!789', 'Admin'),
('mike.johnson@example.com', 'mike_j', 'MikeJ2024$', 'Customer'),
('emily.davis@example.com', 'emily_d', 'EmilySecure789', 'Admin'),
('robert.brown@example.com', 'robert_b', 'R0b3rt_Password', 'Customer'),
('lucy.martin@example.com', 'lucy_martin', 'LucyMartin@2024', 'Admin'),
('david.white@example.com', 'david_white', 'D@v!dP@ss', 'Customer'),
('sophia.clark@example.com', 'sophia_clark', 'SophiaC_1!', 'Admin'),
('chris.moore@example.com', 'chris_m', 'Chr1sM00re#', 'Customer'),
('anna.lee@example.com', 'anna_lee', 'AnnaL_P@ssw0rd', 'Admin');

INSERT INTO Users (Email, Username, Password, Role) VALUES
('admin@example.com', 'admin', 'Password1', 'Admin'),
('customer@example.com', 'customer', 'Password1', 'Customer');

select * from Tasks;
select * from Users;