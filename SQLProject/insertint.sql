


insert into cities(city_name) values ('???????')
insert into cities(city_name) values ('?? ????')
insert into cities(city_name) values ('????')
insert into cities(city_name) values ('??? ???')
insert into cities(city_name) values ('??? ???')
insert into cities(city_name) values ('???')
insert into cities(city_name) values ('????')
insert into cities(city_name) values ('????')

select * from cities
delete cities where city_name='????'



insert into persons values (234234256,'????','???','1978-04-26',4,'???','0564545676')
insert into persons values (256486374,'????','???','1980-06-16',2,'???','0526765223')
insert into persons values (134546567,'??????','???','1990-11-05',1,'????','0524558659')
insert into persons values (125452152,'???','?????','1991-07-30',5,'???',null)
insert into persons values (215425145,'?????','??????','1975-04-01',1,'???',null)
insert into persons values (256486374,'????','????','1995-12-26',4,'???','0564545676')
insert into persons values (215425856,'?????','???','1992-11-25',6,'????','0524155685')
insert into persons values (253269852,'????','????','2001-12-26',3,'???',null)
insert into persons values (232514215,'???','??????','2002-05-09',5,'???',null)
insert into persons values (124152356,'?????','????','1980-10-10',2,'???',null)
insert into persons values (125412585,'????','??','1973-08-19',5,'???',null)
insert into persons values (125268542,'?????','????','1985-12-10',2,'????','0524152436')
,(232573773,'?????','???','1999-12-06',4,'???','0547836776')
 ,(546777390,'??????','???','2000-01-01',4,'????','0547836775')
 ,(234803887,'???','???????','1985-05-06',2,'????','0536765982')
 ,(453683793,'?????','????','1995-09-08',1,'???','0527616766')
 ,(234856667,'???','???','1978-01-06',3,'???','0556738778')
 ,(213455367,'???','????','1985-05-13',5,'????',NULL)
, (108563946,'?????','????????','1965-09-05',6,'????',NULL)
 	 ,(129918360,'?????	',' ????????','1980-07-26',6,'????','0556666382')
 	 ,(477758577,'?????','??? ????','1990-06-29',5,'???','057778883')
	 ,(129916760,'????',' ??????','1980-08-29',1,'????',NULL)
	 insert into persons values(129567360,'?????',' ?????','1963-01-06',1,'???',NULL)
	 ,(129955603,'?????',' ?? ????','1998-12-25',1,'???','054568382')
     ,(156067743,'?????',' ??? ','1990-06-29',1,'???','058988382') 
	 

	  update persons
    set phone='0521425621'
    where tz=129567360 
	 
	 
	
	 select * from persons 
	 select * from prisoners
	 select * from jails
	 insert into [dbo].[prisoners_reason] values ('???'),('?????'),('???? ????'),('????'),('?????')
	
	insert into prisoners values (1,1,125)
	,(3,5,524)
	,(5,1,1325)
	,(6,3,452)
	,(8,2,711)
	,(9,4,251),(10,5,854),(32,2,635),(33,3,854),(35,4,524),(37,3,745),(43,4,856),(44,1,635),(48,4,9656),(49,5,854)

	insert into shifts values ('06:00','12:30'),('12:30','19:00'),('19:00','01:30'),('01:30:00:00','06:00')

	insert into worker_type values ('????'),('????'),('???')

	
	insert into jails values ('??????',7,0771462362,250),('???? ????',7,0725142582,300),('????',1,0752148362,1500),('?????',2,NULL,550)

	insert into workers values (2,1,2,'2002-06-23',100),(4,2,1,'2009-12-03',102),(7,1,1,'2010-09-28',101),(11,1,2,'1995-03-01',102),(34,2,3,'2020-01-23',101),(36,2,1,'2015-08-10',103),(38,3,4,'2001-03-28',101)
	,(45,3,2,'2003-05-14',100),(50,1,2,'2013-08-09',103)
	insert into workers values (43,2,2,'2018-05-23',101)
	
	


	insert into spaicl_event values ('??????? ????'),('????? ???'),('???? ?????'),('?? ?????')
	insert into spaicl_event values ('????? ?????')
	select * from spaicl_event
	

	insert into files values (1,5,'2000-01-20',null,15),(3,5,'2015-05-23',null,10),(5,5,'2019-06-01',null,4),(6,5,'2019-12-01',null,30)
	,(8,5,'2019-08-01',null,25),(9,5,'2021-01-14',null,8),(10,5,'2015-08-23',null,19),(32,5,'2020-09-12',null,6),(33,5,'2021-06-30',null,25)
	,(35,5,'2020-12-05',null,5),(37,5,'2021-01-14',null,35),(43,5,'2009-06-12',null,10),(44,5,'2021-02-16',null,5),(48,5,'1990-01-14',null,15)
	,(49,5,'2019-03-02',null,3)
	,(9,2,'2021-02-14',null,1),(1,1,'2001-05-14',null,-4),(5,3,'2020-06-12',null,-1),(10,4,'2017-01-20',null,1),(10,1,'2019-02-14',null,-3)
	,(6,3,'2021-03-20',null,-5)

	insert into jails_per_prisoner values (1,100,'2000-01-20','2011-01-20'),(2,101,'2015-05-23',null),(3,102,'2019-06-01',null),(4,102,'2019-12-01',null)
	,(5,103,'2019-08-01',null),(6,100,'2021-01-14',null),(7,102,'2015-08-23',null),(8,101,'2020-09-12',null),(9,102,'2021-06-30',null)
	,(10,101,'2020-12-05',null),(11,100,'2021-01-14',null),(12,100,'2009-06-12','2018-04-02'),(12,102,'2015-04-02','2019-06-12'),(13,103,'2021-02-16',null),(14,103,'1990-01-14','2005-01-14')
	,(15,100,'2019-03-02',null)