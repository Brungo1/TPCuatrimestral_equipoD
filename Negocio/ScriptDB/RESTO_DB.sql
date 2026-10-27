CREATE DATABASE RESTO_DB;
GO

USE RESTO_DB;
GO

CREATE TABLE CategoriasInsumo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Insumos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    IdCategoria INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    StockActual INT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdCategoria) REFERENCES CategoriasInsumo(Id)
);
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasenia VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    IdPerfil INT NOT NULL, -- 1 = Gerente, 2 = Mesero
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Mesas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroMesa INT NOT NULL UNIQUE,
    Capacidad INT NOT NULL,
    IdEstado INT NOT NULL DEFAULT 1, -- 1 = Libre, 2 = Ocupada, 3 = Cerrada
    IdMeseroAsignado INT NULL,
    Activa BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdMeseroAsignado) REFERENCES Usuarios(Id)
);
GO

CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdMesa INT NOT NULL,
    IdMesero INT NOT NULL,
    FechaHoraApertura DATETIME NOT NULL DEFAULT GETDATE(),
    FechaHoraCierre DATETIME NULL,
    IdEstado INT NOT NULL DEFAULT 1, -- 1 = Abierto, 2 = Cerrado, 3 = Cancelado
    FOREIGN KEY (IdMesa) REFERENCES Mesas(Id),
    FOREIGN KEY (IdMesero) REFERENCES Usuarios(Id)
);
GO

CREATE TABLE DetallesPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdInsumo INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(Id),
    FOREIGN KEY (IdInsumo) REFERENCES Insumos(Id)
);
GO

-- Datos precargados
INSERT INTO CategoriasInsumo (Descripcion, Activo) VALUES
('Entradas', 1),
('Platos Principales', 1),
('Bebidas sin Alcohol', 1),
('Bebidas con Alcohol', 1),
('Postres', 1);

INSERT INTO Insumos (Nombre, Descripcion, IdCategoria, Precio, StockActual, Activo) VALUES
('Empanada de Carne', 'Empanada de carne cortada a cuchillo', 1, 1800.00, 80, 1),
('Provoleta Especial', 'Queso provolone grillado con orégano', 1, 6500.00, 25, 1),
('Rabas a la Romana', 'Porción de calamares rebozados con limón', 1, 9200.00, 20, 1),
('Milanesa Napolitana con Fritas', 'Milanesa de ternera con papas fritas', 2, 11500.00, 35, 1),
('Bife de Chorizo', 'Corte vacuno a la parrilla con guarnición', 2, 15800.00, 30, 1),
('Sorrentinos de Jamón y Queso', 'Pasta casera con salsa a elección', 2, 10200.00, 28, 1),
('Agua Mineral 500ml', 'Agua con o sin gas', 3, 2200.00, 100, 1),
('Gaseosa Línea Coca-Cola 500ml', 'Coca-Cola, Sprite o Fanta', 3, 2600.00, 120, 1),
('Cerveza Artesanal Pinta', 'IPA, Honey, Scottish o Porter', 4, 4200.00, 60, 1),
('Vino Malbec Reserva 750ml', 'Botella de vino tinto Malbec', 4, 14500.00, 20, 1),
('Flan Casero Mixto', 'Con dulce de leche y crema', 5, 4500.00, 30, 1),
('Tiramisú', 'Postre tradicional con mascarpone y café', 5, 5400.00, 18, 1);
GO
