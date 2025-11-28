### **Lista de tareas**

| **Orden** | **Tarea**                             | **Duracion (hs)** | **Dependencias** |
|-------|-------------------------------------------|:-------------:|:------------:|
|a  |Realizar relevamiento (Que se debe hacer)      | 7             | -            |
|b	|Realizar el DER, diagrama de clases y casos de usos | 6	            | a            |
|c	|Realizar el DDL, SP, INSERTS y USERS                        | 7	            | b            |
|d	|Realizar capa Core	                            | 12	            | c            |
|e	|Realizar capa Dapper	                        | 8	            | d            |
|f	|Realizar capa Servicios	                    | 6	            | e            |
|g	|Investigar sobre JWT e implementarlo	        | 4	            | f            |
|h	|Configurar el JWT en el swagger  | 5	            | g            |
|i	|Realizar capa de Tests (XUnit)               | 6	            | f            |
|j  | Realizar capa de presentación (WebApi/Endpoints) | 15  |     d, e, f |
|l	|Documentacion	                                | 6       | i, h         |

### **Gantt**

```mermaid

gantt
    title Plan de aprendizaje
    dateFormat  YYYY-MM-DD

    section Relevamiento
    a : a, 2025-08-30, 10d

    section DER, UML, CASOS DE USO
    b : after a, 3d

    section DDL, SP, INSERTS y USERS
    c : c, after b, 4d

    section Realizar capa Core
    d : d, after c, 7d

    section Realizar capa Dapper
    e : e, after d, 5d

    section Realizar capa Servicios
    f : f, after e, 3d

    section Investigar Tokens
    g : g, after f, 2d

    section Configurar Tokens
    h : h, after g, 3d
    
    section Capa de tests
    i: i, after f, 3d

    section Capa de presentacion
    j : j, after f, 7d

    section Documentacion
    l : l, after h, 3d



```