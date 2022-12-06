
----view -----
----שולף שמות של אסירים ושם בית כלא

create view jails_prisoner 
as 
select per.first_name ,per.lasr_name,j.jail_name
from prisoners as pri
join persons as per
on pri.person_id=per.person_id
join jails_per_prisoner as jpp
on jpp.prisoner_id=pri.prisoner_id
join jails as j
on j.jail_id=jpp.jail_id


select * from jails_prisoner
select * from persons
select * from jails_per_prisoner
go
---פונקציה 
---- בודקת כמה שנים נישאר לאסיר זה לרצות
alter function dbo.func_year (@tz bigint)
returns int 
as
begin
declare @date int, @year int 
		set @date=(
select year(f.event_date)
from files as f
join persons as p 
on p.person_id=f.person_id 
join spaicl_event as pe
on pe.event_id=f.event_id
where pe.event_name='התחלת ריצוי' and p.tz=@tz)

set @year=(
select sum(f.mis_year)
from files as f
join persons as p 
on p.person_id=f.person_id 
where p.tz=@tz
group by  p.person_id
)
if(@year-(year(getdate())-@date)<1)
return 0

return (@year-(year(getdate())-@date))

end

go

select  dbo.func_year (256486374)

----- פרוצדורה
----מקבלת קוד אסיר וקוד כלא ובודקת אם אסיר זה היה בכלא זה

go
create  procedure dbo.pro_prisoner (@prisoner_id int,@jail_id int)
as
begin 
 if exists(select prisoner_id from prisoners where prisoner_id=@prisoner_id)
       if exists(select jail_id from jails where jail_id=@jail_id)
	      if exists(select * from jails_per_prisoner where prisoner_id=@prisoner_id and jail_id=@jail_id)
		  begin
		      print 'אסיר זה היה כבר בבית כלא זה'  
               update jails_per_prisoner set enter_date=GETDATE() where prisoner_id=@prisoner_id
			   update jails_per_prisoner set end_date=null where prisoner_id=@prisoner_id
		  end
		  else
		   insert into jails_per_prisoner (prisoner_id,jail_id,enter_date) values (@prisoner_id,@jail_id,getdate()) 
		   else
		   print 'קוד כלא לא קיים'
		   else 
		   print 'קוד אסיר לא קיים'
		   end

		   exec dbo.pro_prisoner 20, 100
		    go
		   -----טריגר
		   ------כשמעדכנים אסיר בבית כלא בודק אם נשאר מקומות בבית כלא זה
		   create trigger tri_insert_prisoner on [dbo].[jails_per_prisoner] for insert
		   as
		   begin 
		   declare @countp int, @maxj int ,@jailid int
		   select @jailid=jail_id from inserted
		   select @countp= COUNT(*) from jails_per_prisoner p where p.jail_id=@jailid
		   select @maxj=max_prisoners from jails where jails.jail_id=@jailid
		   if(@countp<@maxj)
		   print 'האסיר עודכן . נותרו'+convert(varchar(30) ,(@maxj-@countp))+'מקומות מתוך'+convert(varchar(30) ,(@maxj))+' מקומות'
		   else 
		   begin 
		   print 'בית כלא זה מלא'
		   rollback  
		   end
		   end

		   insert into jails_per_prisoner values (15,100,GETDATE(),null)

		   select * from jails_per_prisoner

		   delete from jails_per_prisoner where jails_per_prisoner_id=19

		   ----union
		  ----שולף את העובדים במשמרות 1 ו2

		   select p.first_name,p.lasr_name ,s.beginning_time ,s.end_time
		   from persons p
		   join workers w
		   on w.person_id=p.person_id
		   join shifts s
		   on s.shift_id=w.shift_id
		   where s.shift_id=1
		   union
		    select p.first_name,p.lasr_name ,s.beginning_time ,s.end_time
		   from persons p
		   join workers w
		   on w.person_id=p.person_id
		   join shifts s
		   on s.shift_id=w.shift_id
		   where s.shift_id=2
		   order by s.beginning_time

		 -----  subselect
		--- שולף את המנהל שעובד הכי הרבה שנים
		 select p.first_name,wt.worker_type_name ,w.date_start_work
		 from persons p
		 join workers w
		 on w.person_id=p.person_id
		 join worker_type wt
		 on wt.worker_type_id=w.worker_type_id and wt.worker_type_id=1
		 where year(w.date_start_work)=( select min(year(w.date_start_work))
		 from workers w)
		 

-----try catch
begin try
 insert into shifts (beginning_time,[end_time])
 values('08:00:00','12')
 print 'הנתון הוכנס בהצלחה'
 end try
 begin catch
 print 'שגיעה, הנתון לא הוכנס בצורה נכונה'
 end catch

 select * from shifts

 