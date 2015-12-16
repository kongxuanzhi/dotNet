CREATE PROCEDURE [dbo].[usp_DeleteById]
    @tableName varchar(40), --删除的表名
	@id int,  -- 删除行号
	@fDelete bit=1 -- 1真删除，0假删除，默认真删除,传0进来，就是假删
AS
begin
	declare @sqlstr nvarchar(300);
	if (@fDelete=0)
	   set @sqlstr = 'update '+ @tableName+' set isDelete=1 where id='+cast(@id as varchar(100))+'';
	else
	   set @sqlstr = 'delete '+ @tableName+' where id ='+cast(@id as varchar(100))+'';
	 exec(@sqlstr)
end