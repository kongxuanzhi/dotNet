
select * from T_Login

-- DateDiff(second,dt.date,CONVERT(varchar(100), GETDATE(), 120)

CREATE PROCEDURE usp_Login
@pageSize int = 15,
@index int = 1,
@totalPageCount int output,
@userId varchar(200)='%', --¶©²ÍÁ÷Ë®ºÅ
@loginTime varchar(300)='%'
AS
begin
	declare  @final table
	(
		id int, userId varchar(150), loginTime varchar(200), que int
	) 
	insert into @final 
	select 
	*,ROW_NUMBER() over(order by id desc) as que
	from T_Login as dt 
	where userId like '%'+@userId +'%'
	and loginTime like '%'+ @loginTime +'%'

	select * from @final where que between (@index-1)*@pageSize+1 and @index*@pageSize;
	
	declare @TC int = ( select COUNT(*) from @final)
	if @TC % @pageSize = 0
	  set @totalPageCount =   @TC / @pageSize;
	else
      set @totalPageCount = @TC / @pageSize + 1;
end