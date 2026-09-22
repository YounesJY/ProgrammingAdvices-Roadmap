use c21_db1;

--- -------------------------------------------
--- -------------------------------------------
declare @result int = 0;
exec @result = usp_checkpersonexists @personid = 6; -- replace 123 with the actual personid


if @result = 1
    print 'person exists.';
else
    print 'person does not exist.';
--- -------------------------------------------
--- -------------------------------------------