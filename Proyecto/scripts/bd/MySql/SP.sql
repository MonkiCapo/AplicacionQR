-- Active: 1752667437026@@127.0.0.1@3306@5to_appqr

/*STORED PROCEDURE PARA CANCELAR FUNCION*/
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

/*STORED PROCEDURE PARA PAGAR ORDEN*/
DELIMITER //

CREATE PROCEDURE PagarOrden(IN p_IdOrden INT)
main: BEGIN
    DECLARE v_Estado VARCHAR(20);
    DECLARE v_StockInsuficiente INT DEFAULT 0;

    -- Manejador de errores
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    SELECT Estado INTO v_Estado FROM Orden WHERE IdOrden = p_IdOrden LIMIT 1;

    IF ROW_COUNT() = 0 THEN
        SELECT 'Orden no encontrada' AS Mensaje;
        LEAVE main;
    END IF;

    IF v_Estado = 'Pagado' THEN
        SELECT 'La orden ya está pagada' AS Mensaje;
        LEAVE main;
    END IF;

    IF v_Estado IN ('Cancelado', 'Anulada') THEN
        SELECT 'No se puede pagar una orden cancelada o anulada' AS Mensaje;
        LEAVE main;
    END IF;

    START TRANSACTION;

    SELECT COUNT(*) INTO v_StockInsuficiente
    FROM Entrada e
    INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
    WHERE e.IdOrden = p_IdOrden AND t.Stock <= 0;

    IF v_StockInsuficiente > 0 THEN
        ROLLBACK;
        SELECT 'Stock insuficiente para una o más tarifas' AS Mensaje;
        LEAVE main;
    END IF;

    UPDATE Orden SET Estado = 'Pagado' WHERE IdOrden = p_IdOrden;

    
    UPDATE Tarifa t
    INNER JOIN Entrada e ON t.IdTarifa = e.IdTarifa
    SET t.Stock = t.Stock - 1
    WHERE e.IdOrden = p_IdOrden;

    UPDATE Entrada SET Estado = 'Pagado' WHERE IdOrden = p_IdOrden;

    COMMIT;

    SELECT 'Orden pagada exitosamente' AS Mensaje;
END//

DELIMITER ;

DELIMITER //

/*Stored ProcNO DIJE NADA, TEMON TEMONedure para Cancelar Orden*/

CREATE PROCEDURE CancelarOrden(IN p_IdOrden INT)
main: BEGIN
    DECLARE v_Estado VARCHAR(20);

    -- Manejador de errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    -- Verificar existencia de la orden
    SELECT Estado INTO v_Estado FROM Orden WHERE IdOrden = p_IdOrden LIMIT 1;

    IF ROW_COUNT() = 0 THEN
        SELECT 'Orden no encontrada' AS Mensaje;
        LEAVE main;
    END IF;

    -- Validar si ya está cancelada o anulada
    IF v_Estado IN ('Cancelado', 'Anulada') THEN
        SELECT 'La orden ya está cancelada o anulada' AS Mensaje;
        LEAVE main;
    END IF;

    -- Validar si está pagada (no se puede cancelar una orden pagada)
    IF v_Estado = 'Pagado' THEN
        SELECT 'No se puede cancelar una orden ya pagada' AS Mensaje;
        LEAVE main;
    END IF;

    START TRANSACTION;

    -- Actualizar estado de la orden a Cancelado
    UPDATE Orden SET Estado = 'Cancelado' WHERE IdOrden = p_IdOrden;

    COMMIT;

    SELECT 'Orden cancelada exitosamente' AS Mensaje;
END//

DELIMITER ;

DELIMITER //

CREATE PROCEDURE AnularEntrada(IN p_IdEntrada INT)
main: BEGIN
    DECLARE v_Estado VARCHAR(45);

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    SELECT Estado INTO v_Estado FROM Entrada WHERE IdEntrada = p_IdEntrada LIMIT 1;
    IF ROW_COUNT() = 0 THEN
        SELECT 'Entrada no encontrada' AS Mensaje;
        LEAVE main;
    END IF;

    IF v_Estado = 'Anulada' THEN
        SELECT 'La entrada ya esta anulada' AS Mensaje;
        LEAVE main;
    END IF;

    START TRANSACTION;

    UPDATE Entrada SET Estado = 'Anulada' WHERE IdEntrada = p_IdEntrada;

    IF Estado = 'Pagado' THEN
        UPDATE Tarifa t
        INNER JOIN Entrada e ON t.IdTarifa = e.IdTarifa
        SET t.Stock = t.Stock + 1
        WHERE e.IdEntrada = p_IdEntrada;
    END IF;

    COMMIT;

    SELECT 'Entrada anulada exitosamente' AS Mensaje;
END//