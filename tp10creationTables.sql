/*------------------------------------------------------------
*        Script SQLSERVER 
------------------------------------------------------------*/


/*------------------------------------------------------------
-- Table: site_intervention
------------------------------------------------------------*/
CREATE TABLE site_intervention(
	ID_site    VARCHAR (40) NOT NULL ,
	nom_site   VARCHAR (60) NOT NULL ,
	latitude   REAL  NOT NULL ,
	longitude  REAL  NOT NULL ,
	ID_commune INT  NOT NULL ,
	CONSTRAINT prk_constraint_site_intervention PRIMARY KEY NONCLUSTERED (ID_site)
);


/*------------------------------------------------------------
-- Table: Commune
------------------------------------------------------------*/
CREATE TABLE Commune(
	ID_commune     INT IDENTITY (1,1) NOT NULL ,
	nom            VARCHAR (40) NOT NULL ,
	ID_departement INT   ,
	CONSTRAINT prk_constraint_Commune PRIMARY KEY NONCLUSTERED (ID_commune)
);


/*------------------------------------------------------------
-- Table: departement
------------------------------------------------------------*/
CREATE TABLE departement(
	ID_departement INT IDENTITY (1,1) NOT NULL ,
	nom            VARCHAR (40) NOT NULL ,
	CONSTRAINT prk_constraint_departement PRIMARY KEY NONCLUSTERED (ID_departement)
);


/*------------------------------------------------------------
-- Table: intervention
------------------------------------------------------------*/
CREATE TABLE intervention(
	id_intervention INT IDENTITY (1,1) NOT NULL ,
	date_debut      DATETIME NOT NULL ,
	date_fin        DATETIME NOT NULL ,
	ID_site         VARCHAR (40) NOT NULL ,
	CONSTRAINT prk_constraint_intervention PRIMARY KEY NONCLUSTERED (id_intervention)
);


/*------------------------------------------------------------
-- Table: période
------------------------------------------------------------*/
CREATE TABLE periode(
	ID_periode INT IDENTITY (1,1) NOT NULL ,
	nom        VARCHAR (40) NOT NULL ,
	CONSTRAINT prk_constraint_periode PRIMARY KEY NONCLUSTERED (ID_periode)
);


/*------------------------------------------------------------
-- Table: type_intervention
------------------------------------------------------------*/
CREATE TABLE type_intervention(
	ID_type INT IDENTITY (1,1) NOT NULL ,
	nom     VARCHAR (40) NOT NULL ,
	CONSTRAINT prk_constraint_type_intervention PRIMARY KEY NONCLUSTERED (ID_type)
);


/*------------------------------------------------------------
-- Table: theme
------------------------------------------------------------*/
CREATE TABLE theme(
	ID_theme INT IDENTITY (1,1) NOT NULL ,
	nom      VARCHAR (40) NOT NULL ,
	CONSTRAINT prk_constraint_theme PRIMARY KEY NONCLUSTERED (ID_theme)
);


/*------------------------------------------------------------
-- Table: periodeIntervention
------------------------------------------------------------*/
CREATE TABLE periodeIntervention(
	ID_site    VARCHAR (40) NOT NULL ,
	ID_periode INT  NOT NULL ,
	CONSTRAINT prk_constraint_periodeIntervention PRIMARY KEY NONCLUSTERED (ID_site,ID_periode)
);


/*------------------------------------------------------------
-- Table: typeIntervention
------------------------------------------------------------*/
CREATE TABLE typeIntervention(
	ID_site VARCHAR (40) NOT NULL ,
	ID_type INT  NOT NULL ,
	CONSTRAINT prk_constraint_typeIntervention PRIMARY KEY NONCLUSTERED (ID_site,ID_type)
);


/*------------------------------------------------------------
-- Table: themeIntervention
------------------------------------------------------------*/
CREATE TABLE themeIntervention(
	ID_site  VARCHAR (40) NOT NULL ,
	ID_theme INT  NOT NULL ,
	CONSTRAINT prk_constraint_themeIntervention PRIMARY KEY NONCLUSTERED (ID_site,ID_theme)
);



ALTER TABLE site_intervention ADD CONSTRAINT FK_site_intervention_ID_commune FOREIGN KEY (ID_commune) REFERENCES Commune(ID_commune);
ALTER TABLE Commune ADD CONSTRAINT FK_Commune_ID_departement FOREIGN KEY (ID_departement) REFERENCES departement(ID_departement);
ALTER TABLE intervention ADD CONSTRAINT FK_intervention_ID_site FOREIGN KEY (ID_site) REFERENCES site_intervention(ID_site);
ALTER TABLE periodeIntervention ADD CONSTRAINT FK_periodeIntervention_ID_site FOREIGN KEY (ID_site) REFERENCES site_intervention(ID_site);
ALTER TABLE periodeIntervention ADD CONSTRAINT FK_periodeIntervention_ID_periode FOREIGN KEY (ID_periode) REFERENCES periode(ID_periode);
ALTER TABLE typeIntervention ADD CONSTRAINT FK_typeIntervention_ID_site FOREIGN KEY (ID_site) REFERENCES site_intervention(ID_site);
ALTER TABLE typeIntervention ADD CONSTRAINT FK_typeIntervention_ID_type FOREIGN KEY (ID_type) REFERENCES type_intervention(ID_type);
ALTER TABLE themeIntervention ADD CONSTRAINT FK_themeIntervention_ID_site FOREIGN KEY (ID_site) REFERENCES site_intervention(ID_site);
ALTER TABLE themeIntervention ADD CONSTRAINT FK_themeIntervention_ID_theme FOREIGN KEY (ID_theme) REFERENCES theme(ID_theme);
