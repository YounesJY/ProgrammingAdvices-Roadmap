-- Get Minimum Engine CC , Maximum Engine CC , and Average Engine CC of all Vehicles


-- SOLUTION : 
SELECT 
	 MinimumEngineCC = MIN(Engine_CC),
	 MaximumEngineCC = MAX(Engine_CC),
	 AverageEngineCC = AVG(Engine_CC)
FROM VehicleDetails