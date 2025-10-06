-- Active: 1752667437026@@127.0.0.1@3306@mysql
DROP DATABASE IF EXISTS AppQR

CREATE DATABASE AppQR

USE AppQR

-- ======================
-- TABLA: Local
-- ======================
CREATE TABLE Local (
    IdLocal INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(200) NOT NULL
);

-- ======================
-- TABLA: Sector
-- ======================
CREATE TABLE Sector (
    IdSector INT AUTO_INCREMENT PRIMARY KEY,
    capacidad INT NOT NULL,
    IdLocal INT NOT NULL,
    FOREIGN KEY (IdLocal) REFERENCES Local(IdLocal)
);

-- ======================
-- TABLA: Evento
-- ======================
CREATE TABLE Evento (
    IdEvento INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL
);

-- ======================
-- TABLA: Funcion
-- ======================
CREATE TABLE Funcion (
    IdFuncion INT AUTO_INCREMENT PRIMARY KEY,
    FechaHora DATETIME NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    IdEvento INT NOT NULL,
    FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento)
);

-- ======================
-- TABLA: Tarifa
-- ======================
CREATE TABLE Tarifa (
    IdTarifa INT AUTO_INCREMENT PRIMARY KEY,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    IdFuncion INT NOT NULL,
    FOREIGN KEY (IdFuncion) REFERENCES Funcion(IdFuncion)
);

-- ======================
-- TABLA: Cliente
-- ======================
CREATE TABLE Cliente (
    DNI INT PRIMARY KEY UNIQUE NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20)
);

-- ======================
-- TABLA: Usuario
-- ======================

CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(100) NOT NULL UNIQUE,
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
    IdUsuario INT NOT NULL,
    estado VARCHAR(50) NOT NULL,
    precioTotal DECIMAL(10,2) NOT NULL,
    fechaHora DATETIME NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
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

-- ======================
-- TABLA: RefreshToken
-- ======================
CREATE TABLE RefreshTokens (
    IdRefreshTokens INT AUTO_INCREMENT PRIMARY KEY,
    Token VARCHAR(200) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Expiration DATETIME NOT NULL
);
