# 📱 Proyecto Aplicación QR — Documentación Completa

## 🎉 Bienvenido

Aplicación QR es un sistema digital de boletería que permite a los usuarios visualizar eventos, comprar entradas y obtener un **código QR único**, que luego puede validarse para autorizar el acceso. Incluye autenticación mediante **JWT**, roles de usuario, arquitectura en capas y persistencia con **Dapper**.

---

# 🧑‍💻 Equipo de desarrollo

* **Lizasoain, Ezequiel**
* **Piuca, Yanina**
* **Cuello, Fabrizio**
* **Pistan, Tihago**

---

# 🧱 Arquitectura del Proyecto

El sistema está construido siguiendo una arquitectura en capas que permite separar responsabilidades, mejorar la mantenibilidad y facilitar el testeo.

## 🏗️ Capas principales

### **1. Capa Core (Dominio)**

Contiene:

* Entidades del dominio
* Interfaces de repositorios
* Validaciones básicas

Esta capa **no depende** de infraestructura. Modelo limpio conforme al Principio de Inversión de Dependencias (DIP).

### **2. Capa Dapper (Infraestructura)**

Implementa:

* Repositorios usando ADO.NET y Dapper
* Conexiones a MySQL
* Consultas SQL

Aplica el Patrón Repositorio para separar la lógica de negocio del acceso a datos.

### **3. Capa Services**

Gestiona la lógica de negocio.

* Orquesta repositorios
* Realiza validaciones adicionales
* Procesa órdenes
* Genera entradas y QR

### **4. Capa WebApi (Presentación)**

Contiene:

* Uso de Minimal API para los Endpoints
* Configuración JWT
* Swagger/OpenAPI
* Inyección de dependencias
* Manejo de roles y autorización

### **5. Capa Tests**

Incluye pruebas unitarias con:

* xUnit
* Moq para mocking de repositorios

Las pruebas siguen el patrón **Arrange → Act → Assert**.

---

# 🧬 Entidades del dominio

Se modelaron las siguientes clases:

* **Usuario**
* **Cliente**
* **Evento**
* **Local**
* **Sector**
* **Funcion**
* **Tarifa**
* **Orden**
* **Entrada**
* **QR**
* **RefreshToken**

Los usuarios tienen roles:

* **Administrador**
* **Organizador**
* **Cliente**
* **Default**

---

# 🔒 Autenticación y Autorización (JWT)

La aplicación utiliza JSON Web Tokens.

### Proceso:

1. El usuario inicia sesión.
2. El servidor genera un token firmado.
3. Cada request incluye: `Authorization: Bearer <token>`
4. Se validan roles y permisos dependiendo del endpoint.

### Endpoints de autenticación

* `POST /auth/register`
* `POST /auth/login`
* `POST /auth/refresh`
* `POST /auth/logout`
* `GET /auth/me`
* `GET /auth/roles`
* `PUT /usuarios/{id}/roles`

---

# 🚀 Flujo de compra y generación de QR

1. Usuario inicia sesión.
2. Consulta eventos, funciones y tarifas.
3. Genera una **Orden**.
4. Luego del pago, se emite una **Entrada**.
5. Se genera un **QR único** vinculado a esa entrada.
6. El QR puede **validarse una sola vez**.

---

# 💾 Persistencia con Dapper

Ventajas:

* Ligero y rápido
* SQL directo, flexible
* Mapeo automático de resultados

Cada entidad tiene su repositorio:

* `RepoEvento`
* `RepoOrden`
* `RepoEntrada`
* etc.

Se utilizo MariaDB, pero MySQL también funciona para aplicar diferentes usuarios según rol.

---

# 🧩 Manejo de usuarios MySQL según rol

El usuario de base de datos se determina desde las **claims** del JWT.

Roles → Usuarios MariaDB/MySQL:

* Administrador → usuario con privilegios altos
* Organizador → CRUD parcial
* Cliente → consulta + creación de órdenes
* Default → permisos mínimos

Antes de iniciar sesión, se usa un usuario MariaDB/MySQL "por defecto".

---

# 🌐 Endpoints principales

Los recursos se gestionan con endpoints REST:

### 📍 Locales

### 📍 Sectores

### 📍 Eventos

### 📍 Funciones

### 📍 Tarifas

### 📍 Clientes

### 📍 Órdenes

### 📍 Entradas

### 📍 Códigos QR

Todos probables con **Swagger UI**.

---

# 🧪 Pruebas Unitarias

* Implementadas con **xUnit**
* Uso de **Moq** para simular repositorios
* Pruebas de reglas de negocio, flujos y validaciones

---

# 📚 Documentación interactiva — Swagger

El proyecto utiliza **Swashbuckle** para generar automáticamente:

* Esquemas de entidades
* Descripciones de cada endpoint
* Respuestas con sus códigos HTTP

---

# 🔗 Generación de URLs dinámicas

Se utilizan:

* **LinkGenerator**
* **IHttpContextAccessor**

Esto permite crear URLs dependientes del servidor actual (host + puerto).

---

# ⚙️ Instalación del Proyecto

## 1️⃣ Requisitos

* SDK .NET 8
* MySQL o MariaDB (Ambos sirven)

## 2️⃣ Clonar el repositorio

```bash
git clone https://github.com/MonkiCapo/AplicacionQR.git
```

## 3️⃣ Configurar usuarios MySQL

Modificar contraseñas en los scripts SQL.

## 4️⃣ Configurar `appsettings.json`

Actualizar cadenas de conexión:

```json
"Users": {
    "Admin":"Server=localhost;Uid=admin;Pwd=Trigg3rs!;Database=5to_AppQR;",
    "Cliente": "Server=localhost;Uid=cliente;Pwd=Trigg3rs!;Database=5to_AppQR;",
    "Organizador":"Server=localhost;Uid=organizador;Pwd=Trigg3rs!;Database=5to_AppQR;",
    "Default":"Server=localhost;Uid=default;Pwd=Trigg3rs!;Database=5to_AppQR;"
  }
```

Agregar usuarios root:

```json
"Root": {
  "UserRoot1":"Server=localhost;Uid=tuUser;Pwd=tuContraseña;Database=5to_AppQR;"
}
```

---

# 📦 Estructura del Proyecto

```
AppQR.Core       → Entidades e interfaces
AppQR.Dapper     → Persistencia Dapper
AppQR.Services   → Lógica de negocio
AppQR.WebApi     → API ASP.NET Core
AppQR.Test       → Pruebas unitarias
```

---

# 🎉 ¡Proyecto listo para usar!

Con todo configurado, solo resta ejecutar la API y comenzar a probar los endpoints mediante Swagger.
