
alter proc usp_select
@tableName varchar(200)
as
begin 
   declare @strSql varchar(100)  = 'select * from '+ @tableName; 
   exec(@strSql);
end
exec usp_select T_Items