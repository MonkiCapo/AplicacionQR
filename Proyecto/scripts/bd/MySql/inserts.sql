

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
('Concierto de Rock', 'Publicado', '2025-12-15 20:00:00', '2025-12-15 23:30:00'),
('Obra de Teatro Clásico', 'Publicado', '2025-11-20 21:00:00', '2025-11-20 23:00:00'),
('Final de E-Sports', 'Publicado', '2026-02-10 14:00:00', '2026-02-10 19:00:00');

-- ======================
-- TABLA: Funcion
-- ======================
-- Asignamos funciones a los eventos (IdEvento 1, 2, 3)
INSERT INTO Funcion (Nombre, FechaHora, Estado, IdEvento) VALUES
('Función Única - Concierto', '2025-12-15 21:00:00', 'Activo', 1),
('Función de Estreno', '2025-11-20 21:00:00', 'Activo', 2),
('Función Matinée', '2025-11-21 18:00:00', 'Activo', 2);

-- ======================
-- TABLA: Tarifa
-- ======================
-- Asignamos tarifas a las funciones (IdFuncion 1, 2, 3)
INSERT INTO Tarifa (Tipo, Precio, Stock, Estado, IdFuncion) VALUES
('General', 15000.00, 5000, 'Creado', 1),
('VIP', 35000.00, 500, 'Creado', 1),
('Infantil', 12000.00, 300, 'Creado', 2);