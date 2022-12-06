


create table cities 
(city_id int identity not null ,
city_name varchar(20) not null,
constraint pk_city primary key (city_id)
)

create table persons 
(person_id int identity not null,
constraint pk_person primary key (person_id),
tz bigint not null,
first_name varchar(20) not null,
lasr_name varchar(30) not null,
date_born date not null,
city_id int not null,
constraint fk_person_city foreign key (city_id) references cities (city_id),
gender varchar(5) not null ,
phone varchar(10) null
)

 create table prisoners_reason
 (reason_id int identity not null,
 constraint pk_reason primary key (reason_id),
 prisoners_reason varchar(40) not null,
 )

 create table prisoners(
 prisoner_id int identity not null
 constraint pk_prisoner primary key (prisoner_id),
 person_id int not null,
 constraint fk_tz_prisoner foreign key (person_id) references persons (person_id),

 reason_id int not null ,
 constraint fk_reason_prisoner foreign key (reason_id) references prisoners_reason (reason_id),
 cell_number int not null,

 )
 

 create table worker_type
 (worker_type_id int identity not null,
 constraint pk_worker_type primary key (worker_type_id),
 worker_type_name varchar(50) not null,
 )

 create table shifts (
 shift_id int identity not null,
 constraint pk_shift primary key (shift_id),
 beginning_time time not null,
 end_time time not null,
 )

 create table workers(
 worker_id int identity not null,
 constraint pk_worker primary key (worker_id),
 person_id int not null,
 constraint fk_tz_worker foreign key (person_id) references persons (person_id),
 worker_type_id int not null ,
 constraint fk_worker_workertype foreign key (worker_type_id) references worker_type (worker_type_id),
 shift_id int not null,
 constraint fk_worker_shift foreign key (shift_id) references shifts (shift_id),
 date_start_work date not null,
 
)
alter table workers add  jail_id int null 
alter table workers add constraint fk_jail_worker foreign key (jail_id) references jails (jail_id)


 create table jails 
 (jail_id int identity(100,1)  not null,
 constraint pk_jail primary  key (jail_id),
 jail_name varchar(50) not null,
 city_id int not null,
 constraint fk_jail_city foreign key (city_id) references cities (city_id),
 phone varchar(10) null,
 max_prisoners int not null,
 diractor_id int null,
 constraint fk_diractor_jail foreign key (diractor_id) references workers (worker_id),
 )
 ALTER TABLE jails DROP CONSTRAINT fk_diractor_jail
 alter table jails drop column diractor_id

 create table spaicl_event 
 (event_id int identity not null,
 constraint pk_event primary key (event_id),
 event_name varchar(50) not null,
 )

create table files 
(event_file_id int identity not null,
constraint pk_file primary key (event_file_id),
person_id int not null,
constraint fk_file_prisoner foreign key (person_id) references persons (person_id),
event_id int not null,
constraint fk_file_event foreign key (event_id) references spaicl_event (event_id),
event_date date not null,
describe varchar(100) null,
mis_year int not null,
)

create table jails_per_prisoner 
(jails_per_prisoner_id int identity not null,
person_id int not null,
constraint fk_prisoner_per foreign key (person_id) references persons (person_id),
jail_id int not null,
constraint fk_jail_per foreign key (jail_id) references jails (jail_id),
enter_date date not  null,
end_date date not  null,

)

alter table jails_per_prisoner add constraint pk_jails_per_prisoner primary key (jails_per_prisoner_id)
 

exec sp_rename 'dbo.jails_per_prisoner.person_id','prisoner_id','column'

ALTER TABLE [dbo].[jails_per_prisoner] DROP CONSTRAINT [fk_prisoner_per]

alter table jails_per_prisoner add  constraint fk_prisoner_per foreign key (prisoner_id) references prisoners (prisoner_id)

alter table jails_per_prisoner alter column  end_date date null

drop database jail 


