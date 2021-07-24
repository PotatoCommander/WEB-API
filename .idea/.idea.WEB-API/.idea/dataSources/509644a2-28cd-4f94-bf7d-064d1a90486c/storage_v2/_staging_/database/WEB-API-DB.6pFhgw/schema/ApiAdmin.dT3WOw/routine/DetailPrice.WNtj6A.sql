CREATE FUNCTION DetailPrice(@Id int, @quantity bigint)
        RETURNS DECIMAL
    AS
    BEGIN
        declare @price decimal
        select @price =  @quantity * Price FROM Products WHERE Id = @Id
        RETURN ISNULL(@price,0)
    END
go

