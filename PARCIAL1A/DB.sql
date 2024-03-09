CREATE TABLE Autores (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Posts (
    Id INT PRIMARY KEY,
    Titulo VARCHAR(50) NOT NULL,
    Contenido VARCHAR(50) NOT NULL,
    FechaPublicacion DATETIME NOT NULL,
    AutorId INT NOT NULL,
    FOREIGN KEY (AutorId) REFERENCES Autores(Id)
);

CREATE TABLE Libros (
    Id INT PRIMARY KEY,
    Titulo VARCHAR(50) NOT NULL
);

CREATE TABLE AutorLibro (
    AutorId INT NOT NULL,
    LibroId INT NOT NULL,
    Orden INT NOT NULL,
    FOREIGN KEY (AutorId) REFERENCES Autores(Id),
    FOREIGN KEY (LibroId) REFERENCES Libros(Id),
    PRIMARY KEY (AutorId, libroid)
);