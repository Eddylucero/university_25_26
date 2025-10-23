
CREATE DATABASE university2526;
USE university2526;

CREATE TABLE Faculty (
    id_fac INTEGER AUTO_INCREMENT PRIMARY KEY,
    name_fac VARCHAR(100) NOT NULL,
    acronym_fac VARCHAR (15),
    dean_name_fac VARCHAR(100) NOT NULL,
    phone_fac VARCHAR(15),
    email_fac VARCHAR(50),
    logo_fac VARCHAR (500),
    year_foundation_fac INTEGER
);

INSERT INTO Faculty (name_fac,acronym_fac, dean_name_fac, phone_fac, email_fac, year_foundation_fac) 
VALUES ('Ciencias de la Ingenieria y Aplicada','CIYA', 'ING. EDDY Y JOHAN', '0984932349', 'eddy-johan@gmail.com', 2015);


INSERT INTO Faculty (name_fac,acronym_fac, dean_name_fac, phone_fac, email_fac, year_foundation_fac) 
VALUES ('Facultad de Ciencias Agropecuarias y Recursos Naturales','CAREN', 'ING. EDDY Y JOHAN', '0984932349', 'eddy-johan@gmail.com', 2018);


INSERT INTO Faculty (name_fac,acronym_fac, dean_name_fac, phone_fac, email_fac, year_foundation_fac) 
VALUES ('Facultad de Ciencias Administrativas y Economias','CAYE', 'ING. EDDY Y JOHAN', '0984932349', 'eddy-johan@gmail.com', 2017);

INSERT INTO Faculty (name_fac,acronym_fac, dean_name_fac, phone_fac, email_fac, year_foundation_fac) 
VALUES ('Facultad de Ciencias Sociales Artes y Educacion','CSAYW', 'ING. EDDY Y JOHAN', '0984932349', 'eddy-johan@gmail.com', 2019);

CREATE TABLE Carrer (
    id_carre INT AUTO_INCREMENT PRIMARY KEY,
    name_carre VARCHAR(100) NOT NULL,
    duration_carre INT NOT NULL,
    director_carre VARCHAR (50),
    code_carre VARCHAR (50),
    id_facultad INT NOT NULL,
    FOREIGN KEY (id_facultad) REFERENCES Faculty(id_fac)
);

CREATE TABLE Teacher (
    id_docente INT AUTO_INCREMENT PRIMARY KEY,
    names_teach VARCHAR(100) NOT NULL,
    address_teach VARCHAR (50),
    email_teach VARCHAR(100),
    phone_teach VARCHAR(20),
    id_carre INT NOT NULL,
    FOREIGN KEY (id_carre) REFERENCES Carrer(id_carre)
);