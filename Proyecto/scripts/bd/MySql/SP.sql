DELIMITER //

CREATE PROCEDURE CancelarFuncion(p_idfuncion INT)
BEGIN
    START TRANSACTION;

    /* Verifica si existe la funcion */

    IF (SELECT COUNT(*) FROM Funcion WHERE IdFuncion = p_IdFuncion) = 0 THEN
        SELECT 'Función no encontrada' AS Mensaje;
        ROLLBACK;
        LEAVE BEGIN;
    END IF;

    /*Verifica si la funcion ya fue cancelada*/

    IF (SELECT Estado FROM Funcion WHERE IdFuncion = p_IdFuncion) = 'Cancelado' THEN
        SELECT 'La funcion ya está cancelada' AS Mensaje;
        ROLLBACK;
        LEAVE BEGIN;
    END IF;

    UPDATE Funcion
    SET Estado = 'Cancelado'
    WHERE IdFuncion = p_IdFuncion;

    UPDATE Entrada e
    INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
    SET e.Estado = 'Cancelado'
    WHERE t.IdFuncion = p_IdFuncion;
    
    UPDATE Orden o
    INNER JOIN Entrada e ON o.IdOrden = e.IdOrden
    INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
    SET o.Estado = 'Cancelado'
    WHERE t.IdFuncion = p_IdFuncion;

    COMMIT;

    SELECT 'La función fue cancelada correctamente' AS Mensaje;
END //

DELIMITER ;