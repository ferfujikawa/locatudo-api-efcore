CREATE DATABASE locatudo;

\c locatudo;

CREATE TABLE public.departments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL
);

CREATE TABLE public.equipments (
	id uuid NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);