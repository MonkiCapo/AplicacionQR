

-- ======================
-- TABLA: Local
-- ======================
INSERT INTO Local (Nombre, Direccion) VALUES
('Estadio Velez Sarsfield', 'Av. Juan B. Justo 9200, CABA'),
('Movistar Arena', 'Humboldt 450, CABA'),
('Teatro Colón', 'Cerrito 628, CABA');

-- ======================
-- TABLA: Sector
-- ======================
-- Asignamos sectores a los locales (IdLocal 1, 2, 3)
INSERT INTO Sector (capacidad, IdLocal) VALUES
(15000, 1), -- Sector 'Platea Alta' de Velez
(5000, 1),  -- Sector 'Campo' de Velez
(3000, 2);  -- Sector 'Platea Baja' de Movistar Arena

-- ======================
-- TABLA: Evento
-- ======================
INSERT INTO Evento (Nombre, Estado, FechaInicio, FechaFin) VALUES
('Concierto de Rock', 'Activo', '2025-12-15 20:00:00', '2025-12-15 23:30:00'),
('Obra de Teatro Clásico', 'Activo', '2025-11-20 21:00:00', '2025-11-20 23:00:00'),
('Final de E-Sports', 'Planeado', '2026-02-10 14:00:00', '2026-02-10 19:00:00');

-- ======================
-- TABLA: Funcion
-- ======================
-- Asignamos funciones a los eventos (IdEvento 1, 2, 3)
INSERT INTO Funcion (Nombre, FechaHora, Estado, IdEvento) VALUES
('Función Única - Concierto', '2025-12-15 21:00:00', 'Activa', 1),
('Función de Estreno', '2025-11-20 21:00:00', 'Activa', 2),
('Función Matinée', '2025-11-21 18:00:00', 'Activa', 2);

-- ======================
-- TABLA: Tarifa
-- ======================
-- Asignamos tarifas a las funciones (IdFuncion 1, 2, 3)
INSERT INTO Tarifa (Tipo, Precio, Stock, Estado, IdFuncion) VALUES
('Campo General', 15000.00, 5000, 'Disponible', 1),
('Platea VIP', 35000.00, 500, 'Disponible', 1),
('Pullman', 12000.00, 300, 'Disponible', 2);

-- ======================
-- TABLA: Cliente
-- ======================
INSERT INTO Cliente (DNI, nombre, telefono) VALUES
(30111222, 'Juan Pérez', '1150001234'),
(32444555, 'María González', '1151112233'),
(28999888, 'Carlos Rodríguez', '1152223344');

-- ======================
-- TABLA: Usuario
-- ======================
-- Asignamos usuarios a los clientes (DNI 30111222, 32444555, 28999888)
INSERT INTO Usuario (NombreUsuario, Email, Contraseña, Rol, DNI) VALUES
('jperez', 'juan.perez@email.com', 'hash_contraseña_1', 'Cliente', 30111222),
('mgonzalez', 'maria.gonzalez@email.com', 'hash_contraseña_2', 'Cliente', 32444555),
('admin_carlos', 'carlos.admin@email.com', 'hash_contraseña_admin', 'Admin', 28999888);

-- ======================
-- TABLA: Orden
-- ======================
-- Asignamos órdenes a los usuarios (IdUsuario 1, 2, 3)
INSERT INTO Orden (IdUsuario, Estado, PrecioTotal, Fecha) VALUES
(1, 'Completada', 35000.00, '2025-11-01 10:30:00'),
(2, 'Pendiente', 24000.00, '2025-11-05 14:00:00'),
(1, 'Completada', 15000.00, '2025-11-08 18:45:00');

-- ======================
-- TABLA: Entrada
-- ======================
-- Asignamos entradas a las órdenes (IdOrden 1, 2, 3) y tarifas (IdTarifa 1, 2, 3)
INSERT INTO Entrada (IdTarifa, IdOrden, Estado) VALUES
(2, 1, 'Válida'), -- 1 Entrada Platea VIP (35000) para la Orden 1
(3, 2, 'Pendiente'), -- 1 Entrada Pullman (12000) para la Orden 2
(3, 2, 'Pendiente'); -- 1 Entrada Pullman (12000) para la Orden 2 (Total 24000)

-- ======================
-- TABLA: RefreshToken
-- ======================
INSERT INTO RefreshTokens (Token, Email, Expiration) VALUES
('token_largo_aleatorio_1', 'juan.perez@email.com', '2025-12-01 23:59:59'),
('token_largo_aleatorio_2', 'maria.gonzalez@email.com', '2025-12-02 23:59:59'),
('token_largo_aleatorio_admin', 'carlos.admin@email.com', '2025-12-03 23:59:59');

-- ====================
-- TABLA: QR
-- ====================
-- Asignamos QRs a las entradas (IdEntrada 1, 2, 3)
INSERT INTO QR (IdEntrada, url) VALUES
(1, 'https://mi-app.com/qr/data/uuid-entrada-1'),
(2, 'https://mi-app.com/qr/data/uuid-entrada-2'),
(3, 'https://mi-app.com/qr/data/uuid-entrada-3');