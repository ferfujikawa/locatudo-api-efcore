CREATE DATABASE locatudo;

\c locatudo;

CREATE TABLE departments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL
);