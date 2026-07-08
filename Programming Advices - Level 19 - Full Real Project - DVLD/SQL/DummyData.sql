-- ============================================
-- ADD MORE PEOPLE (No IDENTITY_INSERT)
-- ============================================
INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
VALUES
('NAT-2016', 'Adam', 'John', NULL, 'Wilson', '1995-04-12', 0, '123 Main St, New York, USA', '+12025551234', 'adam.wilson@email.com', 2, NULL),
('NAT-2017', 'Sophie', 'Marie', NULL, 'Martin', '1998-08-25', 1, '456 Oak Ave, Los Angeles, USA', '+12025555678', 'sophie.martin@email.com', 2, NULL),
('NAT-2018', 'James', 'Robert', NULL, 'Thompson', '2000-01-10', 0, '789 Pine St, Chicago, USA', '+12025559012', 'james.thompson@email.com', 2, NULL),
('NAT-2019', 'Olivia', 'Jane', NULL, 'Anderson', '1993-11-30', 1, '321 Maple Dr, London, UK', '+442079876543', 'olivia.anderson@email.com', 3, NULL),
('NAT-2020', 'William', 'Thomas', NULL, 'Martinez', '1996-06-18', 0, '654 Church St, Toronto, Canada', '+14165553456', 'william.martinez@email.com', 4, NULL),
('NAT-2021', 'Mariam', 'Ali', NULL, 'Hassan', '1994-03-03', 1, '12 Al Azhar St, Cairo, Egypt', '+20100678901', 'mariam.hassan@email.com', 1, NULL),
('NAT-2022', 'Kareem', 'Ahmed', NULL, 'Ibrahim', '1997-09-14', 0, '7 Nasr City, Cairo, Egypt', '+20100789012', 'kareem.ibrahim@email.com', 1, NULL),
('NAT-2023', 'Nour', 'Hassan', NULL, 'Farouk', '1991-12-01', 1, '3 Maadi, Cairo, Egypt', '+20100890123', 'nour.farouk@email.com', 1, NULL)
GO

-- ============================================
-- ADD MORE USERS
-- ============================================
-- First, get the new PersonIDs
DECLARE @PersonID1 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2016')
DECLARE @PersonID2 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2017')
DECLARE @PersonID3 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2018')

INSERT INTO Users (PersonID, UserName, Password, IsActive)
VALUES
(@PersonID1, 'adam_wilson', 'Adam@123', 1),
(@PersonID2, 'sophie_martin', 'Sophie@123', 1),
(@PersonID3, 'james_thompson', 'James@123', 1)
GO

-- ============================================
-- ADD MORE DRIVERS
-- ============================================
DECLARE @PersonID1 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2016')
DECLARE @PersonID2 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2017')
DECLARE @PersonID3 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2018')

INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
VALUES
(@PersonID1, 1, GETDATE()),
(@PersonID2, 1, GETDATE()),
(@PersonID3, 1, GETDATE())
GO

-- ============================================
-- ADD MORE APPLICATIONS
-- ============================================
DECLARE @PersonID1 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2016')
DECLARE @PersonID2 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2017')
DECLARE @PersonID3 INT = (SELECT PersonID FROM People WHERE NationalNo = 'NAT-2018')

INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatusID, LastStatusDate, PaidFees, CreatedByUserID)
VALUES
(@PersonID1, GETDATE(), 1, 1, GETDATE(), 15.00, 1),
(@PersonID2, GETDATE(), 1, 1, GETDATE(), 15.00, 1),
(@PersonID3, GETDATE(), 1, 1, GETDATE(), 15.00, 1)
GO

-- ============================================
-- ADD MORE LOCAL DRIVING LICENSE APPLICATIONS
-- ============================================
-- Get the new ApplicationIDs (assuming they're the last 3)
DECLARE @AppID1 INT = (SELECT MAX(ApplicationID) - 2 FROM Applications)
DECLARE @AppID2 INT = (SELECT MAX(ApplicationID) - 1 FROM Applications)
DECLARE @AppID3 INT = (SELECT MAX(ApplicationID) FROM Applications)

INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
VALUES
(@AppID1, 1),
(@AppID2, 1),
(@AppID3, 1)
GO

