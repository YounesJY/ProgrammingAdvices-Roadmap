-- Get Total Number Of Doors Manufactured by 'Ford'


-- SOLUTION 1 : USING 'JOINS' [Simple logic + StrightForward]
SELECT 
	Makes.Make,
	TotalNumberOfManufacturedDoors = SUM(VehicleDetails.NumDoors)
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID
WHERE Makes.Make = N'Ford'									  
GROUP BY Makes.Make
-- HAVING Makes.Make = N'Ford'							  
-- =================================================== !! VERY IMPORTANT NOTE !! ==========================================================================================
-- !! We'll get the SAME RESULT either by using WHERE or HAVING, BUT ....																								 !!
-- !! USING WHERE  --> Keep ONLY the Records with Make 'Ford'			--> Aggregate data & calculate SUM for this specific Make 										 !!
-- !! USING HAVING --> Keep ALL  the Records REGUARDLESS of their Makes --> Aggregate data & calculate SUM for ALL Makes --> FILTER and keep the record with make 'Ford' !!
-- !! REMEMBER [WHERE  <--> Filter data BEFORE Aggregation] + [HAVING <--> Filter data AFTER  Aggregation]																 !!
-- !! We're targeting 'Ford' ? --> Use WHERE and keep only records with 'Ford' then do the aggregation [Faster + More Efficient + Less Resources Usage]					 !!
-- ========================================================================================================================================================================


-- SOLUTION 2 : USING 'Coorelated SubQuery' [More Advanced + Iteration-based --> For Each Record]
-- For each returned record (Make), we're calculating the TotalNumberOfManufacturedDoors for this Make (on the fly), then move to the next record
-- =================================================== !! VERY IMPORTANT NOTE !! ===================================================================
-- !! The 'Coorelated SubQuery' CAN'T be executed as a STANDALONE Query, BECAUSE it relays on an outer reference (ref) from the MAIN/OUTER Query  !!
-- !! If you got this error "The multi-part identifier "ref.MakeID" could not be bound." then EVERYTHING IS GOOD ''\__(^_^)__/''				  !!
-- =================================================================================================================================================
SELECT 
	Make,
	TotalNumberOfManufacturedDoors = (
				SELECT SUM(VehicleDetails.NumDoors)
				FROM VehicleDetails
				WHERE VehicleDetails.MakeID = ref.MakeID
	)
FROM Makes AS ref
WHERE Make = N'Ford';
-- !! the use of WHERE is to calculate the TotalNumberOfManufacturedDoors by 'Ford' , not by all makes [like in Problem 43]
-- !! By removing WHERE, you're getting the EQUIVALENT OF USING 'LEFT/RIGHT' JOIN by keeping all makes REGUARDLESS if they did/didn't make any vehicles


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ============================================================================================


