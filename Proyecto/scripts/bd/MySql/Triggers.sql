DELIMITER $$

CREATE TRIGGER BefInsUsuario
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    -- Si la contraseña no está ya en formato hash, la encripta
    SET NEW.Contraseña = SHA2(NEW.Contraseña, 256);
END $$;


DELIMITER $$
CREATE TRIGGER BefUpdUsuario
BEFORE UPDATE ON Usuario
FOR EACH ROW
BEGIN
    IF NEW.Contraseña <> OLD.Contraseña THEN
        SET NEW.Contraseña = SHA2(NEW.Contraseña, 256);
    END IF;
END $$;