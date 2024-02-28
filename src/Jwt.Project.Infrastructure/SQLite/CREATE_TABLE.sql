-- SQLite
DROP TABLE IF EXISTS Customers;

CREATE TABLE Customers (
    id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    login TEXT NOT NULL,
    password TEXT NOT NULL,
    role TEXT NOT NULL
);

SELECT * FROM Customers;

INSERT INTO Customers (id, name, login, password, role) VALUES ("1", "Name", "Login", "Password", "Role");

UPDATE Customers
SET name = 'Novo Nome',
    password = 'Nova Senha'
WHERE login = 'admin';

SELECT * FROM Customers WHERE login = "admin";
