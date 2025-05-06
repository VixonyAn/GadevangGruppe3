  drop table Bane

Create Table Bruger ( 

	BrugerId int primary key, 

	Brugernavn varchar(30) not null, 

	Adgangskode varchar(50) not null, 

	Email varchar(50) not null, 

	Telefon varchar(8) not null, 

	Verificeret bit not null, 

	Medlemskab int not null, 

	Position int not null 

); 

 

Create Table Bane 

( 

	BaneId int primary key,

	BaneType int not null, 

	BaneMiljø int not null, 

	Beskrivelse varchar(250) 

); 


Create Table Begivenhed ( 

    EventId int  primary key, 

    Titel varchar(50) not null, 

    Sted varchar(60) not null, 

    Dato date not null, 

    Beskrivelse varchar(255) not null, 

    MedlemMax int not null, 

    Pris decimal(10,2) 

); 

Create Table Booking( 
	BookingId int not null,
	BaneId int not null,
	StartTid DateTime not null,
	SlutTid DateTime not null,
	Bruger1 int not null,
	Bruger2 int,
	Beskrivelse varchar(250)

Foreign Key (BaneId) References Bane (BaneId),
Foreign Key (Bruger1) References Bruger (BrugerId),
Foreign Key (Bruger2) References Bruger (BrugerId),
); 

Create Table TilmeldBegivenhed ( 
    BrugerId int not null, 
    EventId int not null, 
    Kommentar varchar(255), 
    Foreign Key (BrugerId) References Bruger (BrugerId), 
    Foreign Key (EventId) References Begivenhed (EventId), 
    Primary Key (BrugerId, EventId) 
); 

