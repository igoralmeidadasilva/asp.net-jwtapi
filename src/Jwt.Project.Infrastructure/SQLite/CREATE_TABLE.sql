-- SQLite
DROP TABLE IF EXISTS Customers;

CREATE TABLE Customers (
    id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    login TEXT NOT NULL,
    password TEXT NOT NULL
);

SELECT * FROM Customers;

UPDATE Customers
SET name = 'Novo Nome',
    password = 'Nova Senha'
WHERE login = 'admin';
