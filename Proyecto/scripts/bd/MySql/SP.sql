-- Active: 1752667437026@@127.0.0.1@3306@appqr
DELIMITER //

CREATE PROCEDURE CancelarFuncion(IN p_IdFuncion INT)
main_block: BEGIN
    DECLARE v_count INT DEFAULT 0;
    DECLARE v_estado VARCHAR(50);
    DECLARE v_mensaje VARCHAR(255);

    -- Handler para errores: si algo falla, rollback y mensaje de error
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error durante la cancelación de la función. Se revirtieron los cambios.' AS Mensaje;
    END;

    -- Verifica si existe la función
    SELECT COUNT(*) INTO v_count
    FROM Funcion
    WHERE IdFuncion = p_IdFuncion;

    IF v_count = 0 THEN
        SELECT 'Función no encontrada' AS Mensaje;
        LEAVE main_block;
    END IF;

    -- Verifica si la función ya está cancelada
    SELECT Estado INTO v_estado
    FROM Funcion
    WHERE IdFuncion = p_IdFuncion;

    IF v_estado = 'Cancelado' THEN
        SELECT 'La función ya está cancelada' AS Mensaje;
        LEAVE main_block;
    END IF;

    -- Inicia la transacción
    START TRANSACTION;

    -- Cancela la función
    UPDATE Funcion
    SET Estado = 'Cancelado'
    WHERE IdFuncion = p_IdFuncion;

    -- Cancela las entradas asociadas
    UPDATE Entrada e
    INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
    SET e.Estado = 'Cancelado'
    WHERE t.IdFuncion = p_IdFuncion;

    -- Cancela las órdenes relacionadas
    UPDATE Orden o
    INNER JOIN Entrada e ON o.IdOrden = e.IdOrden
    INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
    SET o.Estado = 'Cancelado'
    WHERE t.IdFuncion = p_IdFuncion;

    -- Si todo salió bien, confirmar los cambios
    COMMIT;

    SELECT 'La función fue cancelada correctamente' AS Mensaje;
END //

DELIMITER ;
