CREATE FUNCTION GetProductCount(@Id int)
                            RETURNS BIGINT
                        AS
                        BEGIN
                            declare @total BIGINT
                            select @total =  SUM (Quantity) FROM OrderDetails WHERE ProductId = @Id
                            RETURN ISNULL(@total,0)
                        END
go

