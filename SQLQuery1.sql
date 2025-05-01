  

Create Table Bruger ( 

	BrugerId int Identity(1,1) primary key, 

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

	BaneId int Identity(1,1) primary key, 

	BaneType int not null, 

	BaneMiljø int not null, 

	Beskrivelse varchar(250) 

); 


Create Table Begivenhed ( 

    EventId int Identity(1,1) primary key, 

    Titel varchar(50) not null, 

    Sted varchar(60) not null, 

    Dato date not null, 

    Beskrivelse varchar(255) not null, 

    MedlemMax int not null, 

    Pris decimal(10,2) 

); 

