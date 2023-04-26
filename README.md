# API Desafio

## Descrição

O endpoint raiz da API Desafio retorna uma mensagem indicando que a API está online e disponível para uso.

## Endpoint

### GET /

Retorna uma resposta com o status HTTP 200 OK e o corpo da resposta contendo a mensagem "Api online!".

# Account Controller

## Endpoint `/v1/accounts`

### `POST`

Registra um novo usuário para utilizar a API

- **URL:** `/v1/accounts`
- **Método:** `POST`
- **Corpo da Requisição:**
    ```json
    {
        "Name": "Nome do usuário",
        "Email": "email@example.com"
    }
    ```
- **Resposta de Sucesso:**
    - **Código:** 200 OK
    - **Corpo da Resposta:**
        ```json
        {
            "user": "email@example.com",
            "password": "senha_gerada"
        }
        ```

## Endpoint `/v1/accounts/login`

### `POST`

Realiza o login do usuário e gera seu token

- **URL:** `/v1/accounts/login`
- **Método:** `POST`
- **Corpo da Requisição:**
    ```json
    {
        "Email": "email@example.com",
        "Password": "senha"
    }
    ```
- **Resposta de Sucesso:**
    - **Código:** 200 OK
    - **Corpo da Resposta:**
        ```json
        {
            "data": "token",
            "error": null
        }
        ```

# Category Controller

## Endpoint `/v1/categories`

### `GET`

Retorna todas as categorias com os IDs dos produtos associados.

- **URL:** `/v1/categories`
- **Método:** `GET`
- **Resposta de Sucesso:**
    - **Código:** 200 OK
    - **Corpo da Resposta:**
        ```json
        [
            {
                "Id": 1,
                "Name": "Nome da Categoria",
                "ProductIds": [1, 2, 3]
            },
            {
                "Id": 2,
                "Name": "Outra Categoria",
                "ProductIds": [4, 5, 6]
            }
        ]
        ```

### `GET /categories/{id}`

Retorna uma categoria específica com base no ID fornecido.

- **URL:** `/v1/categories/{id}`
- **Método:** `GET`
- **Parâmetros da URL:**
    - **id** (obrigatório): ID da categoria
- **Resposta de Sucesso:**
    - **Código:** 200 OK
    - **Corpo da Resposta:**
        ```json
        {
            "id": 1,
            "name": "Nome da Categoria",
            "createdAt": "Data de Criação",
            "updatedAt": "Data de Atualização",
            "productIds": [1, 2, 3]
        }
        ```

### `POST /categories`

Cria uma nova categoria.

- **URL:** `/v1/categories`
- **Método:** `POST`
- **Corpo da Requisição:**
    ```json
    {
        "Name": "Nome da Categoria"
    }
    ```
- **Resposta de Sucesso:**
    - **Código:** 200 OK
    - **Corpo da Resposta:**
        ```json
        {
            "id": 1,
            "name": "Nome da Categoria",
            "createdAt": "Data de Criação"
        }
        ```

### `PUT /categories/{id}`

Atualiza uma categoria existente com base no ID fornecido.

- **URL:** `/v1/categories/{id}`
- **Método:** `PUT`
- **Parâmetros da URL:**
    - **id** (obrigatório): ID da categoria
- **Corpo da Requisição:**
    ```json
    {
        "Name": "Novo Nome da Categoria"
    }
    ```
- **Resposta de Sucesso:**
    - **Código:** 200 OK

### `DELETE /categories/{id}`

Exclui uma categoria com base no ID fornecido.

- **URL:** `/v1/categories/{id}`
- **Método:** `DELETE`
- **Parâmetros da URL:**
    - **id** (obrigatório): ID da categoria
- **Resposta de Sucesso:**
    - **Código:** 200 OK

