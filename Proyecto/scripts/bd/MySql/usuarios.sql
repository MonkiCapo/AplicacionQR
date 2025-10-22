-- Crear usuarios
CREATE USER 'cliente_appqr'@'%' IDENTIFIED BY 'cliente123';
CREATE USER 'gerente_appqr'@'%' IDENTIFIED BY 'gerente123';
CREATE USER 'admin_appqr'@'%' IDENTIFIED BY 'admin123';

-- ========================
-- CLIENTE: solo lectura en tablas relevantes
-- ========================
GRANT SELECT ON AppQR.Evento TO 'cliente_appqr'@'%';
GRANT SELECT ON AppQR.Funcion TO 'cliente_appqr'@'%';
GRANT SELECT ON AppQR.Tarifa TO 'cliente_appqr'@'%';
GRANT SELECT ON AppQR.Cliente TO 'cliente_appqr'@'%';
GRANT SELECT ON AppQR.Entrada TO 'cliente_appqr'@'%';
GRANT SELECT ON AppQR.QR TO 'cliente_appqr'@'%';

-- ========================
-- GERENTE: acceso de gestión + tarifas y órdenes
-- ========================
GRANT SELECT, INSERT, UPDATE, DELETE ON AppQR.Cliente TO 'gerente_appqr'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON AppQR.Usuario TO 'gerente_appqr'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON AppQR.Evento TO 'gerente_appqr'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON AppQR.Funcion TO 'gerente_appqr'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON AppQR.Tarifa TO 'gerente_appqr'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON AppQR.Orden TO 'gerente_appqr'@'%';
GRANT SELECT ON AppQR.Entrada TO 'gerente_appqr'@'%';
GRANT SELECT ON AppQR.QR TO 'gerente_appqr'@'%';

-- ========================
--  ADMIN: control total
-- ========================
GRANT ALL PRIVILEGES ON AppQR.* TO 'admin_appqr'@'%';

-- ========================
-- Aplicar los cambios
-- ========================
FLUSH PRIVILEGES;
