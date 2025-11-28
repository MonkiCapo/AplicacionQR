-- Active: 1760010688681@@127.0.0.1@3306@5to_appqr
SET autocommit=0;
START TRANSACTION;

DROP USER IF EXISTS 'admin'@'localhost';
CREATE USER IF NOT EXISTS 'admin'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT ALL ON 5to_AppQR.* TO 'admin'@'localhost';

DROP USER IF EXISTS 'cliente'@'localhost';
CREATE USER IF NOT EXISTS 'cliente'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT ON 5to_AppQR.Usuario TO 'cliente'@'localhost';
GRANT SELECT, INSERT, UPDATE ON 5to_AppQR.Cliente TO 'cliente'@'localhost';
GRANT SELECT ON 5to_AppQR.Evento TO 'cliente'@'localhost';
GRANT SELECT ON 5to_AppQR.Local TO 'cliente'@'localhost';
GRANT SELECT, UPDATE ON 5to_AppQR.Tarifa TO 'cliente'@'localhost';
GRANT SELECT ON 5to_AppQR.Funcion TO 'cliente'@'localhost';
GRANT SELECT ON 5to_AppQR.Sector TO 'cliente'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_AppQR.Orden TO 'cliente'@'localhost';
GRANT ALL ON 5to_AppQR.Entrada TO 'cliente'@'localhost';
GRANT ALL ON 5to_AppQR.QR TO 'cliente'@'localhost';
GRANT SELECT, DELETE ON 5to_AppQR.refreshtokens TO 'cliente'@'localhost';
GRANT EXECUTE ON PROCEDURE 5to_AppQR.PagarOrden TO 'cliente'@'localhost';
GRANT EXECUTE ON PROCEDURE 5to_AppQR.CancelarOrden TO 'cliente'@'localhost';
GRANT EXECUTE ON PROCEDURE 5to_AppQR.AnularEntrada TO 'cliente'@'localhost';


DROP USER IF EXISTS 'organizador'@'localhost';
CREATE USER IF NOT EXISTS 'organizador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT ON 5to_AppQR.Usuario TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_AppQR.Evento TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT, DELETE ON 5to_AppQR.Local TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_AppQR.Sector TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_AppQR.Funcion TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_AppQR.Tarifa TO 'organizador'@'localhost';
GRANT ALL ON 5to_AppQR.Entrada TO 'organizador'@'localhost';
GRANT ALL ON 5to_AppQR.QR TO 'organizador'@'localhost';
GRANT EXECUTE ON PROCEDURE 5to_AppQR.CancelarFuncion TO 'organizador'@'localhost';


DROP USER IF EXISTS 'default'@'localhost';
CREATE USER IF NOT EXISTS 'default'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT INSERT, SELECT ON 5to_AppQR.Usuario TO 'default'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_AppQR.RefreshTokens TO 'default'@'localhost';
GRANT SELECT, INSERT ON 5to_AppQR.Cliente TO 'default'@'localhost';
GRANT SELECT, UPDATE ON 5to_AppQR.Entrada TO 'default'@'localhost';
GRANT SELECT ON 5to_AppQR.QR TO 'default'@'localhost';
GRANT SELECT ON 5to_AppQR.Tarifa TO 'default'@'localhost';
GRANT SELECT ON 5to_AppQR.Orden TO 'default'@'localhost';

COMMIT;