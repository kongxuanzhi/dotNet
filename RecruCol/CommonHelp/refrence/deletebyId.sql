CREATE PROCEDURE [dbo].[usp_DeleteById]
    @tableName varchar(40), --ɾ���ı���
	@id int,  -- ɾ���к�
	@fDelete bit=1 -- 1��ɾ����0��ɾ����Ĭ����ɾ��,��0���������Ǽ�ɾ
AS
begin
	declare @sqlstr nvarchar(300);
	if (@fDelete=0)
	   set @sqlstr = 'update '+ @tableName+' set isDelete=1 where id='+cast(@id as varchar(100))+'';
	else
	   set @sqlstr = 'delete '+ @tableName+' where id ='+cast(@id as varchar(100))+'';
	 exec(@sqlstr)
end