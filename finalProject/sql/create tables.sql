-- create tables 


create table Users(
userId int identity not null,
userName varchar(50) not null,
email  varchar(50) not null,
password varchar(10) not null,
constraint pk_userId primary key (userId)
)

create table Categories(
categoryId int identity not null,
categoryName varchar(50) not null,
userId int,
constraint pk_category primary key (categoryId),
constraint fk_user_category foreign key (userId) references Users(userId)
)

create table Colors(
colorId int identity not null,
colorName varchar(20) not null,
constraint pk_colors primary key (colorId)
)

create table Items(
itemId int identity not null,
categoryId int  not null,
colorId int,
entryDate datetime not null,
img image not null,
userId int not null,
constraint pk_items primary key (itemId),
constraint fk_item_user foreign key (userId) references Users(userId),
constraint fk_item_color foreign key (colorId) references Colors(colorId)
)

create table Tags(
tagId int identity not null,
tagName varchar(30) not null,
userId int not null,
constraint pk_tags primary key (tagId),
constraint fk_tag_user foreign key (userId) references Users(userId)
)

create table Tag_Item(
tagItemId int identity not null,
tagId int not null,
itemId int not null,
constraint pk_tagItem primary key (tagItemId),
constraint fk_tagItem_tag foreign key (tagId) references Tags(tagId),
constraint fk_tagItem_item foreign key (itemId) references Items(itemId)
)

create table Outfits(
outfitId int identity not null,
outfitName varchar(50),
userId int not null,
constraint pk_outfits primary key (outfitId),
constraint fk_outfit_user foreign key (userId) references Users(userId)
)

create table Outfit_Items(
outfitItemId int identity not null,
itemId int not null,
outfitId int not null,
constraint pk_outfit_item primary key (outfitItemId),
constraint fk_outfitItem_item foreign key (itemId) references Items (itemId),
constraint fk_outfitItem_outfit foreign key (outfitId) references Outfits(outfitId)
)


create table Uses(
useId int identity not null,
itemId int not null,
dateUse datetime not null,
constraint pk_uses primary key (useId),
constraint fk_use_item foreign key (itemId) references Items (itemId)
)
 
 create table Chips(
 chipId int not null,
 itemId int not null,
 itemLocation varchar(20) not null,
 constraint pk_chip primary key (chipId),
 constraint fk_chip_item foreign key (itemId) references Items(itemId)
 )









