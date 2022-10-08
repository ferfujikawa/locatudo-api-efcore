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

CREATE TABLE public.employees (
	id uuid NOT NULL PRIMARY KEY,
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL,
	department_id uuid NOT NULL,
	CONSTRAINT fk_employee_department FOREIGN KEY (department_id) REFERENCES public.departments (id) ON UPDATE CASCADE ON DELETE RESTRICT
);