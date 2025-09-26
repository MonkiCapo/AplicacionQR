DROP DATABASE IF EXISTS AppQR

CREATE DATABASE AppQR

USE AppQR

-- ======================
-- TABLA: Local
-- ======================
CREATE TABLE Local (
    IdLocal INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL
);

-- ======================
-- TABLA: Sector
-- ======================
CREATE TABLE Sector (
    IdSector INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    capacidad INT NOT NULL,
    IdLocal INT NOT NULL,
    FOREIGN KEY (IdLocal) REFERENCES Local(IdLocal)
);

-- ======================
-- TABLA: Evento
-- ======================
CREATE TABLE Evento (
    IdEvento INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    estado VARCHAR(50) NOT NULL,
    IdLocal INT NOT NULL,
    FOREIGN KEY (IdLocal) REFERENCES Local(IdLocal)
);

-- ======================
-- TABLA: Funcion
-- ======================
CREATE TABLE Funcion (
    IdFuncion INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATETIME NOT NULL,
    IdEvento INT NOT NULL,
    FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento)
);

-- ======================
-- TABLA: Tarifa
-- ======================
CREATE TABLE Tarifa (
    IdTarifa INT AUTO_INCREMENT PRIMARY KEY,
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    estado VARCHAR(50) NOT NULL,
    IdFuncion INT NOT NULL,
    FOREIGN KEY (IdFuncion) REFERENCES Funcion(IdFuncion)
);

-- ======================
-- TABLA: Cliente
-- ======================
CREATE TABLE Cliente (
    DNI UNSIGNED INT PRIMARY KEY UNIQUE NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20)
);

-- ======================
-- TABLA: Usuario
-- ======================

CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    nombreUsuario VARCHAR(100) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Contraseña VARCHAR(255) NOT NULL,
    Rol VARCHAR(50) NOT NULL,
    DNI INT NOT NULL,
    FOREIGN KEY (DNI) REFERENCES Cliente(DNI)
);


-- ======================
-- TABLA: Orden
-- ======================
CREATE TABLE Orden (
    IdOrden INT AUTO_INCREMENT PRIMARY KEY,
    IdCliente INT NOT NULL,
    estado VARCHAR(50) NOT NULL,
    precioTotal DECIMAL(10,2) NOT NULL,
    fechaHora DATETIME NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);

-- ======================
-- TABLA: Entrada
-- ======================
CREATE TABLE Entrada (
    IdEntrada INT AUTO_INCREMENT PRIMARY KEY,
    IdTarifa INT NOT NULL,
    fechaValida DATETIME NOT NULL,
    QR VARCHAR(255) NOT NULL,
    IdOrden INT NOT NULL,
    estado VARCHAR(50) NOT NULL,
    FOREIGN KEY (IdTarifa) REFERENCES Tarifa(IdTarifa),
    FOREIGN KEY (IdOrden) REFERENCES Orden(IdOrden)
);
